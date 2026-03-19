using System;
using Timberborn.CursorToolSystem;
using Timberborn.DebuggingUI;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;

namespace Timberborn.NavigationUI
{
	// Token: 0x02000004 RID: 4
	public class NavigationDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public NavigationDebuggingPanel(DebuggingPanel debuggingPanel, INavigationDebuggingService navigationDebuggingService, CursorDebugger cursorDebugger)
		{
			this._debuggingPanel = debuggingPanel;
			this._navigationDebuggingService = navigationDebuggingService;
			this._cursorDebugger = cursorDebugger;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DB File Offset: 0x000002DB
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Navigation");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020EE File Offset: 0x000002EE
		public string GetText()
		{
			return this._navigationDebuggingService.InfoAt(this._cursorDebugger.Position);
		}

		// Token: 0x04000006 RID: 6
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000007 RID: 7
		public readonly INavigationDebuggingService _navigationDebuggingService;

		// Token: 0x04000008 RID: 8
		public readonly CursorDebugger _cursorDebugger;
	}
}
