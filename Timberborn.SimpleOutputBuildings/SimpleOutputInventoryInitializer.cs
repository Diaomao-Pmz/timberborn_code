using System;
using Timberborn.Goods;
using Timberborn.InventoryNeedSystem;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.SimpleOutputBuildings
{
	// Token: 0x0200000B RID: 11
	public class SimpleOutputInventoryInitializer : IDedicatedDecoratorInitializer<SimpleOutputInventory, Inventory>
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002257 File Offset: 0x00000457
		public SimpleOutputInventoryInitializer(IGoodService goodService, InventoryNeedBehaviorInitializer inventoryNeedBehaviorInitializer, InventoryInitializerFactory inventoryInitializerFactory)
		{
			this._goodService = goodService;
			this._inventoryNeedBehaviorInitializer = inventoryNeedBehaviorInitializer;
			this._inventoryInitializerFactory = inventoryInitializerFactory;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002274 File Offset: 0x00000474
		public void Initialize(SimpleOutputInventory subject, Inventory decorator)
		{
			IAllowedGoodProvider component = subject.GetComponent<IAllowedGoodProvider>();
			SimpleOutputInventorySpec component2 = subject.GetComponent<SimpleOutputInventorySpec>();
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.CreateWithUnlimitedCapacity(decorator, SimpleOutputInventoryInitializer.InventoryComponentName);
			inventoryInitializer.HasPublicOutput();
			SimpleOutputInventoryInitializer.AddGoodDisallower(subject, inventoryInitializer);
			if (component2.IgnorableCapacity)
			{
				inventoryInitializer.SetIgnorableCapacity();
			}
			this.AllowGoodsAsTakeable(inventoryInitializer, component2.Capacity, component);
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
			this._inventoryNeedBehaviorInitializer.AddNeedBehavior(decorator);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022E4 File Offset: 0x000004E4
		public static void AddGoodDisallower(SimpleOutputInventory subject, InventoryInitializer inventoryInitializer)
		{
			IGoodDisallower component = subject.GetComponent<IGoodDisallower>();
			inventoryInitializer.AddGoodDisallower(component);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002300 File Offset: 0x00000500
		public void AllowGoodsAsTakeable(InventoryInitializer inventoryInitializer, int allowedAmount, IAllowedGoodProvider provider)
		{
			foreach (string goodId in (((provider != null) ? provider.GetAllowedGoods() : null) ?? this._goodService.Goods))
			{
				StorableGood storableGood = StorableGood.CreateAsTakeable(goodId);
				StorableGoodAmount good = new StorableGoodAmount(storableGood, allowedAmount);
				inventoryInitializer.AddAllowedGood(good);
			}
		}

		// Token: 0x0400000E RID: 14
		public static readonly string InventoryComponentName = "SimpleOutputInventory";

		// Token: 0x0400000F RID: 15
		public readonly IGoodService _goodService;

		// Token: 0x04000010 RID: 16
		public readonly InventoryNeedBehaviorInitializer _inventoryNeedBehaviorInitializer;

		// Token: 0x04000011 RID: 17
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;
	}
}
