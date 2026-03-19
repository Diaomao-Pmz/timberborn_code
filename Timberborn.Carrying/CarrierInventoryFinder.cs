using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterNavigation;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Navigation;

namespace Timberborn.Carrying
{
	// Token: 0x0200000A RID: 10
	public class CarrierInventoryFinder : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000022CE File Offset: 0x000004CE
		public CarrierInventoryFinder(CarryAmountCalculator carryAmountCalculator)
		{
			this._carryAmountCalculator = carryAmountCalculator;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022DD File Offset: 0x000004DD
		public void Awake()
		{
			this._goodCarrier = base.GetComponent<GoodCarrier>();
			this._goodReserver = base.GetComponent<GoodReserver>();
			this._navigator = base.GetComponent<Navigator>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002310 File Offset: 0x00000510
		public bool TryCarryFromAnyInventory(string goodId, Inventory receivingInventory)
		{
			return this.TryCarryFromAnyInventoryInternal(goodId, receivingInventory, (Inventory _) => true, null);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002350 File Offset: 0x00000550
		public bool TryCarryFromAnyInventory(string goodId, Inventory receivingInventory, Predicate<Inventory> inventoryFilter)
		{
			return this.TryCarryFromAnyInventoryInternal(goodId, receivingInventory, inventoryFilter, null);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000236F File Offset: 0x0000056F
		public bool TryCarryFromAnyInventoryLimited(string goodId, Inventory receivingInventory, int maxAmount)
		{
			return this.TryCarryFromAnyInventoryInternal(goodId, receivingInventory, (Inventory _) => true, new int?(maxAmount));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023A0 File Offset: 0x000005A0
		public bool TryCarryToAnyInventory(string goodId, Inventory givingInventory, Predicate<Inventory> inventoryFilter)
		{
			Accessible enabledComponent = givingInventory.GetEnabledComponent<Accessible>();
			DistrictCenter district = givingInventory.GetComponent<DistrictBuilding>().District;
			if (district)
			{
				GoodAmount goodAmount = new GoodAmount(goodId, 1);
				float num;
				Inventory inventory = district.GetComponent<DistrictInventoryPicker>().ClosestInventoryWithCapacity(enabledComponent, goodAmount, inventoryFilter, out num);
				if (inventory != null)
				{
					GoodAmount goodAmount2 = this._carryAmountCalculator.AmountToCarry(this._goodCarrier.LiftingCapacity, goodId, inventory, givingInventory);
					if (goodAmount2.Amount > 0)
					{
						this._goodReserver.ReserveNotLessThanStockAmount(givingInventory, goodAmount2);
						this._goodReserver.ReserveCapacity(inventory, goodAmount2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000242C File Offset: 0x0000062C
		public bool TryCarryToInventory(string goodId, Inventory givingInventory, Inventory receivingInventory)
		{
			if (receivingInventory)
			{
				GoodAmount goodAmount = this._carryAmountCalculator.AmountToCarry(this._goodCarrier.LiftingCapacity, goodId, receivingInventory, givingInventory);
				if (goodAmount.Amount > 0)
				{
					this._goodReserver.ReserveNotLessThanStockAmount(givingInventory, goodAmount);
					this._goodReserver.ReserveCapacity(receivingInventory, goodAmount);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002484 File Offset: 0x00000684
		public Inventory GetClosestInventoryWithCapacity(string goodId, Accessible accessible, out float distance)
		{
			GoodAmount goodAmount = new GoodAmount(goodId, 1);
			distance = float.MaxValue;
			DistrictCenter district = CarrierInventoryFinder.GetDistrict(accessible);
			if (district != null)
			{
				return district.GetComponent<DistrictInventoryPicker>().ClosestInventoryWithCapacity(accessible, goodAmount, (Inventory _) => true, out distance);
			}
			if (this._citizen.HasAssignedDistrict)
			{
				DistrictInventoryPicker component = this._citizen.AssignedDistrict.GetComponent<DistrictInventoryPicker>();
				return component.ClosestInventoryWithCapacity(accessible.Transform.position, goodAmount, out distance) ?? component.ClosestInventoryWithCapacity(this._navigator.CurrentAccessOrPosition(), goodAmount, out distance);
			}
			return null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002524 File Offset: 0x00000724
		public bool TryCarryFromAnyInventoryInternal(string goodId, Inventory receivingInventory, Predicate<Inventory> inventoryFilter, int? maxAmount = null)
		{
			Accessible enabledComponent = receivingInventory.GetEnabledComponent<Accessible>();
			DistrictCenter district = receivingInventory.GetComponent<DistrictBuilding>().District;
			if (district)
			{
				Inventory inventory = district.GetComponent<DistrictInventoryPicker>().ClosestInventoryWithStock(enabledComponent, goodId, inventoryFilter);
				if (inventory != null && this.TryReserveInventories(goodId, receivingInventory, inventory, maxAmount))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002570 File Offset: 0x00000770
		public bool TryReserveInventories(string goodId, Inventory receivingInventory, Inventory givingInventory, int? maxAmount)
		{
			GoodAmount carriableGood = this.GetCarriableGood(goodId, receivingInventory, givingInventory, maxAmount);
			if (carriableGood.Amount > 0)
			{
				if (maxAmount != null)
				{
					this._goodReserver.ReserveExactStockAmount(givingInventory, carriableGood);
				}
				else
				{
					this._goodReserver.ReserveNotLessThanStockAmount(givingInventory, carriableGood);
				}
				this._goodReserver.ReserveCapacity(receivingInventory, carriableGood);
				return true;
			}
			return false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025C8 File Offset: 0x000007C8
		public GoodAmount GetCarriableGood(string goodId, Inventory receivingInventory, Inventory givingInventory, int? maxAmount)
		{
			GoodAmount result = this._carryAmountCalculator.AmountToCarry(this._goodCarrier.LiftingCapacity, goodId, receivingInventory, givingInventory);
			if (maxAmount != null)
			{
				int amount = result.Amount;
				int? num = maxAmount;
				if (amount > num.GetValueOrDefault() & num != null)
				{
					return new GoodAmount(goodId, maxAmount.Value);
				}
			}
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002624 File Offset: 0x00000824
		public static DistrictCenter GetDistrict(Accessible accessible)
		{
			DistrictBuilding component = accessible.GetComponent<DistrictBuilding>();
			if (!component)
			{
				return null;
			}
			return component.GetDistrictOrConstructionDistrict();
		}

		// Token: 0x04000010 RID: 16
		public readonly CarryAmountCalculator _carryAmountCalculator;

		// Token: 0x04000011 RID: 17
		public GoodCarrier _goodCarrier;

		// Token: 0x04000012 RID: 18
		public GoodReserver _goodReserver;

		// Token: 0x04000013 RID: 19
		public Navigator _navigator;

		// Token: 0x04000014 RID: 20
		public Citizen _citizen;
	}
}
