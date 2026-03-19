using System;
using Timberborn.InventorySystem;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemBatchControl
{
	// Token: 0x02000004 RID: 4
	public class InventoryCapacityBatchControlGood
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public InventoryCapacityBatchControlGood(Label capacityAmount, Inventory inventory, string goodId)
		{
			this._capacityAmount = capacityAmount;
			this._inventory = inventory;
			this._goodId = goodId;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
		public void UpdateGoodAmount()
		{
			this._capacityAmount.text = this._inventory.AmountInStock(this._goodId).ToString();
		}

		// Token: 0x04000006 RID: 6
		public readonly Label _capacityAmount;

		// Token: 0x04000007 RID: 7
		public readonly Inventory _inventory;

		// Token: 0x04000008 RID: 8
		public readonly string _goodId;
	}
}
