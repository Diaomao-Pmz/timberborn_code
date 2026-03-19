using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.Wellbeing
{
	// Token: 0x02000011 RID: 17
	public class WellbeingTier
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002550 File Offset: 0x00000750
		public WellbeingTier(WellbeingTierSpec tierSpec)
		{
			this._tierSpec = tierSpec;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000256A File Offset: 0x0000076A
		public static WellbeingTier Create(WellbeingTierSpec tierSpec)
		{
			WellbeingTier wellbeingTier = new WellbeingTier(tierSpec);
			wellbeingTier.CachePredefinedBonuses();
			return wellbeingTier;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002578 File Offset: 0x00000778
		public bool TryGetTierBonus(int wellbeing, out WellbeingTierBonus tierBonus)
		{
			if (wellbeing >= 0)
			{
				tierBonus = ((wellbeing < this._predefinedBonuses.Count) ? this.GetPredefinedBonus(wellbeing) : this.GetCalculatedBonus(wellbeing));
				return true;
			}
			tierBonus = default(WellbeingTierBonus);
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025AC File Offset: 0x000007AC
		public bool TryGetNextTierBonus(int wellbeing, out WellbeingTierBonus nextTierBonus)
		{
			if (wellbeing >= 0)
			{
				nextTierBonus = ((wellbeing < this._predefinedBonuses.Count - 1) ? this.GetPredefinedNextBonus(wellbeing) : this.GetCalculatedNextBonus(wellbeing));
				return true;
			}
			nextTierBonus = default(WellbeingTierBonus);
			return false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025E4 File Offset: 0x000007E4
		public void CachePredefinedBonuses()
		{
			WellbeingTierBonus item = default(WellbeingTierBonus);
			int num = this._tierSpec.Bonuses.Max((WellbeingTierBonusSpec bonus) => bonus.Wellbeing);
			int i;
			int j;
			for (i = 0; i <= num; i = j + 1)
			{
				WellbeingTierBonusSpec wellbeingTierBonusSpec = this._tierSpec.Bonuses.FirstOrDefault((WellbeingTierBonusSpec bonus) => bonus.Wellbeing == i);
				if (wellbeingTierBonusSpec != null)
				{
					item = new WellbeingTierBonus(wellbeingTierBonusSpec.Wellbeing, wellbeingTierBonusSpec.Multiplier);
				}
				this._predefinedBonuses.Add(item);
				j = i;
			}
			this._lastPredefinedTier = this._predefinedBonuses.Last<WellbeingTierBonus>();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026AE File Offset: 0x000008AE
		public WellbeingTierBonus GetPredefinedBonus(int wellbeing)
		{
			return this._predefinedBonuses[wellbeing];
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026BC File Offset: 0x000008BC
		public WellbeingTierBonus GetCalculatedBonus(int wellbeing)
		{
			int wellbeing2 = this._lastPredefinedTier.Wellbeing;
			float bonus = this._lastPredefinedTier.Bonus;
			int num = (wellbeing - wellbeing2) / this._tierSpec.WellbeingThreshold;
			float bonus2 = bonus + (float)num * this._tierSpec.MultiplierIncrement;
			return new WellbeingTierBonus(wellbeing2 + num * this._tierSpec.WellbeingThreshold, bonus2);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002718 File Offset: 0x00000918
		public WellbeingTierBonus GetPredefinedNextBonus(int wellbeing)
		{
			float bonus = this._predefinedBonuses[wellbeing].Bonus;
			for (int i = wellbeing + 1; i < this._predefinedBonuses.Count; i++)
			{
				float bonus2 = this._predefinedBonuses[i].Bonus;
				if (Math.Abs(bonus2 - bonus) > WellbeingTier.BonusComparisionTolerance)
				{
					return new WellbeingTierBonus(i, bonus2);
				}
			}
			return default(WellbeingTierBonus);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002788 File Offset: 0x00000988
		public WellbeingTierBonus GetCalculatedNextBonus(int wellbeing)
		{
			int wellbeing2 = this._lastPredefinedTier.Wellbeing;
			float bonus = this._lastPredefinedTier.Bonus;
			int num = (wellbeing - wellbeing2) / this._tierSpec.WellbeingThreshold;
			int wellbeing3 = wellbeing2 + (num + 1) * this._tierSpec.WellbeingThreshold;
			float bonus2 = bonus + (float)(num + 1) * this._tierSpec.MultiplierIncrement;
			return new WellbeingTierBonus(wellbeing3, bonus2);
		}

		// Token: 0x04000020 RID: 32
		public static readonly float BonusComparisionTolerance = 1E-05f;

		// Token: 0x04000021 RID: 33
		public readonly WellbeingTierSpec _tierSpec;

		// Token: 0x04000022 RID: 34
		public readonly List<WellbeingTierBonus> _predefinedBonuses = new List<WellbeingTierBonus>();

		// Token: 0x04000023 RID: 35
		public WellbeingTierBonus _lastPredefinedTier;
	}
}
