using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Bots;
using Timberborn.GameFactionSystem;
using Timberborn.NeedSpecs;
using Timberborn.SingletonSystem;

namespace Timberborn.Wellbeing
{
	// Token: 0x0200000E RID: 14
	public class WellbeingLimitService : ILoadableSingleton
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002313 File Offset: 0x00000513
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000231B File Offset: 0x0000051B
		public int MaxBeaverWellbeing { get; private set; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002324 File Offset: 0x00000524
		public WellbeingLimitService(FactionNeedService factionNeedService)
		{
			this._factionNeedService = factionNeedService;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002333 File Offset: 0x00000533
		public void Load()
		{
			this.MaxBeaverWellbeing = WellbeingLimitService.GetMaxWellbeing(this._factionNeedService.GetBeaverNeeds());
			this._maxBotWellbeing = WellbeingLimitService.GetMaxWellbeing(this._factionNeedService.GetBotNeeds());
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002361 File Offset: 0x00000561
		public int GetMaxWellbeing(WellbeingTracker wellbeingTracker)
		{
			if (!wellbeingTracker.HasComponent<BotSpec>())
			{
				return this.MaxBeaverWellbeing;
			}
			return this._maxBotWellbeing;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002378 File Offset: 0x00000578
		public static int GetMaxWellbeing(IEnumerable<NeedSpec> needSpecs)
		{
			return needSpecs.Sum((NeedSpec spec) => spec.GetFavorableWellbeing());
		}

		// Token: 0x04000015 RID: 21
		public readonly FactionNeedService _factionNeedService;

		// Token: 0x04000016 RID: 22
		public int _maxBotWellbeing;
	}
}
