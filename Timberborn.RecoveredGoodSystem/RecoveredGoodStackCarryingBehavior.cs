using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Navigation;
using Timberborn.WorkSystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x0200000D RID: 13
	public class RecoveredGoodStackCarryingBehavior : Behavior, IAwakableComponent
	{
		// Token: 0x06000037 RID: 55 RVA: 0x0000275E File Offset: 0x0000095E
		public RecoveredGoodStackCarryingBehavior(CarryAmountCalculator carryAmountCalculator)
		{
			this._carryAmountCalculator = carryAmountCalculator;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000276D File Offset: 0x0000096D
		public void Awake()
		{
			this._carrierInventoryFinder = base.GetComponent<CarrierInventoryFinder>();
			this._behaviorAgent = base.GetComponent<BehaviorAgent>();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002788 File Offset: 0x00000988
		public Decision FindInventoryAndStartCarrying(RecoveredGoodStack recoveredGoodStack)
		{
			Accessible accessible = recoveredGoodStack.GetComponent<RecoveredGoodStackAccessible>().Accessible;
			NoStorageStatus component = recoveredGoodStack.GetComponent<NoStorageStatus>();
			foreach (GoodAmount goodAmount in recoveredGoodStack.Inventory.Stock)
			{
				Inventory inventory = this.FindBestInventory(accessible, goodAmount);
				if (inventory != null)
				{
					Accessible enabledComponent = inventory.GetEnabledComponent<Accessible>();
					Decision result = this.StartCarrying(recoveredGoodStack, enabledComponent, inventory);
					if (!result.ShouldReleaseNow)
					{
						component.DeactivateNoStorageStatus();
						return result;
					}
				}
			}
			component.ActivateNoStorageStatus();
			return Decision.ReleaseNow();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002838 File Offset: 0x00000A38
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this.Exists() && this.IsReachable() && this.RetrieveGoodStack(agent))
			{
				return Decision.ReleaseNextTick();
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002860 File Offset: 0x00000A60
		public Inventory FindBestInventory(Accessible start, GoodAmount goodAmount)
		{
			float num;
			Inventory inventory = this._carrierInventoryFinder.GetClosestInventoryWithCapacity(goodAmount.GoodId, start, out num);
			Workplace workplace = this._behaviorAgent.GetComponent<Worker>().Workplace;
			Inventory enabledComponent = workplace.GetEnabledComponent<Inventory>();
			if (enabledComponent.UnreservedCapacity(goodAmount.GoodId) > 0)
			{
				Accessible enabledComponent2 = workplace.GetEnabledComponent<Accessible>();
				float num2;
				if ((enabledComponent2.FindRoadPath(start, out num2) || enabledComponent2.FindRoadToTerrainPath(start.Transform.position, out num2)) && (inventory == null || num2 < num))
				{
					inventory = enabledComponent;
				}
			}
			return inventory;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028E0 File Offset: 0x00000AE0
		public Decision StartCarrying(RecoveredGoodStack recoveredGoodStack, Accessible inventoryAccessible, Inventory targetInventory)
		{
			this._recoveredGoodStack = recoveredGoodStack;
			this._inventoryAccessible = inventoryAccessible;
			this._targetInventory = targetInventory;
			return this.Decide(this._behaviorAgent);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002903 File Offset: 0x00000B03
		public bool Exists()
		{
			return this._recoveredGoodStack && !this._recoveredGoodStack.GetComponent<EntityComponent>().Deleted;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002927 File Offset: 0x00000B27
		public bool IsReachable()
		{
			return this._inventoryAccessible.IsReachableByRoadToTerrain(this._recoveredGoodStack.GetEnabledComponent<Accessible>());
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002940 File Offset: 0x00000B40
		public bool RetrieveGoodStack(BehaviorAgent agent)
		{
			foreach (GoodAmount good in this._recoveredGoodStack.Inventory.UnreservedStock())
			{
				if (this._targetInventory.UnreservedCapacity(good.GoodId) > 0)
				{
					GoodCarrier component = agent.GetComponent<GoodCarrier>();
					GoodAmount goodAmount = this._carryAmountCalculator.AmountToCarry(component.LiftingCapacity, good, this._targetInventory);
					GoodReserver component2 = agent.GetComponent<GoodReserver>();
					component2.ReserveNotLessThanStockAmount(this._recoveredGoodStack.Inventory, goodAmount);
					component2.ReserveCapacity(this._targetInventory, goodAmount);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000021 RID: 33
		public readonly CarryAmountCalculator _carryAmountCalculator;

		// Token: 0x04000022 RID: 34
		public CarrierInventoryFinder _carrierInventoryFinder;

		// Token: 0x04000023 RID: 35
		public BehaviorAgent _behaviorAgent;

		// Token: 0x04000024 RID: 36
		public RecoveredGoodStack _recoveredGoodStack;

		// Token: 0x04000025 RID: 37
		public Accessible _inventoryAccessible;

		// Token: 0x04000026 RID: 38
		public Inventory _targetInventory;
	}
}
