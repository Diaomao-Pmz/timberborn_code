using System;
using System.Text;
using Timberborn.BonusSystem;
using Timberborn.CoreUI;
using Timberborn.MortalSystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.WellbeingUI;

namespace Timberborn.MortalSystemUI
{
	// Token: 0x02000004 RID: 4
	public class LethalNeedEffectDescriber : INeedEffectDescriber
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public LethalNeedEffectDescriber(BonusDescriber bonusDescriber)
		{
			this._bonusDescriber = bonusDescriber;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		public void DescribeNeedEffects(StringBuilder content, NeedManager needManager, NeedSpec needSpec)
		{
			if (!needManager.NeedIsFavorable(needSpec.Id))
			{
				LethalNeedSpec spec = needSpec.GetSpec<LethalNeedSpec>();
				if (spec != null)
				{
					string value = spec.DeathWarning.Value;
					content.AppendLine(" " + SpecialStrings.RowStarter + this._bonusDescriber.ColorNegative(value));
				}
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly BonusDescriber _bonusDescriber;
	}
}
