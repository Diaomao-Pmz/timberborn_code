using System;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BonusSystem
{
	// Token: 0x02000007 RID: 7
	public class BonusDescriber : ILoadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BonusDescriber(BonusTypeSpecService bonusTypeSpecService, ISpecService specService)
		{
			this._bonusTypeSpecService = bonusTypeSpecService;
			this._specService = specService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public void Load()
		{
			BonusDescriberColorsSpec singleSpec = this._specService.GetSingleSpec<BonusDescriberColorsSpec>();
			this._positiveBonusHighlight = singleSpec.PositiveBonusHighlight;
			this._negativeBonusHighlight = singleSpec.NegativeBonusHighlight;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002145 File Offset: 0x00000345
		public string Describe(BonusSpec bonusSpec)
		{
			return this.Describe(bonusSpec.Id, bonusSpec.MultiplierDelta, false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000215A File Offset: 0x0000035A
		public string DescribeColored(BonusSpec bonusSpec)
		{
			return this.Describe(bonusSpec.Id, bonusSpec.MultiplierDelta, true);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000216F File Offset: 0x0000036F
		public string ColorPositive(string description)
		{
			return this.Color(description, true);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002179 File Offset: 0x00000379
		public string ColorNegative(string description)
		{
			return this.Color(description, false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002184 File Offset: 0x00000384
		public string Describe(string bonusId, float multiplierDelta, bool colored)
		{
			bool flag = multiplierDelta > 0f;
			string str = flag ? "+" : "-";
			string str2 = string.Format("{0:0}%", Math.Abs(multiplierDelta) * 100f);
			string text = this._bonusTypeSpecService.GetSpec(bonusId).DisplayName.Value + ": " + str + str2;
			if (!colored)
			{
				return text;
			}
			return this.Color(text, flag);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021F8 File Offset: 0x000003F8
		public string Color(string description, bool positive)
		{
			string text = ColorUtility.ToHtmlStringRGB(positive ? this._positiveBonusHighlight : this._negativeBonusHighlight);
			return string.Concat(new string[]
			{
				"<color=#",
				text,
				">",
				description,
				"</color>"
			});
		}

		// Token: 0x04000008 RID: 8
		public readonly BonusTypeSpecService _bonusTypeSpecService;

		// Token: 0x04000009 RID: 9
		public readonly ISpecService _specService;

		// Token: 0x0400000A RID: 10
		public Color _positiveBonusHighlight;

		// Token: 0x0400000B RID: 11
		public Color _negativeBonusHighlight;
	}
}
