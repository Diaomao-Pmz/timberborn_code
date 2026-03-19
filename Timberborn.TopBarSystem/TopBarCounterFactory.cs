using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.Goods;
using Timberborn.ResourceCountingSystemUI;
using Timberborn.StockpilesUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.TopBarSystem
{
	// Token: 0x02000006 RID: 6
	public class TopBarCounterFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002164 File Offset: 0x00000364
		public TopBarCounterFactory(VisualElementLoader visualElementLoader, TopBarCounterRowFactory topBarCounterRowFactory, ITooltipRegistrar tooltipRegistrar, ContextualResourceCountingService contextualResourceCountingService, GoodStockpilesTooltipFactory goodStockpilesTooltipFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._topBarCounterRowFactory = topBarCounterRowFactory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._contextualResourceCountingService = contextualResourceCountingService;
			this._goodStockpilesTooltipFactory = goodStockpilesTooltipFactory;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002194 File Offset: 0x00000394
		public ITopBarCounter CreateSimpleCounter(GoodGroupSpec goodGroupSpec, string goodId, VisualElement root)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/TopBar/SimpleTopBarCounter");
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = goodGroupSpec.Icon.Asset;
			this._tooltipRegistrar.Register(UQueryExtensions.Q<VisualElement>(visualElement, "CounterWrapper", null), () => this._goodStockpilesTooltipFactory.Create(goodId));
			TopBarCounterFactory.ConfigureForDistrictMode(visualElement);
			root.Add(visualElement);
			return new TopBarCounterRow(this._contextualResourceCountingService, goodId, visualElement, UQueryExtensions.Q<Label>(visualElement, "Count", null), UQueryExtensions.Q<VisualElement>(visualElement, "Fill", null), true);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002240 File Offset: 0x00000440
		public ITopBarCounter CreateExtendableCounter(GoodGroupSpec goodGroupSpec, IEnumerable<string> goods, VisualElement root)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/TopBar/ExtendableTopBarCounter");
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = goodGroupSpec.Icon.Asset;
			this._tooltipRegistrar.Register(UQueryExtensions.Q<VisualElement>(visualElement, "CounterWrapper", null), goodGroupSpec.DisplayName.Value);
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(visualElement, "CounterItems", null);
			TopBarCounterFactory.ConfigureForDistrictMode(visualElement);
			TopBarCounterFactory.ConfigureVisibilityToggling(visualElement, visualElement2);
			root.Add(visualElement);
			return new ExtendableTopBarCounter(this._topBarCounterRowFactory.Create(goods, visualElement2), UQueryExtensions.Q<Label>(visualElement, "EmptyCounterPlaceholder", null), UQueryExtensions.Q<Label>(visualElement, "Count", null));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022E7 File Offset: 0x000004E7
		public static void ConfigureForDistrictMode(VisualElement counter)
		{
			UQueryExtensions.Q<VisualElement>(counter, null, "top-bar-counter").AddToClassList("top-bar-counter__wrapper--district");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002300 File Offset: 0x00000500
		public static void ConfigureVisibilityToggling(VisualElement root, VisualElement items)
		{
			Button toggler = UQueryExtensions.Q<Button>(root, "ExtensionToggler", null);
			VisualElement background = UQueryExtensions.Q<VisualElement>(root, "Background", null);
			UQueryExtensions.Q<VisualElement>(root, "CounterWrapper", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				TopBarCounterFactory.ToggleVisibility(toggler, items, background);
			}, 0);
			toggler.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				TopBarCounterFactory.ToggleVisibility(toggler, items, background);
			}, 0);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002374 File Offset: 0x00000574
		public static void ToggleVisibility(Button toggler, VisualElement items, VisualElement background)
		{
			bool flag = items.IsDisplayed();
			toggler.EnableInClassList(TopBarCounterFactory.HiddenClass, flag);
			background.ToggleDisplayStyle(!flag);
			items.ToggleDisplayStyle(!flag);
		}

		// Token: 0x0400000A RID: 10
		public static readonly string HiddenClass = "extension-clamp--hidden";

		// Token: 0x0400000B RID: 11
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000C RID: 12
		public readonly TopBarCounterRowFactory _topBarCounterRowFactory;

		// Token: 0x0400000D RID: 13
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000E RID: 14
		public readonly ContextualResourceCountingService _contextualResourceCountingService;

		// Token: 0x0400000F RID: 15
		public readonly GoodStockpilesTooltipFactory _goodStockpilesTooltipFactory;
	}
}
