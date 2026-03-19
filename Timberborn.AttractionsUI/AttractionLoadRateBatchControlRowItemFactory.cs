using System;
using System.Collections.Generic;
using Timberborn.Attractions;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.TimeSystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.AttractionsUI
{
	// Token: 0x0200000C RID: 12
	public class AttractionLoadRateBatchControlRowItemFactory
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002825 File Offset: 0x00000A25
		public AttractionLoadRateBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, IDayNightCycle dayNightCycle)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002844 File Offset: 0x00000A44
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			AttractionLoadRate component = entity.GetComponent<AttractionLoadRate>();
			if (component != null)
			{
				string elementName = "Game/BatchControl/AttractionLoadRateBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				this._tooltipRegistrar.RegisterLocalizable(visualElement, AttractionLoadRateBatchControlRowItemFactory.LoadRateLocKey);
				IEnumerable<VisualElement> loadRateRoots = this.CreateLoadRates(visualElement);
				AttractionLoadRateBatchControlRowItem attractionLoadRateBatchControlRowItem = new AttractionLoadRateBatchControlRowItem(this._dayNightCycle, visualElement, component, loadRateRoots);
				attractionLoadRateBatchControlRowItem.Initialize();
				return attractionLoadRateBatchControlRowItem;
			}
			return null;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000289D File Offset: 0x00000A9D
		public IEnumerable<VisualElement> CreateLoadRates(VisualElement root)
		{
			int num;
			for (int i = 0; i < 24; i = num + 1)
			{
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/AttractionLoadRate");
				root.Add(visualElement);
				yield return visualElement;
				num = i;
			}
			yield break;
		}

		// Token: 0x0400002A RID: 42
		public static readonly string LoadRateLocKey = "Attractions.LoadRate";

		// Token: 0x0400002B RID: 43
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002C RID: 44
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400002D RID: 45
		public readonly IDayNightCycle _dayNightCycle;
	}
}
