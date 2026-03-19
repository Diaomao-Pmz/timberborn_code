using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Debugging;
using Timberborn.EntitySystem;
using Timberborn.GoodConsumingBuildingSystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Reproduction;
using Timberborn.Workshops;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x0200000E RID: 14
	public class InventoryFillerDevModule : IDevModule
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002A29 File Offset: 0x00000C29
		public InventoryFillerDevModule(EntityComponentRegistry entityComponentRegistry)
		{
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A38 File Offset: 0x00000C38
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Fill input inventories", new Action(this.FillInventories))).Build();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A60 File Offset: 0x00000C60
		public void FillInventories()
		{
			this.FillInventories(from manufactory in this._entityComponentRegistry.GetEnabled<Manufactory>()
			select manufactory.Inventory);
			this.FillInventories(from manufactory in this._entityComponentRegistry.GetEnabled<GoodConsumingBuilding>()
			select manufactory.Inventory);
			this.FillInventories(from manufactory in this._entityComponentRegistry.GetEnabled<BreedingPod>()
			select manufactory.Inventory);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B0C File Offset: 0x00000D0C
		public void FillInventories(IEnumerable<Inventory> inventories)
		{
			foreach (Inventory inventory in inventories)
			{
				this.FillInventory(inventory);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B54 File Offset: 0x00000D54
		public void FillInventory(Inventory inventory)
		{
			foreach (string goodId in inventory.InputGoods)
			{
				InventoryFillerDevModule.GiveGood(goodId, inventory);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public static void GiveGood(string goodId, Inventory inventory)
		{
			int num = inventory.UnreservedCapacity(goodId);
			if (num > 0)
			{
				inventory.Give(new GoodAmount(goodId, num));
			}
		}

		// Token: 0x04000031 RID: 49
		public readonly EntityComponentRegistry _entityComponentRegistry;
	}
}
