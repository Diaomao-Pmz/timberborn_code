using System;
using System.Diagnostics;

namespace Timberborn.Metrics
{
	// Token: 0x02000010 RID: 16
	public class TimerMetric : ITimerMetric
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002542 File Offset: 0x00000742
		public long ElapsedMilliseconds
		{
			get
			{
				return this._stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000254F File Offset: 0x0000074F
		public void Resume()
		{
			this._stopwatch.Start();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000255C File Offset: 0x0000075C
		public void Pause()
		{
			this._stopwatch.Stop();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002569 File Offset: 0x00000769
		public void Reset()
		{
			this._stopwatch.Reset();
		}

		// Token: 0x04000019 RID: 25
		public readonly Stopwatch _stopwatch = new Stopwatch();
	}
}
