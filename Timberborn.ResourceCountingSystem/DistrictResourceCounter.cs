using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x02000005 RID: 5
	public class DistrictResourceCounter : TickableComponent, IAwakableComponent, IPostInitializableEntity
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002260 File Offset: 0x00000460
		public void Awake()
		{
			this._districtInventoryRegistry = base.GetComponent<DistrictInventoryRegistry>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000226E File Offset: 0x0000046E
		public void PostInitializeEntity()
		{
			this.UpdateCounters();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000226E File Offset: 0x0000046E
		public override void StartTickable()
		{
			this.UpdateCounters();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000226E File Offset: 0x0000046E
		public override void Tick()
		{
			this.UpdateCounters();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000226E File Offset: 0x0000046E
		[OnEvent]
		public void OnNewGameInitialized(NewGameInitializedEvent newGameInitializedEvent)
		{
			this.UpdateCounters();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002278 File Offset: 0x00000478
		public ResourceCount GetResourceCount(string goodId)
		{
			return ResourceCount.Create(this._stockCounter.GetInputOutputStock(goodId), this._stockCounter.GetOutputStock(goodId), this._capacityCounter.GetInputOutputCapacity(goodId), this._capacityCounter.GetOutputCapacity(goodId), this._availableCarriedGoods.Amount(goodId), this._reservedCarriedGoods.Amount(goodId), this._processedGoodCounter.GetProcessedStock(goodId), this._processedGoodCounter.GetInputStock(goodId));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022EA File Offset: 0x000004EA
		public void Add(IGoodCarrier goodCarrier)
		{
			this._goodCarriers.Add(goodCarrier);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022F8 File Offset: 0x000004F8
		public void Remove(IGoodCarrier goodCarrier)
		{
			this._goodCarriers.Remove(goodCarrier);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002307 File Offset: 0x00000507
		public void Add(IGoodProcessor goodProcessor)
		{
			this._processedGoodCounter.Add(goodProcessor);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002315 File Offset: 0x00000515
		public void Remove(IGoodProcessor goodProcessor)
		{
			this._processedGoodCounter.Remove(goodProcessor);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002324 File Offset: 0x00000524
		public void UpdateCounters()
		{
			ReadOnlyHashSet<Inventory> inventories = this._districtInventoryRegistry.Inventories;
			this._stockCounter.UpdateStock(inventories);
			this._capacityCounter.UpdateCapacity(inventories);
			this._processedGoodCounter.UpdateStock();
			this.UpdateCarriedGoods();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002368 File Offset: 0x00000568
		public void UpdateCarriedGoods()
		{
			this._availableCarriedGoods.Clear();
			this._reservedCarriedGoods.Clear();
			foreach (IGoodCarrier goodCarrier in this._goodCarriers)
			{
				if (goodCarrier.IsCarrying)
				{
					if (goodCarrier.CountGoodsAsAvailable)
					{
						this._availableCarriedGoods.Add(goodCarrier.CarriedGoods);
					}
					else
					{
						this._reservedCarriedGoods.Add(goodCarrier.CarriedGoods);
					}
				}
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly StockCounter _stockCounter = new StockCounter();

		// Token: 0x0400000A RID: 10
		public readonly CapacityCounter _capacityCounter = new CapacityCounter();

		// Token: 0x0400000B RID: 11
		public readonly ProcessedGoodCounter _processedGoodCounter = new ProcessedGoodCounter();

		// Token: 0x0400000C RID: 12
		public readonly GoodRegistry _availableCarriedGoods = new GoodRegistry();

		// Token: 0x0400000D RID: 13
		public readonly GoodRegistry _reservedCarriedGoods = new GoodRegistry();

		// Token: 0x0400000E RID: 14
		public readonly List<IGoodCarrier> _goodCarriers = new List<IGoodCarrier>();

		// Token: 0x0400000F RID: 15
		public DistrictInventoryRegistry _districtInventoryRegistry;
	}
}
