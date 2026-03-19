using System;
using System.Text.RegularExpressions;

namespace Timberborn.ErrorReporting
{
	// Token: 0x02000007 RID: 7
	public static class ErrorReportSanitizer
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002580 File Offset: 0x00000780
		public static string Sanitize(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return "";
			}
			string input2 = ErrorReportSanitizer.WindowsAndMacRegex.Replace(input, "${prefix}***");
			return ErrorReportSanitizer.LinuxRegex.Replace(input2, "${prefix}***");
		}

		// Token: 0x0400000E RID: 14
		public static readonly Regex WindowsAndMacRegex = new Regex("(?<prefix>[/\\\\]Users[/\\\\])[^/\\\\]+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400000F RID: 15
		public static readonly Regex LinuxRegex = new Regex("(?<prefix>/home/)[^/\\\\]+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
