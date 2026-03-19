using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Buildings;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000014 RID: 20
	public class ConstructionSiteInventoryInitializer : IDedicatedDecoratorInitializer<ConstructionSite, Inventory>
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003670 File Offset: 0x00001870
		public ConstructionSiteInventoryInitializer(IGoodService goodService, InventoryInitializerFactory inventoryInitializerFactory)
		{
			this._goodService = goodService;
			this._inventoryInitializerFactory = inventoryInitializerFactory;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003688 File Offset: 0x00001888
		public void Initialize(ConstructionSite subject, Inventory decorator)
		{
			BuildingSpec component = subject.GetComponent<BuildingSpec>();
			this.ValidateCostGoods(component);
			List<StorableGoodAmount> list = (from requiredGood in component.BuildingCost
			select new StorableGoodAmount(StorableGood.CreateAsGivable(requiredGood.Id), requiredGood.Amount)).ToList<StorableGoodAmount>();
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.Create(decorator, list.Sum((StorableGoodAmount good) => good.Amount), ConstructionSiteInventoryInitializer.InventoryComponentName);
			inventoryInitializer.AddAllowedGoods(list);
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000371C File Offset: 0x0000191C
		public void ValidateCostGoods(BuildingSpec buildingSpec)
		{
			foreach (GoodAmountSpec goodAmountSpec in buildingSpec.BuildingCost)
			{
				if (!this._goodService.Goods.Contains(goodAmountSpec.Id))
				{
					throw new InvalidOperationException(string.Concat(new string[]
					{
						"Cost good ",
						goodAmountSpec.Id,
						" for building ",
						buildingSpec.Blueprint.Name,
						" does not exist"
					}));
				}
			}
		}

		// Token: 0x0400004A RID: 74
		public static readonly string InventoryComponentName = "ConstructionSite";

		// Token: 0x0400004B RID: 75
		public readonly IGoodService _goodService;

		// Token: 0x0400004C RID: 76
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;
	}
}
