using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.Metrics
{
	// Token: 0x02000007 RID: 7
	public class MetricsContext
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020F4 File Offset: 0x000002F4
		public ITimerMetric GetTimer(string key)
		{
			TimerMetric timerMetric;
			if (!this._timerMetrics.TryGetValue(key, out timerMetric))
			{
				timerMetric = new TimerMetric();
				this._timerMetrics[key] = timerMetric;
			}
			return timerMetric;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002125 File Offset: 0x00000325
		public IEnumerable<NamedTimerMetric> GetAllTimers()
		{
			return from keyAndTimer in this._timerMetrics
			select new NamedTimerMetric(keyAndTimer.Key, keyAndTimer.Value.ElapsedMilliseconds);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002154 File Offset: 0x00000354
		public void ResetAllTimers()
		{
			foreach (TimerMetric timerMetric in this._timerMetrics.Values)
			{
				timerMetric.Reset();
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly Dictionary<string, TimerMetric> _timerMetrics = new Dictionary<string, TimerMetric>();
	}
}
