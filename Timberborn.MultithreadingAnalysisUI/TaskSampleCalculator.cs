using System;
using System.Diagnostics;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x0200000B RID: 11
	public static class TaskSampleCalculator
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002B5D File Offset: 0x00000D5D
		public static double TicksToMs(long ticks)
		{
			return (double)ticks * 1000.0 / (double)Stopwatch.Frequency;
		}
	}
}
