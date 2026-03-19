using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x02000013 RID: 19
	public class UnconnectedBuildingStatus : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener
	{
		// Token: 0x06000048 RID: 72 RVA: 0x0000287C File Offset: 0x00000A7C
		public UnconnectedBuildingStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000288C File Offset: 0x00000A8C
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._unconnectedBuildingBlockers = base.GetComponentsAllocating<IUnconnectedBuildingBlocker>();
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("UnconnectedBuilding", this._loc.T(UnconnectedBuildingStatus.UnconnectedLocKey), this._loc.T(UnconnectedBuildingStatus.UnconnectedShortLocKey), 0f);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028F4 File Offset: 0x00000AF4
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
			foreach (IUnconnectedBuildingBlocker unconnectedBuildingBlocker in this._unconnectedBuildingBlockers)
			{
				unconnectedBuildingBlocker.IsUnconnectedBlockedChanged += this.OnIsUnconnectedBlockedChanged;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002964 File Offset: 0x00000B64
		public void OnEnterFinishedState()
		{
			this.UpdateStatus();
			this._districtBuilding.ReassignedInstantDistrict += this.OnReassignedInstantDistrict;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002983 File Offset: 0x00000B83
		public void OnExitFinishedState()
		{
			this._statusToggle.Deactivate();
			this._districtBuilding.ReassignedInstantDistrict -= this.OnReassignedInstantDistrict;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029A7 File Offset: 0x00000BA7
		public bool Blocked
		{
			get
			{
				if (this._unconnectedBuildingBlockers != null)
				{
					return this._unconnectedBuildingBlockers.FastAny((IUnconnectedBuildingBlocker blocker) => blocker.IsUnconnectedBlocked);
				}
				return false;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029DD File Offset: 0x00000BDD
		public void OnReassignedInstantDistrict(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029DD File Offset: 0x00000BDD
		public void OnIsUnconnectedBlockedChanged(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029E5 File Offset: 0x00000BE5
		public void UpdateStatus()
		{
			this._statusToggle.Toggle(this._blockObject.IsFinished && !this._districtBuilding.InstantDistrict && !this.Blocked);
		}

		// Token: 0x04000026 RID: 38
		public static readonly string UnconnectedLocKey = "Status.Buildings.Unconnected";

		// Token: 0x04000027 RID: 39
		public static readonly string UnconnectedShortLocKey = "Status.Buildings.Unconnected.Short";

		// Token: 0x04000028 RID: 40
		public readonly ILoc _loc;

		// Token: 0x04000029 RID: 41
		public DistrictBuilding _districtBuilding;

		// Token: 0x0400002A RID: 42
		public BlockObject _blockObject;

		// Token: 0x0400002B RID: 43
		public List<IUnconnectedBuildingBlocker> _unconnectedBuildingBlockers;

		// Token: 0x0400002C RID: 44
		public StatusToggle _statusToggle;
	}
}
