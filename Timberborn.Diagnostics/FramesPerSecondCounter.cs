using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Diagnostics
{
	// Token: 0x02000005 RID: 5
	public class FramesPerSecondCounter : IUpdatableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020FD File Offset: 0x000002FD
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002105 File Offset: 0x00000305
		public float AverageFramesPerSecond { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000210E File Offset: 0x0000030E
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002116 File Offset: 0x00000316
		public float MinFramesPerSecond { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002120 File Offset: 0x00000320
		public void UpdateSingleton()
		{
			float unscaledTime = Time.unscaledTime;
			FramesPerSecondCounter.Sample item = new FramesPerSecondCounter.Sample(unscaledTime, 1f / Time.unscaledDeltaTime);
			while (!this._samples.IsEmpty<FramesPerSecondCounter.Sample>() && this._samples[0].Timestamp < unscaledTime - FramesPerSecondCounter.SamplingPeriodInSeconds)
			{
				this._samples.RemoveAt(0);
			}
			this._samples.Add(item);
			float num = 0f;
			float num2 = float.PositiveInfinity;
			for (int i = 0; i < this._samples.Count; i++)
			{
				float framesPerSecond = this._samples[i].FramesPerSecond;
				num += framesPerSecond;
				if (framesPerSecond < num2)
				{
					num2 = framesPerSecond;
				}
			}
			this.AverageFramesPerSecond = num / (float)this._samples.Count;
			this.MinFramesPerSecond = num2;
		}

		// Token: 0x04000006 RID: 6
		public static readonly float SamplingPeriodInSeconds = 3f;

		// Token: 0x04000007 RID: 7
		public static readonly int EstimatedNumberOfSamples = (int)(200f * FramesPerSecondCounter.SamplingPeriodInSeconds);

		// Token: 0x0400000A RID: 10
		public readonly List<FramesPerSecondCounter.Sample> _samples = new List<FramesPerSecondCounter.Sample>(FramesPerSecondCounter.EstimatedNumberOfSamples);

		// Token: 0x02000006 RID: 6
		public readonly struct Sample
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600000C RID: 12 RVA: 0x00002226 File Offset: 0x00000426
			public float Timestamp { get; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600000D RID: 13 RVA: 0x0000222E File Offset: 0x0000042E
			public float FramesPerSecond { get; }

			// Token: 0x0600000E RID: 14 RVA: 0x00002236 File Offset: 0x00000436
			public Sample(float timestamp, float framesPerSecond)
			{
				this.Timestamp = timestamp;
				this.FramesPerSecond = framesPerSecond;
			}
		}
	}
}
