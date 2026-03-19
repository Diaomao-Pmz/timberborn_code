using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;
using UnityEngine;

namespace Timberborn.GoodConsumingBuildingSystem
{
	// Token: 0x0200000A RID: 10
	public class GoodConsumingBuildingInventoryInitializer : IDedicatedDecoratorInitializer<GoodConsumingBuilding, Inventory>
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000289A File Offset: 0x00000A9A
		public GoodConsumingBuildingInventoryInitializer(InventoryInitializerFactory inventoryInitializerFactory)
		{
			this._inventoryInitializerFactory = inventoryInitializerFactory;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028AC File Offset: 0x00000AAC
		public void Initialize(GoodConsumingBuilding subject, Inventory decorator)
		{
			GoodConsumingBuildingSpec component = subject.GetComponent<GoodConsumingBuildingSpec>();
			List<StorableGoodAmount> list = new List<StorableGoodAmount>();
			foreach (ConsumedGoodSpec consumedGoodSpec in component.ConsumedGoods)
			{
				StorableGood storableGood = StorableGood.CreateAsGivable(consumedGoodSpec.GoodId);
				int amount = Mathf.CeilToInt(consumedGoodSpec.GoodPerHour * (float)component.FullInventoryWorkHours);
				list.Add(new StorableGoodAmount(storableGood, amount));
			}
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.Create(decorator, list.Sum((StorableGoodAmount good) => good.Amount), GoodConsumingBuildingInventoryInitializer.InventoryComponentName);
			inventoryInitializer.AddAllowedGoods(list);
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
		}

		// Token: 0x04000019 RID: 25
		public static readonly string InventoryComponentName = "GoodConsumingBuilding";

		// Token: 0x0400001A RID: 26
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;
	}
}
