using System;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x02000006 RID: 6
	public class HazardousWeatherEndedEvent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000025A0 File Offset: 0x000007A0
		public IHazardousWeather HazardousWeather { get; }

		// Token: 0x06000016 RID: 22 RVA: 0x000025A8 File Offset: 0x000007A8
		public HazardousWeatherEndedEvent(IHazardousWeather hazardousWeather)
		{
			this.HazardousWeather = hazardousWeather;
		}
	}
}
