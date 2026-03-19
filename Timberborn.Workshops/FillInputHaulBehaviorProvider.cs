using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.Hauling;
using Timberborn.InventorySystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200000A RID: 10
	public class FillInputHaulBehaviorProvider : BaseComponent, IAwakableComponent, IHaulBehaviorProvider
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000022A9 File Offset: 0x000004A9
		public FillInputHaulBehaviorProvider(InventoryFillCalculator inventoryFillCalculator)
		{
			this._inventoryFillCalculator = inventoryFillCalculator;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B8 File Offset: 0x000004B8
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._inventories = base.GetComponent<Inventories>();
			this._fillInputWorkplaceBehavior = base.GetComponent<FillInputWorkplaceBehavior>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022E0 File Offset: 0x000004E0
		public void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors)
		{
			foreach (Inventory inventory in this._inventories.EnabledInventories)
			{
				if (this._blockableObject.IsUnblocked)
				{
					float weight = 1f - this._inventoryFillCalculator.GetInputFillPercentage(inventory);
					weightedBehaviors.Add(new WeightedBehavior(weight, this._fillInputWorkplaceBehavior));
				}
			}
		}

		// Token: 0x0400000E RID: 14
		public readonly InventoryFillCalculator _inventoryFillCalculator;

		// Token: 0x0400000F RID: 15
		public BlockableObject _blockableObject;

		// Token: 0x04000010 RID: 16
		public Inventories _inventories;

		// Token: 0x04000011 RID: 17
		public FillInputWorkplaceBehavior _fillInputWorkplaceBehavior;
	}
}
