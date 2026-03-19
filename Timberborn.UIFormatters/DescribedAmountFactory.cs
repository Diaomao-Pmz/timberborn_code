using System;
using Timberborn.CoreUI;
using Timberborn.TooltipSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.UIFormatters
{
	// Token: 0x02000004 RID: 4
	public class DescribedAmountFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public DescribedAmountFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public VisualElement CreatePlain(string rootClass, string amount, Sprite icon, string tooltip)
		{
			VisualElement visualElement = this.CreatePlain(rootClass, amount, tooltip);
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = icon;
			return visualElement;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		public VisualElement CreatePlain(string rootClass, string amount, string tooltip)
		{
			VisualElement visualElement = this.CreatePlain(rootClass, amount);
			this._tooltipRegistrar.Register(visualElement, tooltip);
			return visualElement;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002118 File Offset: 0x00000318
		public VisualElement CreatePlain(string rootClass, string amount)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/DescribedAmountPlain");
			visualElement.AddToClassList(rootClass);
			UQueryExtensions.Q<Label>(visualElement, "Amount", null).text = amount;
			return visualElement;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		public VisualElement CreateBordered(string amount, Sprite icon, string tooltip)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityDescription/DescribedAmountBordered");
			UQueryExtensions.Q<Label>(visualElement, "Amount", null).text = amount;
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = icon;
			this._tooltipRegistrar.Register(visualElement, tooltip);
			return visualElement;
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly ITooltipRegistrar _tooltipRegistrar;
	}
}
