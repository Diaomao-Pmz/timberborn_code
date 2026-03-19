using System;
using System.Text;
using Timberborn.BonusSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200001E RID: 30
	public class WellbeingBonusTooltipFactory
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00003E40 File Offset: 0x00002040
		public WellbeingBonusTooltipFactory(VisualElementLoader visualElementLoader, ILoc loc, IWellbeingTierService wellbeingTierService, BonusTypeSpecService bonusTypeSpecService, WellbeingNameHelper wellbeingNameHelper, WellbeingLimitService wellbeingLimitService, BonusDescriber bonusDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._wellbeingTierService = wellbeingTierService;
			this._bonusTypeSpecService = bonusTypeSpecService;
			this._wellbeingNameHelper = wellbeingNameHelper;
			this._wellbeingLimitService = wellbeingLimitService;
			this._bonusDescriber = bonusDescriber;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003E94 File Offset: 0x00002094
		public VisualElement Create(WellbeingTracker wellbeingTracker, string bonusId)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/WellbeingBonusTooltip");
			BonusTypeSpec spec = this._bonusTypeSpecService.GetSpec(bonusId);
			int wellbeing = wellbeingTracker.Wellbeing;
			this.UpdateWellbeingBonus(wellbeingTracker, spec, wellbeing);
			this.UpdateNextTierWellbeingBonus(visualElement, wellbeingTracker, spec, wellbeing);
			this.UpdateNeedPenalties(wellbeingTracker, spec);
			UQueryExtensions.Q<Label>(visualElement, "Description", null).text = this._contentBuilder.ToStringWithoutNewLineEnd();
			UQueryExtensions.Q<Label>(visualElement, "Name", null).text = spec.DisplayName.Value;
			this._contentBuilder.Clear();
			return visualElement;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003F28 File Offset: 0x00002128
		public void UpdateWellbeingBonus(WellbeingTracker wellbeingTracker, BonusTypeSpec bonusTypeSpec, int wellbeing)
		{
			WellbeingTierBonus wellbeingTierBonus;
			this._wellbeingTierService.TryGetTierBonus(wellbeingTracker, bonusTypeSpec.Id, wellbeing, out wellbeingTierBonus);
			string arg = WellbeingBonusTooltipFactory.FormatBonus(wellbeingTierBonus.Bonus);
			string wellbeingName = this._wellbeingNameHelper.GetWellbeingName(wellbeingTracker);
			string text = string.Format("{0} {1}: {2}", wellbeingName, wellbeingTierBonus.Wellbeing, arg);
			if (wellbeingTierBonus.Bonus > 0f)
			{
				text = this._bonusDescriber.ColorPositive(text);
			}
			this._contentBuilder.AppendLine(text);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003FA8 File Offset: 0x000021A8
		public void UpdateNextTierWellbeingBonus(VisualElement root, WellbeingTracker wellbeingTracker, BonusTypeSpec bonusTypeSpec, int wellbeing)
		{
			WellbeingTierBonus wellbeingTierBonus;
			bool flag = this._wellbeingTierService.TryGetNextTierBonus(wellbeingTracker, bonusTypeSpec.Id, wellbeing, out wellbeingTierBonus);
			int maxWellbeing = this._wellbeingLimitService.GetMaxWellbeing(wellbeingTracker);
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(root, "NextTier", null);
			if (flag && wellbeingTierBonus.Wellbeing <= maxWellbeing)
			{
				visualElement.ToggleDisplayStyle(true);
				string text = WellbeingBonusTooltipFactory.FormatBonus(wellbeingTierBonus.Bonus);
				string text2 = this._loc.T(WellbeingBonusTooltipFactory.NextTierLocKey);
				string wellbeingName = this._wellbeingNameHelper.GetWellbeingName(wellbeingTracker);
				UQueryExtensions.Q<Label>(root, "NextTierDescription", null).text = string.Format("{0}:\n{1} {2}: {3}", new object[]
				{
					text2,
					wellbeingName,
					wellbeingTierBonus.Wellbeing,
					text
				});
				return;
			}
			visualElement.ToggleDisplayStyle(false);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000406C File Offset: 0x0000226C
		public void UpdateNeedPenalties(WellbeingTracker wellbeingTracker, BonusTypeSpec bonusTypeSpec)
		{
			NeedManager component = wellbeingTracker.GetComponent<NeedManager>();
			foreach (NeedSpec needSpec in component.NeedSpecs)
			{
				PunitiveNeedSpec spec = needSpec.GetSpec<PunitiveNeedSpec>();
				if (spec != null)
				{
					foreach (BonusSpec bonusSpec in spec.Penalties)
					{
						if (bonusSpec.Id == bonusTypeSpec.Id && !component.NeedIsFavorable(needSpec.Id))
						{
							this.AddNeedPenalty(bonusSpec, needSpec.DisplayName.Value);
						}
					}
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000410C File Offset: 0x0000230C
		public void AddNeedPenalty(BonusSpec penalty, string needDisplayName)
		{
			string str = WellbeingBonusTooltipFactory.FormatBonus(penalty.MultiplierDelta);
			string description = needDisplayName + ": " + str;
			this._contentBuilder.AppendLine(this._bonusDescriber.ColorNegative(description));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000414A File Offset: 0x0000234A
		public static string FormatBonus(float bonusValue)
		{
			return bonusValue.ToString(WellbeingBonusTooltipFactory.BonusFormat);
		}

		// Token: 0x04000087 RID: 135
		public static readonly string BonusFormat = "+#%;-#%;0%";

		// Token: 0x04000088 RID: 136
		public static readonly string NextTierLocKey = "Wellbeing.NextTier";

		// Token: 0x04000089 RID: 137
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400008A RID: 138
		public readonly ILoc _loc;

		// Token: 0x0400008B RID: 139
		public readonly IWellbeingTierService _wellbeingTierService;

		// Token: 0x0400008C RID: 140
		public readonly BonusTypeSpecService _bonusTypeSpecService;

		// Token: 0x0400008D RID: 141
		public readonly WellbeingNameHelper _wellbeingNameHelper;

		// Token: 0x0400008E RID: 142
		public readonly WellbeingLimitService _wellbeingLimitService;

		// Token: 0x0400008F RID: 143
		public readonly BonusDescriber _bonusDescriber;

		// Token: 0x04000090 RID: 144
		public readonly StringBuilder _contentBuilder = new StringBuilder();
	}
}
