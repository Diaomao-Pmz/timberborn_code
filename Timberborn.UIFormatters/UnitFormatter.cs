using System;
using Timberborn.Localization;

namespace Timberborn.UIFormatters
{
	// Token: 0x02000009 RID: 9
	public static class UnitFormatter
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002443 File Offset: 0x00000643
		public static string FormatTicks(int value, ILoc loc)
		{
			return loc.T<int>(UnitFormatter.TickUnitLocKey, value);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002451 File Offset: 0x00000651
		public static string FormatHours(int value, ILoc loc)
		{
			return loc.T<string>(UnitFormatter.HourUnitLocKey, string.Format("{0}", value));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000246E File Offset: 0x0000066E
		public static string FormatHours(string value, ILoc loc)
		{
			return loc.T<string>(UnitFormatter.HourUnitLocKey, value);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000247C File Offset: 0x0000067C
		public static string FormatDays(float value, ILoc loc)
		{
			return loc.T<string>(UnitFormatter.DayUnitLocKey, string.Format("{0:F1}", value));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002499 File Offset: 0x00000699
		public static string FormatDays(string value, ILoc loc)
		{
			return loc.T<string>(UnitFormatter.DayUnitLocKey, value);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024A7 File Offset: 0x000006A7
		public static string FormatFlow(float value, ILoc loc)
		{
			return loc.T<string>(UnitFormatter.FlowUnitLocKey, string.Format("{0:F2}", value));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024C4 File Offset: 0x000006C4
		public static string FormatDistance(float value, ILoc loc)
		{
			return loc.T<string>(UnitFormatter.DistanceUnitLocKey, string.Format("{0:F2}", value));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024E1 File Offset: 0x000006E1
		public static string FormatDistance(int value, ILoc loc)
		{
			return loc.T<string>(UnitFormatter.DistanceUnitLocKey, string.Format("{0}", value));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024FE File Offset: 0x000006FE
		public static string FormatAngle(int value, ILoc loc)
		{
			return loc.T<int>(UnitFormatter.AngleUnitLocKey, value);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000250C File Offset: 0x0000070C
		public static string FormatPower(int value, ILoc loc)
		{
			return loc.T<int>(UnitFormatter.PowerUnitLocKey, value);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000251A File Offset: 0x0000071A
		public static string FormatPowerCapacity(int value, ILoc loc)
		{
			return loc.T<int>(UnitFormatter.PowerCapacityUnitLocKey, value);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002528 File Offset: 0x00000728
		public static string FormatPowerCapacityPerMeter(int value, ILoc loc)
		{
			return loc.T<int>(UnitFormatter.PowerCapacityPerMeterUnitLocKey, value);
		}

		// Token: 0x04000011 RID: 17
		public static readonly string TickUnitLocKey = "Unit.Tick.NumberAndUnit";

		// Token: 0x04000012 RID: 18
		public static readonly string HourUnitLocKey = "Unit.Hour.NumberAndUnit";

		// Token: 0x04000013 RID: 19
		public static readonly string DayUnitLocKey = "Unit.Day.NumberAndUnit";

		// Token: 0x04000014 RID: 20
		public static readonly string FlowUnitLocKey = "Unit.CubicMeterPerSecond.NumberAndUnit";

		// Token: 0x04000015 RID: 21
		public static readonly string DistanceUnitLocKey = "Unit.Meter.NumberAndUnit";

		// Token: 0x04000016 RID: 22
		public static readonly string AngleUnitLocKey = "Unit.Degree.NumberAndUnit";

		// Token: 0x04000017 RID: 23
		public static readonly string PowerUnitLocKey = "Unit.HorsePower.NumberAndUnit";

		// Token: 0x04000018 RID: 24
		public static readonly string PowerCapacityUnitLocKey = "Unit.HorsePowerHour.NumberAndUnit";

		// Token: 0x04000019 RID: 25
		public static readonly string PowerCapacityPerMeterUnitLocKey = "Unit.HorsePowerHourPerMeter.NumberAndUnit";
	}
}
