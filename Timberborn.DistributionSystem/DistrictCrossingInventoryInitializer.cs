using System;
using Timberborn.Goods;
using Timberborn.InventoryNeedSystem;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.DistributionSystem
{
	// Token: 0x0200000E RID: 14
	public class DistrictCrossingInventoryInitializer : IDedicatedDecoratorInitializer<DistrictCrossingInventory, Inventory>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002D5B File Offset: 0x00000F5B
		public DistrictCrossingInventoryInitializer(IGoodService goodService, InventoryNeedBehaviorInitializer inventoryNeedBehaviorInitializer, InventoryInitializerFactory inventoryInitializerFactory)
		{
			this._goodService = goodService;
			this._inventoryNeedBehaviorInitializer = inventoryNeedBehaviorInitializer;
			this._inventoryInitializerFactory = inventoryInitializerFactory;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D78 File Offset: 0x00000F78
		public void Initialize(DistrictCrossingInventory subject, Inventory decorator)
		{
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.CreateWithUnlimitedCapacity(decorator, DistrictCrossingInventoryInitializer.InventoryComponentName);
			this.AllowEveryGoodAsTakeable(inventoryInitializer);
			inventoryInitializer.HasPublicOutput();
			inventoryInitializer.SetIgnorableCapacity();
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
			this._inventoryNeedBehaviorInitializer.AddNeedBehavior(decorator);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public void AllowEveryGoodAsTakeable(InventoryInitializer inventoryInitializer)
		{
			foreach (string goodId in this._goodService.Goods)
			{
				StorableGood storableGood = StorableGood.CreateAsTakeable(goodId);
				StorableGoodAmount good = new StorableGoodAmount(storableGood, DistrictCrossingInventoryInitializer.DistrictCrossingCapacity);
				inventoryInitializer.AddAllowedGood(good);
			}
		}

		// Token: 0x0400001C RID: 28
		public static readonly string InventoryComponentName = "DistrictCrossing";

		// Token: 0x0400001D RID: 29
		public static readonly int DistrictCrossingCapacity = 30;

		// Token: 0x0400001E RID: 30
		public readonly IGoodService _goodService;

		// Token: 0x0400001F RID: 31
		public readonly InventoryNeedBehaviorInitializer _inventoryNeedBehaviorInitializer;

		// Token: 0x04000020 RID: 32
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;
	}
}
