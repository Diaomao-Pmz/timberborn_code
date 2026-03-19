using System;
using System.Collections.Generic;
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
using Timberborn.TemplateSystem;
using Timberborn.WorkSystem;
using Timberborn.YielderFinding;
using Timberborn.Yielding;

namespace Timberborn.Gathering
{
	// Token: 0x02000012 RID: 18
	public class GatherWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00002D43 File Offset: 0x00000F43
		public GatherWorkplaceBehavior(YielderFinder yielderFinder, GoodStackService<GathererFlag> goodStackService)
		{
			this._yielderFinder = yielderFinder;
			this._goodStackService = goodStackService;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002D64 File Offset: 0x00000F64
		public void Awake()
		{
			this._gatherablePrioritizer = base.GetComponent<GatherablePrioritizer>();
			this._yieldStatus = base.GetComponent<YieldStatus>();
			this._inventory = base.GetComponent<SimpleOutputInventory>().Inventory;
			this._gathererFlag = base.GetComponent<GathererFlag>();
			this._inRangeYielders = base.GetComponent<InRangeYielders>();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002DB2 File Offset: 0x00000FB2
		public void InitializeEntity()
		{
			this._accessible = base.GetComponent<BuildingAccessible>().Accessible;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public override Decision Decide(BehaviorAgent agent)
		{
			GoodStackRetrieverBehavior component = agent.GetComponent<GoodStackRetrieverBehavior>();
			Decision decision = component.StartRetrieving(this._goodStackService, this._accessible, this._inventory, new Func<string, bool>(this._gathererFlag.CanGather));
			if (!decision.ShouldReleaseNow)
			{
				return Decision.TransferNow(component, decision);
			}
			GoodCarrier component2 = agent.GetComponent<GoodCarrier>();
			YielderSearchResult yielderSearchResult = this.FindYielder(this._accessible, component2.LiftingCapacity, this._gatherablePrioritizer.PrioritizedGatherable);
			this._yieldStatus.UpdateStatus(yielderSearchResult);
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

		// Token: 0x06000074 RID: 116 RVA: 0x00002EC0 File Offset: 0x000010C0
		public YielderSearchResult FindYielder(Accessible start, int liftingCapacity, GatherableSpec gatherableSpec)
		{
			if (gatherableSpec != null)
			{
				string templateName = gatherableSpec.GetSpec<TemplateSpec>().TemplateName;
				YielderSearchResult result = this.FindYielder(start, liftingCapacity, templateName);
				if (result.HasYielder)
				{
					return result;
				}
			}
			return this.FindYielder(start, liftingCapacity, null);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F00 File Offset: 0x00001100
		public YielderSearchResult FindYielder(Accessible start, int liftingCapacity, string templateName = null)
		{
			bool yielders = this._inRangeYielders.GetYielders(this._yieldersCache, templateName);
			if (this._yieldersCache.Count > 0)
			{
				YielderSearchResult result = this._yielderFinder.FindLivingYielderWithoutAccessible(this._inventory, start, liftingCapacity, this._yieldersCache);
				this._yieldersCache.Clear();
				return result;
			}
			if (!yielders)
			{
				return YielderSearchResult.CreateNoYielderInRange();
			}
			return YielderSearchResult.CreateEmpty();
		}

		// Token: 0x04000028 RID: 40
		public readonly YielderFinder _yielderFinder;

		// Token: 0x04000029 RID: 41
		public readonly GoodStackService<GathererFlag> _goodStackService;

		// Token: 0x0400002A RID: 42
		public Accessible _accessible;

		// Token: 0x0400002B RID: 43
		public GatherablePrioritizer _gatherablePrioritizer;

		// Token: 0x0400002C RID: 44
		public YieldStatus _yieldStatus;

		// Token: 0x0400002D RID: 45
		public Inventory _inventory;

		// Token: 0x0400002E RID: 46
		public GathererFlag _gathererFlag;

		// Token: 0x0400002F RID: 47
		public InRangeYielders _inRangeYielders;

		// Token: 0x04000030 RID: 48
		public readonly List<Yielder> _yieldersCache = new List<Yielder>();
	}
}
