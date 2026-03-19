using System;
using Timberborn.Localization;

namespace Timberborn.UIFormatters
{
	// Token: 0x02000007 RID: 7
	public class TimestampFormatter
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000023B0 File Offset: 0x000005B0
		public TimestampFormatter(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023BF File Offset: 0x000005BF
		public string FormatLongLocalized(int cycle, int day)
		{
			return this._loc.T<int, int>(TimestampFormatter.CycleAndDayLongLocKey, cycle, day);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D3 File Offset: 0x000005D3
		public string FormatShortLocalized(int cycle, int day)
		{
			return this._loc.T<int, int>(TimestampFormatter.CycleAndDayShortLocKey, cycle, day);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E7 File Offset: 0x000005E7
		public string FormatShort(int cycle, int day)
		{
			return string.Format("{0}-{1}", cycle, day);
		}

		// Token: 0x0400000E RID: 14
		public static readonly string CycleAndDayLongLocKey = "Weather.CycleAndDayLong";

		// Token: 0x0400000F RID: 15
		public static readonly string CycleAndDayShortLocKey = "Weather.CycleAndDayShort";

		// Token: 0x04000010 RID: 16
		public readonly ILoc _loc;
	}
}
