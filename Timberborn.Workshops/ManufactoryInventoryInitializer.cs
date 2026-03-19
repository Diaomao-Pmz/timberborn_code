using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Goods;
using Timberborn.InventoryNeedSystem;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Workshops
{
	// Token: 0x02000016 RID: 22
	public class ManufactoryInventoryInitializer : IDedicatedDecoratorInitializer<Manufactory, Inventory>
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003669 File Offset: 0x00001869
		public ManufactoryInventoryInitializer(InventoryInitializerFactory inventoryInitializerFactory, InventoryNeedBehaviorInitializer inventoryNeedBehaviorInitializer, RecipeGoodsProcessor recipeGoodsProcessor)
		{
			this._inventoryInitializerFactory = inventoryInitializerFactory;
			this._inventoryNeedBehaviorInitializer = inventoryNeedBehaviorInitializer;
			this._recipeGoodsProcessor = recipeGoodsProcessor;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003688 File Offset: 0x00001888
		public void Initialize(Manufactory subject, Inventory decorator)
		{
			ManufactorySpec component = subject.GetComponent<ManufactorySpec>();
			Dictionary<StorableGood, int> dictionary = this._recipeGoodsProcessor.ProcessRecipes(component.ProductionRecipeIds, 1);
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.Create(decorator, dictionary.Values.Sum(), ManufactoryInventoryInitializer.InventoryComponentName);
			inventoryInitializer.AddAllowedGoods(from pair in dictionary
			select new StorableGoodAmount(pair.Key, pair.Value));
			RecipeGoodDisallower component2 = subject.GetComponent<RecipeGoodDisallower>();
			inventoryInitializer.AddGoodDisallower(component2);
			inventoryInitializer.HasPublicOutput();
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
			this._inventoryNeedBehaviorInitializer.AddNeedBehavior(decorator);
		}

		// Token: 0x04000046 RID: 70
		public static readonly string InventoryComponentName = "Manufactory";

		// Token: 0x04000047 RID: 71
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;

		// Token: 0x04000048 RID: 72
		public readonly InventoryNeedBehaviorInitializer _inventoryNeedBehaviorInitializer;

		// Token: 0x04000049 RID: 73
		public readonly RecipeGoodsProcessor _recipeGoodsProcessor;
	}
}
