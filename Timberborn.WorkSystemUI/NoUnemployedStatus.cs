using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using Timberborn.WorkSystem;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000007 RID: 7
	public class NoUnemployedStatus : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public NoUnemployedStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		public void Awake()
		{
			this._workplace = base.GetComponent<Workplace>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._workplace.WorkerAssigned += delegate(object _, WorkerChangedEventArgs _)
			{
				this.UpdateStatus();
			};
			this._workplace.WorkerUnassigned += delegate(object _, WorkerChangedEventArgs _)
			{
				this.UpdateStatus();
			};
			this._blockableObject.ObjectUnblocked += delegate(object _, EventArgs _)
			{
				this.UpdateStatus();
			};
			this._blockableObject.ObjectBlocked += delegate(object _, EventArgs _)
			{
				this.UpdateStatus();
			};
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlert("NoUnemployed", this._loc.T(NoUnemployedStatus.NoUnemployedLocKey), this._loc.T(NoUnemployedStatus.NoUnemployedShortLocKey), 0.1f);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D2 File Offset: 0x000003D2
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E5 File Offset: 0x000003E5
		public void OnEnterFinishedState()
		{
			this.UpdateStatus();
			this._districtBuilding.ReassignedInstantDistrict += this.OnReassignedInstantDistrict;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002204 File Offset: 0x00000404
		public void OnExitFinishedState()
		{
			this._statusToggle.Deactivate();
			this._districtBuilding.ReassignedInstantDistrict -= this.OnReassignedInstantDistrict;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002228 File Offset: 0x00000428
		public void OnReassignedInstantDistrict(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002230 File Offset: 0x00000430
		public void UpdateStatus()
		{
			if (this._workplace.NumberOfAssignedWorkers == 0 && this._blockableObject.IsUnblocked && this._districtBuilding.InstantDistrict)
			{
				this._statusToggle.Activate();
				return;
			}
			this._statusToggle.Deactivate();
		}

		// Token: 0x04000008 RID: 8
		public static readonly string NoUnemployedLocKey = "Status.Work.NoUnemployed";

		// Token: 0x04000009 RID: 9
		public static readonly string NoUnemployedShortLocKey = "Status.Work.NoUnemployed.Short";

		// Token: 0x0400000A RID: 10
		public readonly ILoc _loc;

		// Token: 0x0400000B RID: 11
		public Workplace _workplace;

		// Token: 0x0400000C RID: 12
		public DistrictBuilding _districtBuilding;

		// Token: 0x0400000D RID: 13
		public BlockableObject _blockableObject;

		// Token: 0x0400000E RID: 14
		public StatusToggle _statusToggle;
	}
}
