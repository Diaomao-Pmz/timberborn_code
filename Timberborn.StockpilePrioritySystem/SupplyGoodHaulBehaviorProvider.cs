using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.Hauling;
using Timberborn.InventorySystem;
using Timberborn.Stockpiles;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x0200000C RID: 12
	public class SupplyGoodHaulBehaviorProvider : BaseComponent, IAwakableComponent, IHaulBehaviorProvider
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000287B File Offset: 0x00000A7B
		public SupplyGoodHaulBehaviorProvider(InventoryFillCalculator inventoryFillCalculator)
		{
			this._inventoryFillCalculator = inventoryFillCalculator;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000288A File Offset: 0x00000A8A
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._goodSupplier = base.GetComponent<GoodSupplier>();
			this._supplyGoodWorkplaceBehavior = base.GetComponent<SupplyGoodWorkplaceBehavior>();
			this._inventory = base.GetComponent<Stockpile>().Inventory;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028C4 File Offset: 0x00000AC4
		public void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors)
		{
			if (this._goodSupplier.IsSupplying && this._blockableObject.IsUnblocked)
			{
				float inputFillPercentage = this._inventoryFillCalculator.GetInputFillPercentage(this._inventory);
				weightedBehaviors.Add(new WeightedBehavior(inputFillPercentage, this._supplyGoodWorkplaceBehavior));
			}
		}

		// Token: 0x0400001C RID: 28
		public readonly InventoryFillCalculator _inventoryFillCalculator;

		// Token: 0x0400001D RID: 29
		public BlockableObject _blockableObject;

		// Token: 0x0400001E RID: 30
		public GoodSupplier _goodSupplier;

		// Token: 0x0400001F RID: 31
		public SupplyGoodWorkplaceBehavior _supplyGoodWorkplaceBehavior;

		// Token: 0x04000020 RID: 32
		public Inventory _inventory;
	}
}
