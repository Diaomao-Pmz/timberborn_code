using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.Effects;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.NeedBehaviorSystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.SingletonSystem;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.InventoryNeedSystem
{
	// Token: 0x02000006 RID: 6
	public class InventoryNeedBehavior : NeedBehavior, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020D7 File Offset: 0x000002D7
		public InventoryNeedBehavior(IGoodService goodService, EventBus eventBus)
		{
			this._goodService = goodService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F8 File Offset: 0x000002F8
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			base.DisableComponent();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002124 File Offset: 0x00000324
		public void Initialize(Inventory inventory)
		{
			Asserts.FieldIsNull<InventoryNeedBehavior>(this, this._inventory, "_inventory");
			this._inventory = inventory;
			this._inventory.InventoryChanged += this.OnInventoryChanged;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002158 File Offset: 0x00000358
		public override Vector3? ActionPosition(NeedManager needManager)
		{
			if (this._enterable.CanReserveSlot)
			{
				Vector3? unblockedSingleAccess = this._buildingAccessible.Accessible.UnblockedSingleAccess;
				if (unblockedSingleAccess != null)
				{
					Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
					return new Vector3?(valueOrDefault);
				}
			}
			return null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public override Decision Decide(BehaviorAgent agent)
		{
			GoodReserver component = agent.GetComponent<GoodReserver>();
			if (!this._inventory.Enabled)
			{
				if (component.StockReservation.Inventory != this._inventory)
				{
					return Decision.ReleaseNextTick();
				}
				return InventoryNeedBehavior.UnreserveGood(component);
			}
			else
			{
				if (component.StockReservation.Inventory != this._inventory)
				{
					GoodAmount good = this.FindMostOptimalGood(agent.GetComponent<Appraiser>(), this._inventory);
					if (good.Amount <= 0)
					{
						return Decision.ReleaseNextTick();
					}
					component.ReserveExactStockAmount(this._inventory, good);
				}
				WalkInsideExecutor component2 = agent.GetComponent<WalkInsideExecutor>();
				switch (component2.LaunchIgnoringAccessibleValidity(this._enterable))
				{
				case ExecutorStatus.Success:
					return this.ConsumeGood(agent.GetComponent<NeedManager>(), component);
				case ExecutorStatus.Failure:
					return InventoryNeedBehavior.UnreserveGood(component);
				case ExecutorStatus.Running:
					return Decision.ReturnWhenFinished(component2);
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000227F File Offset: 0x0000047F
		public void OnEnterFinishedState()
		{
			if (this._inventory)
			{
				this._districtBuilding.ReassignedDistrict += this.OnReassignedDistrict;
				base.EnableComponent();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022AB File Offset: 0x000004AB
		public void OnExitFinishedState()
		{
			if (this._inventory)
			{
				this._districtBuilding.ReassignedDistrict -= this.OnReassignedDistrict;
				this.RemoveDistrictNeedBehaviorService();
				base.DisableComponent();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022E0 File Offset: 0x000004E0
		public GoodAmount FindMostOptimalGood(Appraiser appraiser, Inventory inventory)
		{
			IEnumerable<GoodAmount> enumerable = inventory.UnreservedTakeableStock();
			float num = 0f;
			GoodAmount result = default(GoodAmount);
			foreach (GoodAmount goodAmount in enumerable)
			{
				GoodAmount goodAmount2 = new GoodAmount(goodAmount.GoodId, 1);
				float num2 = this.AppraiseGood(goodAmount2, appraiser);
				if (num2 > num)
				{
					num = num2;
					result = goodAmount2;
				}
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000235C File Offset: 0x0000055C
		public static Decision UnreserveGood(GoodReserver goodReserver)
		{
			goodReserver.UnreserveStock();
			return Decision.ReleaseNextTick();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000236C File Offset: 0x0000056C
		public Decision ConsumeGood(NeedManager needManager, GoodReserver goodReserver)
		{
			GoodAmount goodAmount = goodReserver.StockReservation.GoodAmount;
			InventoryNeedBehavior.RemoveGoodFromInventory(goodReserver);
			this.ApplyConsumptionEffects(needManager, goodAmount);
			this._eventBus.Post(new GoodConsumedEvent(goodAmount.GoodId));
			return Decision.ReturnNextTick();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023B8 File Offset: 0x000005B8
		public static void RemoveGoodFromInventory(GoodReserver goodReserver)
		{
			GoodReservation stockReservation = goodReserver.StockReservation;
			GoodAmount goodAmount = stockReservation.GoodAmount;
			goodReserver.UnreserveStock();
			stockReservation.Inventory.Take(goodAmount);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023E8 File Offset: 0x000005E8
		public void ApplyConsumptionEffects(NeedManager needManager, GoodAmount goodAmount)
		{
			foreach (InstantEffectSpec instantEffectSpec in this._goodService.GetGood(goodAmount.GoodId).ConsumptionEffects)
			{
				InstantEffect instantEffect = InstantEffect.FromSpec(instantEffectSpec, goodAmount.Amount);
				needManager.ApplyEffect(instantEffect);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000243C File Offset: 0x0000063C
		public float AppraiseGood(GoodAmount good, Appraiser appraiser)
		{
			IEnumerable<InstantEffect> instantEffects = this.GoodEffects(good);
			return appraiser.AppraiseEffects(instantEffects);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002458 File Offset: 0x00000658
		public IEnumerable<InstantEffect> GoodEffects(GoodAmount good)
		{
			return from effectSpec in this._goodService.GetGood(good.GoodId).ConsumptionEffects
			select InstantEffect.FromSpec(effectSpec, good.Amount);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024A0 File Offset: 0x000006A0
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			GoodSpec good = this._goodService.GetGood(e.GoodId);
			if (good.HasConsumptionEffects)
			{
				this.UpdateAvailableConsumables(good);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024D0 File Offset: 0x000006D0
		public void UpdateAvailableConsumables(GoodSpec good)
		{
			if (this._inventory.HasUnreservedStock(good.Id))
			{
				if (this._availableConsumables.TryAdd(good.Id, good.ConsumptionEffects) && this._districtNeedBehaviorService)
				{
					this._districtNeedBehaviorService.AddNeedBehavior(good.ConsumptionEffects, this);
					return;
				}
			}
			else if (this._availableConsumables.Remove(good.Id) && this._districtNeedBehaviorService)
			{
				this._districtNeedBehaviorService.RemoveNeedBehavior(good.ConsumptionEffects, this);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000256C File Offset: 0x0000076C
		public void OnReassignedDistrict(object sender, EventArgs e)
		{
			this.RemoveDistrictNeedBehaviorService();
			DistrictCenter district = this._districtBuilding.District;
			if (district)
			{
				this._districtNeedBehaviorService = district.GetComponent<DistrictNeedBehaviorService>();
				foreach (IReadOnlyList<InstantEffectSpec> effects in this._availableConsumables.Values)
				{
					this._districtNeedBehaviorService.AddNeedBehavior(effects, this);
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025F0 File Offset: 0x000007F0
		public void RemoveDistrictNeedBehaviorService()
		{
			if (this._districtNeedBehaviorService)
			{
				foreach (IReadOnlyList<InstantEffectSpec> effects in this._availableConsumables.Values)
				{
					this._districtNeedBehaviorService.RemoveNeedBehavior(effects, this);
				}
				this._districtNeedBehaviorService = null;
			}
		}

		// Token: 0x04000007 RID: 7
		public readonly IGoodService _goodService;

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public BuildingAccessible _buildingAccessible;

		// Token: 0x0400000A RID: 10
		public Enterable _enterable;

		// Token: 0x0400000B RID: 11
		public Inventory _inventory;

		// Token: 0x0400000C RID: 12
		public DistrictBuilding _districtBuilding;

		// Token: 0x0400000D RID: 13
		public DistrictNeedBehaviorService _districtNeedBehaviorService;

		// Token: 0x0400000E RID: 14
		public readonly Dictionary<string, IReadOnlyList<InstantEffectSpec>> _availableConsumables = new Dictionary<string, IReadOnlyList<InstantEffectSpec>>();
	}
}
