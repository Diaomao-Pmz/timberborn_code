using System;
using System.Collections.Generic;

namespace Timberborn.Metrics
{
	// Token: 0x0200000E RID: 14
	public class NamedMetricsContext
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000024F1 File Offset: 0x000006F1
		public string Name { get; }

		// Token: 0x06000029 RID: 41 RVA: 0x000024F9 File Offset: 0x000006F9
		public NamedMetricsContext(string name, MetricsContext metricsContext)
		{
			this.Name = name;
			this._metricsContext = metricsContext;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000250F File Offset: 0x0000070F
		public IEnumerable<NamedTimerMetric> GetAllTimers()
		{
			return this._metricsContext.GetAllTimers();
		}

		// Token: 0x04000016 RID: 22
		public readonly MetricsContext _metricsContext;
	}
}
