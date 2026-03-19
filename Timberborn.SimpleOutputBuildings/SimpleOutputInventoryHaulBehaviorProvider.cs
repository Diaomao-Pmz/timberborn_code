using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.Emptying;
using Timberborn.Hauling;
using Timberborn.InventorySystem;

namespace Timberborn.SimpleOutputBuildings
{
	// Token: 0x0200000A RID: 10
	public class SimpleOutputInventoryHaulBehaviorProvider : BaseComponent, IAwakableComponent, IHaulBehaviorProvider
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021D1 File Offset: 0x000003D1
		public SimpleOutputInventoryHaulBehaviorProvider(InventoryFillCalculator inventoryFillCalculator)
		{
			this._inventoryFillCalculator = inventoryFillCalculator;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021E0 File Offset: 0x000003E0
		public void Awake()
		{
			this._emptyOutputWorkplaceBehavior = base.GetComponent<EmptyOutputWorkplaceBehavior>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._inventory = base.GetComponent<SimpleOutputInventory>().Inventory;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000220C File Offset: 0x0000040C
		public void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors)
		{
			if (this._inventory.Enabled && this._blockableObject.IsUnblocked)
			{
				float inStockOutputFillPercentage = this._inventoryFillCalculator.GetInStockOutputFillPercentage(this._inventory);
				weightedBehaviors.Add(new WeightedBehavior(inStockOutputFillPercentage, this._emptyOutputWorkplaceBehavior));
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly InventoryFillCalculator _inventoryFillCalculator;

		// Token: 0x0400000B RID: 11
		public EmptyOutputWorkplaceBehavior _emptyOutputWorkplaceBehavior;

		// Token: 0x0400000C RID: 12
		public BlockableObject _blockableObject;

		// Token: 0x0400000D RID: 13
		public Inventory _inventory;
	}
}
