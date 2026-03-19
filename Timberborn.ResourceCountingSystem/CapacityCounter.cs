using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.InventorySystem;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x02000004 RID: 4
	public class CapacityCounter
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public void UpdateCapacity(ReadOnlyHashSet<Inventory> inventories)
		{
			this._outputCapacity.Clear();
			this._inputOutputCapacity.Clear();
			foreach (Inventory inventory in inventories)
			{
				this.CountInventoryCapacity(inventory);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002128 File Offset: 0x00000328
		public int GetInputOutputCapacity(string goodId)
		{
			return CollectionExtensions.GetValueOrDefault<string, int>(this._inputOutputCapacity, goodId, 0);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002137 File Offset: 0x00000337
		public int GetOutputCapacity(string goodId)
		{
			return CollectionExtensions.GetValueOrDefault<string, int>(this._outputCapacity, goodId, 0);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002148 File Offset: 0x00000348
		public void CountInventoryCapacity(Inventory inventory)
		{
			inventory.GetCapacity(this._temporaryCapacityCache);
			foreach (GoodAmount good in this._temporaryCapacityCache)
			{
				this.CountCapacity(good, inventory);
			}
			this._temporaryCapacityCache.Clear();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021B4 File Offset: 0x000003B4
		public void CountCapacity(GoodAmount good, Inventory inventory)
		{
			if (inventory.Gives(good.GoodId))
			{
				Dictionary<string, int> capacity = inventory.PublicInput ? this._inputOutputCapacity : this._outputCapacity;
				CapacityCounter.CountCapacity(good, capacity);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F0 File Offset: 0x000003F0
		public static void CountCapacity(GoodAmount good, IDictionary<string, int> capacity)
		{
			string goodId = good.GoodId;
			if (capacity.ContainsKey(goodId))
			{
				string key = goodId;
				capacity[key] += good.Amount;
				return;
			}
			capacity[goodId] = good.Amount;
		}

		// Token: 0x04000006 RID: 6
		public readonly Dictionary<string, int> _outputCapacity = new Dictionary<string, int>();

		// Token: 0x04000007 RID: 7
		public readonly Dictionary<string, int> _inputOutputCapacity = new Dictionary<string, int>();

		// Token: 0x04000008 RID: 8
		public readonly List<GoodAmount> _temporaryCapacityCache = new List<GoodAmount>();
	}
}
