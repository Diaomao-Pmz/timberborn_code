using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000008 RID: 8
	public class BenchmarkParameters
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002846 File Offset: 0x00000A46
		public int SamplingLengthInSeconds { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000284E File Offset: 0x00000A4E
		public int WarmUpLengthInSeconds { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002856 File Offset: 0x00000A56
		public int GameSpeed { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000285E File Offset: 0x00000A5E
		public bool DetailedSamplesAvailable { get; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002866 File Offset: 0x00000A66
		public BenchmarkParameters(int samplingLengthInSeconds, int warmUpLengthInSeconds, int gameSpeed, IEnumerable<PerformanceSample> performanceSamples, bool detailedSamplesAvailable)
		{
			this.SamplingLengthInSeconds = samplingLengthInSeconds;
			this.WarmUpLengthInSeconds = warmUpLengthInSeconds;
			this.GameSpeed = gameSpeed;
			this._performanceSamples = performanceSamples.ToList<PerformanceSample>();
			this.DetailedSamplesAvailable = detailedSamplesAvailable;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002898 File Offset: 0x00000A98
		public ReadOnlyList<PerformanceSample> PerformanceSamples
		{
			get
			{
				return this._performanceSamples.AsReadOnlyList<PerformanceSample>();
			}
		}

		// Token: 0x04000024 RID: 36
		public readonly List<PerformanceSample> _performanceSamples;
	}
}
