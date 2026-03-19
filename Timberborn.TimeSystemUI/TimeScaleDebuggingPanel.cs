using System;
using Timberborn.DebuggingUI;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TimeSystemUI
{
	// Token: 0x02000009 RID: 9
	public class TimeScaleDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000028ED File Offset: 0x00000AED
		public TimeScaleDebuggingPanel(DebuggingPanel debuggingPanel)
		{
			this._debuggingPanel = debuggingPanel;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028FC File Offset: 0x00000AFC
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Time scale");
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000290F File Offset: 0x00000B0F
		public string GetText()
		{
			return string.Format("Real game speed (Time.timeScale): {0}", Time.timeScale);
		}

		// Token: 0x0400002B RID: 43
		public readonly DebuggingPanel _debuggingPanel;
	}
}
