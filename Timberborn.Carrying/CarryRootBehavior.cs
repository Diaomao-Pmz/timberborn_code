using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Navigation;
using Timberborn.WalkingSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Carrying
{
	// Token: 0x0200000E RID: 14
	public class CarryRootBehavior : RootBehavior, IAwakableComponent, IStartableComponent, IJobBehavior
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000027CC File Offset: 0x000009CC
		public CarryRootBehavior(CarryAmountCalculator carryAmountCalculator)
		{
			this._carryAmountCalculator = carryAmountCalculator;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027DB File Offset: 0x000009DB
		public void Awake()
		{
			this._goodCarrier = base.GetComponent<GoodCarrier>();
			this._goodReserver = base.GetComponent<GoodReserver>();
			this._goodCarrierCapacityReserver = base.GetComponent<GoodCarrierCapacityReserver>();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002801 File Offset: 0x00000A01
		public void Start()
		{
			this._walkToAccessibleExecutor = base.GetComponent<WalkToAccessibleExecutor>();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002810 File Offset: 0x00000A10
		public override Decision Decide(BehaviorAgent agent)
		{
			Decision result;
			if (this.TryToDeliver(out result))
			{
				return result;
			}
			Decision result2;
			if (this.TryToRetrieve(out result2))
			{
				return result2;
			}
			if (this._goodReserver.HasReservedStock)
			{
				this._goodReserver.UnreserveStock();
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002854 File Offset: 0x00000A54
		public bool TryToDeliver(out Decision decision)
		{
			if (this._goodCarrier.IsCarrying)
			{
				if (!this._goodReserver.HasReservedCapacity && !this._goodCarrierCapacityReserver.ReserveCapacityForCarrier())
				{
					this._goodCarrier.EmptyHands();
					decision = Decision.ReleaseNow();
				}
				else
				{
					Accessible enabledComponent = this._goodReserver.CapacityReservation.Inventory.GetEnabledComponent<Accessible>();
					Decision decision2;
					switch (this._walkToAccessibleExecutor.Launch(enabledComponent))
					{
					case ExecutorStatus.Success:
						decision2 = this.CompleteDelivery();
						break;
					case ExecutorStatus.Failure:
						decision2 = this.UnreserveCapacity();
						break;
					case ExecutorStatus.Running:
						decision2 = Decision.ReturnWhenFinished(this._walkToAccessibleExecutor);
						break;
					default:
						throw new ArgumentOutOfRangeException();
					}
					decision = decision2;
				}
				return true;
			}
			decision = default(Decision);
			return false;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002914 File Offset: 0x00000B14
		public Decision CompleteDelivery()
		{
			GoodReservation capacityReservation = this._goodReserver.CapacityReservation;
			this._goodReserver.UnreserveCapacity();
			if (capacityReservation.Inventory.HasUnreservedCapacity(capacityReservation.GoodAmount))
			{
				capacityReservation.Inventory.Give(capacityReservation.GoodAmount);
				this._goodCarrier.EmptyHands();
				return Decision.ReleaseNow();
			}
			return Decision.ReleaseNextTick();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002976 File Offset: 0x00000B76
		public Decision UnreserveCapacity()
		{
			this._goodReserver.UnreserveCapacity();
			return Decision.ReleaseNextTick();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002988 File Offset: 0x00000B88
		public bool TryToRetrieve(out Decision decision)
		{
			if (this._goodReserver.HasReservedCapacity)
			{
				if (this._goodReserver.HasReservedStock)
				{
					Accessible enabledComponent = this._goodReserver.StockReservation.Inventory.GetEnabledComponent<Accessible>();
					Decision decision2;
					switch (this._walkToAccessibleExecutor.LaunchIgnoringAccessibleValidity(enabledComponent))
					{
					case ExecutorStatus.Success:
						decision2 = this.CompleteRetrieval();
						break;
					case ExecutorStatus.Failure:
						decision2 = this.UnreserveStock();
						break;
					case ExecutorStatus.Running:
						decision2 = Decision.ReturnWhenFinished(this._walkToAccessibleExecutor);
						break;
					default:
						throw new ArgumentOutOfRangeException();
					}
					decision = decision2;
				}
				else
				{
					this._goodReserver.UnreserveCapacity();
					decision = Decision.ReleaseNextTick();
				}
				return true;
			}
			decision = default(Decision);
			return false;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A3C File Offset: 0x00000C3C
		public Decision CompleteRetrieval()
		{
			GoodReservation stockReservation = this._goodReserver.StockReservation;
			this._goodReserver.UnreserveStock();
			GoodAmount goodAmount = stockReservation.FixedAmount ? stockReservation.GoodAmount : this.RecalculateAmountToRetrieve(stockReservation);
			stockReservation.Inventory.Take(goodAmount);
			GoodCarrier goodCarrier = this._goodCarrier;
			GoodAmount goods = goodAmount;
			Inventory inventory = this._goodReserver.CapacityReservation.Inventory;
			goodCarrier.PutGoodsInHands(goods, inventory != null && inventory.Gives(goodAmount.GoodId));
			Decision result;
			if (!this.TryToDeliver(out result))
			{
				return Decision.ReturnNextTick();
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002ACA File Offset: 0x00000CCA
		public Decision UnreserveStock()
		{
			this._goodReserver.UnreserveStock();
			return Decision.ReleaseNextTick();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002ADC File Offset: 0x00000CDC
		public GoodAmount RecalculateAmountToRetrieve(GoodReservation goodReservation)
		{
			GoodReservation capacityReservation = this._goodReserver.CapacityReservation;
			this._goodReserver.UnreserveCapacity();
			string goodId = goodReservation.GoodAmount.GoodId;
			GoodAmount goodAmount = this._carryAmountCalculator.AmountToCarry(this._goodCarrier.LiftingCapacity, goodId, capacityReservation.Inventory, goodReservation.Inventory);
			this._goodReserver.ReserveCapacity(capacityReservation.Inventory, goodAmount);
			return goodAmount;
		}

		// Token: 0x0400001A RID: 26
		public readonly CarryAmountCalculator _carryAmountCalculator;

		// Token: 0x0400001B RID: 27
		public GoodCarrier _goodCarrier;

		// Token: 0x0400001C RID: 28
		public GoodReserver _goodReserver;

		// Token: 0x0400001D RID: 29
		public GoodCarrierCapacityReserver _goodCarrierCapacityReserver;

		// Token: 0x0400001E RID: 30
		public WalkToAccessibleExecutor _walkToAccessibleExecutor;
	}
}
