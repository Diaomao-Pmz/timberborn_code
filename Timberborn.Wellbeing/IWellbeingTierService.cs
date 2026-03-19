using System;
using System.Collections.Generic;

namespace Timberborn.Wellbeing
{
	// Token: 0x02000009 RID: 9
	public interface IWellbeingTierService
	{
		// Token: 0x0600000B RID: 11
		IEnumerable<string> GetTierableBonuses(WellbeingTracker wellbeingTracker);

		// Token: 0x0600000C RID: 12
		bool TryGetTierBonus(WellbeingTracker wellbeingTracker, string bonusId, int wellbeing, out WellbeingTierBonus tierBonus);

		// Token: 0x0600000D RID: 13
		bool TryGetNextTierBonus(WellbeingTracker wellbeingTracker, string bonusId, int wellbeing, out WellbeingTierBonus nextTierBonus);
	}
}
