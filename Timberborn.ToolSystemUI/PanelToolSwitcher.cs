using System;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.ToolSystemUI
{
	// Token: 0x0200000A RID: 10
	public class PanelToolSwitcher : ILoadableSingleton
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000024E3 File Offset: 0x000006E3
		public PanelToolSwitcher(EventBus eventBus, ToolService toolService)
		{
			this._eventBus = eventBus;
			this._toolService = toolService;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024F9 File Offset: 0x000006F9
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002507 File Offset: 0x00000707
		[OnEvent]
		public void OnPanelShown(PanelShownEvent panelShownEvent)
		{
			if (!panelShownEvent.IsDialog && !this._toolService.IsDefaultToolActive)
			{
				this._toolService.ExitTool();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002529 File Offset: 0x00000729
		[OnEvent]
		public void OnPanelHidden(PanelHiddenEvent panelHiddenEvent)
		{
			if (!panelHiddenEvent.WasDialog && !panelHiddenEvent.AnyPanelShown)
			{
				this._toolService.SwitchToDefaultTool();
			}
		}

		// Token: 0x04000012 RID: 18
		public readonly EventBus _eventBus;

		// Token: 0x04000013 RID: 19
		public readonly ToolService _toolService;
	}
}
