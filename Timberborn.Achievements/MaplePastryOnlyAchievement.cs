using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AchievementSystem;
using Timberborn.GameFactionSystem;
using Timberborn.Goods;
using Timberborn.ResourceCountingSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000030 RID: 48
	public class MaplePastryOnlyAchievement : Achievement
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00003E71 File Offset: 0x00002071
		public MaplePastryOnlyAchievement(IGoodService goodService, EventBus eventBus, ResourceCountingService resourceCountingService, FactionService factionService)
		{
			this._goodService = goodService;
			this._eventBus = eventBus;
			this._resourceCountingService = resourceCountingService;
			this._factionService = factionService;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003EA1 File Offset: 0x000020A1
		public override string Id
		{
			get
			{
				return "MAPLE_PASTRY_ONLY";
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003EA8 File Offset: 0x000020A8
		[OnEvent]
		public void OnNighttimeStart(NighttimeStartEvent nighttimeStartEvent)
		{
			if (this.HasRequiredGood() && !this.HasAnyForbiddenGood())
			{
				base.Unlock();
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003EC0 File Offset: 0x000020C0
		public override void EnableInternal()
		{
			if (this._factionService.Current.Id == AchievementHelper.Folktails)
			{
				this._eventBus.Register(this);
				this._forbiddenGoods.AddRange(from good in this._goodService.GetGoodsForGroup(MaplePastryOnlyAchievement.FoodGroupId)
				where good != MaplePastryOnlyAchievement.RequiredGoodId
				select good);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003F34 File Offset: 0x00002134
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003F44 File Offset: 0x00002144
		public bool HasRequiredGood()
		{
			return this._resourceCountingService.GetGlobalResourceCount(MaplePastryOnlyAchievement.RequiredGoodId).AvailableStock >= MaplePastryOnlyAchievement.RequiredGoodCount;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003F74 File Offset: 0x00002174
		public bool HasAnyForbiddenGood()
		{
			foreach (string goodId in this._forbiddenGoods)
			{
				if (this._resourceCountingService.GetGlobalResourceCount(goodId).AvailableStock > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000072 RID: 114
		public static readonly string FoodGroupId = "Food";

		// Token: 0x04000073 RID: 115
		public static readonly string RequiredGoodId = "MaplePastry";

		// Token: 0x04000074 RID: 116
		public static readonly int RequiredGoodCount = 1000;

		// Token: 0x04000075 RID: 117
		public readonly IGoodService _goodService;

		// Token: 0x04000076 RID: 118
		public readonly EventBus _eventBus;

		// Token: 0x04000077 RID: 119
		public readonly ResourceCountingService _resourceCountingService;

		// Token: 0x04000078 RID: 120
		public readonly FactionService _factionService;

		// Token: 0x04000079 RID: 121
		public readonly List<string> _forbiddenGoods = new List<string>();
	}
}
