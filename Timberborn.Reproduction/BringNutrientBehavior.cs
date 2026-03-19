using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.WorkSystem;

namespace Timberborn.Reproduction
{
	// Token: 0x0200000C RID: 12
	public class BringNutrientBehavior : CommunityServiceBehavior, IAwakableComponent
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void Awake()
		{
			this._citizen = base.GetComponent<Citizen>();
			this._carrierInventoryFinder = base.GetComponent<CarrierInventoryFinder>();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B04 File Offset: 0x00000D04
		public override Decision Decide(BehaviorAgent agent)
		{
			DistrictCenter assignedDistrict = this._citizen.AssignedDistrict;
			if (assignedDistrict)
			{
				DistrictBreedingPodService component = assignedDistrict.GetComponent<DistrictBreedingPodService>();
				BreedingPod breedingPod;
				while (component.TryDequeueNeedingNutrients(out breedingPod))
				{
					if (breedingPod && this.StartCarrying(breedingPod))
					{
						return Decision.ReleaseNextTick();
					}
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B54 File Offset: 0x00000D54
		public bool StartCarrying(BreedingPod breedingPod)
		{
			foreach (GoodAmountSpec goodAmountSpec in breedingPod.NutrientsPerCycle)
			{
				if (this._carrierInventoryFinder.TryCarryFromAnyInventory(goodAmountSpec.Id, breedingPod.Inventory))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000020 RID: 32
		public Citizen _citizen;

		// Token: 0x04000021 RID: 33
		public CarrierInventoryFinder _carrierInventoryFinder;
	}
}
