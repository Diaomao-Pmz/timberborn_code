using System;
using Bindito.Core;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class BenchmarkingConfigurator : Configurator
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002378 File Offset: 0x00000578
		public override void Configure()
		{
			base.Bind<Benchmarker>().AsSingleton();
			base.Bind<SavingBenchmarker>().AsSingleton();
			base.Bind<PerformanceSampler>().AsSingleton();
			base.Bind<BenchmarkReportCreator>().AsSingleton();
			base.Bind<StatisticsCalculator>().AsSingleton();
			base.Bind<BenchmarkLogger>().AsSingleton();
			base.Bind<PerformanceSampleFormatter>().AsSingleton();
			base.Bind<BenchmarkPanel>().AsSingleton();
			base.Bind<BenchmarkStarter>().AsSingleton();
		}
	}
}
