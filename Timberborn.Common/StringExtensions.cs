using System;
using System.Text;

namespace Timberborn.Common
{
	// Token: 0x0200002E RID: 46
	public static class StringExtensions
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003768 File Offset: 0x00001968
		public static string ToPascalCase(this string inputString)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				return inputString;
			}
			bool flag = true;
			foreach (char c in inputString)
			{
				if (char.IsWhiteSpace(c))
				{
					flag = true;
				}
				else
				{
					if (flag)
					{
						c = char.ToUpper(c);
						flag = false;
					}
					else
					{
						c = char.ToLower(c);
					}
					StringExtensions.StringBuilder.Append(c);
				}
			}
			return StringExtensions.StringBuilder.ToStringAndClear();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000037D4 File Offset: 0x000019D4
		public static string SplitPascalCase(this string inputString)
		{
			for (int i = 0; i < inputString.Length; i++)
			{
				if (StringExtensions.ShouldBeSplit(i, inputString))
				{
					StringExtensions.StringBuilder.Append(' ');
				}
				StringExtensions.StringBuilder.Append(inputString[i]);
			}
			return StringExtensions.StringBuilder.ToStringAndClear();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003824 File Offset: 0x00001A24
		public static bool ShouldBeSplit(int index, string inputString)
		{
			return StringExtensions.IsMiddleChar(index, inputString) && char.IsUpper(inputString[index]) && (char.IsLower(inputString[index + 1]) || char.IsLower(inputString[index - 1]));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000385F File Offset: 0x00001A5F
		public static bool IsMiddleChar(int index, string inputString)
		{
			return index > 0 && index < inputString.Length - 1;
		}

		// Token: 0x04000045 RID: 69
		public static readonly StringBuilder StringBuilder = new StringBuilder();
	}
}
