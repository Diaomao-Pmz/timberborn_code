using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;
using Timberborn.WindSystem;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x02000013 RID: 19
	public class WindPoweredGeneratorAnimator : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public void Awake()
		{
			this._windRotationAnimator = base.GetComponent<WindRotationAnimator>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			base.DisableComponent();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C00 File Offset: 0x00000E00
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C08 File Offset: 0x00000E08
		public override void Tick()
		{
			if (this._mechanicalNode.OutputMultiplier > 0f && this._mechanicalNode.Active)
			{
				this._windRotationAnimator.UnsuspendAnimation();
				return;
			}
			this._windRotationAnimator.SuspendAnimation();
		}

		// Token: 0x0400002E RID: 46
		public WindRotationAnimator _windRotationAnimator;

		// Token: 0x0400002F RID: 47
		public MechanicalNode _mechanicalNode;
	}
}
