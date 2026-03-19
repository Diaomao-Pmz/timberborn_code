using System;
using Timberborn.BaseComponentSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.WeatherSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x0200000C RID: 12
	public class HazardousWeatherObserver : BaseComponent
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002686 File Offset: 0x00000886
		public HazardousWeatherObserver(HazardousWeatherService hazardousWeatherService, WeatherService weatherService)
		{
			this._hazardousWeatherService = hazardousWeatherService;
			this._weatherService = weatherService;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000269C File Offset: 0x0000089C
		public bool IsBadtideWeather
		{
			get
			{
				return this._weatherService.IsHazardousWeather && this._hazardousWeatherService.CurrentCycleHazardousWeather is BadtideWeather;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000026C0 File Offset: 0x000008C0
		public bool IsDroughtWeather
		{
			get
			{
				return this._weatherService.IsHazardousWeather && this._hazardousWeatherService.CurrentCycleHazardousWeather is DroughtWeather;
			}
		}

		// Token: 0x0400001A RID: 26
		public readonly HazardousWeatherService _hazardousWeatherService;

		// Token: 0x0400001B RID: 27
		public readonly WeatherService _weatherService;
	}
}
