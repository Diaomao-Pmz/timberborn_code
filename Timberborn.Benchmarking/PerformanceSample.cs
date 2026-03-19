using System;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000010 RID: 16
	public readonly struct PerformanceSample
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003284 File Offset: 0x00001484
		public float TimeInSeconds { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000328C File Offset: 0x0000148C
		public float DeltaInSeconds { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003294 File Offset: 0x00001494
		public double TickLengthInSeconds { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000329C File Offset: 0x0000149C
		public float CpuMainThreadTime { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000032A4 File Offset: 0x000014A4
		public float CpuRenderThreadTime { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000032AC File Offset: 0x000014AC
		public float CpuWaitTime { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000032B4 File Offset: 0x000014B4
		public float CpuTotalTime { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000032BC File Offset: 0x000014BC
		public float GpuTime { get; }

		// Token: 0x0600006D RID: 109 RVA: 0x000032C4 File Offset: 0x000014C4
		public PerformanceSample(float timeInSeconds, float deltaInSeconds, double tickLengthInSeconds, float cpuMainThreadTime, float cpuRenderThreadTime, float cpuWaitTime, float cpuTotalTime, float gpuTime)
		{
			this.TimeInSeconds = timeInSeconds;
			this.DeltaInSeconds = deltaInSeconds;
			this.TickLengthInSeconds = tickLengthInSeconds;
			this.CpuMainThreadTime = cpuMainThreadTime;
			this.CpuRenderThreadTime = cpuRenderThreadTime;
			this.CpuWaitTime = cpuWaitTime;
			this.CpuTotalTime = cpuTotalTime;
			this.GpuTime = gpuTime;
		}
	}
}
