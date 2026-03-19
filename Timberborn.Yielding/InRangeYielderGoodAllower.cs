using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.TickSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Yielding
{
	// Token: 0x02000007 RID: 7
	public class InRangeYielderGoodAllower : TickableComponent, IAwakableComponent, IFinishedStateListener, IInitializableGoodDisallower, IGoodDisallower
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler<DisallowedGoodsChangedEventArgs> DisallowedGoodsChanged;

		// Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		public void Awake()
		{
			this._inRangeYielders = base.GetComponent<InRangeYielders>();
			this._workplace = base.GetComponent<Workplace>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002187 File Offset: 0x00000387
		public void Initialize(Inventory inventory)
		{
			this._inventory = inventory;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002190 File Offset: 0x00000390
		public override void Tick()
		{
			for (int i = this._incomingGoods.Count - 1; i >= 0; i--)
			{
				string good = this._incomingGoods[i];
				if (this._inventory.ReservedCapacity(good) == 0)
				{
					this._incomingGoods.RemoveAt(i);
				}
			}
			if (this._incomingGoods.Count == 0)
			{
				base.DisableComponent();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F0 File Offset: 0x000003F0
		public void OnEnterFinishedState()
		{
			this._inRangeYielders.YieldersChanged += this.OnYieldersChanged;
			this._inRangeYielders.YielderAdded += this.OnYielderAdded;
			this._inventory.InventoryCapacityReservationChanged += this.OnInventoryCapacityReservationChanged;
			this.UpdateAllowedGoods();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002248 File Offset: 0x00000448
		public void OnExitFinishedState()
		{
			this._inRangeYielders.YieldersChanged -= this.OnYieldersChanged;
			this._inRangeYielders.YielderAdded -= this.OnYielderAdded;
			this._inventory.InventoryCapacityReservationChanged -= this.OnInventoryCapacityReservationChanged;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000229A File Offset: 0x0000049A
		public int AllowedAmount(string goodId)
		{
			if (!this.AllowsGood(goodId))
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022AC File Offset: 0x000004AC
		public void OnYieldersChanged(object sender, EventArgs e)
		{
			this.UpdateAllowedGoods();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B4 File Offset: 0x000004B4
		public void OnYielderAdded(object sender, Yielder yielder)
		{
			string id = yielder.YielderSpec.Yield.Id;
			if (this._allowedGoods.Add(id))
			{
				EventHandler<DisallowedGoodsChangedEventArgs> disallowedGoodsChanged = this.DisallowedGoodsChanged;
				if (disallowedGoodsChanged == null)
				{
					return;
				}
				disallowedGoodsChanged(this, new DisallowedGoodsChangedEventArgs(id));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022F8 File Offset: 0x000004F8
		public void OnInventoryCapacityReservationChanged(object sender, InventoryAmountChangedEventArgs e)
		{
			GoodAmount goodAmount = e.GoodAmount;
			if (goodAmount.Amount > 0 && !this._incomingGoods.Contains(goodAmount.GoodId))
			{
				this._incomingGoods.Add(goodAmount.GoodId);
				base.EnableComponent();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002343 File Offset: 0x00000543
		public bool AllowsGood(string goodId)
		{
			return (this._allowedGoods.Contains(goodId) && this._workplace.NumberOfAssignedWorkers > 0) || this._inventory.AmountInStock(goodId) > 0 || this._incomingGoods.Contains(goodId);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002380 File Offset: 0x00000580
		public void UpdateAllowedGoods()
		{
			this._inRangeYielders.GetYields(this._yieldsCache);
			foreach (StorableGoodAmount storableGoodAmount in this._inventory.AllowedGoods)
			{
				string goodId = storableGoodAmount.StorableGood.GoodId;
				if (!this.TryAdd(goodId))
				{
					this.TryRemove(goodId);
				}
			}
			this._yieldsCache.Clear();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002414 File Offset: 0x00000614
		public bool TryAdd(string goodId)
		{
			if (this._yieldsCache.Contains(goodId) && !this._allowedGoods.Contains(goodId))
			{
				this.Add(goodId);
				return true;
			}
			return false;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000243C File Offset: 0x0000063C
		public void Add(string goodId)
		{
			this._allowedGoods.Add(goodId);
			EventHandler<DisallowedGoodsChangedEventArgs> disallowedGoodsChanged = this.DisallowedGoodsChanged;
			if (disallowedGoodsChanged == null)
			{
				return;
			}
			disallowedGoodsChanged(this, new DisallowedGoodsChangedEventArgs(goodId));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002462 File Offset: 0x00000662
		public void TryRemove(string goodId)
		{
			if (this._allowedGoods.Contains(goodId) && !this._yieldsCache.Contains(goodId))
			{
				this.Remove(goodId);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002487 File Offset: 0x00000687
		public void Remove(string goodId)
		{
			this._allowedGoods.Remove(goodId);
			EventHandler<DisallowedGoodsChangedEventArgs> disallowedGoodsChanged = this.DisallowedGoodsChanged;
			if (disallowedGoodsChanged == null)
			{
				return;
			}
			disallowedGoodsChanged(this, new DisallowedGoodsChangedEventArgs(goodId));
		}

		// Token: 0x04000009 RID: 9
		public InRangeYielders _inRangeYielders;

		// Token: 0x0400000A RID: 10
		public Workplace _workplace;

		// Token: 0x0400000B RID: 11
		public readonly HashSet<string> _allowedGoods = new HashSet<string>();

		// Token: 0x0400000C RID: 12
		public readonly HashSet<string> _yieldsCache = new HashSet<string>();

		// Token: 0x0400000D RID: 13
		public readonly List<string> _incomingGoods = new List<string>();

		// Token: 0x0400000E RID: 14
		public Inventory _inventory;
	}
}
