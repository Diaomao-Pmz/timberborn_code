using System;
using Timberborn.CoreUI;
using Timberborn.Goods;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200000B RID: 11
	public class GoodSelectionBoxRowFactory
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002371 File Offset: 0x00000571
		public GoodSelectionBoxRowFactory(VisualElementLoader visualElementLoader, GoodsGroupSpecService goodsGroupSpecService)
		{
			this._visualElementLoader = visualElementLoader;
			this._goodsGroupSpecService = goodsGroupSpecService;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002388 File Offset: 0x00000588
		public GoodSelectionBoxRow Create(string goodGroupId)
		{
			GoodGroupSpec spec = this._goodsGroupSpecService.GetSpec(goodGroupId);
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/GoodSelectionBoxRow");
			UQueryExtensions.Q<Image>(visualElement, "HeaderIcon", null).sprite = spec.Icon.Asset;
			return new GoodSelectionBoxRow(visualElement, spec.Order, UQueryExtensions.Q<VisualElement>(visualElement, "Icons", null));
		}

		// Token: 0x04000019 RID: 25
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001A RID: 26
		public readonly GoodsGroupSpecService _goodsGroupSpecService;
	}
}
