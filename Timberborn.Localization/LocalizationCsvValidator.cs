using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LINQtoCSV;
using UnityEngine;

namespace Timberborn.Localization
{
	// Token: 0x0200000B RID: 11
	public class LocalizationCsvValidator : ILocalizationCsvValidator
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002328 File Offset: 0x00000528
		public void Validate(TextAsset textAsset)
		{
			this.ValidateCsvIntegrity(textAsset);
			this.ValidateCommaAndSemicolonSpaces(textAsset);
			this.InformAboutErrors();
			this._errors.Clear();
			this._criticalErrors.Clear();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002358 File Offset: 0x00000558
		public void ValidateCsvIntegrity(TextAsset textAsset)
		{
			using (MemoryStream memoryStream = new MemoryStream(textAsset.bytes))
			{
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					int num = 1;
					IEnumerable<LocalizationCsvValidator.ValidationRow> enumerable = new CsvContext().Read<LocalizationCsvValidator.ValidationRow>(streamReader);
					int num2 = enumerable.Count<LocalizationCsvValidator.ValidationRow>();
					int num3 = 0;
					foreach (LocalizationCsvValidator.ValidationRow validationRow in enumerable)
					{
						int count = validationRow.Count;
						if (count > 0)
						{
							int lineNbr = validationRow.First<DataRowItem>().LineNbr;
							if (lineNbr - num > 1)
							{
								this.AddCriticalError("Empty line found", textAsset.name, lineNbr - 1);
							}
							num = lineNbr + LocalizationCsvValidator.GetNumberOfNewLinesInContent(validationRow);
							if (count > 3)
							{
								this.AddCriticalError("Unnecessary comma found", textAsset.name, num);
							}
							if (count < 3 && num3 != num2 - 1)
							{
								this.AddError("Invalid number of columns found", textAsset.name, lineNbr);
							}
							foreach (DataRowItem dataRowItem in validationRow)
							{
								if (!string.IsNullOrEmpty(dataRowItem.Value) && dataRowItem.Value.EndsWith(" "))
								{
									this.AddError("Unnecessary space at the end of column found", textAsset.name, lineNbr);
									break;
								}
							}
						}
						num3++;
					}
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000251C File Offset: 0x0000071C
		public void ValidateCommaAndSemicolonSpaces(TextAsset textAsset)
		{
			using (StringReader stringReader = new StringReader(textAsset.text))
			{
				int num = 0;
				string text = stringReader.ReadLine();
				while (text != null)
				{
					string text2 = text.Replace("\"\"", string.Empty);
					if (text2.Contains(", \"") || text2.Contains("\" ,"))
					{
						this.AddCriticalError("Space between comma and semicolon found", textAsset.name, num);
					}
					text = stringReader.ReadLine();
					num++;
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025A8 File Offset: 0x000007A8
		public void InformAboutErrors()
		{
			string arg = "Localization file contains errors:";
			if (this._errors.Length > 0)
			{
				Debug.LogError(string.Format("{0}{1}{2}", arg, Environment.NewLine, this._errors));
			}
			if (this._criticalErrors.Length > 0)
			{
				throw new InvalidDataException(string.Format("{0}{1}{2}", arg, Environment.NewLine, this._criticalErrors));
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000260E File Offset: 0x0000080E
		public void AddError(string exception, string sourceName, int lineNumber)
		{
			this._errors.AppendLine(string.Format("{0} in {1} (line {2})", exception, sourceName, lineNumber));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000262E File Offset: 0x0000082E
		public void AddCriticalError(string exception, string sourceName, int lineNumber)
		{
			this._criticalErrors.AppendLine(string.Format("{0} in {1} (line {2})", exception, sourceName, lineNumber));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002650 File Offset: 0x00000850
		public static int GetNumberOfNewLinesInContent(LocalizationCsvValidator.ValidationRow rowItems)
		{
			int num = 0;
			for (int i = 1; i < rowItems.Count; i++)
			{
				int num2 = num;
				string value = rowItems[i].Value;
				int num3;
				if (value == null)
				{
					num3 = 0;
				}
				else
				{
					num3 = value.Count((char c) => c.Equals('\n'));
				}
				num = num2 + num3;
			}
			return num;
		}

		// Token: 0x0400001C RID: 28
		public readonly StringBuilder _errors = new StringBuilder();

		// Token: 0x0400001D RID: 29
		public readonly StringBuilder _criticalErrors = new StringBuilder();

		// Token: 0x0200000C RID: 12
		public class ValidationRow : List<DataRowItem>, IDataRow
		{
		}
	}
}
