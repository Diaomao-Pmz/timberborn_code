using System;
using Timberborn.BlueprintSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.WeatherSystem;
using UnityEngine;

namespace Timberborn.SkySystem
{
	// Token: 0x02000009 RID: 9
	public class DayStageCycle : ILoadableSingleton
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002683 File Offset: 0x00000883
		public DayStageCycle(IDayNightCycle dayNightCycle, WeatherService weatherService, HazardousWeatherService hazardousWeatherService, ISpecService specService)
		{
			this._dayNightCycle = dayNightCycle;
			this._weatherService = weatherService;
			this._hazardousWeatherService = hazardousWeatherService;
			this._specService = specService;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026A8 File Offset: 0x000008A8
		public void Load()
		{
			DayStageCycleSpec singleSpec = this._specService.GetSingleSpec<DayStageCycleSpec>();
			this._sunriseSunsetLengthInHours = singleSpec.SunriseSunsetLengthInHours;
			this._transitionLengthInHours = singleSpec.TransitionLengthInHours;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026D9 File Offset: 0x000008D9
		public DayStageTransition GetCurrentTransition()
		{
			if (!this._dayNightCycle.IsDaytime)
			{
				return this.Transition(TimeOfDay.Nighttime, TimeOfDay.Daytime, DayStage.Sunset, DayStage.Night, DayStage.Sunrise);
			}
			return this.Transition(TimeOfDay.Daytime, TimeOfDay.Nighttime, DayStage.Sunrise, DayStage.Day, DayStage.Sunset);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002700 File Offset: 0x00000900
		public DayStageTransition Transition(TimeOfDay currentTimeOfDay, TimeOfDay nextTimeOfDay, DayStage dayStage1, DayStage dayStage2, DayStage dayStage3)
		{
			float num = 24f - this._dayNightCycle.FluidHoursToNextStartOf(currentTimeOfDay);
			float hoursToNextDayStage = this._dayNightCycle.FluidHoursToNextStartOf(nextTimeOfDay);
			if (num >= this._sunriseSunsetLengthInHours)
			{
				return this.Transition(dayStage2, dayStage3, hoursToNextDayStage);
			}
			return this.Transition(dayStage1, dayStage2, this._sunriseSunsetLengthInHours - num);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002754 File Offset: 0x00000954
		public DayStageTransition Transition(DayStage currentDayStage, DayStage nextDayStage, float hoursToNextDayStage)
		{
			float num = 1f - Mathf.Clamp01(hoursToNextDayStage / this._transitionLengthInHours);
			float transitionProgress = Mathf.SmoothStep(0f, 1f, num);
			IHazardousWeather currentCycleHazardousWeather = this._hazardousWeatherService.CurrentCycleHazardousWeather;
			string text = (currentCycleHazardousWeather != null) ? currentCycleHazardousWeather.Id : null;
			string currentDayStageHazardousWeatherId = this._weatherService.IsHazardousWeather ? text : null;
			string nextDayStageHazardousWeatherId = ((nextDayStage == DayStage.Sunrise) ? this._weatherService.NextDayIsHazardousWeather() : this._weatherService.IsHazardousWeather) ? text : null;
			return new DayStageTransition(currentDayStage, currentDayStageHazardousWeatherId, nextDayStage, nextDayStageHazardousWeatherId, transitionProgress);
		}

		// Token: 0x04000017 RID: 23
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000018 RID: 24
		public readonly WeatherService _weatherService;

		// Token: 0x04000019 RID: 25
		public readonly HazardousWeatherService _hazardousWeatherService;

		// Token: 0x0400001A RID: 26
		public readonly ISpecService _specService;

		// Token: 0x0400001B RID: 27
		public float _sunriseSunsetLengthInHours;

		// Token: 0x0400001C RID: 28
		public float _transitionLengthInHours;
	}
}
