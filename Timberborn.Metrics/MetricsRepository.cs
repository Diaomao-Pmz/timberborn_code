using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.Metrics
{
	// Token: 0x0200000B RID: 11
	public class MetricsRepository
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002344 File Offset: 0x00000544
		public ITimerMetric GetTimer(string contextKey, string timerKey)
		{
			MetricsContext metricsContext;
			if (!this._contexts.TryGetValue(contextKey, out metricsContext))
			{
				metricsContext = new MetricsContext();
				this._contexts[contextKey] = metricsContext;
			}
			return metricsContext.GetTimer(timerKey);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000237B File Offset: 0x0000057B
		public IEnumerable<NamedMetricsContext> GetAllContexts()
		{
			return from keyAndContext in this._contexts
			select new NamedMetricsContext(keyAndContext.Key, keyAndContext.Value);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023A8 File Offset: 0x000005A8
		public void ResetAllTimers()
		{
			foreach (MetricsContext metricsContext in this._contexts.Values)
			{
				metricsContext.ResetAllTimers();
			}
		}

		// Token: 0x0400000C RID: 12
		public readonly Dictionary<string, MetricsContext> _contexts = new Dictionary<string, MetricsContext>();
	}
}
