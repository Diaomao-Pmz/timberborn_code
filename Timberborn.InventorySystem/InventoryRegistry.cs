using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Goods;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200001B RID: 27
	public class InventoryRegistry
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00004360 File Offset: 0x00002560
		public InventoryRegistry(ReadOnlyList<string> goods)
		{
			foreach (string key in goods)
			{
				this._inventoriesWithCapacity[key] = new HashSet<Inventory>();
				this._inventoriesWithStock[key] = new HashSet<Inventory>();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000043F4 File Offset: 0x000025F4
		public ReadOnlyHashSet<Inventory> Inventories
		{
			get
			{
				return this._inventories.AsReadOnlyHashSet<Inventory>();
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004401 File Offset: 0x00002601
		public ReadOnlyHashSet<Inventory> InventoriesWithCapacity(string goodId)
		{
			return this._inventoriesWithCapacity[goodId].AsReadOnlyHashSet<Inventory>();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004414 File Offset: 0x00002614
		public ReadOnlyHashSet<Inventory> InventoriesWithStock(string goodId)
		{
			return this._inventoriesWithStock[goodId].AsReadOnlyHashSet<Inventory>();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004428 File Offset: 0x00002628
		public void RegisterInventory(Inventory inventory)
		{
			this._inventories.Add(inventory);
			string text;
			HashSet<Inventory> hashSet;
			if (inventory.PublicInput)
			{
				foreach (KeyValuePair<string, HashSet<Inventory>> keyValuePair in this._inventoriesWithCapacity)
				{
					keyValuePair.Deconstruct(ref text, ref hashSet);
					string goodId = text;
					HashSet<Inventory> hashSet2 = hashSet;
					if (inventory.HasUnreservedCapacity(goodId))
					{
						hashSet2.Add(inventory);
					}
				}
			}
			if (inventory.PublicOutput)
			{
				foreach (KeyValuePair<string, HashSet<Inventory>> keyValuePair in this._inventoriesWithStock)
				{
					keyValuePair.Deconstruct(ref text, ref hashSet);
					string goodId2 = text;
					HashSet<Inventory> hashSet3 = hashSet;
					if (inventory.HasUnreservedStock(goodId2))
					{
						hashSet3.Add(inventory);
					}
				}
			}
			if (inventory.PublicInput || inventory.PublicOutput)
			{
				inventory.InventoryChanged += this.OnInventoryChanged;
				inventory.UnwantedStockDisappeared += this.OnUnwantedStockDisappeared;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004548 File Offset: 0x00002748
		public void UnregisterInventory(Inventory inventory)
		{
			this._inventories.Remove(inventory);
			if (inventory.PublicInput)
			{
				foreach (HashSet<Inventory> hashSet in this._inventoriesWithCapacity.Values)
				{
					hashSet.Remove(inventory);
				}
			}
			if (inventory.PublicOutput)
			{
				foreach (HashSet<Inventory> hashSet2 in this._inventoriesWithStock.Values)
				{
					hashSet2.Remove(inventory);
				}
			}
			if (inventory.PublicInput || inventory.PublicOutput)
			{
				inventory.InventoryChanged -= this.OnInventoryChanged;
				inventory.UnwantedStockDisappeared -= this.OnUnwantedStockDisappeared;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004638 File Offset: 0x00002838
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.UpdateRegistries((Inventory)sender, e.GoodId);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004650 File Offset: 0x00002850
		public void OnUnwantedStockDisappeared(object sender, EventArgs e)
		{
			Inventory inventory = (Inventory)sender;
			foreach (StorableGoodAmount storableGoodAmount in inventory.AllowedGoods)
			{
				this.UpdateRegistries(inventory, storableGoodAmount.StorableGood.GoodId);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000046C0 File Offset: 0x000028C0
		public void UpdateRegistries(Inventory inventory, string good)
		{
			if (inventory.PublicInput)
			{
				HashSet<Inventory> hashSet = this._inventoriesWithCapacity[good];
				if (inventory.HasUnreservedCapacity(good))
				{
					hashSet.Add(inventory);
				}
				else
				{
					hashSet.Remove(inventory);
				}
			}
			if (inventory.PublicOutput)
			{
				HashSet<Inventory> hashSet2 = this._inventoriesWithStock[good];
				if (inventory.HasUnreservedStock(good))
				{
					hashSet2.Add(inventory);
					return;
				}
				hashSet2.Remove(inventory);
			}
		}

		// Token: 0x04000054 RID: 84
		public readonly Dictionary<string, HashSet<Inventory>> _inventoriesWithCapacity = new Dictionary<string, HashSet<Inventory>>();

		// Token: 0x04000055 RID: 85
		public readonly Dictionary<string, HashSet<Inventory>> _inventoriesWithStock = new Dictionary<string, HashSet<Inventory>>();

		// Token: 0x04000056 RID: 86
		public readonly HashSet<Inventory> _inventories = new HashSet<Inventory>();
	}
}
