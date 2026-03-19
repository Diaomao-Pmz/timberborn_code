using System;
using System.Text;
using Timberborn.Common;
using Timberborn.DebuggingUI;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.TimeSystemUI
{
	// Token: 0x02000004 RID: 4
	public class ClockDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public ClockDebuggingPanel(IDayNightCycle dayNightCycle, DebuggingPanel debuggingPanel)
		{
			this._dayNightCycle = dayNightCycle;
			this._debuggingPanel = debuggingPanel;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Clock");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
		public string GetText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Hours passed today: {0}", this._dayNightCycle.HoursPassedToday));
			stringBuilder.AppendLine(string.Format("Day progress: {0}", this._dayNightCycle.DayProgress));
			stringBuilder.AppendLine(string.Format("Partial day number: {0}", this._dayNightCycle.PartialDayNumber));
			return stringBuilder.ToStringWithoutNewLineEnd();
		}

		// Token: 0x04000006 RID: 6
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000007 RID: 7
		public readonly DebuggingPanel _debuggingPanel;
	}
}
