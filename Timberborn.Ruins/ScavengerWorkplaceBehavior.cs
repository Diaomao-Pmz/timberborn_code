using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Buildings;
using Timberborn.Carrying;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Navigation;
using Timberborn.SimpleOutputBuildings;
using Timberborn.WorkSystem;
using Timberborn.YielderFinding;
using Timberborn.Yielding;

namespace Timberborn.Ruins
{
	// Token: 0x02000015 RID: 21
	public class ScavengerWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000321B File Offset: 0x0000141B
		public ScavengerWorkplaceBehavior(YielderFinder yielderFinder)
		{
			this._yielderFinder = yielderFinder;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003235 File Offset: 0x00001435
		public void Awake()
		{
			this._yieldStatus = base.GetComponent<YieldStatus>();
			this._inventory = base.GetComponent<SimpleOutputInventory>().Inventory;
			this._inRangeYielders = base.GetComponent<InRangeYielders>();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003260 File Offset: 0x00001460
		public void InitializeEntity()
		{
			this._accessible = base.GetComponent<BuildingAccessible>().Accessible;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003274 File Offset: 0x00001474
		public override Decision Decide(BehaviorAgent agent)
		{
			GoodCarrier component = agent.GetComponent<GoodCarrier>();
			YielderSearchResult yielderSearchResult = this.FindYielder(this._accessible, component.LiftingCapacity);
			this._yieldStatus.UpdateStatus(yielderSearchResult);
			if (yielderSearchResult.HasYielder)
			{
				GoodAmount yield = yielderSearchResult.Yield;
				yielderSearchResult.Yielder.Reservable.Reserve();
				agent.GetComponent<GoodReserver>().ReserveCapacity(this._inventory, yield);
				agent.GetComponent<YielderRemover>().ReserveForRemoval(yielderSearchResult.Yielder, yield);
				YieldRemoverBehavior component2 = agent.GetComponent<YieldRemoverBehavior>();
				Decision decision = component2.Decide(agent);
				if (!decision.ShouldReleaseNow)
				{
					return Decision.TransferNow(component2, decision);
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003318 File Offset: 0x00001518
		public YielderSearchResult FindYielder(Accessible start, int liftingCapacity)
		{
			bool yielders = this._inRangeYielders.GetYielders(this._yieldersCache, null);
			if (this._yieldersCache.Count > 0)
			{
				YielderSearchResult result = this._yielderFinder.FindYielderWithAccessible(this._inventory, start, liftingCapacity, this._yieldersCache);
				this._yieldersCache.Clear();
				return result;
			}
			if (!yielders)
			{
				return YielderSearchResult.CreateNoYielderInRange();
			}
			return YielderSearchResult.CreateEmpty();
		}

		// Token: 0x04000032 RID: 50
		public readonly YielderFinder _yielderFinder;

		// Token: 0x04000033 RID: 51
		public Accessible _accessible;

		// Token: 0x04000034 RID: 52
		public YieldStatus _yieldStatus;

		// Token: 0x04000035 RID: 53
		public Inventory _inventory;

		// Token: 0x04000036 RID: 54
		public InRangeYielders _inRangeYielders;

		// Token: 0x04000037 RID: 55
		public readonly List<Yielder> _yieldersCache = new List<Yielder>();
	}
}
