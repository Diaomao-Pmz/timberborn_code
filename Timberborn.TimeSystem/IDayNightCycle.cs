using System;
using System.Runtime.CompilerServices;

namespace Timberborn.TimeSystem
{
	// Token: 0x0200000E RID: 14
	public interface IDayNightCycle
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005D RID: 93
		float DayLengthInSeconds { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005E RID: 94
		int DayNumber { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005F RID: 95
		float DaytimeLengthInHours { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000060 RID: 96
		float NighttimeLengthInHours { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97
		float HoursPassedToday { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000062 RID: 98
		float DayProgress { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000063 RID: 99
		float PartialDayNumber { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000064 RID: 100
		bool IsDaytime { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000065 RID: 101
		bool IsNighttime { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000066 RID: 102
		float FixedDeltaTimeInHours { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000067 RID: 103
		float FluidSecondsPassedToday { get; }

		// Token: 0x06000068 RID: 104
		float DayNumberHoursFromNow(float hours);

		// Token: 0x06000069 RID: 105
		[return: TupleElementNames(new string[]
		{
			"start",
			"end"
		})]
		ValueTuple<float, float> BoundsInHours(TimeOfDay timeOfDay);

		// Token: 0x0600006A RID: 106
		float HoursToNextStartOf(TimeOfDay timeOfDay);

		// Token: 0x0600006B RID: 107
		float SecondsToHours(float seconds);

		// Token: 0x0600006C RID: 108
		int HoursToTicks(float hours);

		// Token: 0x0600006D RID: 109
		float TicksToHours(int ticks);

		// Token: 0x0600006E RID: 110
		float FluidHoursToNextStartOf(TimeOfDay timeOfDay);

		// Token: 0x0600006F RID: 111
		void SetTimeToNextDay();

		// Token: 0x06000070 RID: 112
		void JumpTimeInHours(float hours);
	}
}
