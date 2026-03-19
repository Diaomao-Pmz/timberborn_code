using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Wonders
{
	// Token: 0x0200001A RID: 26
	public class WonderInventoryInitializer : IDedicatedDecoratorInitializer<WonderInventory, Inventory>
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000031B5 File Offset: 0x000013B5
		public WonderInventoryInitializer(InventoryInitializerFactory inventoryInitializerFactory)
		{
			this._inventoryInitializerFactory = inventoryInitializerFactory;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000031C4 File Offset: 0x000013C4
		public void Initialize(WonderInventory subject, Inventory decorator)
		{
			List<StorableGoodAmount> list = (from good in subject.GetComponent<WonderInventorySpec>().RequiredGoods
			select new StorableGoodAmount(StorableGood.CreateAsGivable(good.Id), good.Amount)).ToList<StorableGoodAmount>();
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.Create(decorator, list.Sum((StorableGoodAmount g) => g.Amount), WonderInventoryInitializer.InventoryComponentName);
			inventoryInitializer.AddAllowedGoods(list);
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
		}

		// Token: 0x04000042 RID: 66
		public static readonly string InventoryComponentName = "Wonder";

		// Token: 0x04000043 RID: 67
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;

		// Token: 0x04000044 RID: 68
		public readonly Inventory _inventory;
	}
}
