using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.GameDistricts;
using Timberborn.InventorySystem;
using Timberborn.LaborSystem;

namespace Timberborn.Emptying
{
	// Token: 0x0200000E RID: 14
	public class RemoveUnwantedStockLaborBehavior : LaborBehavior, IAwakableComponent
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002B24 File Offset: 0x00000D24
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B34 File Offset: 0x00000D34
		public override Decision Decide(BehaviorAgent agent)
		{
			DistrictCenter district = this._districtBuilding.District;
			if (district)
			{
				DistrictUnwantedStockInventoryRegistry component = district.GetComponent<DistrictUnwantedStockInventoryRegistry>();
				EmptyingStarter component2 = agent.GetComponent<EmptyingStarter>();
				foreach (Inventory inventory in component.InventoriesWithUnwantedStock)
				{
					if (inventory && component2.StartEmptyingUnwantedStock(inventory))
					{
						return Decision.ReleaseNextTick();
					}
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0400001E RID: 30
		public DistrictBuilding _districtBuilding;
	}
}
