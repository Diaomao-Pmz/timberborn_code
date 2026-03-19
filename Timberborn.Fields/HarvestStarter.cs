using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Navigation;
using Timberborn.WorkSystem;
using Timberborn.YielderFinding;
using Timberborn.Yielding;

namespace Timberborn.Fields
{
	// Token: 0x02000010 RID: 16
	public class HarvestStarter : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002825 File Offset: 0x00000A25
		public HarvestStarter(YielderFinder yielderFinder)
		{
			this._yielderFinder = yielderFinder;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002840 File Offset: 0x00000A40
		public void Awake()
		{
			this._goodReserver = base.GetComponent<GoodReserver>();
			this._yieldRemover = base.GetComponent<YielderRemover>();
			this._yieldRemoverBehavior = base.GetComponent<YieldRemoverBehavior>();
			this._worker = base.GetComponent<Worker>();
			this._goodCarrier = base.GetComponent<GoodCarrier>();
			this._behaviorAgent = base.GetComponent<BehaviorAgent>();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002898 File Offset: 0x00000A98
		public Decision StartHarvesting(Inventory receivingInventory, InRangeYielders inRangeYielders, string prioritizedName)
		{
			YielderSearchResult yielderSearchResult2;
			if (!string.IsNullOrWhiteSpace(prioritizedName))
			{
				YielderSearchResult yielderSearchResult = this.FindYielder(receivingInventory, inRangeYielders, prioritizedName);
				if (yielderSearchResult.HasYielder)
				{
					yielderSearchResult2 = yielderSearchResult;
					goto IL_27;
				}
			}
			yielderSearchResult2 = this.FindYielder(receivingInventory, inRangeYielders, null);
			IL_27:
			YielderSearchResult searchResult = yielderSearchResult2;
			return this.StartHarvesting(receivingInventory, searchResult);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000028D8 File Offset: 0x00000AD8
		public Decision StartHarvesting(Inventory receivingInventory, YielderSearchResult searchResult)
		{
			if (searchResult.HasYielder)
			{
				GoodAmount yield = searchResult.Yield;
				searchResult.Yielder.Reservable.Reserve();
				this._goodReserver.ReserveCapacity(receivingInventory, yield);
				this._yieldRemover.ReserveForRemoval(searchResult.Yielder, yield);
				Decision decision = this._yieldRemoverBehavior.Decide(this._behaviorAgent);
				if (!decision.ShouldReleaseNow)
				{
					return Decision.TransferNow(this._yieldRemoverBehavior, decision);
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002958 File Offset: 0x00000B58
		public YielderSearchResult FindYielder(Inventory receivingInventory, InRangeYielders inRangeYielders, string prioritizedName)
		{
			inRangeYielders.GetYielders(this._yieldersCache, prioritizedName);
			Accessible enabledComponent = this._worker.Workplace.GetEnabledComponent<Accessible>();
			int liftingCapacity = this._goodCarrier.LiftingCapacity;
			YielderSearchResult result = this._yielderFinder.FindLivingYielderWithoutAccessible(receivingInventory, enabledComponent, liftingCapacity, this._yieldersCache);
			this._yieldersCache.Clear();
			return result;
		}

		// Token: 0x0400001C RID: 28
		public readonly YielderFinder _yielderFinder;

		// Token: 0x0400001D RID: 29
		public GoodReserver _goodReserver;

		// Token: 0x0400001E RID: 30
		public YielderRemover _yieldRemover;

		// Token: 0x0400001F RID: 31
		public YieldRemoverBehavior _yieldRemoverBehavior;

		// Token: 0x04000020 RID: 32
		public Worker _worker;

		// Token: 0x04000021 RID: 33
		public GoodCarrier _goodCarrier;

		// Token: 0x04000022 RID: 34
		public BehaviorAgent _behaviorAgent;

		// Token: 0x04000023 RID: 35
		public readonly List<Yielder> _yieldersCache = new List<Yielder>();
	}
}
