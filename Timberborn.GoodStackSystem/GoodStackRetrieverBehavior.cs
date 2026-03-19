using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockSystem;
using Timberborn.Carrying;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Navigation;
using Timberborn.TemplateSystem;
using Timberborn.WorkSystem;
using UnityEngine;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x02000010 RID: 16
	public class GoodStackRetrieverBehavior : Behavior, IAwakableComponent, IJobBehavior
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000294D File Offset: 0x00000B4D
		public GoodStackRetrieverBehavior(CarryAmountCalculator carryAmountCalculator)
		{
			this._carryAmountCalculator = carryAmountCalculator;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000295C File Offset: 0x00000B5C
		public void Awake()
		{
			this._behaviorAgent = base.GetComponent<BehaviorAgent>();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000296A File Offset: 0x00000B6A
		public Decision StartRetrieving(IGoodStackService goodStackService, Accessible accessible, Inventory inventory)
		{
			return this.StartRetrieving(goodStackService, accessible, inventory, (string _) => true);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002994 File Offset: 0x00000B94
		public Decision StartRetrieving(IGoodStackService goodStackService, Accessible accessible, Inventory inventory, Func<string, bool> templateNameValidation)
		{
			this._goodStackService = goodStackService;
			this._accessible = accessible;
			this._inventory = inventory;
			this._templateNameValidation = templateNameValidation;
			return this.Decide(this._behaviorAgent);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029C0 File Offset: 0x00000BC0
		public override Decision Decide(BehaviorAgent agent)
		{
			for (int i = 0; i < this._goodStackService.GoodStacks.Count; i++)
			{
				GoodStack goodStack = this._goodStackService.GoodStacks[i];
				if (GoodStackRetrieverBehavior.Exists(goodStack) && this._templateNameValidation(goodStack.GetComponent<TemplateSpec>().TemplateName) && this.IsReachable(goodStack) && this.RetrieveGoodStack(goodStack, agent))
				{
					return Decision.ReleaseNextTick();
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A3E File Offset: 0x00000C3E
		public static bool Exists(GoodStack goodStack)
		{
			return goodStack && !goodStack.GetComponent<EntityComponent>().Deleted;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A58 File Offset: 0x00000C58
		public bool IsReachable(GoodStack goodStack)
		{
			Vector3 worldCenterGrounded = goodStack.GetComponent<BlockObjectCenter>().WorldCenterGrounded;
			return this._accessible.IsReachableByTerrain(worldCenterGrounded);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A80 File Offset: 0x00000C80
		public bool RetrieveGoodStack(GoodStack goodStack, BehaviorAgent agent)
		{
			foreach (GoodAmount good in goodStack.Inventory.UnreservedStock())
			{
				if (this._inventory.UnreservedCapacity(good.GoodId) > 0)
				{
					GoodCarrier component = agent.GetComponent<GoodCarrier>();
					GoodAmount goodAmount = this._carryAmountCalculator.AmountToCarry(component.LiftingCapacity, good, this._inventory);
					GoodReserver component2 = agent.GetComponent<GoodReserver>();
					component2.ReserveNotLessThanStockAmount(goodStack.Inventory, goodAmount);
					component2.ReserveCapacity(this._inventory, goodAmount);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000028 RID: 40
		public readonly CarryAmountCalculator _carryAmountCalculator;

		// Token: 0x04000029 RID: 41
		public BehaviorAgent _behaviorAgent;

		// Token: 0x0400002A RID: 42
		public IGoodStackService _goodStackService;

		// Token: 0x0400002B RID: 43
		public Accessible _accessible;

		// Token: 0x0400002C RID: 44
		public Inventory _inventory;

		// Token: 0x0400002D RID: 45
		public Func<string, bool> _templateNameValidation;
	}
}
