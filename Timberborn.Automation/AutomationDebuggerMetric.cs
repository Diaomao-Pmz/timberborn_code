using System;
using System.Diagnostics;

namespace Timberborn.Automation
{
	// Token: 0x02000008 RID: 8
	public class AutomationDebuggerMetric
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000025EC File Offset: 0x000007EC
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000025F4 File Offset: 0x000007F4
		public double Total { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025FD File Offset: 0x000007FD
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002605 File Offset: 0x00000805
		public double Max { get; private set; }

		// Token: 0x06000027 RID: 39 RVA: 0x00002610 File Offset: 0x00000810
		public void Register(Stopwatch stopwatch)
		{
			double totalMilliseconds = stopwatch.Elapsed.TotalMilliseconds;
			this.Total += totalMilliseconds;
			if (totalMilliseconds > this.Max)
			{
				this.Max = totalMilliseconds;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000264A File Offset: 0x0000084A
		public void Reset()
		{
			this.Total = 0.0;
			this.Max = 0.0;
		}
	}
}
