using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.GameDistricts;
using Timberborn.InventorySystem;
using Timberborn.LaborSystem;

namespace Timberborn.Emptying
{
	// Token: 0x0200000B RID: 11
	public class EmptyInventoriesLaborBehavior : LaborBehavior, IAwakableComponent
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000028F5 File Offset: 0x00000AF5
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002904 File Offset: 0x00000B04
		public override Decision Decide(BehaviorAgent agent)
		{
			DistrictCenter district = this._districtBuilding.District;
			if (district)
			{
				DistrictEmptiableInventoriesRegistry component = district.GetComponent<DistrictEmptiableInventoriesRegistry>();
				EmptyingStarter component2 = agent.GetComponent<EmptyingStarter>();
				foreach (Inventories inventories in component.EmptiableInventories)
				{
					foreach (Inventory inventory in inventories.EnabledInventories)
					{
						if (inventory && component2.StartEmptying(inventory))
						{
							return Decision.ReleaseNextTick();
						}
					}
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0400001A RID: 26
		public DistrictBuilding _districtBuilding;
	}
}
