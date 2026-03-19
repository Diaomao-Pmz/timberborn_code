using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.InventorySystem;
using Timberborn.Navigation;
using Timberborn.Yielding;

namespace Timberborn.YielderFinding
{
	// Token: 0x02000007 RID: 7
	public class YielderFinder
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002331 File Offset: 0x00000531
		public YielderFinder(ClosestYielderFinder closestYielderFinder)
		{
			this._closestYielderFinder = closestYielderFinder;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002340 File Offset: 0x00000540
		public YielderSearchResult FindLivingYielderWithoutAccessible(Inventory receivingInventory, Accessible start, int liftingCapacity, IEnumerable<Yielder> yielders)
		{
			IEnumerable<ReachableYielder> reachableYielders = from yielder in yielders
			select YielderFinder.RegularYielderAsReachable(start, yielder);
			return this._closestYielderFinder.FindLivingYielder(receivingInventory, liftingCapacity, reachableYielders);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000237C File Offset: 0x0000057C
		public YielderSearchResult FindYielderWithAccessible(Inventory receivingInventory, Accessible start, int liftingCapacity, IEnumerable<Yielder> yielders)
		{
			IEnumerable<ReachableYielder> reachableYielders = from yielder in yielders
			select YielderFinder.AccessibleYielderAsReachable(start, yielder);
			return this._closestYielderFinder.FindYielder(receivingInventory, liftingCapacity, reachableYielders);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023B8 File Offset: 0x000005B8
		public static ReachableYielder RegularYielderAsReachable(Accessible start, Yielder yielder)
		{
			float distance;
			if (!start.FindTerrainPath(yielder.CenterPosition, out distance))
			{
				return default(ReachableYielder);
			}
			return new ReachableYielder(yielder, distance);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023E8 File Offset: 0x000005E8
		public static ReachableYielder AccessibleYielderAsReachable(Accessible start, Yielder yielder)
		{
			Accessible enabledComponent = yielder.GetEnabledComponent<Accessible>();
			float distance;
			if (!start.FindTerrainPath(enabledComponent, out distance))
			{
				return default(ReachableYielder);
			}
			return new ReachableYielder(yielder, distance);
		}

		// Token: 0x0400000B RID: 11
		public readonly ClosestYielderFinder _closestYielderFinder;
	}
}
