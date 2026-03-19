using System;
using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;
using System.Text;
using Timberborn.PlatformUtilities;
using Timberborn.Versioning;
using UnityEngine;

namespace Timberborn.ErrorReporting
{
	// Token: 0x02000005 RID: 5
	public class ErrorReporter : MonoBehaviour
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002108 File Offset: 0x00000308
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000210F File Offset: 0x0000030F
		public static string ReportFilePath { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002117 File Offset: 0x00000317
		public static bool ErrorReported
		{
			get
			{
				return !string.IsNullOrWhiteSpace(ErrorReporter.LogString) || !string.IsNullOrWhiteSpace(ErrorReporter.StackTrace);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		public static void CreateErrorReport()
		{
			ErrorReporter.ReportFilePath = null;
			ErrorReporter.reportTimestamp = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd-HH\\hmm\\mss\\s");
			string str = Application.isEditor ? "-editor" : "";
			string path = "error-report-" + ErrorReporter.reportTimestamp + str + ".zip";
			string text = Path.Combine(ErrorReporter.ErrorReportsFolder, path);
			Debug.Log("Creating an error report: " + text);
			try
			{
				Directory.CreateDirectory(ErrorReporter.ErrorReportsFolder);
				using (FileStream fileStream = new FileStream(text, FileMode.CreateNew))
				{
					using (ZipArchive zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Update))
					{
						ErrorReporter.AddVersionEntry(zipArchive);
						ErrorReporter.AddExceptionEntry(zipArchive);
						ErrorReporter.AddStartingItemEntry(zipArchive);
						ErrorReporter.AddSaveEntry(zipArchive);
						ErrorReporter.AddPlayerLogEntry(zipArchive);
					}
				}
			}
			catch (Exception arg)
			{
				Debug.LogWarning(string.Format("Failed to create an error report: {0}", arg));
			}
			ErrorReporter.ReportFilePath = text;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002250 File Offset: 0x00000450
		public static void AddCommentToReport(string comment)
		{
			ErrorReporter.AddPlainTextEntryToExistingReport("3 Comment", comment);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000225D File Offset: 0x0000045D
		public static void AddEmailToReport(string email)
		{
			ErrorReporter.AddPlainTextEntryToExistingReport("4 Email", email);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000226C File Offset: 0x0000046C
		public static void AddVersionEntry(ZipArchive archive)
		{
			ErrorReporter.AddPlainTextEntry(archive, "0 Version", GameVersions.CurrentVersion.Formatted);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002294 File Offset: 0x00000494
		public static void AddExceptionEntry(ZipArchive archive)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(ErrorReporter.LogString))
			{
				stringBuilder.AppendLine(ErrorReporter.LogString);
			}
			if (!string.IsNullOrWhiteSpace(ErrorReporter.StackTrace))
			{
				stringBuilder.AppendLine(ErrorReporter.StackTrace);
			}
			ErrorReporter.AddPlainTextEntry(archive, "1 Exception", stringBuilder.ToString());
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E8 File Offset: 0x000004E8
		public static void AddPlayerLogEntry(ZipArchive archive)
		{
			string text = Path.Combine(Application.temporaryCachePath, "TimberbornTemporaryLog");
			File.Copy(Application.consoleLogPath, text, true);
			string text2 = File.ReadAllText(text);
			ErrorReporter.AddPlainTextEntry(archive, "2 Player log", text2);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002324 File Offset: 0x00000524
		public static void AddStartingItemEntry(ZipArchive archive)
		{
			ImmutableArray<byte> data = WorldDataService.Data;
			if (new ImmutableArray<byte>?(data) != null && data.Length > 0)
			{
				string extension = Path.GetExtension(WorldDataService.SourceFileName);
				using (Stream stream = archive.CreateEntry("5 Starting item " + ErrorReporter.reportTimestamp + extension).Open())
				{
					stream.Write(data.AsSpan());
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023A8 File Offset: 0x000005A8
		public static void AddSaveEntry(ZipArchive archive)
		{
			byte[] exceptionSave = ErrorReporter.ExceptionSave;
			if (exceptionSave != null && exceptionSave.Length != 0)
			{
				using (Stream stream = archive.CreateEntry("6 Error save " + ErrorReporter.reportTimestamp + ".timber").Open())
				{
					stream.Write(ErrorReporter.ExceptionSave, 0, ErrorReporter.ExceptionSave.Length);
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002418 File Offset: 0x00000618
		public static void AddPlainTextEntryToExistingReport(string name, string text)
		{
			if (!string.IsNullOrWhiteSpace(text))
			{
				Debug.Log("Adding " + name + " to error report: " + ErrorReporter.ReportFilePath);
				try
				{
					using (FileStream fileStream = new FileStream(ErrorReporter.ReportFilePath, FileMode.Open))
					{
						using (ZipArchive zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Update))
						{
							ErrorReporter.AddPlainTextEntry(zipArchive, name, text);
						}
					}
				}
				catch (Exception arg)
				{
					Debug.LogWarning(string.Format("Failed to add {0} to error report: {1}", name, arg));
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024BC File Offset: 0x000006BC
		public static void AddPlainTextEntry(ZipArchive archive, string name, string text)
		{
			if (!string.IsNullOrWhiteSpace(text))
			{
				using (Stream stream = archive.CreateEntry(name + " " + ErrorReporter.reportTimestamp + ".txt").Open())
				{
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						streamWriter.WriteLine(ErrorReportSanitizer.Sanitize(text));
					}
				}
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly string ErrorReportsFolder = Path.Combine(UserDataFolder.Folder, "Error reports");

		// Token: 0x04000009 RID: 9
		public static string LogString;

		// Token: 0x0400000A RID: 10
		public static string StackTrace;

		// Token: 0x0400000B RID: 11
		public static byte[] ExceptionSave;

		// Token: 0x0400000D RID: 13
		[HideInInspector]
		public static string reportTimestamp;
	}
}
