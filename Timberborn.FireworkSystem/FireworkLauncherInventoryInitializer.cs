using System;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.FireworkSystem
{
	// Token: 0x02000009 RID: 9
	public class FireworkLauncherInventoryInitializer : IDedicatedDecoratorInitializer<FireworkLauncher, Inventory>
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public FireworkLauncherInventoryInitializer(InventoryInitializerFactory inventoryInitializerFactory)
		{
			this._inventoryInitializerFactory = inventoryInitializerFactory;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public void Initialize(FireworkLauncher subject, Inventory decorator)
		{
			FireworkLauncherSpec component = subject.GetComponent<FireworkLauncherSpec>();
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.Create(decorator, component.GoodAmount, FireworkLauncherInventoryInitializer.InventoryComponentName);
			StorableGood storableGood = StorableGood.CreateAsGivable(component.GoodId);
			inventoryInitializer.AddAllowedGood(new StorableGoodAmount(storableGood, component.GoodAmount));
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
		}

		// Token: 0x0400003B RID: 59
		public static readonly string InventoryComponentName = "FireworkLauncher";

		// Token: 0x0400003C RID: 60
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;
	}
}
