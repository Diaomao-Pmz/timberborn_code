using System;
using Timberborn.CoreUI;
using Timberborn.Cutting;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.Growing;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.Yielding;
using Timberborn.YieldingUI;
using UnityEngine.UIElements;

namespace Timberborn.GrowingUI
{
	// Token: 0x02000005 RID: 5
	public class GrowableToolPanelItemFactory
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000241C File Offset: 0x0000061C
		public GrowableToolPanelItemFactory(VisualElementLoader visualElementLoader, ILoc loc, YieldTooltipFactory yieldTooltipFactory, ITooltipRegistrar tooltipRegistrar, GoodDescriber goodDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._yieldTooltipFactory = yieldTooltipFactory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002470 File Offset: 0x00000670
		public VisualElement Create(GrowableSpec growableSpec)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/ToolPanel/ResourceYieldPanelItem");
			string text = growableSpec.GrowthTimeInDays.ToString("0.#");
			UQueryExtensions.Q<Label>(visualElement, "GrowthTime", null).text = this._loc.T<string>(this._growthTimePhrase, text);
			Label label = UQueryExtensions.Q<Label>(visualElement, "YieldAmount", null);
			Image image = UQueryExtensions.Q<Image>(visualElement, "YieldIcon", null);
			CuttableSpec spec = growableSpec.GetSpec<CuttableSpec>();
			if (spec != null)
			{
				YielderSpec yielder = spec.Yielder;
				GoodAmountSpec yield = yielder.Yield;
				label.text = yield.Amount.ToString();
				image.sprite = this._goodDescriber.GetIcon(yield.Id);
				this._tooltipRegistrar.Register(visualElement, this._yieldTooltipFactory.Create(yielder, text, null));
			}
			else
			{
				this._tooltipRegistrar.Register(visualElement, this._loc.T<string>(GrowableToolPanelItemFactory.GrowingTimeLocKey, text));
			}
			label.ToggleDisplayStyle(spec != null);
			image.EnableInClassList(GrowableToolPanelItemFactory.NoYieldClass, spec == null);
			UQueryExtensions.Q<VisualElement>(visualElement, "Calendar", null).AddToClassList(GrowableToolPanelItemFactory.IconClass);
			return visualElement;
		}

		// Token: 0x04000017 RID: 23
		public static readonly string GrowingTimeLocKey = "Growing.Time";

		// Token: 0x04000018 RID: 24
		public static readonly string IconClass = "resource-yield__icon--calendar";

		// Token: 0x04000019 RID: 25
		public static readonly string NoYieldClass = "resource-yield__no-yield";

		// Token: 0x0400001A RID: 26
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001B RID: 27
		public readonly ILoc _loc;

		// Token: 0x0400001C RID: 28
		public readonly YieldTooltipFactory _yieldTooltipFactory;

		// Token: 0x0400001D RID: 29
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400001E RID: 30
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x0400001F RID: 31
		public readonly Phrase _growthTimePhrase = Phrase.New().Format<string>(new Func<string, ILoc, string>(UnitFormatter.FormatDays));
	}
}
