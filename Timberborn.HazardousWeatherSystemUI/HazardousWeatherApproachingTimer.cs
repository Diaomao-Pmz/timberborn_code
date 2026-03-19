using System;
using Timberborn.BlueprintSystem;
using Timberborn.GameCycleSystem;
using Timberborn.SingletonSystem;
using Timberborn.WeatherSystem;

namespace Timberborn.HazardousWeatherSystemUI
{
	// Token: 0x0200000A RID: 10
	public class HazardousWeatherApproachingTimer : ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002170 File Offset: 0x00000370
		public HazardousWeatherApproachingTimer(EventBus eventBus, WeatherService weatherService, GameCycleService gameCycleService, ISpecService specService)
		{
			this._eventBus = eventBus;
			this._weatherService = weatherService;
			this._gameCycleService = gameCycleService;
			this._specService = specService;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002195 File Offset: 0x00000395
		public void Load()
		{
			this._spec = this._specService.GetSingleSpec<HazardousWeatherUISpec>();
			this._eventBus.Register(this);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021B4 File Offset: 0x000003B4
		public void PostLoad()
		{
			if (this.GetProgress() > 0f && !this.TooCloseToNotify)
			{
				this.NotifyHazardousWeatherApproaching();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021D1 File Offset: 0x000003D1
		public float GetProgress()
		{
			if (this._weatherService.HazardousWeatherDuration <= 0)
			{
				return 0f;
			}
			return 1f - this.DaysToHazardousWeather / (float)this._spec.ApproachingNotificationDays;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002200 File Offset: 0x00000400
		[OnEvent]
		public void OnCycleDayStarted(CycleDayStartedEvent cycleDayStartedEvent)
		{
			if (this._gameCycleService.CycleDay == this._weatherService.TemperateWeatherDuration - this._spec.ApproachingNotificationDays + 1)
			{
				this.NotifyHazardousWeatherApproaching();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000222E File Offset: 0x0000042E
		public bool TooCloseToNotify
		{
			get
			{
				return this.DaysToHazardousWeather < this._spec.MaxDayProgressLeftToNotify;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002243 File Offset: 0x00000443
		public float DaysToHazardousWeather
		{
			get
			{
				return (float)this._weatherService.HazardousWeatherStartCycleDay - this._gameCycleService.PartialCycleDay;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000225D File Offset: 0x0000045D
		public void NotifyHazardousWeatherApproaching()
		{
			if (this._weatherService.HazardousWeatherDuration > 0)
			{
				this._eventBus.Post(new HazardousWeatherApproachingEvent());
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly WeatherService _weatherService;

		// Token: 0x0400000A RID: 10
		public readonly GameCycleService _gameCycleService;

		// Token: 0x0400000B RID: 11
		public readonly ISpecService _specService;

		// Token: 0x0400000C RID: 12
		public HazardousWeatherUISpec _spec;
	}
}
