using System;
using UnityEngine;

namespace Timberborn.Benchmarking
{
	// Token: 0x0200000F RID: 15
	public class FrameTimingSampler
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000317C File Offset: 0x0000137C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003184 File Offset: 0x00001384
		public float CpuMainThreadTime { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000318D File Offset: 0x0000138D
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003195 File Offset: 0x00001395
		public float CpuRenderThreadTime { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000319E File Offset: 0x0000139E
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000031A6 File Offset: 0x000013A6
		public float CpuWaitTime { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000031AF File Offset: 0x000013AF
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000031B7 File Offset: 0x000013B7
		public float CpuTotalTime { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000031C0 File Offset: 0x000013C0
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000031C8 File Offset: 0x000013C8
		public float GpuTime { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000031D1 File Offset: 0x000013D1
		public bool Enabled
		{
			get
			{
				return FrameTimingManager.IsFeatureEnabled();
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000031D8 File Offset: 0x000013D8
		public void UpdateSamples()
		{
			if (this.Enabled)
			{
				FrameTimingManager.CaptureFrameTimings();
				if (FrameTimingManager.GetLatestTimings(1U, this._frameTimings) > 0U)
				{
					FrameTiming frameTiming = this._frameTimings[0];
					this.CpuMainThreadTime = 0.001f * (float)frameTiming.cpuMainThreadFrameTime;
					this.CpuRenderThreadTime = 0.001f * (float)frameTiming.cpuRenderThreadFrameTime;
					this.CpuWaitTime = 0.001f * (float)frameTiming.cpuMainThreadPresentWaitTime;
					this.CpuTotalTime = 0.001f * (float)frameTiming.cpuFrameTime;
					this.GpuTime = 0.001f * (float)frameTiming.gpuFrameTime;
				}
			}
		}

		// Token: 0x04000045 RID: 69
		public readonly FrameTiming[] _frameTimings = new FrameTiming[1];
	}
}
