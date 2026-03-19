using System;
using Timberborn.TimeSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200004D RID: 77
	public class TimerIntervalFactory
	{
		// Token: 0x0600034A RID: 842 RVA: 0x00009341 File Offset: 0x00007541
		public TimerIntervalFactory(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009350 File Offset: 0x00007550
		public TimerInterval CreateFromTicks(int ticks)
		{
			TimerInterval timerInterval = new TimerInterval(this._dayNightCycle);
			timerInterval.SetTicks(ticks);
			return timerInterval;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00009364 File Offset: 0x00007564
		public TimerInterval CreateFromHours(float hours, IntervalType intervalType)
		{
			TimerInterval timerInterval = new TimerInterval(this._dayNightCycle);
			if (intervalType != IntervalType.Hours)
			{
				if (intervalType != IntervalType.Days)
				{
					throw new ArgumentOutOfRangeException("intervalType", intervalType, null);
				}
				timerInterval.SetDays(hours / 24f);
			}
			else
			{
				timerInterval.SetHours(hours);
			}
			return timerInterval;
		}

		// Token: 0x04000196 RID: 406
		public readonly IDayNightCycle _dayNightCycle;
	}
}
