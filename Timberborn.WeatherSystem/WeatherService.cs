using System;
using Timberborn.GameCycleSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.WeatherSystem
{
	// Token: 0x02000006 RID: 6
	public class WeatherService : ILoadableSingleton
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002300 File Offset: 0x00000500
		public WeatherService(EventBus eventBus, TemperateWeatherDurationService temperateWeatherDurationService, GameCycleService gameCycleService, HazardousWeatherService hazardousWeatherService)
		{
			this._eventBus = eventBus;
			this._temperateWeatherDurationService = temperateWeatherDurationService;
			this._gameCycleService = gameCycleService;
			this._hazardousWeatherService = hazardousWeatherService;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002325 File Offset: 0x00000525
		public int HazardousWeatherDuration
		{
			get
			{
				return this._hazardousWeatherService.HazardousWeatherDuration;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002332 File Offset: 0x00000532
		public int TemperateWeatherDuration
		{
			get
			{
				return this._temperateWeatherDurationService.TemperateWeatherDuration;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000233F File Offset: 0x0000053F
		public int HazardousWeatherStartCycleDay
		{
			get
			{
				return this.TemperateWeatherDuration + 1;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002349 File Offset: 0x00000549
		public bool IsHazardousWeather
		{
			get
			{
				return this._gameCycleService.CycleDay >= this.HazardousWeatherStartCycleDay;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002361 File Offset: 0x00000561
		public int CycleLengthInDays
		{
			get
			{
				return this.TemperateWeatherDuration + this.HazardousWeatherDuration;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002370 File Offset: 0x00000570
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002380 File Offset: 0x00000580
		public bool NextDayIsHazardousWeather()
		{
			int num = this._gameCycleService.CycleDay + 1;
			return num >= this.HazardousWeatherStartCycleDay && num <= this.CycleLengthInDays;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023B2 File Offset: 0x000005B2
		[OnEvent]
		public void OnCycleDayStarted(CycleDayStartedEvent cycleDayStartedEvent)
		{
			if (this._gameCycleService.CycleDay == this.HazardousWeatherStartCycleDay)
			{
				this._hazardousWeatherService.StartHazardousWeather();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023D2 File Offset: 0x000005D2
		[OnEvent]
		public void OnCycleEndedEvent(CycleEndedEvent cycleEndedEvent)
		{
			if (this.HazardousWeatherDuration > 0)
			{
				this._hazardousWeatherService.EndHazardousWeather();
			}
		}

		// Token: 0x04000014 RID: 20
		public readonly EventBus _eventBus;

		// Token: 0x04000015 RID: 21
		public readonly TemperateWeatherDurationService _temperateWeatherDurationService;

		// Token: 0x04000016 RID: 22
		public readonly GameCycleService _gameCycleService;

		// Token: 0x04000017 RID: 23
		public readonly HazardousWeatherService _hazardousWeatherService;
	}
}
