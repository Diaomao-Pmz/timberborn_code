using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000012 RID: 18
	public class PerformanceSampler : ILateUpdatableSingleton
	{
		// Token: 0x06000071 RID: 113 RVA: 0x000033B9 File Offset: 0x000015B9
		public PerformanceSampler(Ticker ticker)
		{
			this._ticker = ticker;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000033C8 File Offset: 0x000015C8
		public ReadOnlyList<PerformanceSample> Samples
		{
			get
			{
				return this._samples.AsReadOnlyList<PerformanceSample>();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000033D5 File Offset: 0x000015D5
		public bool DetailedSamplesAvailable
		{
			get
			{
				return this._frameSampler.Enabled;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033E2 File Offset: 0x000015E2
		public void LateUpdateSingleton()
		{
			if (this._enabled)
			{
				this.TakeSample();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000033F4 File Offset: 0x000015F4
		public void StartSampling(int samplingLengthInSeconds)
		{
			int capacity = PerformanceSampler.EstimatedNumberOfSamplesInSecond * samplingLengthInSeconds;
			this._samples = new List<PerformanceSample>(capacity);
			this._frameSampler = new FrameTimingSampler();
			this._enabled = true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003427 File Offset: 0x00001627
		public void StopSampling()
		{
			this._enabled = false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003430 File Offset: 0x00001630
		public void TakeSample()
		{
			this._frameSampler.UpdateSamples();
			this._samples.Add(new PerformanceSample(Time.unscaledTime, Time.unscaledDeltaTime, this._lengthOfPreviousTickInSeconds, this._frameSampler.CpuMainThreadTime, this._frameSampler.CpuRenderThreadTime, this._frameSampler.CpuWaitTime, this._frameSampler.CpuTotalTime, this._frameSampler.GpuTime));
			this._lengthOfPreviousTickInSeconds = this._ticker.LengthOfLastTickInSeconds;
		}

		// Token: 0x0400004E RID: 78
		public static readonly int EstimatedNumberOfSamplesInSecond = 200;

		// Token: 0x0400004F RID: 79
		public readonly Ticker _ticker;

		// Token: 0x04000050 RID: 80
		public List<PerformanceSample> _samples;

		// Token: 0x04000051 RID: 81
		public FrameTimingSampler _frameSampler;

		// Token: 0x04000052 RID: 82
		public double _lengthOfPreviousTickInSeconds;

		// Token: 0x04000053 RID: 83
		public bool _enabled;
	}
}
