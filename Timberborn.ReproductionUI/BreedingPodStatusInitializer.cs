using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.Reproduction;
using Timberborn.StatusSystem;
using Timberborn.TickSystem;

namespace Timberborn.ReproductionUI
{
	// Token: 0x0200000B RID: 11
	public class BreedingPodStatusInitializer : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000255B File Offset: 0x0000075B
		public BreedingPodStatusInitializer(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000256C File Offset: 0x0000076C
		public void Awake()
		{
			this._breedingPod = base.GetComponent<BreedingPod>();
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("LackOfNutrients", this._loc.T(BreedingPodStatusInitializer.ProgressHaltedLocKey), this._loc.T(BreedingPodStatusInitializer.ProgressHaltedShortLocKey), 0f);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025BA File Offset: 0x000007BA
		public override void StartTickable()
		{
			this.UpdateToggle();
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025D3 File Offset: 0x000007D3
		public override void Tick()
		{
			this.UpdateToggle();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025DB File Offset: 0x000007DB
		public void UpdateToggle()
		{
			if (this._breedingPod.ProgressHalted)
			{
				this._statusToggle.Activate();
				return;
			}
			this._statusToggle.Deactivate();
		}

		// Token: 0x0400001F RID: 31
		public static readonly string ProgressHaltedLocKey = "Status.Breeding.ProgressHalted";

		// Token: 0x04000020 RID: 32
		public static readonly string ProgressHaltedShortLocKey = "Status.Breeding.ProgressHalted.Short";

		// Token: 0x04000021 RID: 33
		public readonly ILoc _loc;

		// Token: 0x04000022 RID: 34
		public BreedingPod _breedingPod;

		// Token: 0x04000023 RID: 35
		public StatusToggle _statusToggle;
	}
}
