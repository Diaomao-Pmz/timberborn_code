using System;
using System.Text;
using Timberborn.BonusSystem;
using Timberborn.CoreUI;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.WellbeingUI;
using Timberborn.WorkSystem;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000024 RID: 36
	public class WorkSystemNeedEffectDescriber : INeedEffectDescriber
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public WorkSystemNeedEffectDescriber(BonusDescriber bonusDescriber)
		{
			this._bonusDescriber = bonusDescriber;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003E00 File Offset: 0x00002000
		public void DescribeNeedEffects(StringBuilder content, NeedManager needManager, NeedSpec needSpec)
		{
			NeedPreventingWorkSpec workPreventingSpec = WorkSystemNeedEffectDescriber.GetWorkPreventingSpec(needManager, needSpec);
			if (workPreventingSpec != null)
			{
				string value = workPreventingSpec.WorkRefusalWarning.Value;
				content.AppendLine(" " + SpecialStrings.RowStarter + this._bonusDescriber.ColorNegative(value));
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003E4C File Offset: 0x0000204C
		public static NeedPreventingWorkSpec GetWorkPreventingSpec(NeedManager needManager, NeedSpec needSpec)
		{
			NeedPreventingWorkSpec spec = needSpec.GetSpec<NeedPreventingWorkSpec>();
			if (spec != null)
			{
				bool flag = !needManager.NeedIsFavorable(needSpec.Id);
				Worker component = needManager.GetComponent<Worker>();
				if (flag && component)
				{
					return spec;
				}
			}
			return null;
		}

		// Token: 0x040000A3 RID: 163
		public readonly BonusDescriber _bonusDescriber;
	}
}
