using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Reproduction
{
	// Token: 0x02000009 RID: 9
	public class BreedingPodInventoryInitializer : IDedicatedDecoratorInitializer<BreedingPod, Inventory>
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002653 File Offset: 0x00000853
		public BreedingPodInventoryInitializer(InventoryInitializerFactory inventoryInitializerFactory)
		{
			this._inventoryInitializerFactory = inventoryInitializerFactory;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002664 File Offset: 0x00000864
		public void Initialize(BreedingPod subject, Inventory decorator)
		{
			List<StorableGoodAmount> list = new List<StorableGoodAmount>();
			BreedingPodSpec component = subject.GetComponent<BreedingPodSpec>();
			foreach (GoodAmountSpec goodAmountSpec in component.NutrientsPerCycle)
			{
				StorableGood storableGood = StorableGood.CreateAsGivable(goodAmountSpec.Id);
				list.Add(new StorableGoodAmount(storableGood, goodAmountSpec.Amount * component.CyclesCapacity));
			}
			InventoryInitializer inventoryInitializer = this._inventoryInitializerFactory.Create(decorator, list.Sum((StorableGoodAmount good) => good.Amount), BreedingPodInventoryInitializer.InventoryComponentName);
			inventoryInitializer.AddAllowedGoods(list);
			inventoryInitializer.Initialize();
			subject.InitializeInventory(decorator);
		}

		// Token: 0x04000016 RID: 22
		public static readonly string InventoryComponentName = "BreedingPod";

		// Token: 0x04000017 RID: 23
		public readonly InventoryInitializerFactory _inventoryInitializerFactory;
	}
}
