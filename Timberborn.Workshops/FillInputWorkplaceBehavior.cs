using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.Emptying;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.WorkSystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200000B RID: 11
	public class FillInputWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002368 File Offset: 0x00000568
		public void Awake()
		{
			this._inventories = base.GetComponent<Inventories>();
			this._emptiable = base.GetComponent<Emptiable>();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002384 File Offset: 0x00000584
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this._emptiable || !this._emptiable.IsMarkedForEmptying)
			{
				CarrierInventoryFinder component = agent.GetComponent<CarrierInventoryFinder>();
				foreach (Inventory inventory in this._inventories.EnabledInventories)
				{
					if (this.StartCarrying(inventory, component))
					{
						return Decision.ReleaseNextTick();
					}
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002414 File Offset: 0x00000614
		public bool StartCarrying(Inventory inventory, CarrierInventoryFinder carrierInventoryFinder)
		{
			foreach (string goodId in inventory.InputGoods)
			{
				this._inputGoodsOrdered.Add(new GoodAmount(goodId, inventory.UnreservedAmountInStock(goodId)));
			}
			foreach (GoodAmount goodAmount in this._inputGoodsOrdered)
			{
				if (carrierInventoryFinder.TryCarryFromAnyInventory(goodAmount.GoodId, inventory))
				{
					this._inputGoodsOrdered.Clear();
					return true;
				}
			}
			this._inputGoodsOrdered.Clear();
			return false;
		}

		// Token: 0x04000012 RID: 18
		public Inventories _inventories;

		// Token: 0x04000013 RID: 19
		public Emptiable _emptiable;

		// Token: 0x04000014 RID: 20
		public readonly SortedSet<GoodAmount> _inputGoodsOrdered = new SortedSet<GoodAmount>(new GoodAmountComparer());
	}
}
