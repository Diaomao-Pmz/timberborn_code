using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.Hauling
{
	// Token: 0x02000012 RID: 18
	public class NoHaulingPostStatus : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002AAF File Offset: 0x00000CAF
		public NoHaulingPostStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002ABE File Offset: 0x00000CBE
		public void Awake()
		{
			this._haulCandidate = base.GetComponent<HaulCandidate>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._noHaulingPostStatus = StatusToggle.CreateNormalStatusWithFloatingIcon("OutOfHaulersRange", this._loc.T(NoHaulingPostStatus.NoHaulingPostLocKey), 0f);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B00 File Offset: 0x00000D00
		public void Initialize(Func<bool> activePredicate)
		{
			Asserts.FieldIsNull<NoHaulingPostStatus>(this, this._activePredicate, "_activePredicate");
			this._activePredicate = activePredicate;
			base.GetComponent<StatusSubject>().RegisterStatus(this._noHaulingPostStatus);
			this._haulCandidate.InHaulingCenterRangeChanged += this.OnInHaulingCenterRangeChanged;
			this._districtBuilding.ReassignedDistrict += this.OnDistrictReassigned;
			this.UpdateStatus();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B6A File Offset: 0x00000D6A
		public void Disable()
		{
			this._haulCandidate.InHaulingCenterRangeChanged -= this.OnInHaulingCenterRangeChanged;
			this.DeactivateStatus();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B8C File Offset: 0x00000D8C
		public void UpdateStatus()
		{
			if (this._activePredicate() && !this._haulCandidate.IsInHaulingCenterRange)
			{
				DistrictCenter districtOrConstructionDistrict = this._districtBuilding.GetDistrictOrConstructionDistrict();
				if (districtOrConstructionDistrict != null && districtOrConstructionDistrict)
				{
					this._noHaulingPostStatus.Activate();
					return;
				}
			}
			this.DeactivateStatus();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002BDF File Offset: 0x00000DDF
		public void OnInHaulingCenterRangeChanged(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BDF File Offset: 0x00000DDF
		public void OnDistrictReassigned(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BE7 File Offset: 0x00000DE7
		public void DeactivateStatus()
		{
			this._noHaulingPostStatus.Deactivate();
		}

		// Token: 0x0400001D RID: 29
		public static readonly string NoHaulingPostLocKey = "Status.Hauling.NoHaulingPost";

		// Token: 0x0400001E RID: 30
		public readonly ILoc _loc;

		// Token: 0x0400001F RID: 31
		public HaulCandidate _haulCandidate;

		// Token: 0x04000020 RID: 32
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000021 RID: 33
		public StatusToggle _noHaulingPostStatus;

		// Token: 0x04000022 RID: 34
		public Func<bool> _activePredicate;
	}
}
