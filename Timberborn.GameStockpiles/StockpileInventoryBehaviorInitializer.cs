using System;
using Timberborn.InventoryNeedSystem;
using Timberborn.InventorySystem;
using Timberborn.SingletonSystem;
using Timberborn.Stockpiles;

namespace Timberborn.GameStockpiles
{
	// Token: 0x02000007 RID: 7
	public class StockpileInventoryBehaviorInitializer : ILoadableSingleton
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002184 File Offset: 0x00000384
		public StockpileInventoryBehaviorInitializer(InventoryNeedBehaviorInitializer inventoryNeedBehaviorInitializer, StockpileInventoryInitializer stockpileInventoryInitializer)
		{
			this._inventoryNeedBehaviorInitializer = inventoryNeedBehaviorInitializer;
			this._stockpileInventoryInitializer = stockpileInventoryInitializer;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000219A File Offset: 0x0000039A
		public void Load()
		{
			this._stockpileInventoryInitializer.InventoryInitialized += this.OnInventoryInitialized;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B3 File Offset: 0x000003B3
		public void OnInventoryInitialized(object sender, Inventory inventory)
		{
			this._inventoryNeedBehaviorInitializer.AddNeedBehavior(inventory);
		}

		// Token: 0x04000008 RID: 8
		public readonly InventoryNeedBehaviorInitializer _inventoryNeedBehaviorInitializer;

		// Token: 0x04000009 RID: 9
		public readonly StockpileInventoryInitializer _stockpileInventoryInitializer;
	}
}
