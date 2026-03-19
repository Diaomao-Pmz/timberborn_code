using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.InventorySystem;
using Timberborn.Stockpiles;
using Timberborn.WorkSystem;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x0200000D RID: 13
	public class SupplyGoodWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000290F File Offset: 0x00000B0F
		public void Awake()
		{
			this._goodSupplier = base.GetComponent<GoodSupplier>();
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
			this._inventory = base.GetComponent<Stockpile>().Inventory;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000293A File Offset: 0x00000B3A
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this.CanGiveGoods() && this.StartCarrying(agent))
			{
				return Decision.ReleaseNextTick();
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002958 File Offset: 0x00000B58
		public bool CanGiveGoods()
		{
			return this._goodSupplier.IsSupplying && this._inventory.Enabled && this._singleGoodAllower.HasAllowedGood && this._singleGoodAllower.AllowedAmount(this._singleGoodAllower.AllowedGood) > 0;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029A8 File Offset: 0x00000BA8
		public bool StartCarrying(BehaviorAgent agent)
		{
			CarrierInventoryFinder component = agent.GetComponent<CarrierInventoryFinder>();
			string allowedGood = this._singleGoodAllower.AllowedGood;
			return component.TryCarryToAnyInventory(allowedGood, this._inventory, new Predicate<Inventory>(SupplyGoodWorkplaceBehavior.CanGiveTo));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029E0 File Offset: 0x00000BE0
		public static bool CanGiveTo(Inventory inventory)
		{
			GoodSupplier component = inventory.GetComponent<GoodSupplier>();
			return !component || !component.IsSupplying;
		}

		// Token: 0x04000021 RID: 33
		public GoodSupplier _goodSupplier;

		// Token: 0x04000022 RID: 34
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x04000023 RID: 35
		public Inventory _inventory;
	}
}
