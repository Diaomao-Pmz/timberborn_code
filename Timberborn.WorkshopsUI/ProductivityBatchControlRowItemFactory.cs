using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x0200001B RID: 27
	public class ProductivityBatchControlRowItemFactory
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00003938 File Offset: 0x00001B38
		public ProductivityBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003958 File Offset: 0x00001B58
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			WorkshopProductivityCounter workshopProductivityCounter = entity.GetComponent<WorkshopProductivityCounter>();
			if (workshopProductivityCounter != null)
			{
				string elementName = "Game/BatchControl/ProductivityBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Image productivity = UQueryExtensions.Q<Image>(visualElement, "Productivity", null);
				this._tooltipRegistrar.Register(visualElement, () => this.GetTooltipText(workshopProductivityCounter));
				return new ProductivityBatchControlRowItem(visualElement, productivity, workshopProductivityCounter);
			}
			return null;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000039D0 File Offset: 0x00001BD0
		public string GetTooltipText(WorkshopProductivityCounter workshopProductivityCounter)
		{
			string str = NumberFormatter.FormatAsPercentCeiled((double)workshopProductivityCounter.CalculateProductivity());
			return this._loc.T(ProductivityBatchControlRowItemFactory.ProductivityLocKey) + " " + str;
		}

		// Token: 0x0400007C RID: 124
		public static readonly string ProductivityLocKey = "Work.Productivity";

		// Token: 0x0400007D RID: 125
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400007E RID: 126
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400007F RID: 127
		public readonly ILoc _loc;
	}
}
