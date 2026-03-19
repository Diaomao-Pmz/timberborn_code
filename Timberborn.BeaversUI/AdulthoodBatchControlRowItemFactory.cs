using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Beavers;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.BeaversUI
{
	// Token: 0x02000005 RID: 5
	public class AdulthoodBatchControlRowItemFactory
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002103 File Offset: 0x00000303
		public AdulthoodBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002120 File Offset: 0x00000320
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Child child = entity.GetComponent<Child>();
			if (child != null && child.Enabled)
			{
				string elementName = "Game/BatchControl/AdulthoodBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Label progressLabel = UQueryExtensions.Q<Label>(visualElement, "Progress", null);
				this._tooltipRegistrar.Register(visualElement, () => this.GetTooltipText(child));
				return new AdulthoodBatchControlRowItem(visualElement, progressLabel, child);
			}
			return null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021A4 File Offset: 0x000003A4
		public string GetTooltipText(Child child)
		{
			string str = NumberFormatter.FormatAsPercentFloored((double)child.GrowthProgress);
			return this._loc.T(AdulthoodBatchControlRowItemFactory.ProgressLocKey) + ": " + str;
		}

		// Token: 0x04000009 RID: 9
		public static readonly string ProgressLocKey = "Beaver.Adulthood";

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;
	}
}
