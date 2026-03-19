using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.InventorySystem;
using Timberborn.WorkSystem;

namespace Timberborn.Emptying
{
	// Token: 0x0200000C RID: 12
	public class EmptyInventoriesWorkplaceBehavior : WorkplaceBehavior, IStartableComponent
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000029E4 File Offset: 0x00000BE4
		public void Start()
		{
			this._inventories = base.GetComponent<Inventories>();
			this._emptiable = base.GetComponent<Emptiable>();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A00 File Offset: 0x00000C00
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this._emptiable.IsMarkedForEmptying)
			{
				EmptyingStarter component = agent.GetComponent<EmptyingStarter>();
				foreach (Inventory inventory in this._inventories.EnabledInventories)
				{
					if (component.StartEmptying(inventory))
					{
						return Decision.ReleaseNextTick();
					}
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0400001B RID: 27
		public Inventories _inventories;

		// Token: 0x0400001C RID: 28
		public Emptiable _emptiable;
	}
}
