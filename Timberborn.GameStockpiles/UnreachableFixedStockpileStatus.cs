using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.GameStockpiles
{
	// Token: 0x02000008 RID: 8
	public class UnreachableFixedStockpileStatus : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021C1 File Offset: 0x000003C1
		public UnreachableFixedStockpileStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D0 File Offset: 0x000003D0
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._unreachableStatus = StatusToggle.CreateNormalStatus("UnconnectedBuilding", this._loc.T(UnreachableFixedStockpileStatus.UnreachableLocKey));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021FE File Offset: 0x000003FE
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._unreachableStatus);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002211 File Offset: 0x00000411
		public void OnEnterFinishedState()
		{
			this.UpdateStatus();
			this._districtBuilding.ReassignedInstantDistrict += this.OnReassignedInstantDistrict;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002230 File Offset: 0x00000430
		public void OnExitFinishedState()
		{
			this._unreachableStatus.Deactivate();
			this._districtBuilding.ReassignedInstantDistrict -= this.OnReassignedInstantDistrict;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002254 File Offset: 0x00000454
		public void OnReassignedInstantDistrict(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000225C File Offset: 0x0000045C
		public void UpdateStatus()
		{
			if (this._districtBuilding.InstantDistrict)
			{
				this._unreachableStatus.Deactivate();
				return;
			}
			this._unreachableStatus.Activate();
		}

		// Token: 0x0400000A RID: 10
		public static readonly string UnreachableLocKey = "Status.FixedStockpile.Unreachable";

		// Token: 0x0400000B RID: 11
		public readonly ILoc _loc;

		// Token: 0x0400000C RID: 12
		public DistrictBuilding _districtBuilding;

		// Token: 0x0400000D RID: 13
		public StatusToggle _unreachableStatus;
	}
}
