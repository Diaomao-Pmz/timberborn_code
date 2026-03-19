using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DeteriorationSystem;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.DeteriorationSystemUI
{
	// Token: 0x02000005 RID: 5
	public class DeteriorableBatchControlRowItemFactory
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002101 File Offset: 0x00000301
		public DeteriorableBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002120 File Offset: 0x00000320
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Deteriorable deteriorable = entity.GetComponent<Deteriorable>();
			if (deteriorable != null)
			{
				string elementName = "Game/BatchControl/DeteriorableBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Label progressLabel = UQueryExtensions.Q<Label>(visualElement, "Progress", null);
				this._tooltipRegistrar.Register(visualElement, () => this.GetTooltipText(deteriorable));
				return new DeteriorableBatchControlRowItem(visualElement, progressLabel, deteriorable);
			}
			return null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		public string GetTooltipText(Deteriorable deteriorable)
		{
			string str = NumberFormatter.FormatAsPercentFloored((double)deteriorable.DeteriorationProgress);
			return this._loc.T(DeteriorableBatchControlRowItemFactory.DurabilityLocKey) + ": " + str;
		}

		// Token: 0x04000009 RID: 9
		public static readonly string DurabilityLocKey = "Bot.Durability";

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;
	}
}
