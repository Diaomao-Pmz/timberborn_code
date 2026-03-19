using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.InventorySystem;
using Timberborn.Stockpiles;
using Timberborn.WorkSystem;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x02000008 RID: 8
	public class ObtainGoodWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002469 File Offset: 0x00000669
		public void Awake()
		{
			this._inventory = base.GetComponent<Stockpile>().Inventory;
			this._goodObtainer = base.GetComponent<GoodObtainer>();
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002494 File Offset: 0x00000694
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this.CanObtain() && this.StartCarrying(agent))
			{
				return Decision.ReleaseNextTick();
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024B4 File Offset: 0x000006B4
		public bool CanObtain()
		{
			return this._goodObtainer.IsObtaining && this._inventory.Enabled && this._singleGoodAllower.HasAllowedGood && this._singleGoodAllower.AllowedAmount(this._singleGoodAllower.AllowedGood) > 0;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002504 File Offset: 0x00000704
		public bool StartCarrying(BehaviorAgent agent)
		{
			CarrierInventoryFinder component = agent.GetComponent<CarrierInventoryFinder>();
			string allowedGood = this._singleGoodAllower.AllowedGood;
			return component.TryCarryFromAnyInventory(allowedGood, this._inventory, new Predicate<Inventory>(ObtainGoodWorkplaceBehavior.CanObtainFrom));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000253C File Offset: 0x0000073C
		public static bool CanObtainFrom(Inventory inventory)
		{
			GoodObtainer component = inventory.GetComponent<GoodObtainer>();
			return !component || !component.IsObtaining;
		}

		// Token: 0x04000015 RID: 21
		public Inventory _inventory;

		// Token: 0x04000016 RID: 22
		public GoodObtainer _goodObtainer;

		// Token: 0x04000017 RID: 23
		public SingleGoodAllower _singleGoodAllower;
	}
}
