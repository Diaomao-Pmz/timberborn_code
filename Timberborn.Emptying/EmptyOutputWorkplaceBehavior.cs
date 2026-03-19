using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.InventorySystem;
using Timberborn.WorkSystem;

namespace Timberborn.Emptying
{
	// Token: 0x0200000D RID: 13
	public class EmptyOutputWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002A8C File Offset: 0x00000C8C
		public void Awake()
		{
			this._inventories = base.GetComponent<Inventories>();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A9C File Offset: 0x00000C9C
		public override Decision Decide(BehaviorAgent agent)
		{
			EmptyingStarter component = agent.GetComponent<EmptyingStarter>();
			foreach (Inventory inventory in this._inventories.EnabledInventories)
			{
				if (inventory.OutputGoods.Count > 0 && component.StartEmptying(inventory))
				{
					return Decision.ReleaseNextTick();
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0400001D RID: 29
		public Inventories _inventories;
	}
}
