using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.InventorySystem;
using Timberborn.WorkSystem;

namespace Timberborn.Emptying
{
	// Token: 0x0200000F RID: 15
	public class RemoveUnwantedStockWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public void Awake()
		{
			this._inventories = base.GetComponent<Inventories>();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public override Decision Decide(BehaviorAgent agent)
		{
			EmptyingStarter component = agent.GetComponent<EmptyingStarter>();
			foreach (Inventory inventory in this._inventories.EnabledInventories)
			{
				if (inventory.HasUnwantedStock && component.StartEmptyingUnwantedStock(inventory))
				{
					return Decision.ReleaseNextTick();
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0400001F RID: 31
		public Inventories _inventories;
	}
}
