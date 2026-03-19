using System;

namespace Timberborn.Metrics
{
	// Token: 0x0200000F RID: 15
	public class NamedTimerMetric
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000251C File Offset: 0x0000071C
		public string Name { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002524 File Offset: 0x00000724
		public long ElapsedMilliseconds { get; }

		// Token: 0x0600002D RID: 45 RVA: 0x0000252C File Offset: 0x0000072C
		public NamedTimerMetric(string name, long elapsedMilliseconds)
		{
			this.Name = name;
			this.ElapsedMilliseconds = elapsedMilliseconds;
		}
	}
}
