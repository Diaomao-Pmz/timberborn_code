using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.Goods;
using Timberborn.WorkSystem;

namespace Timberborn.Reproduction
{
	// Token: 0x0200000E RID: 14
	public class BringNutrientWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002C34 File Offset: 0x00000E34
		public void Awake()
		{
			this._breedingPod = base.GetComponent<BreedingPod>();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C42 File Offset: 0x00000E42
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this._breedingPod.NeedsNutrients && this.StartCarrying(agent))
			{
				return Decision.ReleaseNextTick();
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C68 File Offset: 0x00000E68
		public bool StartCarrying(BehaviorAgent agent)
		{
			CarrierInventoryFinder component = agent.GetComponent<CarrierInventoryFinder>();
			foreach (GoodAmountSpec goodAmountSpec in this._breedingPod.NutrientsPerCycle)
			{
				if (component.TryCarryFromAnyInventory(goodAmountSpec.Id, this._breedingPod.Inventory))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000026 RID: 38
		public BreedingPod _breedingPod;
	}
}
