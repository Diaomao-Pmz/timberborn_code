using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Wonders
{
	// Token: 0x0200000A RID: 10
	public class NotEnoughWorkersWonderBlocker : BaseComponent, IAwakableComponent, IStartableComponent, IWonderBlocker
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000215C File Offset: 0x0000035C
		public NotEnoughWorkersWonderBlocker(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000216C File Offset: 0x0000036C
		public void Awake()
		{
			this._wonder = base.GetComponent<Wonder>();
			this._workplace = base.GetComponent<Workplace>();
			this._statusToggle = StatusToggle.CreateNormalStatus("NoUnemployed", this._loc.T(NotEnoughWorkersWonderBlocker.DisallowReasonLocKey));
			this._workplace.WorkerAssigned += delegate(object _, WorkerChangedEventArgs _)
			{
				this.UpdateStatus();
			};
			this._workplace.WorkerUnassigned += delegate(object _, WorkerChangedEventArgs _)
			{
				this.UpdateStatus();
			};
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021DF File Offset: 0x000003DF
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F4 File Offset: 0x000003F4
		public bool IsWonderBlocked()
		{
			return this._workplace.AssignedWorkers.Count != this._workplace.MaxWorkers;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002224 File Offset: 0x00000424
		public void UpdateStatus()
		{
			if (!this.IsWonderBlocked() || this._wonder.IsActive)
			{
				this._statusToggle.Deactivate();
				return;
			}
			this._statusToggle.Activate();
		}

		// Token: 0x0400000B RID: 11
		public static readonly string DisallowReasonLocKey = "Status.Wonder.NotEnoughWorkers";

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;

		// Token: 0x0400000D RID: 13
		public Wonder _wonder;

		// Token: 0x0400000E RID: 14
		public Workplace _workplace;

		// Token: 0x0400000F RID: 15
		public StatusToggle _statusToggle;
	}
}
