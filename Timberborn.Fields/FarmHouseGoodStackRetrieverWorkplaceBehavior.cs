using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Buildings;
using Timberborn.GoodStackSystem;
using Timberborn.InventorySystem;
using Timberborn.Navigation;
using Timberborn.SimpleOutputBuildings;
using Timberborn.WorkSystem;

namespace Timberborn.Fields
{
	// Token: 0x0200000A RID: 10
	public class FarmHouseGoodStackRetrieverWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000238C File Offset: 0x0000058C
		public FarmHouseGoodStackRetrieverWorkplaceBehavior(GoodStackService<FarmHouse> goodStackService)
		{
			this._goodStackService = goodStackService;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000239B File Offset: 0x0000059B
		public void Awake()
		{
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
			this._inventory = base.GetComponent<SimpleOutputInventory>().Inventory;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023BC File Offset: 0x000005BC
		public override Decision Decide(BehaviorAgent agent)
		{
			Accessible accessible = this._buildingAccessible.Accessible;
			GoodStackRetrieverBehavior component = agent.GetComponent<GoodStackRetrieverBehavior>();
			Decision decision = component.StartRetrieving(this._goodStackService, accessible, this._inventory);
			if (!decision.ShouldReleaseNow)
			{
				return Decision.TransferNow(component, decision);
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0400000E RID: 14
		public readonly GoodStackService<FarmHouse> _goodStackService;

		// Token: 0x0400000F RID: 15
		public BuildingAccessible _buildingAccessible;

		// Token: 0x04000010 RID: 16
		public Inventory _inventory;
	}
}
