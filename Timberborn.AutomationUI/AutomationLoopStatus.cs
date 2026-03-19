using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.AutomationUI
{
	// Token: 0x0200000C RID: 12
	public class AutomationLoopStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002636 File Offset: 0x00000836
		public AutomationLoopStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002648 File Offset: 0x00000848
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("AutomationLoop", this._loc.T(AutomationLoopStatus.AutomationLoopLocKey), this._loc.T(AutomationLoopStatus.AutomationLoopShortLocKey), 0f);
			this._automator.IsCyclicOrBlockedChanged += this.OnChanged;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026AD File Offset: 0x000008AD
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026C0 File Offset: 0x000008C0
		public void OnChanged(object sender, EventArgs e)
		{
			this._statusToggle.Toggle(this._automator.IsCyclicOrBlocked);
		}

		// Token: 0x04000019 RID: 25
		public static readonly string AutomationLoopLocKey = "Status.Automation.AutomationLoop";

		// Token: 0x0400001A RID: 26
		public static readonly string AutomationLoopShortLocKey = "Status.Automation.AutomationLoop.Short";

		// Token: 0x0400001B RID: 27
		public readonly ILoc _loc;

		// Token: 0x0400001C RID: 28
		public Automator _automator;

		// Token: 0x0400001D RID: 29
		public StatusToggle _statusToggle;
	}
}
