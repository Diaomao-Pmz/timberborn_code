using System;
using Timberborn.DebuggingUI;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;
using Timberborn.WeatherSystem;

namespace Timberborn.WeatherSystemUI
{
	// Token: 0x02000008 RID: 8
	public class WeatherDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022CD File Offset: 0x000004CD
		public WeatherDebuggingPanel(DebuggingPanel debuggingPanel, TemperateWeatherDurationService temperateWeatherDurationService, HazardousWeatherService hazardousWeatherService)
		{
			this._debuggingPanel = debuggingPanel;
			this._temperateWeatherDurationService = temperateWeatherDurationService;
			this._hazardousWeatherService = hazardousWeatherService;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022EA File Offset: 0x000004EA
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Weather");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002300 File Offset: 0x00000500
		public string GetText()
		{
			int temperateWeatherDuration = this._temperateWeatherDurationService.TemperateWeatherDuration;
			return string.Format("Temperate weather duration: {0}", temperateWeatherDuration) + "\nHazardous weather: " + this._hazardousWeatherService.CurrentCycleHazardousWeather.GetType().Name + string.Format("\nHazardous weather duration: {0}", this._hazardousWeatherService.HazardousWeatherDuration);
		}

		// Token: 0x04000016 RID: 22
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000017 RID: 23
		public readonly TemperateWeatherDurationService _temperateWeatherDurationService;

		// Token: 0x04000018 RID: 24
		public readonly HazardousWeatherService _hazardousWeatherService;
	}
}
