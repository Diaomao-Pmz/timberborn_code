using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.NeedSystem;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200001C RID: 28
	public class WellbeingBatchControlRowItemFactory
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003C7A File Offset: 0x00001E7A
		public WellbeingBatchControlRowItemFactory(VisualElementLoader visualElementLoader, WellbeingSummaryFactory wellbeingSummaryFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._wellbeingSummaryFactory = wellbeingSummaryFactory;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003C90 File Offset: 0x00001E90
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			NeedManager component = entity.GetComponent<NeedManager>();
			if (component && component.GetComponent<WellbeingTracker>())
			{
				string elementName = "Game/BatchControl/WellbeingBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				WellbeingSummary wellbeingSummary = this._wellbeingSummaryFactory.Create(component);
				UQueryExtensions.Q<VisualElement>(visualElement, "Summary", null).Add(wellbeingSummary.Root);
				return new WellbeingBatchControlRowItem(visualElement, wellbeingSummary);
			}
			return null;
		}

		// Token: 0x04000084 RID: 132
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000085 RID: 133
		public readonly WellbeingSummaryFactory _wellbeingSummaryFactory;
	}
}
