using System;
using Timberborn.BonusSystem;
using Timberborn.CoreUI;
using Timberborn.TooltipSystem;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000025 RID: 37
	public class WellbeingSummaryBonusFactory
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004A10 File Offset: 0x00002C10
		public WellbeingSummaryBonusFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, WellbeingBonusTooltipFactory wellbeingBonusTooltipFactory, BonusTypeSpecService bonusTypeSpecService)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._wellbeingBonusTooltipFactory = wellbeingBonusTooltipFactory;
			this._bonusTypeSpecService = bonusTypeSpecService;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004A38 File Offset: 0x00002C38
		public WellbeingSummaryBonus Create(BonusManager bonusManager, string bonusId)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/WellbeingSummaryBonusFragment");
			Label bonusValue = UQueryExtensions.Q<Label>(visualElement, "Value", null);
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = this._bonusTypeSpecService.GetSpec(bonusId).Icon.Asset;
			WellbeingSummaryBonus wellbeingSummaryBonus = new WellbeingSummaryBonus(visualElement, bonusManager, bonusValue, bonusId);
			wellbeingSummaryBonus.UpdateBonus();
			WellbeingTracker wellbeingTracker = bonusManager.GetComponent<WellbeingTracker>();
			this._tooltipRegistrar.Register(visualElement, () => this._wellbeingBonusTooltipFactory.Create(wellbeingTracker, bonusId));
			return wellbeingSummaryBonus;
		}

		// Token: 0x040000BA RID: 186
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000BB RID: 187
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x040000BC RID: 188
		public readonly WellbeingBonusTooltipFactory _wellbeingBonusTooltipFactory;

		// Token: 0x040000BD RID: 189
		public readonly BonusTypeSpecService _bonusTypeSpecService;
	}
}
