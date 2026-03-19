using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Illumination;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x0200001F RID: 31
	public class MechanicalNodeIlluminator : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003917 File Offset: 0x00001B17
		public void Awake()
		{
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003936 File Offset: 0x00001B36
		public override void StartTickable()
		{
			this.UpdateIllumination();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003936 File Offset: 0x00001B36
		public override void Tick()
		{
			this.UpdateIllumination();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003940 File Offset: 0x00001B40
		public void UpdateIllumination()
		{
			bool activeAndPowered = this._mechanicalNode.ActiveAndPowered;
			if (activeAndPowered != this._wasEnabled)
			{
				this.ToggleIllumination(activeAndPowered);
				this._wasEnabled = activeAndPowered;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003970 File Offset: 0x00001B70
		public void ToggleIllumination(bool isEnabled)
		{
			if (isEnabled)
			{
				this._illuminatorToggle.TurnOn();
				return;
			}
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x04000067 RID: 103
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x04000068 RID: 104
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000069 RID: 105
		public bool _wasEnabled;
	}
}
