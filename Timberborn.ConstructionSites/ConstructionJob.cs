using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.Carrying;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200000B RID: 11
	public class ConstructionJob : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002628 File Offset: 0x00000828
		public ConstructionJob(CarryAmountCalculator carryAmountCalculator)
		{
			this._carryAmountCalculator = carryAmountCalculator;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002647 File Offset: 0x00000847
		public void Awake()
		{
			this._constructionSite = base.GetComponent<ConstructionSite>();
			this._constructionSiteAccessible = base.GetComponent<ConstructionSiteAccessible>();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002664 File Offset: 0x00000864
		public ValueTuple<Behavior, Decision> StartConstructionJob(BehaviorAgent agent, Accessible workplaceAccessible)
		{
			DistrictCenter district = workplaceAccessible.GetComponent<DistrictBuilding>().District;
			Vector3 endOfRoad;
			float num;
			if (!this._constructionSite.IsOn || !district || !workplaceAccessible.FindRoadToTerrainPath(this._constructionSiteAccessible.Accessible, out endOfRoad, out num))
			{
				return new ValueTuple<Behavior, Decision>(null, Decision.ReleaseNow());
			}
			if (this._constructionSite.ReadyToBuild)
			{
				BuildBehavior component = agent.GetComponent<BuildBehavior>();
				Decision item = component.StartBuilding(this._constructionSite);
				if (!item.ShouldReleaseNow)
				{
					return new ValueTuple<Behavior, Decision>(component, item);
				}
			}
			ValueTuple<Inventory, string> valueTuple = this.ClosestObjectWithNeededGood(district, endOfRoad, workplaceAccessible);
			Inventory item2 = valueTuple.Item1;
			string item3 = valueTuple.Item2;
			if (item2)
			{
				this.StartNeededMaterialsDelivery(agent, item2, item3);
				return new ValueTuple<Behavior, Decision>(null, Decision.ReleaseNextTick());
			}
			return new ValueTuple<Behavior, Decision>(null, Decision.ReleaseNow());
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000272C File Offset: 0x0000092C
		[return: TupleElementNames(new string[]
		{
			"inventory",
			"goodId"
		})]
		public ValueTuple<Inventory, string> ClosestObjectWithNeededGood(DistrictCenter districtCenter, Vector3 endOfRoad, Accessible workplaceAccessible)
		{
			this._constructionSite.RemainingRequiredGoods(this._remainingGoods);
			ValueTuple<Inventory, string> valueTuple = this.ClosestInventory(districtCenter, endOfRoad, workplaceAccessible);
			Inventory item = valueTuple.Item1;
			string item2 = valueTuple.Item2;
			if (item)
			{
				this._constructionSite.DeactivateLackOfResourcesStatus();
				this._remainingGoods.Clear();
				return new ValueTuple<Inventory, string>(item, item2);
			}
			if (this._remainingGoods.Count > 0)
			{
				this._remainingGoods.Clear();
				this._constructionSite.ActivateLackOfResourcesStatus();
			}
			return new ValueTuple<Inventory, string>(null, null);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027B4 File Offset: 0x000009B4
		[return: TupleElementNames(new string[]
		{
			"inventory",
			"goodId"
		})]
		public ValueTuple<Inventory, string> ClosestInventory(DistrictCenter districtCenter, Vector3 endOfRoad, Accessible workplaceAccessible)
		{
			DistrictInventoryPicker component = districtCenter.GetComponent<DistrictInventoryPicker>();
			foreach (GoodAmount goodAmount in this._remainingGoods)
			{
				Inventory inventory = component.ClosestInventoryWithStock(endOfRoad, goodAmount.GoodId, workplaceAccessible);
				if (inventory)
				{
					return new ValueTuple<Inventory, string>(inventory, goodAmount.GoodId);
				}
			}
			return default(ValueTuple<Inventory, string>);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002840 File Offset: 0x00000A40
		public void StartNeededMaterialsDelivery(BehaviorAgent agent, Inventory inventory, string neededGood)
		{
			GoodCarrier component = agent.GetComponent<GoodCarrier>();
			GoodAmount goodAmount = this._carryAmountCalculator.AmountToCarry(component.LiftingCapacity, neededGood, this._constructionSite.Inventory, inventory);
			if (goodAmount.Amount > 0)
			{
				GoodReserver component2 = agent.GetComponent<GoodReserver>();
				component2.ReserveNotLessThanStockAmount(inventory, goodAmount);
				component2.ReserveCapacity(this._constructionSite.Inventory, goodAmount);
			}
		}

		// Token: 0x0400001F RID: 31
		public readonly CarryAmountCalculator _carryAmountCalculator;

		// Token: 0x04000020 RID: 32
		public ConstructionSite _constructionSite;

		// Token: 0x04000021 RID: 33
		public ConstructionSiteAccessible _constructionSiteAccessible;

		// Token: 0x04000022 RID: 34
		public readonly SortedSet<GoodAmount> _remainingGoods = new SortedSet<GoodAmount>(new GoodAmountComparer());
	}
}
