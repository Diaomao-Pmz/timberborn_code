using System;
using Timberborn.CoreUI;
using Timberborn.Effects;
using Timberborn.GoodsUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.RecoverableGoodSystemUI
{
	// Token: 0x0200000A RID: 10
	public class RecoverableGoodItemFactory
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000025CF File Offset: 0x000007CF
		public RecoverableGoodItemFactory(GoodDescriber goodDescriber, GoodEffectDescriber goodEffectDescriber, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._goodDescriber = goodDescriber;
			this._goodEffectDescriber = goodEffectDescriber;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025F4 File Offset: 0x000007F4
		public RecoverableGoodItem Create(string goodId)
		{
			string elementName = "Game/RecoverableGood/RecoverableGoodItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			string tooltipText = this._goodEffectDescriber.DescribeEffectsWithHeader(goodId);
			this._tooltipRegistrar.Register(visualElement, tooltipText);
			DescribedGood describedGood = this._goodDescriber.GetDescribedGood(goodId);
			UQueryExtensions.Q<Image>(visualElement, "GoodIcon", null).sprite = describedGood.Icon;
			Label amountLabel = UQueryExtensions.Q<Label>(visualElement, "Amount", null);
			return new RecoverableGoodItem(visualElement, goodId, amountLabel);
		}

		// Token: 0x0400001A RID: 26
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x0400001B RID: 27
		public readonly GoodEffectDescriber _goodEffectDescriber;

		// Token: 0x0400001C RID: 28
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400001D RID: 29
		public readonly VisualElementLoader _visualElementLoader;
	}
}
