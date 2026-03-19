using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.GameDistrictsMigration;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000005 RID: 5
	public class CurrentPopulationBatchControlRowItemFactory
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002113 File Offset: 0x00000313
		public CurrentPopulationBatchControlRowItemFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002124 File Offset: 0x00000324
		public IBatchControlRowItem Create(PopulationDistributor populationDistributor, string iconClass)
		{
			string elementName = "Game/BatchControl/PopulationBatchControlRowItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<VisualElement>(visualElement, "PopulationIcon", null).AddToClassList(iconClass);
			return new CurrentPopulationBatchControlRowItem(visualElement, UQueryExtensions.Q<Label>(visualElement, "CurrentPopulation", null), populationDistributor);
		}

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;
	}
}
