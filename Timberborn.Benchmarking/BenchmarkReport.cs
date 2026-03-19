using System;
using System.Text;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000009 RID: 9
	public class BenchmarkReport
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000028A5 File Offset: 0x00000AA5
		public string HumanReadableContent { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000028AD File Offset: 0x00000AAD
		public string CsvHeader { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000028B5 File Offset: 0x00000AB5
		public string CsvRow { get; }

		// Token: 0x06000031 RID: 49 RVA: 0x000028BD File Offset: 0x00000ABD
		public BenchmarkReport(string humanReadableContent, string csvHeader, string csvRow)
		{
			this.HumanReadableContent = humanReadableContent;
			this.CsvHeader = csvHeader;
			this.CsvRow = csvRow;
		}

		// Token: 0x0200000A RID: 10
		public class Builder
		{
			// Token: 0x06000032 RID: 50 RVA: 0x000028DA File Offset: 0x00000ADA
			public BenchmarkReport.Builder AddTitle(string title)
			{
				this._humanReadableContent.AppendLine(title);
				return this;
			}

			// Token: 0x06000033 RID: 51 RVA: 0x000028EA File Offset: 0x00000AEA
			public BenchmarkReport.Builder AddSection(string section)
			{
				this._section = section;
				this._humanReadableContent.AppendLine(section);
				return this;
			}

			// Token: 0x06000034 RID: 52 RVA: 0x00002901 File Offset: 0x00000B01
			public BenchmarkReport.Builder AddEntry(string name, object value)
			{
				return this.AddEntry(name, value, value.ToString());
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00002911 File Offset: 0x00000B11
			public BenchmarkReport.Builder AddEntry(string name, object value, string humanReadableValue)
			{
				this.AppendToHumanReadableContent(name, humanReadableValue);
				this.AppendToCsvHeader(name);
				this.AppendToCsvRow(value);
				return this;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x0000292A File Offset: 0x00000B2A
			public BenchmarkReport Build()
			{
				return new BenchmarkReport(this._humanReadableContent.ToString(), this._csvHeader.ToString(), this._csvEntry.ToString());
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002952 File Offset: 0x00000B52
			public void AppendToHumanReadableContent(string name, string humanReadableValue)
			{
				if (!string.IsNullOrWhiteSpace(this._section))
				{
					this._humanReadableContent.Append(BenchmarkReport.Builder.Indentation);
				}
				this._humanReadableContent.AppendLine(name + ": " + humanReadableValue);
			}

			// Token: 0x06000038 RID: 56 RVA: 0x0000298A File Offset: 0x00000B8A
			public void AppendToCsvHeader(string name)
			{
				this.AppendToCsvLine(this._csvHeader, string.IsNullOrWhiteSpace(this._section) ? name : (this._section + ": " + name));
			}

			// Token: 0x06000039 RID: 57 RVA: 0x000029B9 File Offset: 0x00000BB9
			public void AppendToCsvRow(object value)
			{
				this.AppendToCsvLine(this._csvEntry, value);
			}

			// Token: 0x0600003A RID: 58 RVA: 0x000029C8 File Offset: 0x00000BC8
			public void AppendToCsvLine(StringBuilder csvLine, object value)
			{
				if (csvLine.Length > 0)
				{
					csvLine.Append(",");
				}
				csvLine.Append(string.Format("\"{0}\"", value));
			}

			// Token: 0x04000028 RID: 40
			public static readonly string Indentation = "  ";

			// Token: 0x04000029 RID: 41
			public readonly StringBuilder _humanReadableContent = new StringBuilder();

			// Token: 0x0400002A RID: 42
			public readonly StringBuilder _csvHeader = new StringBuilder();

			// Token: 0x0400002B RID: 43
			public readonly StringBuilder _csvEntry = new StringBuilder();

			// Token: 0x0400002C RID: 44
			public string _section = "";
		}
	}
}
