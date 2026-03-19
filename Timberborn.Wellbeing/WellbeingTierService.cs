using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Beavers;
using Timberborn.BlueprintSystem;
using Timberborn.Bots;
using Timberborn.SingletonSystem;

namespace Timberborn.Wellbeing
{
	// Token: 0x02000017 RID: 23
	public class WellbeingTierService : IWellbeingTierService, ILoadableSingleton
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002B93 File Offset: 0x00000D93
		public WellbeingTierService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BA2 File Offset: 0x00000DA2
		public void Load()
		{
			this._adultWellbeingTiers = this.CreateWellbeingTiers("BeaverAdult");
			this._childWellbeingTiers = this.CreateWellbeingTiers("BeaverChild");
			this._botWellbeingTiers = this.CreateWellbeingTiers("Bot");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BD7 File Offset: 0x00000DD7
		public IEnumerable<string> GetTierableBonuses(WellbeingTracker wellbeingTracker)
		{
			return this.GetWellbeingTiers(wellbeingTracker).Keys;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public bool TryGetTierBonus(WellbeingTracker wellbeingTracker, string bonusId, int wellbeing, out WellbeingTierBonus tierBonus)
		{
			WellbeingTier wellbeingTier;
			if (this.GetWellbeingTiers(wellbeingTracker).TryGetValue(bonusId, out wellbeingTier))
			{
				return wellbeingTier.TryGetTierBonus(wellbeing, out tierBonus);
			}
			tierBonus = default(WellbeingTierBonus);
			return false;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C1C File Offset: 0x00000E1C
		public bool TryGetNextTierBonus(WellbeingTracker wellbeingTracker, string bonusId, int wellbeing, out WellbeingTierBonus nextTierBonus)
		{
			WellbeingTier wellbeingTier;
			if (this.GetWellbeingTiers(wellbeingTracker).TryGetValue(bonusId, out wellbeingTier))
			{
				return wellbeingTier.TryGetNextTierBonus(wellbeing, out nextTierBonus);
			}
			nextTierBonus = default(WellbeingTierBonus);
			return false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C50 File Offset: 0x00000E50
		public Dictionary<string, WellbeingTier> CreateWellbeingTiers(string characterType)
		{
			return (from spec in this._specService.GetSpecs<WellbeingTierSpec>()
			where spec.CharacterType == characterType
			select spec).ToDictionary((WellbeingTierSpec spec) => spec.BonusId, new Func<WellbeingTierSpec, WellbeingTier>(WellbeingTier.Create));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CB6 File Offset: 0x00000EB6
		public Dictionary<string, WellbeingTier> GetWellbeingTiers(WellbeingTracker wellbeingTracker)
		{
			if (wellbeingTracker.HasComponent<BotSpec>())
			{
				return this._botWellbeingTiers;
			}
			if (!wellbeingTracker.GetComponent<Child>())
			{
				return this._adultWellbeingTiers;
			}
			return this._childWellbeingTiers;
		}

		// Token: 0x0400002E RID: 46
		public readonly ISpecService _specService;

		// Token: 0x0400002F RID: 47
		public Dictionary<string, WellbeingTier> _adultWellbeingTiers;

		// Token: 0x04000030 RID: 48
		public Dictionary<string, WellbeingTier> _childWellbeingTiers;

		// Token: 0x04000031 RID: 49
		public Dictionary<string, WellbeingTier> _botWellbeingTiers;
	}
}
