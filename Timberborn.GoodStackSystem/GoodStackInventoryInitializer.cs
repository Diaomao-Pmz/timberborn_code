using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x02000009 RID: 9
	public class GoodStackInventoryInitializer : IDedicatedDecoratorInitializer<IGoodStackInventory, Inventory>
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000233E File Offset: 0x0000053E
		public GoodStackInventoryInitializer(IGoodService goodService, InventoryInitializerFactory inventoryInitializerFactory)
		{
			this._goodService = goodService;
			this._inventoryInitializerFactory = inventoryInitializerFactory;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002354 File Offset: 0x00000554
		public void Initialize(IGoodStackInventory subject, Inventory decorator)
		{
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.CreateWithUnlimitedCapacity(decorator, GoodStackInventoryInitializer.InventoryComponentName);
			IEnumerable<StorableGoodAmount> goods = from storableGood in this._goodService.Goods.Select(new Func<string, StorableGood>(StorableGood.CreateAsTakeable))
			select new StorableGoodAmount(storableGood, int.MaxValue);
			inventoryInitializer.AddAllowedGoods(goods);
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
		}

		// Token: 0x0400000F RID: 15
		public static readonly string InventoryComponentName = "GoodStack";

		// Token: 0x04000010 RID: 16
		public readonly IGoodService _goodService;

		// Token: 0x04000011 RID: 17
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;
	}
}
