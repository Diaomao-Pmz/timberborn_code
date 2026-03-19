using System;
using Timberborn.CommandLine;
using Timberborn.SingletonSystem;

namespace Timberborn.Benchmarking
{
	// Token: 0x0200000E RID: 14
	public class BenchmarkStarter : ILoadableSingleton
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003075 File Offset: 0x00001275
		public BenchmarkStarter(ICommandLineArguments commandLineArguments, Benchmarker benchmarker, SavingBenchmarker savingBenchmarker)
		{
			this._commandLineArguments = commandLineArguments;
			this._benchmarker = benchmarker;
			this._savingBenchmarker = savingBenchmarker;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003092 File Offset: 0x00001292
		public void Load()
		{
			this.StartBenchmark();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000309C File Offset: 0x0000129C
		public void StartBenchmark()
		{
			if (this._commandLineArguments.Has(BenchmarkStarter.BenchmarkLengthKey) && this._commandLineArguments.Has(BenchmarkStarter.BenchmarkSpeedKey))
			{
				int @int = this._commandLineArguments.GetInt(BenchmarkStarter.BenchmarkLengthKey);
				int int2 = this._commandLineArguments.GetInt(BenchmarkStarter.BenchmarkSpeedKey);
				int int3 = this._commandLineArguments.GetInt(BenchmarkStarter.BenchmarkWarmUpLengthKey);
				this._benchmarker.StartBenchmark(@int, int3, int2);
				return;
			}
			if (this._commandLineArguments.Has(BenchmarkStarter.BenchmarkSaveCountKey))
			{
				int int4 = this._commandLineArguments.GetInt(BenchmarkStarter.BenchmarkWarmUpLengthKey);
				int int5 = this._commandLineArguments.GetInt(BenchmarkStarter.BenchmarkSaveCountKey);
				this._savingBenchmarker.StartBenchmark(int4, int5);
			}
		}

		// Token: 0x04000039 RID: 57
		public static readonly string BenchmarkLengthKey = "benchmarkLength";

		// Token: 0x0400003A RID: 58
		public static readonly string BenchmarkSpeedKey = "benchmarkSpeed";

		// Token: 0x0400003B RID: 59
		public static readonly string BenchmarkWarmUpLengthKey = "benchmarkWarmUpLength";

		// Token: 0x0400003C RID: 60
		public static readonly string BenchmarkSaveCountKey = "benchmarkSaveCount";

		// Token: 0x0400003D RID: 61
		public readonly ICommandLineArguments _commandLineArguments;

		// Token: 0x0400003E RID: 62
		public readonly Benchmarker _benchmarker;

		// Token: 0x0400003F RID: 63
		public readonly SavingBenchmarker _savingBenchmarker;
	}
}
