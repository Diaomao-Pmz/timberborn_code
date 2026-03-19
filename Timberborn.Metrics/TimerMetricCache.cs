using System;
using System.Collections.Generic;

namespace Timberborn.Metrics
{
	// Token: 0x02000011 RID: 17
	public class TimerMetricCache<T>
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002589 File Offset: 0x00000789
		public TimerMetricCache(IMetricsService metricsService)
		{
			this._metricsService = metricsService;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025A4 File Offset: 0x000007A4
		public ITimerMetric Get(T metricKey)
		{
			ITimerMetric timerMetric;
			if (!this._timerMetrics.TryGetValue(metricKey, out timerMetric))
			{
				timerMetric = this._metricsService.GetTimerMetric(TimerMetricCache<T>.ContextKey, metricKey.GetType().Name);
				this._timerMetrics[metricKey] = timerMetric;
			}
			return timerMetric;
		}

		// Token: 0x0400001A RID: 26
		public static readonly string ContextKey = typeof(T).Name;

		// Token: 0x0400001B RID: 27
		public readonly IMetricsService _metricsService;

		// Token: 0x0400001C RID: 28
		public readonly Dictionary<T, ITimerMetric> _timerMetrics = new Dictionary<T, ITimerMetric>();
	}
}
