using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.InventorySystem;
using Timberborn.Localization;
using Timberborn.Stockpiles;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000012 RID: 18
	public class StockpileBatchControlRowItemFactory
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002EC0 File Offset: 0x000010C0
		public StockpileBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, GoodDescriber goodDescriber, ILoc loc, DropdownItemsSetter dropdownItemsSetter)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._goodDescriber = goodDescriber;
			this._loc = loc;
			this._dropdownItemsSetter = dropdownItemsSetter;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F10 File Offset: 0x00001110
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Stockpile component = entity.GetComponent<Stockpile>();
			if (component != null)
			{
				string elementName = "Game/BatchControl/StockpileBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Inventory inventory = component.Inventory;
				SingleGoodAllower component2 = component.GetComponent<SingleGoodAllower>();
				UQueryExtensions.Q<Label>(visualElement, "CapacityLimit", null).text = inventory.Capacity.ToString();
				this._tooltipRegistrar.RegisterUpdatable(UQueryExtensions.Q<VisualElement>(visualElement, "CapacityWrapper", null), () => this.GetTooltipText(inventory));
				StockpileDropdownProvider component3 = component.GetComponent<StockpileDropdownProvider>();
				Dropdown dropdown = UQueryExtensions.Q<Dropdown>(visualElement, "GoodSelectionButton", null);
				this._dropdownItemsSetter.SetItems(dropdown, component3);
				StockpileBatchControlRowItem stockpileBatchControlRowItem = new StockpileBatchControlRowItem(visualElement, inventory, component2, UQueryExtensions.Q<Label>(visualElement, "CapacityAmount", null), dropdown, UQueryExtensions.Q<VisualElement>(visualElement, "Fill", null));
				stockpileBatchControlRowItem.Initialize();
				return stockpileBatchControlRowItem;
			}
			return null;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002FFC File Offset: 0x000011FC
		public string GetTooltipText(Inventory inventory)
		{
			this._tooltipText.Clear();
			this._tooltipText.AppendLine("<b>" + this._loc.T(StockpileBatchControlRowItemFactory.InStockLocKey) + "</b>");
			IOrderedEnumerable<string> collection = from good in inventory.Stock
			select this._goodDescriber.Describe(good) into info
			orderby info
			select info;
			this._orderedItems.AddRange(collection);
			if (this._orderedItems.Count > 0)
			{
				foreach (string value in this._orderedItems)
				{
					this._tooltipText.AppendLine(value);
				}
				this._orderedItems.Clear();
				return this._tooltipText.ToStringWithoutNewLineEnd();
			}
			return this._loc.T(StockpileBatchControlRowItemFactory.EmptyLocKey);
		}

		// Token: 0x0400003E RID: 62
		public static readonly string InStockLocKey = "Inventory.InStock";

		// Token: 0x0400003F RID: 63
		public static readonly string EmptyLocKey = "Inventory.IsEmpty";

		// Token: 0x04000040 RID: 64
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000041 RID: 65
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000042 RID: 66
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000043 RID: 67
		public readonly ILoc _loc;

		// Token: 0x04000044 RID: 68
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000045 RID: 69
		public readonly StringBuilder _tooltipText = new StringBuilder();

		// Token: 0x04000046 RID: 70
		public readonly List<string> _orderedItems = new List<string>();
	}
}
