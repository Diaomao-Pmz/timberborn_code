using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.GameCycleSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.TimeSystem;
using Timberborn.WeatherSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200000B RID: 11
	public class DroughtWaterStrengthModifier : BaseComponent, IAwakableComponent, IInitializableEntity, IWaterStrengthModifier
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000247E File Offset: 0x0000067E
		public DroughtWaterStrengthModifier(WaterStrengthService waterStrengthService, HazardousWeatherService hazardousWeatherService, WeatherService weatherService, IDayNightCycle dayNightCycle, HazardousWeatherHistory hazardousWeatherHistory, DroughtWeather droughtWeather, GameCycleService gameCycleService)
		{
			this._waterStrengthService = waterStrengthService;
			this._hazardousWeatherService = hazardousWeatherService;
			this._weatherService = weatherService;
			this._dayNightCycle = dayNightCycle;
			this._hazardousWeatherHistory = hazardousWeatherHistory;
			this._droughtWeather = droughtWeather;
			this._gameCycleService = gameCycleService;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024BB File Offset: 0x000006BB
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024C9 File Offset: 0x000006C9
		public void InitializeEntity()
		{
			this.Enable();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024D1 File Offset: 0x000006D1
		public void Enable()
		{
			this._waterSource.AddWaterStrengthModifier(this);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024DF File Offset: 0x000006DF
		public void Disable()
		{
			this._waterSource.RemoveWaterStrengthModifier(this);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024ED File Offset: 0x000006ED
		public float GetStrengthModifier()
		{
			if (this._weatherService.IsHazardousWeather)
			{
				return (float)(this.IsCycleWithDrought ? 0 : 1);
			}
			return this.GetTemperateWeatherModifier();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002510 File Offset: 0x00000710
		public bool IsCycleWithDrought
		{
			get
			{
				return this._hazardousWeatherService.CurrentCycleHazardousWeather is DroughtWeather;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002528 File Offset: 0x00000728
		public bool PreviousCycleHadDrought
		{
			get
			{
				HazardousWeatherHistoryData hazardousWeatherHistoryData;
				return this._hazardousWeatherHistory.TryGetPreviousHazardousWeatherData(out hazardousWeatherHistoryData) && hazardousWeatherHistoryData.Duration > 0 && hazardousWeatherHistoryData.HazardousWeatherId == this._droughtWeather.Id;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002568 File Offset: 0x00000768
		public float GetTemperateWeatherModifier()
		{
			float transitionTime = this.GetTransitionTime();
			float num = (float)this._weatherService.HazardousWeatherStartCycleDay - transitionTime;
			if (this.ShouldStopWaterFlow(num))
			{
				float transitionProgress = this._gameCycleService.PartialCycleDay - num;
				return 1f - this.GetModifier(transitionProgress, transitionTime);
			}
			float num2 = this._gameCycleService.PartialCycleDay - 1f;
			if (this.PreviousCycleHadDrought && num2 < transitionTime)
			{
				return this.GetModifier(num2, transitionTime);
			}
			return 1f;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025DD File Offset: 0x000007DD
		public float GetTransitionTime()
		{
			return this._waterSource.SpecifiedStrength / (this._dayNightCycle.DayLengthInSeconds * this._waterStrengthService.MaxWaterSourceChangePerSecond);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002602 File Offset: 0x00000802
		public bool ShouldStopWaterFlow(float transitionStartCycleDay)
		{
			return this.IsCycleWithDrought && this._weatherService.HazardousWeatherDuration > 0 && this._gameCycleService.PartialCycleDay >= transitionStartCycleDay;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002630 File Offset: 0x00000830
		public float GetModifier(float transitionProgress, float transitionTime)
		{
			float num = (1f - this._waterStrengthService.MinWaterSourceChangeScaler) * (transitionProgress / transitionTime) + this._waterStrengthService.MinWaterSourceChangeScaler;
			return transitionProgress * this._dayNightCycle.DayLengthInSeconds * this._waterStrengthService.MaxWaterSourceChangePerSecond * num / this._waterSource.SpecifiedStrength;
		}

		// Token: 0x04000011 RID: 17
		public readonly WaterStrengthService _waterStrengthService;

		// Token: 0x04000012 RID: 18
		public readonly HazardousWeatherService _hazardousWeatherService;

		// Token: 0x04000013 RID: 19
		public readonly WeatherService _weatherService;

		// Token: 0x04000014 RID: 20
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000015 RID: 21
		public readonly HazardousWeatherHistory _hazardousWeatherHistory;

		// Token: 0x04000016 RID: 22
		public readonly DroughtWeather _droughtWeather;

		// Token: 0x04000017 RID: 23
		public readonly GameCycleService _gameCycleService;

		// Token: 0x04000018 RID: 24
		public WaterSource _waterSource;

		// Token: 0x04000019 RID: 25
		public float _oldSpecifiedStrength;
	}
}
