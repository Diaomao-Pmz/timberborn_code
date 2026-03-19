using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using Timberborn.WaterContaminationBuildings;

namespace Timberborn.WaterContaminationBuildingsUI
{
	// Token: 0x02000004 RID: 4
	public class BlockedByContaminationBuildingStatus : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BlockedByContaminationBuildingStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public void Awake()
		{
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("BuildingBlockedByContamination", this._loc.T(BlockedByContaminationBuildingStatus.BlockedByContaminationLocKey), this._loc.T(BlockedByContaminationBuildingStatus.BlockedByContaminationShortLocKey), 0f);
			this._contaminationBlockableBuilding = base.GetComponent<ContaminationBlockableBuilding>();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000211E File Offset: 0x0000031E
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002134 File Offset: 0x00000334
		public void OnEnterFinishedState()
		{
			this._contaminationBlockableBuilding.BlockedByContamination += this.OnBlockedByContamination;
			this._contaminationBlockableBuilding.UnblockedByContamination += this.OnUnblockedByContamination;
			if (this._contaminationBlockableBuilding.IsBlocked)
			{
				this._statusToggle.Activate();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002187 File Offset: 0x00000387
		public void OnExitFinishedState()
		{
			this._contaminationBlockableBuilding.BlockedByContamination -= this.OnBlockedByContamination;
			this._contaminationBlockableBuilding.UnblockedByContamination -= this.OnUnblockedByContamination;
			this._statusToggle.Deactivate();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021C2 File Offset: 0x000003C2
		public void OnBlockedByContamination(object o, EventArgs eventArgs)
		{
			this._statusToggle.Activate();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021CF File Offset: 0x000003CF
		public void OnUnblockedByContamination(object o, EventArgs eventArgs)
		{
			this._statusToggle.Deactivate();
		}

		// Token: 0x04000006 RID: 6
		public static readonly string BlockedByContaminationLocKey = "Status.Buildings.BlockedByContamination";

		// Token: 0x04000007 RID: 7
		public static readonly string BlockedByContaminationShortLocKey = "Status.Buildings.BlockedByContamination.Short";

		// Token: 0x04000008 RID: 8
		public readonly ILoc _loc;

		// Token: 0x04000009 RID: 9
		public StatusToggle _statusToggle;

		// Token: 0x0400000A RID: 10
		public ContaminationBlockableBuilding _contaminationBlockableBuilding;
	}
}
