using System;
using Timberborn.BaseComponentSystem;
using Timberborn.MechanicalSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x0200000E RID: 14
	public class RotationMechanicalNodeUpdater : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000025BA File Offset: 0x000007BA
		public RotationMechanicalNodeUpdater(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000025C9 File Offset: 0x000007C9
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._waterGenerator = base.GetComponent<WaterPoweredGenerator>();
			this._waterGenerator.RotationUpdated += delegate(object _, EventArgs _)
			{
				this.UpdateMechanicalNode();
			};
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000025FC File Offset: 0x000007FC
		public void UpdateMechanicalNode()
		{
			bool flag = this._waterGenerator.GeneratorRotation < 0f;
			if (this._mechanicalNode.OutputMultiplier > 0f && flag != this._isReversed)
			{
				this._mechanicalNode.ReverseAllTransputs();
				this._isReversed = flag;
				if (this._mechanicalNode.Graph != null)
				{
					this._eventBus.Post(new MechanicalGraphGeneratorUpdatedEvent(this._mechanicalNode.Graph));
				}
			}
		}

		// Token: 0x04000010 RID: 16
		public readonly EventBus _eventBus;

		// Token: 0x04000011 RID: 17
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000012 RID: 18
		public WaterPoweredGenerator _waterGenerator;

		// Token: 0x04000013 RID: 19
		public bool _isReversed;
	}
}
