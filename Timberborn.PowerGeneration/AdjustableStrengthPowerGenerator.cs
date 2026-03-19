using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.MechanicalSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x02000007 RID: 7
	public class AdjustableStrengthPowerGenerator : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public int MaxValue { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
		public AdjustableStrengthPowerGenerator(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211E File Offset: 0x0000031E
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000212B File Offset: 0x0000032B
		public float GeneratorStrength
		{
			get
			{
				return this._mechanicalNode.OutputMultiplier;
			}
			set
			{
				this._mechanicalNode.SetOutputMultiplier(value);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002139 File Offset: 0x00000339
		public void Awake()
		{
			this.MaxValue = base.GetComponent<MechanicalNodeSpec>().PowerOutput;
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002158 File Offset: 0x00000358
		public void InitializeEntity()
		{
			this.GeneratorStrength = 0.5f;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002165 File Offset: 0x00000365
		public void FlipRotation()
		{
			this._mechanicalNode.ReverseAllTransputs();
			if (this._mechanicalNode.Graph != null)
			{
				this._eventBus.Post(new MechanicalGraphGeneratorUpdatedEvent(this._mechanicalNode.Graph));
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly EventBus _eventBus;

		// Token: 0x0400000A RID: 10
		public MechanicalNode _mechanicalNode;
	}
}
