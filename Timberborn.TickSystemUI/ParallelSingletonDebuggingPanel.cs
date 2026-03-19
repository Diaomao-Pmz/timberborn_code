using System;
using Timberborn.Debugging;
using Timberborn.DebuggingUI;
using Timberborn.Multithreading;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.TickSystemUI
{
	// Token: 0x02000004 RID: 4
	public class ParallelSingletonDebuggingPanel : IDebuggingPanel, ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public ParallelSingletonDebuggingPanel(DebuggingPanel debuggingPanel, ITickableSingletonService tickableSingletonService, DebugModeManager debugModeManager, IParallelizer parallelizer)
		{
			this._debuggingPanel = debuggingPanel;
			this._tickableSingletonService = tickableSingletonService;
			this._debugModeManager = debugModeManager;
			this._parallelizer = parallelizer;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E0 File Offset: 0x000002E0
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Parallel singletons");
			this._text = this.GetText("Waiting for tick...");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002104 File Offset: 0x00000304
		public void Tick()
		{
			if (this._debugModeManager.Enabled)
			{
				this._text = this.GetText(string.Format("Total time: {0:F0}ms", this._tickableSingletonService.LastParallelTickDuration.TotalMilliseconds));
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000214C File Offset: 0x0000034C
		public string GetText()
		{
			return this._text;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002154 File Offset: 0x00000354
		public string GetText(string timeText)
		{
			return string.Format("Number of threads: {0}\n{1}", this._parallelizer.NumberOfThreads, timeText);
		}

		// Token: 0x04000006 RID: 6
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000007 RID: 7
		public readonly ITickableSingletonService _tickableSingletonService;

		// Token: 0x04000008 RID: 8
		public readonly IParallelizer _parallelizer;

		// Token: 0x04000009 RID: 9
		public readonly DebugModeManager _debugModeManager;

		// Token: 0x0400000A RID: 10
		public string _text;
	}
}
