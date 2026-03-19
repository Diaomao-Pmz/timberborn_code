using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.StockpilePrioritySystem;
using UnityEngine.UIElements;

namespace Timberborn.StockpilePriorityUISystem
{
	// Token: 0x02000005 RID: 5
	public class StockpilePriorityBatchControlRowItemFactory
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020E9 File Offset: 0x000002E9
		public StockpilePriorityBatchControlRowItemFactory(VisualElementLoader visualElementLoader, StockpilePriorityToggleFactory stockpilePriorityToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._stockpilePriorityToggleFactory = stockpilePriorityToggleFactory;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			StockpilePriority component = entity.GetComponent<StockpilePriority>();
			if (component != null)
			{
				string elementName = "Game/BatchControl/SelectionToggleBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				StockpilePriorityToggle stockpilePriorityToggle = this._stockpilePriorityToggleFactory.Create(visualElement);
				stockpilePriorityToggle.Show(component);
				return new StockpilePriorityBatchControlRowItem(visualElement, stockpilePriorityToggle);
			}
			return null;
		}

		// Token: 0x04000008 RID: 8
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		public readonly StockpilePriorityToggleFactory _stockpilePriorityToggleFactory;
	}
}
