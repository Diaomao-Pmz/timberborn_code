using System;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Stockpiles
{
	// Token: 0x0200000A RID: 10
	public class StockpileInventoryInitializer : IDedicatedDecoratorInitializer<Stockpile, Inventory>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000022 RID: 34 RVA: 0x00002300 File Offset: 0x00000500
		// (remove) Token: 0x06000023 RID: 35 RVA: 0x00002338 File Offset: 0x00000538
		public event EventHandler<Inventory> InventoryInitialized;

		// Token: 0x06000024 RID: 36 RVA: 0x0000236D File Offset: 0x0000056D
		public StockpileInventoryInitializer(InventoryInitializerFactory inventoryInitializerFactory)
		{
			this._inventoryInitializerFactory = inventoryInitializerFactory;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000237C File Offset: 0x0000057C
		public void Initialize(Stockpile subject, Inventory decorator)
		{
			StockpileSpec component = subject.GetComponent<StockpileSpec>();
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.Create(decorator, component.MaxCapacity, StockpileInventoryInitializer.InventoryComponentName);
			inventoryInitializer.AddAllowedGoodType(component.WhitelistedGoodType);
			inventoryInitializer.HasPublicOutput();
			inventoryInitializer.HasPublicInput();
			SingleGoodAllower component2 = subject.GetComponent<SingleGoodAllower>();
			component2.Initialize(decorator);
			inventoryInitializer.AddGoodDisallower(component2);
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
			EventHandler<Inventory> inventoryInitialized = this.InventoryInitialized;
			if (inventoryInitialized == null)
			{
				return;
			}
			inventoryInitialized(this, decorator);
		}

		// Token: 0x0400000E RID: 14
		public static readonly string InventoryComponentName = "Stockpile";

		// Token: 0x04000010 RID: 16
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;
	}
}
