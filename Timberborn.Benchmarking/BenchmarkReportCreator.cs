using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Timberborn.Buildings;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.ConstructionSites;
using Timberborn.EntitySystem;
using Timberborn.FeatureToggleSystem;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.SceneLoading;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.Benchmarking
{
	// Token: 0x0200000B RID: 11
	public class BenchmarkReportCreator
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002A31 File Offset: 0x00000C31
		public BenchmarkReportCreator(StatisticsCalculator statisticsCalculator, CharacterPopulation characterPopulation, EntityComponentRegistry entityComponentRegistry, ISceneLoader sceneLoader, ITickService tickService)
		{
			this._statisticsCalculator = statisticsCalculator;
			this._characterPopulation = characterPopulation;
			this._entityComponentRegistry = entityComponentRegistry;
			this._sceneLoader = sceneLoader;
			this._tickService = tickService;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A60 File Offset: 0x00000C60
		public BenchmarkReport CreateReport(BenchmarkParameters benchmarkParameters, SaveReference saveReference)
		{
			BenchmarkReport.Builder builder = new BenchmarkReport.Builder();
			builder.AddTitle("Benchmark report");
			this.AddBasicInfo(builder, benchmarkParameters, saveReference);
			this.AddPerformanceReports(builder, benchmarkParameters);
			BenchmarkReportCreator.AddSystemInfo(builder);
			return builder.Build();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A9C File Offset: 0x00000C9C
		public void AddBasicInfo(BenchmarkReport.Builder builder, BenchmarkParameters benchmarkParameters, SaveReference saveReference)
		{
			builder.AddEntry("Date and time", BenchmarkReportCreator.GetCurrentDateAndTime()).AddEntry("Mode", BenchmarkReportCreator.GetApplicationMode()).AddEntry("Save", saveReference.ToString()).AddEntry("Resolution", Screen.currentResolution).AddEntry("Quality", BenchmarkReportCreator.GetNameOfCurrentQualityLevel()).AddEntry("VSync", QualitySettings.vSyncCount).AddEntry("Game speed", string.Format("x{0}", benchmarkParameters.GameSpeed)).AddEntry("Warm up duration", benchmarkParameters.WarmUpLengthInSeconds, string.Format("{0}s", benchmarkParameters.WarmUpLengthInSeconds)).AddEntry("Sampling duration", benchmarkParameters.SamplingLengthInSeconds, string.Format("{0}s", benchmarkParameters.SamplingLengthInSeconds)).AddEntry("Number of characters", this._characterPopulation.NumberOfCharacters).AddEntry("Number of buildings", this._entityComponentRegistry.GetEnabled<Building>().Count<Building>()).AddEntry("Number of construction sites", this._entityComponentRegistry.GetEnabled<ConstructionSite>().Count<ConstructionSite>()).AddEntry("Timestep", this._tickService.TickIntervalInSeconds, string.Format("{0:0.000}s", this._tickService.TickIntervalInSeconds)).AddEntry("Unity version", Application.unityVersion).AddEntry("Application data path", Application.dataPath).AddEntry("Load time", this._sceneLoader.LastLoadTimeMs, string.Format("{0}ms", this._sceneLoader.LastLoadTimeMs)).AddEntry("Feature toggles", BenchmarkReportCreator.GetFeatureToggles());
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C70 File Offset: 0x00000E70
		public void AddPerformanceReports(BenchmarkReport.Builder builder, BenchmarkParameters benchmarkParameters)
		{
			ReadOnlyList<PerformanceSample> performanceSamples = benchmarkParameters.PerformanceSamples;
			this.AddPerformanceReport(builder, "Delta time", from s in performanceSamples
			select s.DeltaInSeconds, true);
			if (benchmarkParameters.DetailedSamplesAvailable)
			{
				this.AddDetailedPerformanceReports(builder, performanceSamples);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public void AddDetailedPerformanceReports(BenchmarkReport.Builder builder, IReadOnlyList<PerformanceSample> performanceSamples)
		{
			this.AddPerformanceReport(builder, "CPU Total", from s in performanceSamples
			select s.CpuTotalTime, true);
			this.AddPerformanceReport(builder, "CPU Main", from s in performanceSamples
			select s.CpuMainThreadTime, false);
			this.AddPerformanceReport(builder, "CPU Render", from s in performanceSamples
			select s.CpuRenderThreadTime, false);
			this.AddPerformanceReport(builder, "CPU Wait", from s in performanceSamples
			select s.CpuWaitTime, false);
			this.AddPerformanceReport(builder, "GPU", from s in performanceSamples
			select s.GpuTime, false);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public void AddPerformanceReport(BenchmarkReport.Builder builder, string sectionName, IEnumerable<float> values, bool showFps)
		{
			List<float> list = values.ToList<float>();
			builder.AddSection(sectionName);
			this.AddPercentile(builder, list, 95f, showFps);
			this.AddPercentile(builder, list, 98f, showFps);
			this.AddPercentile(builder, list, 99.5f, showFps);
			this.AddPercentile(builder, list, 99.9f, showFps);
			BenchmarkReportCreator.AddFrameLength(builder, "Median", this._statisticsCalculator.Median(list), showFps);
			BenchmarkReportCreator.AddFrameLength(builder, "Average", list.Average(), showFps);
			BenchmarkReportCreator.AddFrameLength(builder, "Min", list.Min(), showFps);
			BenchmarkReportCreator.AddFrameLength(builder, "Max", list.Max(), showFps);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002E84 File Offset: 0x00001084
		public void AddPercentile(BenchmarkReport.Builder builder, IEnumerable<float> values, float percentile, bool showFps)
		{
			float frameLengthInSeconds = this._statisticsCalculator.Percentile(values, percentile);
			BenchmarkReportCreator.AddFrameLength(builder, string.Format("{0:0.0}th", percentile), frameLengthInSeconds, showFps);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002EB8 File Offset: 0x000010B8
		public static void AddFrameLength(BenchmarkReport.Builder builder, string name, float frameLengthInSeconds, bool showFps)
		{
			builder.AddEntry(name, frameLengthInSeconds * 1000f, BenchmarkReportCreator.FormatFrameLength(frameLengthInSeconds, showFps) ?? "");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002EE0 File Offset: 0x000010E0
		public static string FormatFrameLength(float frameLengthInSeconds, bool showFps)
		{
			float num = frameLengthInSeconds * 1000f;
			string text = string.Format("{0:0.0} ms", num);
			if (!showFps)
			{
				return text;
			}
			return string.Format("{0} ({1:0.0} fps)", text, 1f / frameLengthInSeconds);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F24 File Offset: 0x00001124
		public static void AddSystemInfo(BenchmarkReport.Builder builder)
		{
			builder.AddSection("System").AddEntry("CPU", SystemInfo.processorType).AddEntry("CPU manufacturer", SystemInfo.processorManufacturer).AddEntry("CPU model", SystemInfo.processorModel).AddEntry("CPU count", SystemInfo.processorCount).AddEntry("CPU frequency", SystemInfo.processorFrequency).AddEntry("GPU", SystemInfo.graphicsDeviceName).AddEntry("GPU memory", string.Format("{0}MB", SystemInfo.graphicsMemorySize)).AddEntry("RAM", string.Format("{0}MB", SystemInfo.systemMemorySize));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002FE0 File Offset: 0x000011E0
		public static string GetCurrentDateAndTime()
		{
			return DateTime.Now.ToString("s", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003004 File Offset: 0x00001204
		public static string GetNameOfCurrentQualityLevel()
		{
			return QualitySettings.names[QualitySettings.GetQualityLevel()];
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002492 File Offset: 0x00000692
		public static string GetApplicationMode()
		{
			if (!Application.isEditor)
			{
				return "Standalone";
			}
			return "Editor";
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003011 File Offset: 0x00001211
		public static string GetFeatureToggles()
		{
			return string.Join(", ", FeatureToggleService.GetToggleNames().Where(new Func<string, bool>(FeatureToggleService.IsToggleOn)));
		}

		// Token: 0x0400002D RID: 45
		public readonly StatisticsCalculator _statisticsCalculator;

		// Token: 0x0400002E RID: 46
		public readonly CharacterPopulation _characterPopulation;

		// Token: 0x0400002F RID: 47
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000030 RID: 48
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x04000031 RID: 49
		public readonly ITickService _tickService;
	}
}
