using System;
using Timberborn.CoreUI;
using Timberborn.ResourceCountingSystemUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000008 RID: 8
	public class GoodSelectionBoxItemFactory
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021A5 File Offset: 0x000003A5
		public GoodSelectionBoxItemFactory(VisualElementLoader visualElementLoader, StockpileOptionsService stockpileOptionsService, ContextualResourceCountingService contextualResourceCountingService, ITooltipRegistrar tooltipRegistrar, GoodStockpilesTooltipFactory goodStockpilesTooltipFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._stockpileOptionsService = stockpileOptionsService;
			this._contextualResourceCountingService = contextualResourceCountingService;
			this._tooltipRegistrar = tooltipRegistrar;
			this._goodStockpilesTooltipFactory = goodStockpilesTooltipFactory;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D4 File Offset: 0x000003D4
		public GoodSelectionBoxItem CreateForGood(string goodId, Action<string> itemAction)
		{
			string elementName = "Game/EntityPanel/StockpileInventoryFragmentItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<Button>(visualElement, "GoodSelectionBoxItem", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				itemAction(goodId);
			}, 0);
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = this._stockpileOptionsService.GetItemIcon(goodId);
			this._tooltipRegistrar.Register(visualElement, () => this._goodStockpilesTooltipFactory.Create(goodId));
			return new GoodSelectionBoxItem(this._contextualResourceCountingService, visualElement, goodId, UQueryExtensions.Q<VisualElement>(visualElement, "Fill", null));
		}

		// Token: 0x0400000D RID: 13
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000E RID: 14
		public readonly StockpileOptionsService _stockpileOptionsService;

		// Token: 0x0400000F RID: 15
		public readonly ContextualResourceCountingService _contextualResourceCountingService;

		// Token: 0x04000010 RID: 16
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000011 RID: 17
		public readonly GoodStockpilesTooltipFactory _goodStockpilesTooltipFactory;
	}
}
