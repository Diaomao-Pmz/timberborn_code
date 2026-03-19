using System;
using System.IO;
using System.Net.Http;
using Timberborn.CommandLine;
using UnityEngine;

namespace Timberborn.ErrorReporting
{
	// Token: 0x02000008 RID: 8
	public static class ErrorReportSender
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000025E4 File Offset: 0x000007E4
		public static bool SendErrorReport(string comment, string email)
		{
			try
			{
				ErrorReporter.AddCommentToReport(comment);
				ErrorReporter.AddEmailToReport(email);
				ErrorReportSender.UploadErrorReport();
			}
			catch (Exception ex)
			{
				string str = "Error when submitting a crash report:\n";
				Exception ex2 = ex;
				Debug.Log(str + ((ex2 != null) ? ex2.ToString() : null));
				return false;
			}
			return true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002638 File Offset: 0x00000838
		public static void UploadErrorReport()
		{
			using (HttpClient httpClient = new HttpClient())
			{
				using (FileStream fileStream = new FileStream(ErrorReporter.ReportFilePath, FileMode.Open))
				{
					using (StreamContent streamContent = new StreamContent(fileStream))
					{
						httpClient.PostAsync(ErrorReportSender.GetUrl(), streamContent).Result.EnsureSuccessStatusCode();
					}
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026C0 File Offset: 0x000008C0
		public static string GetUrl()
		{
			try
			{
				CommandLineArguments commandLineArguments = CommandLineArguments.CreateWithCommandLineArgs();
				if (commandLineArguments.Has(ErrorReportSender.CustomUrlKey))
				{
					return commandLineArguments.GetString(ErrorReportSender.CustomUrlKey);
				}
			}
			catch
			{
			}
			return ErrorReportSender.Url;
		}

		// Token: 0x04000010 RID: 16
		public static readonly string Url = "https://api.timberborn.com/v1/upload-error-report";

		// Token: 0x04000011 RID: 17
		public static readonly string CustomUrlKey = "errorUrl";
	}
}
