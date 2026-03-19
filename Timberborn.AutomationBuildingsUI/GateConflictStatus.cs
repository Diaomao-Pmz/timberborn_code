using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000010 RID: 16
	public class GateConflictStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003126 File Offset: 0x00001326
		public GateConflictStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003138 File Offset: 0x00001338
		public void Awake()
		{
			this._gate = base.GetComponent<Gate>();
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("GateConflict", this._loc.T(GateConflictStatus.ConflictLocKey), this._loc.T(GateConflictStatus.ConflictShortLocKey), 0f);
			this._gate.StateChanged += this.OnStateChanged;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000319D File Offset: 0x0000139D
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000031B0 File Offset: 0x000013B0
		public void OnStateChanged(object sender, EventArgs e)
		{
			this._statusToggle.Toggle(this._gate.IsConflict);
		}

		// Token: 0x0400005B RID: 91
		public static readonly string ConflictLocKey = "Status.Buildings.GateConflict";

		// Token: 0x0400005C RID: 92
		public static readonly string ConflictShortLocKey = "Status.Buildings.GateConflict.Short";

		// Token: 0x0400005D RID: 93
		public readonly ILoc _loc;

		// Token: 0x0400005E RID: 94
		public Gate _gate;

		// Token: 0x0400005F RID: 95
		public StatusToggle _statusToggle;
	}
}
