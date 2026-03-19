using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200000D RID: 13
	public class NothingToDoInRangeStatus : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002599 File Offset: 0x00000799
		public NothingToDoInRangeStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025A8 File Offset: 0x000007A8
		public void Awake()
		{
			this._workplace = base.GetComponent<Workplace>();
			base.GetComponent<BlockableObject>().ObjectBlocked += delegate(object _, EventArgs _)
			{
				this.DeactivateStatus();
			};
			this._workplace.WorkerUnassigned += delegate(object _, WorkerChangedEventArgs _)
			{
				this.OnWorkerUnassigned();
			};
			string text = this._loc.T(NothingToDoInRangeStatus.NothingToDoInRangeLocKey);
			this._nothingToDoInRangeStatus = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("NothingToDo", text, text, 0f);
			base.DisableComponent();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000261D File Offset: 0x0000081D
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._nothingToDoInRangeStatus);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002630 File Offset: 0x00000830
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002638 File Offset: 0x00000838
		public void OnExitFinishedState()
		{
			this.DeactivateStatus();
			base.DisableComponent();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002646 File Offset: 0x00000846
		public void ActivateStatus()
		{
			this._nothingToDoInRangeStatus.Activate();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002653 File Offset: 0x00000853
		public void DeactivateStatus()
		{
			this._nothingToDoInRangeStatus.Deactivate();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002660 File Offset: 0x00000860
		public void OnWorkerUnassigned()
		{
			if (this._workplace.NumberOfAssignedWorkers == 0)
			{
				this.DeactivateStatus();
			}
		}

		// Token: 0x04000011 RID: 17
		public static readonly string NothingToDoInRangeLocKey = "Status.Yielding.NothingToDoInRange";

		// Token: 0x04000012 RID: 18
		public readonly ILoc _loc;

		// Token: 0x04000013 RID: 19
		public StatusToggle _nothingToDoInRangeStatus;

		// Token: 0x04000014 RID: 20
		public Workplace _workplace;
	}
}
