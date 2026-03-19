using System;
using System.Collections.Generic;
using Timberborn.Carrying;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Yielding;

namespace Timberborn.YielderFinding
{
	// Token: 0x02000004 RID: 4
	public class ClosestYielderFinder
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public ClosestYielderFinder(CarryAmountCalculator carryAmountCalculator)
		{
			this._carryAmountCalculator = carryAmountCalculator;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E3 File Offset: 0x000002E3
		public YielderSearchResult FindLivingYielder(Inventory receivingInventory, int liftingCapacity, IEnumerable<ReachableYielder> reachableYielders)
		{
			return this.FindYielder(receivingInventory, liftingCapacity, reachableYielders, true);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020EF File Offset: 0x000002EF
		public YielderSearchResult FindYielder(Inventory receivingInventory, int liftingCapacity, IEnumerable<ReachableYielder> reachableYielders)
		{
			return this.FindYielder(receivingInventory, liftingCapacity, reachableYielders, false);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FB File Offset: 0x000002FB
		public YielderSearchResult FindYielder(Inventory receivingInventory, int liftingCapacity, IEnumerable<ReachableYielder> reachableYielders, bool isLiving)
		{
			if (!this.FindClosestYielders(reachableYielders, isLiving))
			{
				return YielderSearchResult.CreateNoYielderInRange();
			}
			YielderSearchResult result = this.FindYielder(receivingInventory, liftingCapacity);
			this._yielders.Clear();
			this._orderedYielders.Clear();
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000212C File Offset: 0x0000032C
		public bool FindClosestYielders(IEnumerable<ReachableYielder> reachableYielders, bool isLiving)
		{
			bool flag = false;
			foreach (ReachableYielder reachableYielder in reachableYielders)
			{
				Yielder yielder = reachableYielder.Yielder;
				if (yielder)
				{
					bool isYielding = yielder.IsYielding;
					flag = (flag || isYielding || (isLiving && yielder.IsAlive()));
					if (isYielding)
					{
						this.AddCloserYielder(reachableYielder);
					}
				}
			}
			return flag;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021AC File Offset: 0x000003AC
		public void AddCloserYielder(ReachableYielder reachableYielder)
		{
			string goodId = reachableYielder.Yielder.Yield.GoodId;
			ReachableYielder reachableYielder2;
			if (this._yielders.TryGetValue(goodId, out reachableYielder2))
			{
				if (reachableYielder.Distance < reachableYielder2.Distance)
				{
					this._yielders[goodId] = reachableYielder;
					return;
				}
			}
			else
			{
				this._yielders[goodId] = reachableYielder;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000220C File Offset: 0x0000040C
		public YielderSearchResult FindYielder(Inventory receivingInventory, int liftingCapacity)
		{
			this._orderedYielders.AddRange(this._yielders.Values);
			foreach (ReachableYielder reachableYielder in this._orderedYielders)
			{
				Yielder yielder = reachableYielder.Yielder;
				GoodAmount yield = this._carryAmountCalculator.AmountToCarry(liftingCapacity, yielder.Yield, receivingInventory);
				if (yield.Amount > 0)
				{
					return YielderSearchResult.CreateSearchResult(yielder, yield);
				}
			}
			return YielderSearchResult.CreateEmpty();
		}

		// Token: 0x04000006 RID: 6
		public readonly CarryAmountCalculator _carryAmountCalculator;

		// Token: 0x04000007 RID: 7
		public readonly Dictionary<string, ReachableYielder> _yielders = new Dictionary<string, ReachableYielder>();

		// Token: 0x04000008 RID: 8
		public readonly SortedSet<ReachableYielder> _orderedYielders = new SortedSet<ReachableYielder>();
	}
}
