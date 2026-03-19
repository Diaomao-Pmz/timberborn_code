using System;
using Timberborn.AchievementSystem;
using Timberborn.TickSystem;
using Timberborn.Wellbeing;

namespace Timberborn.Achievements
{
	// Token: 0x0200004B RID: 75
	public class ReachMaxAverageWellbeingAchievement : Achievement, ITickableSingleton
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00004918 File Offset: 0x00002B18
		public ReachMaxAverageWellbeingAchievement(WellbeingService wellbeingService, WellbeingLimitService wellbeingLimitService)
		{
			this._wellbeingService = wellbeingService;
			this._wellbeingLimitService = wellbeingLimitService;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000492E File Offset: 0x00002B2E
		public override string Id
		{
			get
			{
				return "REACH_MAX_AVERAGE_WELLBEING";
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004935 File Offset: 0x00002B35
		public void Tick()
		{
			if (base.IsEnabled && this._wellbeingService.AverageGlobalWellbeing >= this._wellbeingLimitService.MaxBeaverWellbeing)
			{
				base.Unlock();
			}
		}

		// Token: 0x0400009C RID: 156
		public readonly WellbeingService _wellbeingService;

		// Token: 0x0400009D RID: 157
		public readonly WellbeingLimitService _wellbeingLimitService;
	}
}
