using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Buildings;
using Timberborn.Carrying;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.GoodStackSystem;
using Timberborn.InventorySystem;
using Timberborn.Navigation;
using Timberborn.SimpleOutputBuildings;
using Timberborn.WorkSystem;
using Timberborn.YielderFinding;
using Timberborn.Yielding;

namespace Timberborn.Forestry
{
	// Token: 0x0200000D RID: 13
	public class LumberjackFlagWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002610 File Offset: 0x00000810
		public LumberjackFlagWorkplaceBehavior(YielderFinder yielderFinder, TreeCuttingArea treeCuttingArea, GoodStackService<LumberjackFlagSpec> goodStackService)
		{
			this._yielderFinder = yielderFinder;
			this._treeCuttingArea = treeCuttingArea;
			this._goodStackService = goodStackService;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000262D File Offset: 0x0000082D
		public void Awake()
		{
			this._yieldStatus = base.GetComponent<YieldStatus>();
			this._inventory = base.GetComponent<SimpleOutputInventory>().Inventory;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000264C File Offset: 0x0000084C
		public void InitializeEntity()
		{
			this._accessible = base.GetComponent<BuildingAccessible>().Accessible;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002660 File Offset: 0x00000860
		public override Decision Decide(BehaviorAgent agent)
		{
			GoodStackRetrieverBehavior component = agent.GetComponent<GoodStackRetrieverBehavior>();
			Decision decision = component.StartRetrieving(this._goodStackService, this._accessible, this._inventory);
			if (!decision.ShouldReleaseNow)
			{
				return Decision.TransferNow(component, decision);
			}
			GoodCarrier component2 = agent.GetComponent<GoodCarrier>();
			YielderSearchResult yielderSearchResult = this.FindCuttable(component2.LiftingCapacity);
			if (yielderSearchResult.HasYielder)
			{
				GoodAmount yield = yielderSearchResult.Yield;
				yielderSearchResult.Yielder.Reservable.Reserve();
				agent.GetComponent<GoodReserver>().ReserveCapacity(this._inventory, yield);
				agent.GetComponent<YielderRemover>().ReserveForRemoval(yielderSearchResult.Yielder, yield);
				YieldRemoverBehavior component3 = agent.GetComponent<YieldRemoverBehavior>();
				Decision decision2 = component3.Decide(agent);
				if (!decision2.ShouldReleaseNow)
				{
					return Decision.TransferNow(component3, decision2);
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002728 File Offset: 0x00000928
		public YielderSearchResult FindCuttable(int liftingCapacity)
		{
			IEnumerable<Yielder> yielders = from yielder in this._treeCuttingArea.YieldersInArea
			where !yielder.Reservable.Reserved
			select yielder;
			YielderSearchResult yielderSearchResult = this._yielderFinder.FindLivingYielderWithoutAccessible(this._inventory, this._accessible, liftingCapacity, yielders);
			this._yieldStatus.UpdateStatus(yielderSearchResult);
			return yielderSearchResult;
		}

		// Token: 0x04000010 RID: 16
		public readonly YielderFinder _yielderFinder;

		// Token: 0x04000011 RID: 17
		public readonly TreeCuttingArea _treeCuttingArea;

		// Token: 0x04000012 RID: 18
		public readonly GoodStackService<LumberjackFlagSpec> _goodStackService;

		// Token: 0x04000013 RID: 19
		public YieldStatus _yieldStatus;

		// Token: 0x04000014 RID: 20
		public Inventory _inventory;

		// Token: 0x04000015 RID: 21
		public Accessible _accessible;
	}
}
