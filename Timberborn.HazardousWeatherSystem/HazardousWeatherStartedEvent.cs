using System;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x0200000E RID: 14
	public class HazardousWeatherStartedEvent
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002C22 File Offset: 0x00000E22
		public IHazardousWeather HazardousWeather { get; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002C2A File Offset: 0x00000E2A
		public HazardousWeatherStartedEvent(IHazardousWeather hazardousWeather)
		{
			this.HazardousWeather = hazardousWeather;
		}
	}
}
