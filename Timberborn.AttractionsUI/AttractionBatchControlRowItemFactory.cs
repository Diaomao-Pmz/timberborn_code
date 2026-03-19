using System;
using Timberborn.Attractions;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EnterableSystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.AttractionsUI
{
	// Token: 0x02000005 RID: 5
	public class AttractionBatchControlRowItemFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000212E File Offset: 0x0000032E
		public AttractionBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002144 File Offset: 0x00000344
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Attraction component = entity.GetComponent<Attraction>();
			if (component != null)
			{
				string elementName = "Game/BatchControl/AttractionBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Label capacityLabel = UQueryExtensions.Q<Label>(visualElement, "Info", null);
				Enterable component2 = component.GetComponent<Enterable>();
				this._tooltipRegistrar.RegisterLocalizable(visualElement, AttractionBatchControlRowItemFactory.VisitorsLocKey);
				return new AttractionBatchControlRowItem(visualElement, capacityLabel, component2);
			}
			return null;
		}

		// Token: 0x04000009 RID: 9
		public static readonly string VisitorsLocKey = "Attractions.Visitors";

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly ITooltipRegistrar _tooltipRegistrar;
	}
}
