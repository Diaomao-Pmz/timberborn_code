using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.Common;
using Timberborn.FileSystem;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.Metrics;
using Timberborn.PlatformUtilities;
using UnityEngine;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000006 RID: 6
	public class BenchmarkLogger
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000023F9 File Offset: 0x000005F9
		public BenchmarkLogger(BenchmarkReportCreator benchmarkReportCreator, GameLoader gameLoader, GameSaveRepository gameSaveRepository, PerformanceSampleFormatter performanceSampleFormatter, IMetricsService metricsService, IFileService fileService)
		{
			this._benchmarkReportCreator = benchmarkReportCreator;
			this._gameLoader = gameLoader;
			this._gameSaveRepository = gameSaveRepository;
			this._performanceSampleFormatter = performanceSampleFormatter;
			this._metricsService = metricsService;
			this._fileService = fileService;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002430 File Offset: 0x00000630
		public void LogBenchmark(BenchmarkParameters benchmarkParameters)
		{
			Asserts.FieldIsNotNull<GameLoader>(this._gameLoader, this._gameLoader.LoadedSave, "LoadedSave");
			SaveReference loadedSave = this._gameLoader.LoadedSave;
			BenchmarkReport benchmarkReport = this._benchmarkReportCreator.CreateReport(benchmarkParameters, loadedSave);
			BenchmarkLogger.LogToConsole(benchmarkReport.HumanReadableContent);
			this.LogToDisk(benchmarkParameters, loadedSave, benchmarkReport.HumanReadableContent);
			this.LogToArchive(benchmarkReport);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002492 File Offset: 0x00000692
		public static string ApplicationMode
		{
			get
			{
				if (!Application.isEditor)
				{
					return "Standalone";
				}
				return "Editor";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024A6 File Offset: 0x000006A6
		public static string RootDirectory
		{
			get
			{
				return Path.Combine(UserDataFolder.Folder, "Benchmarks");
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024B7 File Offset: 0x000006B7
		public static void LogToConsole(string report)
		{
			Debug.Log(report);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024C0 File Offset: 0x000006C0
		public void LogToDisk(BenchmarkParameters benchmarkParameters, SaveReference saveReference, string report)
		{
			string logName = BenchmarkLogger.LogName(benchmarkParameters, saveReference);
			this.LogToFile(logName, "log", report);
			this.LogSamplesToFile(benchmarkParameters.PerformanceSamples, logName);
			this.LogMetricsToFile(logName);
			this.CopySaveNextToLogFile(saveReference, logName);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002504 File Offset: 0x00000704
		public static string LogName(BenchmarkParameters benchmarkParameters, SaveReference saveReference)
		{
			return string.Concat(new string[]
			{
				string.Format("{0}", saveReference),
				string.Format(" {0}s", benchmarkParameters.SamplingLengthInSeconds),
				string.Format(" {0}s", benchmarkParameters.WarmUpLengthInSeconds),
				string.Format(" x{0}", benchmarkParameters.GameSpeed),
				" ",
				BenchmarkLogger.GetCurrentDateAndTime()
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002584 File Offset: 0x00000784
		public static string GetCurrentDateAndTime()
		{
			return DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH\\hmm\\mss\\s");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025AC File Offset: 0x000007AC
		public void LogSamplesToFile(IEnumerable<PerformanceSample> samples, string logName)
		{
			string text = this._performanceSampleFormatter.FormatAsCsv(samples);
			this.LogToFile(logName, "csv", text);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025D4 File Offset: 0x000007D4
		public void LogToFile(string logName, string extension, string text)
		{
			string path = BenchmarkLogger.ModeDirectoryFileNameToPath(logName + "." + extension);
			this._fileService.WriteTextToFile(path, text);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002600 File Offset: 0x00000800
		public void LogMetricsToFile(string logName)
		{
			this._metricsService.WriteCollectedDataToFile(BenchmarkLogger.ModeDirectoryFileNameToPath(logName + " metrics"));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002620 File Offset: 0x00000820
		public void CopySaveNextToLogFile(SaveReference saveReference, string logName)
		{
			if (this._gameSaveRepository.SaveExists(saveReference))
			{
				string sourceFileName = this._gameSaveRepository.SaveNameToFileName(saveReference);
				string destFileName = BenchmarkLogger.ModeDirectoryFileNameToPath(logName + ".json");
				File.Copy(sourceFileName, destFileName);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002660 File Offset: 0x00000860
		public void LogToArchive(BenchmarkReport report)
		{
			string text = BenchmarkLogger.RootDirectoryFileNameToPath("benchmarks.csv");
			BenchmarkLogger.BackupArchiveIfOutdated(report, text);
			if (!File.Exists(text))
			{
				File.WriteAllText(text, report.CsvHeader + "\n");
			}
			File.AppendAllText(text, report.CsvRow + "\n");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026B4 File Offset: 0x000008B4
		public static void BackupArchiveIfOutdated(BenchmarkReport report, string archivePath)
		{
			if (File.Exists(archivePath))
			{
				string a;
				using (StreamReader streamReader = new StreamReader(archivePath))
				{
					a = (streamReader.ReadLine() ?? "");
				}
				if (a != report.CsvHeader)
				{
					string destFileName = BenchmarkLogger.RootDirectoryFileNameToPath("benchmarks pre-" + BenchmarkLogger.GetCurrentDateAndTime() + ".csv");
					File.Move(archivePath, destFileName);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000272C File Offset: 0x0000092C
		public static string ModeDirectoryFileNameToPath(string fileName)
		{
			return Path.Combine(BenchmarkLogger.RootDirectory, BenchmarkLogger.ApplicationMode, fileName);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000273E File Offset: 0x0000093E
		public static string RootDirectoryFileNameToPath(string fileName)
		{
			return Path.Combine(BenchmarkLogger.RootDirectory, BenchmarkLogger.ApplicationMode + " " + fileName);
		}

		// Token: 0x04000014 RID: 20
		public readonly BenchmarkReportCreator _benchmarkReportCreator;

		// Token: 0x04000015 RID: 21
		public readonly GameLoader _gameLoader;

		// Token: 0x04000016 RID: 22
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000017 RID: 23
		public readonly PerformanceSampleFormatter _performanceSampleFormatter;

		// Token: 0x04000018 RID: 24
		public readonly IMetricsService _metricsService;

		// Token: 0x04000019 RID: 25
		public readonly IFileService _fileService;
	}
}
