using System;
using Timberborn.AchievementSystem;
using Timberborn.Beavers;
using Timberborn.TickSystem;
using Timberborn.Wellbeing;

namespace Timberborn.Achievements
{
	// Token: 0x0200004C RID: 76
	public class ReachMaxAverageWellbeingPopulatedAchievement : Achievement, ITickableSingleton
	{
		// Token: 0x06000126 RID: 294 RVA: 0x0000495D File Offset: 0x00002B5D
		public ReachMaxAverageWellbeingPopulatedAchievement(WellbeingService wellbeingService, WellbeingLimitService wellbeingLimitService, BeaverPopulation beaverPopulation)
		{
			this._wellbeingService = wellbeingService;
			this._wellbeingLimitService = wellbeingLimitService;
			this._beaverPopulation = beaverPopulation;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000497A File Offset: 0x00002B7A
		public override string Id
		{
			get
			{
				return "REACH_MAX_AVERAGE_WELLBEING_POPULATED";
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004981 File Offset: 0x00002B81
		public void Tick()
		{
			if (base.IsEnabled && this._beaverPopulation.NumberOfBeavers >= ReachMaxAverageWellbeingPopulatedAchievement.RequiredPopulation && this._wellbeingService.AverageGlobalWellbeing >= this._wellbeingLimitService.MaxBeaverWellbeing)
			{
				base.Unlock();
			}
		}

		// Token: 0x0400009E RID: 158
		public static readonly int RequiredPopulation = 100;

		// Token: 0x0400009F RID: 159
		public readonly WellbeingService _wellbeingService;

		// Token: 0x040000A0 RID: 160
		public readonly WellbeingLimitService _wellbeingLimitService;

		// Token: 0x040000A1 RID: 161
		public readonly BeaverPopulation _beaverPopulation;
	}
}
