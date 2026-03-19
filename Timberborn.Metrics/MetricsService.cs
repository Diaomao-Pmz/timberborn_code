using System;
using System.Collections.Generic;
using Timberborn.CommandLine;
using Timberborn.FileSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Metrics
{
	// Token: 0x0200000D RID: 13
	public class MetricsService : IMetricsService, ILoadableSingleton
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002434 File Offset: 0x00000634
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000243C File Offset: 0x0000063C
		public bool MetricsEnabled { get; private set; }

		// Token: 0x06000022 RID: 34 RVA: 0x00002445 File Offset: 0x00000645
		public MetricsService(MetricsRepository metricsRepository, MetricsFormatter metricsFormatter, IFileService fileService, ICommandLineArguments commandLineArguments)
		{
			this._metricsRepository = metricsRepository;
			this._metricsFormatter = metricsFormatter;
			this._fileService = fileService;
			this._commandLineArguments = commandLineArguments;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000246A File Offset: 0x0000066A
		public void Load()
		{
			this.MetricsEnabled = this._commandLineArguments.Has(MetricsService.MetricsKey);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002482 File Offset: 0x00000682
		public ITimerMetric GetTimerMetric(string contextKey, string timerKey)
		{
			return this._metricsRepository.GetTimer(contextKey, timerKey);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002491 File Offset: 0x00000691
		public void ResetMetrics()
		{
			this._metricsRepository.ResetAllTimers();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024A0 File Offset: 0x000006A0
		public void WriteCollectedDataToFile(string path)
		{
			if (this.MetricsEnabled)
			{
				IEnumerable<NamedMetricsContext> allContexts = this._metricsRepository.GetAllContexts();
				string text = this._metricsFormatter.FormatMetrics(allContexts);
				this._fileService.WriteTextToFile(path + ".csv", text);
			}
		}

		// Token: 0x0400000F RID: 15
		public static readonly string MetricsKey = "metrics";

		// Token: 0x04000011 RID: 17
		public readonly MetricsRepository _metricsRepository;

		// Token: 0x04000012 RID: 18
		public readonly MetricsFormatter _metricsFormatter;

		// Token: 0x04000013 RID: 19
		public readonly IFileService _fileService;

		// Token: 0x04000014 RID: 20
		public readonly ICommandLineArguments _commandLineArguments;
	}
}
