using System;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x0200000C RID: 12
	public class HazardousWeatherSelectedEvent
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002A08 File Offset: 0x00000C08
		public IHazardousWeather SelectedWeather { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A10 File Offset: 0x00000C10
		public int Duration { get; }

		// Token: 0x06000035 RID: 53 RVA: 0x00002A18 File Offset: 0x00000C18
		public HazardousWeatherSelectedEvent(IHazardousWeather selectedWeather, int duration)
		{
			this.SelectedWeather = selectedWeather;
			this.Duration = duration;
		}
	}
}
