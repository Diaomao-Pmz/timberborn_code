using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Carrying;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Navigation;

namespace Timberborn.Emptying
{
	// Token: 0x0200000A RID: 10
	public class EmptyingStarter : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002756 File Offset: 0x00000956
		public EmptyingStarter(CarryAmountCalculator carryAmountCalculator)
		{
			this._carryAmountCalculator = carryAmountCalculator;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002765 File Offset: 0x00000965
		public void Awake()
		{
			this._carrierInventoryFinder = base.GetComponent<CarrierInventoryFinder>();
			this._goodCarrier = base.GetComponent<GoodCarrier>();
			this._goodReserver = base.GetComponent<GoodReserver>();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000278B File Offset: 0x0000098B
		public bool StartEmptying(Inventory inventory)
		{
			return !inventory.IsEmpty && this.StartEmptying(inventory, false);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000279F File Offset: 0x0000099F
		public bool StartEmptyingUnwantedStock(Inventory inventory)
		{
			return !inventory.IsEmpty && this.StartEmptying(inventory, true);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027B4 File Offset: 0x000009B4
		public bool StartEmptying(Inventory inventory, bool unwantedStock)
		{
			IEnumerable<GoodAmount> goods = unwantedStock ? inventory.UnreservedUnwantedStock() : EmptyingStarter.GetUnreservedGoods(inventory);
			ValueTuple<GoodAmount, Inventory> carriableGood = this.GetCarriableGood(inventory, goods);
			GoodAmount item = carriableGood.Item1;
			Inventory item2 = carriableGood.Item2;
			if (item.Amount > 0)
			{
				if (unwantedStock)
				{
					this._goodReserver.ReserveExactStockAmount(inventory, item);
				}
				else
				{
					this._goodReserver.ReserveNotLessThanStockAmount(inventory, item);
				}
				this._goodReserver.ReserveCapacity(item2, item);
				return true;
			}
			return false;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002824 File Offset: 0x00000A24
		public ValueTuple<GoodAmount, Inventory> GetCarriableGood(Inventory inventory, IEnumerable<GoodAmount> goods)
		{
			foreach (GoodAmount good in goods)
			{
				Accessible enabledComponent = inventory.GetEnabledComponent<Accessible>();
				float num;
				Inventory closestInventoryWithCapacity = this._carrierInventoryFinder.GetClosestInventoryWithCapacity(good.GoodId, enabledComponent, out num);
				if (closestInventoryWithCapacity != null)
				{
					GoodAmount item = this._carryAmountCalculator.AmountToCarry(this._goodCarrier.LiftingCapacity, good, closestInventoryWithCapacity);
					if (item.Amount > 0)
					{
						return new ValueTuple<GoodAmount, Inventory>(item, closestInventoryWithCapacity);
					}
				}
			}
			return default(ValueTuple<GoodAmount, Inventory>);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028C4 File Offset: 0x00000AC4
		public static IEnumerable<GoodAmount> GetUnreservedGoods(Inventory inventory)
		{
			Emptiable component = inventory.GetComponent<Emptiable>();
			if (!component || !component.IsMarkedForEmptying)
			{
				return inventory.UnreservedTakeableStock();
			}
			return inventory.UnreservedStock();
		}

		// Token: 0x04000016 RID: 22
		public readonly CarryAmountCalculator _carryAmountCalculator;

		// Token: 0x04000017 RID: 23
		public CarrierInventoryFinder _carrierInventoryFinder;

		// Token: 0x04000018 RID: 24
		public GoodCarrier _goodCarrier;

		// Token: 0x04000019 RID: 25
		public GoodReserver _goodReserver;
	}
}
