using System;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000022 RID: 34
	public class DistrictSelectedEvent
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000459A File Offset: 0x0000279A
		public DistrictCenter DistrictCenter { get; }

		// Token: 0x0600010B RID: 267 RVA: 0x000045A2 File Offset: 0x000027A2
		public DistrictSelectedEvent(DistrictCenter districtCenter)
		{
			this.DistrictCenter = districtCenter;
		}
	}
}
