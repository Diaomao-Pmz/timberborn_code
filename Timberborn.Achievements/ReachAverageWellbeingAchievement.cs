using System;
using Timberborn.AchievementSystem;
using Timberborn.TickSystem;
using Timberborn.Wellbeing;

namespace Timberborn.Achievements
{
	// Token: 0x0200003D RID: 61
	public abstract class ReachAverageWellbeingAchievement : Achievement, ITickableSingleton
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000464C File Offset: 0x0000284C
		public ReachAverageWellbeingAchievement(WellbeingService wellbeingService, int threshold)
		{
			this._wellbeingService = wellbeingService;
			this._threshold = threshold;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004662 File Offset: 0x00002862
		public override string Id
		{
			get
			{
				return string.Format("REACH_{0}_AVERAGE_WELLBEING", this._threshold);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004679 File Offset: 0x00002879
		public void Tick()
		{
			if (base.IsEnabled && this._wellbeingService.AverageGlobalWellbeing >= this._threshold)
			{
				base.Unlock();
			}
		}

		// Token: 0x04000091 RID: 145
		public readonly WellbeingService _wellbeingService;

		// Token: 0x04000092 RID: 146
		public readonly int _threshold;
	}
}
