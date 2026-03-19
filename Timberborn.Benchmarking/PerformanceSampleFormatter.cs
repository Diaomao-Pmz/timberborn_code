using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000011 RID: 17
	public class PerformanceSampleFormatter
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003304 File Offset: 0x00001504
		public string FormatAsCsv(IEnumerable<PerformanceSample> samples)
		{
			string item = "Time[s],Delta[s],Previous Update Tick length[s],Previous Update Base Length[s]";
			IEnumerable<string> second = samples.Select(new Func<PerformanceSample, string>(PerformanceSampleFormatter.SampleToRow));
			IEnumerable<string> values = Enumerables.One<string>(item).Concat(second);
			return string.Join(Environment.NewLine ?? "", values);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000334C File Offset: 0x0000154C
		public static string SampleToRow(PerformanceSample performanceSample)
		{
			float deltaInSeconds = performanceSample.DeltaInSeconds;
			double tickLengthInSeconds = performanceSample.TickLengthInSeconds;
			double num = (double)deltaInSeconds - tickLengthInSeconds;
			return string.Format("{0:0.0000}", performanceSample.TimeInSeconds) + string.Format(",{0:0.0000}", deltaInSeconds) + string.Format(",{0:0.0000}", tickLengthInSeconds) + string.Format(",{0:0.0000}", num);
		}
	}
}
