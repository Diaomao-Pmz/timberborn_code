using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timberborn.Metrics
{
	// Token: 0x02000009 RID: 9
	public class MetricsFormatter
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021E8 File Offset: 0x000003E8
		public string FormatMetrics(IEnumerable<NamedMetricsContext> contexts)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (NamedMetricsContext context in contexts)
			{
				MetricsFormatter.FormatContext(stringBuilder, context);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000223C File Offset: 0x0000043C
		public static void FormatContext(StringBuilder metrics, NamedMetricsContext context)
		{
			List<NamedTimerMetric> list = (from metric in context.GetAllTimers()
			orderby metric.ElapsedMilliseconds descending
			select metric).ToList<NamedTimerMetric>();
			long num = list.Sum((NamedTimerMetric metric) => metric.ElapsedMilliseconds);
			metrics.AppendLine(string.Format("{0} {1:0.00}s", context.Name, (float)num / 1000f));
			metrics.AppendLine("Name,percentage of elapsed");
			foreach (NamedTimerMetric namedTimerMetric in ((IEnumerable<NamedTimerMetric>)list))
			{
				double num2 = (double)namedTimerMetric.ElapsedMilliseconds / (double)num;
				metrics.AppendLine(string.Format("{0},{1:p}", namedTimerMetric.Name, num2));
			}
			metrics.AppendLine();
		}
	}
}
