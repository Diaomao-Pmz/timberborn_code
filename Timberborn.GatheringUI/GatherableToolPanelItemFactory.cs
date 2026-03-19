using System;
using Timberborn.CoreUI;
using Timberborn.Gathering;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.Yielding;
using Timberborn.YieldingUI;
using UnityEngine.UIElements;

namespace Timberborn.GatheringUI
{
	// Token: 0x0200000B RID: 11
	public class GatherableToolPanelItemFactory
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002880 File Offset: 0x00000A80
		public GatherableToolPanelItemFactory(VisualElementLoader visualElementLoader, ILoc loc, YieldTooltipFactory yieldTooltipFactory, ITooltipRegistrar tooltipRegistrar, GoodDescriber goodDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._yieldTooltipFactory = yieldTooltipFactory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028D4 File Offset: 0x00000AD4
		public VisualElement Create(GatherableSpec gatherableSpec)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/ToolPanel/ResourceYieldPanelItem");
			string text = gatherableSpec.YieldGrowthTimeInDays.ToString("0.#");
			UQueryExtensions.Q<Label>(visualElement, "GrowthTime", null).text = this._loc.T<string>(this._growthTimePhrase, text);
			YielderSpec yielder = gatherableSpec.Yielder;
			GoodAmountSpec yield = yielder.Yield;
			UQueryExtensions.Q<Label>(visualElement, "YieldAmount", null).text = yield.Amount.ToString();
			UQueryExtensions.Q<Image>(visualElement, "YieldIcon", null).sprite = this._goodDescriber.GetIcon(yield.Id);
			UQueryExtensions.Q<VisualElement>(visualElement, "Calendar", null).AddToClassList(GatherableToolPanelItemFactory.IconClass);
			this._tooltipRegistrar.Register(visualElement, this._yieldTooltipFactory.Create(yielder, text, this._loc.T(GatherableToolPanelItemFactory.GrowsWhenMatureLocKey)));
			return visualElement;
		}

		// Token: 0x0400002F RID: 47
		public static readonly string GrowsWhenMatureLocKey = "Growing.GrowsWhenMature";

		// Token: 0x04000030 RID: 48
		public static readonly string IconClass = "resource-yield__icon--calendar-cycle";

		// Token: 0x04000031 RID: 49
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000032 RID: 50
		public readonly ILoc _loc;

		// Token: 0x04000033 RID: 51
		public readonly YieldTooltipFactory _yieldTooltipFactory;

		// Token: 0x04000034 RID: 52
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000035 RID: 53
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000036 RID: 54
		public readonly Phrase _growthTimePhrase = Phrase.New().Format<string>(new Func<string, ILoc, string>(UnitFormatter.FormatDays));
	}
}
