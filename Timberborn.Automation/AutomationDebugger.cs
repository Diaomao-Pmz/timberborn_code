using System;
using Timberborn.TickSystem;

namespace Timberborn.Automation
{
	// Token: 0x02000007 RID: 7
	public class AutomationDebugger : ITickableSingleton
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000024F7 File Offset: 0x000006F7
		public AutomationDebuggerMetric PartitioningTimeMs { get; } = new AutomationDebuggerMetric();

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024FF File Offset: 0x000006FF
		public AutomationDebuggerMetric AddingTimeMs { get; } = new AutomationDebuggerMetric();

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002507 File Offset: 0x00000707
		public AutomationDebuggerMetric RemovingTimeMs { get; } = new AutomationDebuggerMetric();

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000250F File Offset: 0x0000070F
		public AutomationDebuggerMetric MergingTimeMs { get; } = new AutomationDebuggerMetric();

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002517 File Offset: 0x00000717
		public AutomationDebuggerMetric PlanningTimeMs { get; } = new AutomationDebuggerMetric();

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000251F File Offset: 0x0000071F
		public AutomationDebuggerMetric EvaluationTimeMs { get; } = new AutomationDebuggerMetric();

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002527 File Offset: 0x00000727
		public AutomationDebuggerMetric TickEvaluationTimeMs { get; } = new AutomationDebuggerMetric();

		// Token: 0x06000021 RID: 33 RVA: 0x00002530 File Offset: 0x00000730
		public void Tick()
		{
			this.PartitioningTimeMs.Reset();
			this.AddingTimeMs.Reset();
			this.RemovingTimeMs.Reset();
			this.MergingTimeMs.Reset();
			this.PlanningTimeMs.Reset();
			this.EvaluationTimeMs.Reset();
			this.TickEvaluationTimeMs.Reset();
		}
	}
}
