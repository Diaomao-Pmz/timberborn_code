using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.GoodsUI;
using Timberborn.ResourceCountingSystemUI;
using Timberborn.StockpilesUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.TopBarSystem
{
	// Token: 0x0200000A RID: 10
	public class TopBarCounterRowFactory
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000024A7 File Offset: 0x000006A7
		public TopBarCounterRowFactory(GoodDescriber goodDescriber, VisualElementLoader visualElementLoader, ContextualResourceCountingService contextualResourceCountingService, ITooltipRegistrar tooltipRegistrar, GoodStockpilesTooltipFactory goodStockpilesTooltipFactory)
		{
			this._goodDescriber = goodDescriber;
			this._visualElementLoader = visualElementLoader;
			this._contextualResourceCountingService = contextualResourceCountingService;
			this._tooltipRegistrar = tooltipRegistrar;
			this._goodStockpilesTooltipFactory = goodStockpilesTooltipFactory;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024D4 File Offset: 0x000006D4
		public IEnumerable<TopBarCounterRow> Create(IEnumerable<string> goods, VisualElement root)
		{
			foreach (string goodId in goods)
			{
				yield return this.Create(goodId, root);
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024F4 File Offset: 0x000006F4
		public TopBarCounterRow Create(string goodId, VisualElement root)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/TopBar/TopBarCounterRow");
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = this._goodDescriber.GetIcon(goodId);
			this._tooltipRegistrar.Register(visualElement, () => this._goodStockpilesTooltipFactory.Create(goodId));
			root.Add(visualElement);
			return new TopBarCounterRow(this._contextualResourceCountingService, goodId, visualElement, UQueryExtensions.Q<Label>(visualElement, "Count", null), UQueryExtensions.Q<VisualElement>(visualElement, "Fill", null), false);
		}

		// Token: 0x0400001C RID: 28
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x0400001D RID: 29
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001E RID: 30
		public readonly ContextualResourceCountingService _contextualResourceCountingService;

		// Token: 0x0400001F RID: 31
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000020 RID: 32
		public readonly GoodStockpilesTooltipFactory _goodStockpilesTooltipFactory;
	}
}
