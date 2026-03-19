using System;
using System.Text;
using Timberborn.Beavers;
using Timberborn.BonusSystem;
using Timberborn.CoreUI;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.WellbeingUI;

namespace Timberborn.BonusSystemUI
{
	// Token: 0x02000007 RID: 7
	public class NeedPenaltyEffectDescriber : INeedEffectDescriber
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000223B File Offset: 0x0000043B
		public NeedPenaltyEffectDescriber(BonusDescriber bonusDescriber)
		{
			this._bonusDescriber = bonusDescriber;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000224A File Offset: 0x0000044A
		public void DescribeNeedEffects(StringBuilder content, NeedManager needManager, NeedSpec needSpec)
		{
			this.DescribePenalties(content, needManager, needSpec);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002258 File Offset: 0x00000458
		public void DescribePenalties(StringBuilder content, NeedManager needManager, NeedSpec needSpec)
		{
			PunitiveNeedSpec spec = needSpec.GetSpec<PunitiveNeedSpec>();
			if (spec != null && !needManager.NeedIsFavorable(needSpec.Id))
			{
				foreach (BonusSpec bonusSpec in spec.Penalties)
				{
					if (NeedPenaltyEffectDescriber.CanDescribePenalty(needManager, bonusSpec.Id))
					{
						string str = this._bonusDescriber.DescribeColored(bonusSpec);
						content.AppendLine(" " + SpecialStrings.RowStarter + str);
					}
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022D4 File Offset: 0x000004D4
		public static bool CanDescribePenalty(NeedManager needManager, string bonusId)
		{
			Child component = needManager.GetComponent<Child>();
			return (bonusId != NeedPenaltyEffectDescriber.ChildOnlyBonusId || component) && (bonusId != NeedPenaltyEffectDescriber.AdultOnlyBonusId || !component);
		}

		// Token: 0x0400000C RID: 12
		public static readonly string AdultOnlyBonusId = "WorkingSpeed";

		// Token: 0x0400000D RID: 13
		public static readonly string ChildOnlyBonusId = "GrowthSpeed";

		// Token: 0x0400000E RID: 14
		public readonly BonusDescriber _bonusDescriber;
	}
}
