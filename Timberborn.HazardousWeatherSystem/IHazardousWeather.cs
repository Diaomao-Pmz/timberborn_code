using System;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x02000010 RID: 16
	public interface IHazardousWeather
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000046 RID: 70
		string Id { get; }

		// Token: 0x06000047 RID: 71
		int GetDurationAtCycle(int cycle);
	}
}
