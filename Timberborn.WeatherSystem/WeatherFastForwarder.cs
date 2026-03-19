using System;
using Timberborn.GameCycleSystem;
using Timberborn.TimeSystem;

namespace Timberborn.WeatherSystem
{
	// Token: 0x02000005 RID: 5
	public class WeatherFastForwarder
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002254 File Offset: 0x00000454
		public WeatherFastForwarder(WeatherService weatherService, IDayNightCycle dayNightCycle, GameCycleService gameCycleService)
		{
			this._weatherService = weatherService;
			this._dayNightCycle = dayNightCycle;
			this._gameCycleService = gameCycleService;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002274 File Offset: 0x00000474
		public void JumpToNextSeason()
		{
			if (this._weatherService.IsHazardousWeather)
			{
				int daysToSkip = this._weatherService.HazardousWeatherStartCycleDay + this._weatherService.HazardousWeatherDuration - this._gameCycleService.CycleDay;
				this.SkipDays(daysToSkip);
				return;
			}
			int daysToSkip2 = this._weatherService.HazardousWeatherStartCycleDay - this._gameCycleService.CycleDay;
			this.SkipDays(daysToSkip2);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022DC File Offset: 0x000004DC
		public void SkipDays(int daysToSkip)
		{
			for (int i = 0; i < daysToSkip; i++)
			{
				this._dayNightCycle.SetTimeToNextDay();
			}
		}

		// Token: 0x04000011 RID: 17
		public readonly WeatherService _weatherService;

		// Token: 0x04000012 RID: 18
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000013 RID: 19
		public readonly GameCycleService _gameCycleService;
	}
}
