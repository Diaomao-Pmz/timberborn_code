using System;
using Timberborn.Debugging;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000006 RID: 6
	public class DebuggingPanelResetter : IDevModule
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000023FA File Offset: 0x000005FA
		public DebuggingPanelResetter(DebuggingPanel debuggingPanel, ObjectDebuggingPanel objectDebuggingPanel)
		{
			this._debuggingPanel = debuggingPanel;
			this._objectDebuggingPanel = objectDebuggingPanel;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002410 File Offset: 0x00000610
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Reset debugging panels position", new Action(this.ResetPanelsPosition))).Build();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002437 File Offset: 0x00000637
		public void ResetPanelsPosition()
		{
			this._debuggingPanel.ResetPanelPosition();
			this._objectDebuggingPanel.ResetPanelPosition();
		}

		// Token: 0x04000017 RID: 23
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000018 RID: 24
		public readonly ObjectDebuggingPanel _objectDebuggingPanel;
	}
}
