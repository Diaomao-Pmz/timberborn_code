using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.InventorySystem;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x0200000D RID: 13
	public class StockCounter
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public void UpdateStock(ReadOnlyHashSet<Inventory> inventories)
		{
			this._outputStock.Clear();
			this._inputOutputStock.Clear();
			foreach (Inventory inventory in inventories)
			{
				this.CountInventoryStock(inventory);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B58 File Offset: 0x00000D58
		public int GetInputOutputStock(string goodId)
		{
			return CollectionExtensions.GetValueOrDefault<string, int>(this._inputOutputStock, goodId, 0);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B67 File Offset: 0x00000D67
		public int GetOutputStock(string goodId)
		{
			return CollectionExtensions.GetValueOrDefault<string, int>(this._outputStock, goodId, 0);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B78 File Offset: 0x00000D78
		public void CountInventoryStock(Inventory inventory)
		{
			foreach (GoodAmount good in inventory.Stock)
			{
				this.CountStock(good, inventory);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public void CountStock(GoodAmount good, Inventory inventory)
		{
			if (inventory.Gives(good.GoodId))
			{
				Dictionary<string, int> stock = inventory.PublicInput ? this._inputOutputStock : this._outputStock;
				StockCounter.CountStock(good, stock);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static void CountStock(GoodAmount good, IDictionary<string, int> stock)
		{
			string goodId = good.GoodId;
			if (stock.ContainsKey(goodId))
			{
				string key = goodId;
				stock[key] += good.Amount;
				return;
			}
			stock[goodId] = good.Amount;
		}

		// Token: 0x04000025 RID: 37
		public readonly Dictionary<string, int> _outputStock = new Dictionary<string, int>();

		// Token: 0x04000026 RID: 38
		public readonly Dictionary<string, int> _inputOutputStock = new Dictionary<string, int>();
	}
}
