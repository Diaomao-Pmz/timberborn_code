using System;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x02000009 RID: 9
	public class HazardousWeatherHistoryData
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000282B File Offset: 0x00000A2B
		public string HazardousWeatherId { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002833 File Offset: 0x00000A33
		public int Duration { get; }

		// Token: 0x06000026 RID: 38 RVA: 0x0000283B File Offset: 0x00000A3B
		public HazardousWeatherHistoryData(string hazardousWeatherId, int duration)
		{
			this.HazardousWeatherId = hazardousWeatherId;
			this.Duration = duration;
		}
	}
}
