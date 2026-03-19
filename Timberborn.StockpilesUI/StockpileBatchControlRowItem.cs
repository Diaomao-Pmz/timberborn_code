using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.InventorySystem;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000011 RID: 17
	public class StockpileBatchControlRowItem : IBatchControlRowItem, IClearableBatchControlRowItem
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002DBF File Offset: 0x00000FBF
		public VisualElement Root { get; }

		// Token: 0x0600004C RID: 76 RVA: 0x00002DC7 File Offset: 0x00000FC7
		public StockpileBatchControlRowItem(VisualElement root, Inventory inventory, SingleGoodAllower singleGoodAllower, Label capacityAmount, Dropdown dropdown, VisualElement fillGauge)
		{
			this.Root = root;
			this._inventory = inventory;
			this._singleGoodAllower = singleGoodAllower;
			this._capacityAmount = capacityAmount;
			this._dropdown = dropdown;
			this._fillGauge = fillGauge;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002DFC File Offset: 0x00000FFC
		public void Initialize()
		{
			this._inventory.InventoryChanged += this.OnInventoryChanged;
			this._singleGoodAllower.DisallowedGoodsChanged += this.OnDisallowedGoodsChanged;
			this.UpdateAmounts();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E32 File Offset: 0x00001032
		public void ClearRowItem()
		{
			this._inventory.InventoryChanged -= this.OnInventoryChanged;
			this._singleGoodAllower.DisallowedGoodsChanged -= this.OnDisallowedGoodsChanged;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E62 File Offset: 0x00001062
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.UpdateAmounts();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E6A File Offset: 0x0000106A
		public void OnDisallowedGoodsChanged(object sender, DisallowedGoodsChangedEventArgs e)
		{
			this._dropdown.UpdateSelectedValue();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E78 File Offset: 0x00001078
		public void UpdateAmounts()
		{
			int totalAmountInStock = this._inventory.TotalAmountInStock;
			this._capacityAmount.text = totalAmountInStock.ToString();
			this._fillGauge.SetHeightAsPercent((float)totalAmountInStock / (float)this._inventory.Capacity);
		}

		// Token: 0x04000039 RID: 57
		public readonly Inventory _inventory;

		// Token: 0x0400003A RID: 58
		public readonly SingleGoodAllower _singleGoodAllower;

		// Token: 0x0400003B RID: 59
		public readonly Dropdown _dropdown;

		// Token: 0x0400003C RID: 60
		public readonly Label _capacityAmount;

		// Token: 0x0400003D RID: 61
		public readonly VisualElement _fillGauge;
	}
}
