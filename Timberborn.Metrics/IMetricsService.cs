using System;

namespace Timberborn.Metrics
{
	// Token: 0x02000004 RID: 4
	public interface IMetricsService
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		bool MetricsEnabled { get; }

		// Token: 0x06000004 RID: 4
		ITimerMetric GetTimerMetric(string contextKey, string timerKey);

		// Token: 0x06000005 RID: 5
		void ResetMetrics();

		// Token: 0x06000006 RID: 6
		void WriteCollectedDataToFile(string path);
	}
}
