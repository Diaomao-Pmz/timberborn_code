using System;
using System.Globalization;

namespace Timberborn.UIFormatters
{
	// Token: 0x02000005 RID: 5
	public static class NumberFormatter
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002194 File Offset: 0x00000394
		public static string Format(int number)
		{
			string result;
			if (number < 1000000)
			{
				if (number >= 1000)
				{
					if (number >= 10000)
					{
						result = NumberFormatter.ToString(Math.Floor((double)((float)number / 1000f)), NumberFormatter.Thousand, 0);
					}
					else
					{
						result = NumberFormatter.ToString(Math.Floor((double)((float)number / 100f)) / 10.0, NumberFormatter.Thousand, 1);
					}
				}
				else
				{
					result = string.Format("{0:F0}", number);
				}
			}
			else if (number >= 10000000)
			{
				result = NumberFormatter.ToString(Math.Floor((double)((float)number / 1000000f)), NumberFormatter.Million, 0);
			}
			else
			{
				result = NumberFormatter.ToString(Math.Floor((double)((float)number / 100000f)) / 10.0, NumberFormatter.Million, 1);
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000225E File Offset: 0x0000045E
		public static string FormatFullNumber(int number)
		{
			return number.ToString("N0", NumberFormatter.NumberFormatInfo);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002274 File Offset: 0x00000474
		public static string CeilToTenthsPlace(double value)
		{
			double num = Math.Ceiling(10.0 * value) / 10.0;
			return string.Format("{0:0.0}", num);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022AC File Offset: 0x000004AC
		public static string FormatAsPercentCeiled(double value)
		{
			return NumberFormatter.FormatAsPercentInternal(Math.Ceiling(value * 100.0));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022C3 File Offset: 0x000004C3
		public static string FormatAsPercentFloored(double value)
		{
			return NumberFormatter.FormatAsPercentInternal(Math.Floor(value * 100.0));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022DA File Offset: 0x000004DA
		public static string FormatAsPercentRounded(double value)
		{
			return NumberFormatter.FormatAsPercentInternal(Math.Round(value * 100.0));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022F1 File Offset: 0x000004F1
		public static string FormatAsPercentInternal(double percentValue)
		{
			return string.Format("{0:F0}%", percentValue);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002303 File Offset: 0x00000503
		public static string ToString(double number, string appendix, int decimalPlaces)
		{
			return number.ToString(string.Format("F{0}", decimalPlaces), CultureInfo.InvariantCulture) + appendix;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string Thousand = "k";

		// Token: 0x04000009 RID: 9
		public static readonly string Million = "M";

		// Token: 0x0400000A RID: 10
		public static readonly NumberFormatInfo NumberFormatInfo = new NumberFormatInfo
		{
			NumberGroupSeparator = " "
		};
	}
}
