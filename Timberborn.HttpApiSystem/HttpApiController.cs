using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200001C RID: 28
	public class HttpApiController : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00004295 File Offset: 0x00002495
		public HttpApiController(HttpApi httpApi, ILoc loc)
		{
			this._httpApi = httpApi;
			this._loc = loc;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000042AB File Offset: 0x000024AB
		public void Awake()
		{
			this._statusToggle = StatusToggle.CreatePriorityStatusWithAlertAndFloatingIcon("ApiStopped", this._loc.T(HttpApiController.ApiStoppedLocKey), this._loc.T(HttpApiController.ApiStoppedShortLocKey), 0f);
			base.DisableComponent();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000042E8 File Offset: 0x000024E8
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000042FB File Offset: 0x000024FB
		public void OnEnterFinishedState()
		{
			this.UpdateStatus();
			this._httpApi.IsRunningChanged += this.OnIsRunningChanged;
			base.EnableComponent();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004320 File Offset: 0x00002520
		public void OnExitFinishedState()
		{
			this._httpApi.IsRunningChanged -= this.OnIsRunningChanged;
			base.DisableComponent();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000433F File Offset: 0x0000253F
		public void OnIsRunningChanged(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004347 File Offset: 0x00002547
		public void UpdateStatus()
		{
			this._statusToggle.Toggle(!this._httpApi.IsRunning);
		}

		// Token: 0x04000075 RID: 117
		public static readonly string ApiStoppedLocKey = "Status.Automation.ApiStopped";

		// Token: 0x04000076 RID: 118
		public static readonly string ApiStoppedShortLocKey = "Status.Automation.ApiStopped.Short";

		// Token: 0x04000077 RID: 119
		public readonly HttpApi _httpApi;

		// Token: 0x04000078 RID: 120
		public readonly ILoc _loc;

		// Token: 0x04000079 RID: 121
		public StatusToggle _statusToggle;
	}
}
