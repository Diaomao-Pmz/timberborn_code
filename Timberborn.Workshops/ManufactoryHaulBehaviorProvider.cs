using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.Emptying;
using Timberborn.Hauling;
using Timberborn.InventorySystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000014 RID: 20
	public class ManufactoryHaulBehaviorProvider : BaseComponent, IAwakableComponent, IHaulBehaviorProvider
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000326D File Offset: 0x0000146D
		public ManufactoryHaulBehaviorProvider(InventoryFillCalculator inventoryFillCalculator)
		{
			this._inventoryFillCalculator = inventoryFillCalculator;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000327C File Offset: 0x0000147C
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._inventories = base.GetComponent<Inventories>();
			this._fillInputWorkplaceBehavior = base.GetComponent<FillInputWorkplaceBehavior>();
			this._emptyOutputWorkplaceBehavior = base.GetComponent<EmptyOutputWorkplaceBehavior>();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000032BC File Offset: 0x000014BC
		public void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors)
		{
			if (this._manufactory && this._manufactory.HasCurrentRecipe && this._blockableObject.IsUnblocked)
			{
				foreach (Inventory inventory in this._inventories.EnabledInventories)
				{
					if (inventory.IsInput)
					{
						float num = 1f - this._inventoryFillCalculator.GetInputFillPercentage(inventory);
						if (num > 0f)
						{
							weightedBehaviors.Add(new WeightedBehavior(num, this._fillInputWorkplaceBehavior));
						}
					}
					if (inventory.IsOutput)
					{
						float outputFillPercentage = this._inventoryFillCalculator.GetOutputFillPercentage(inventory);
						if (outputFillPercentage > 0f)
						{
							weightedBehaviors.Add(new WeightedBehavior(outputFillPercentage, this._emptyOutputWorkplaceBehavior));
						}
					}
				}
			}
		}

		// Token: 0x0400003B RID: 59
		public readonly InventoryFillCalculator _inventoryFillCalculator;

		// Token: 0x0400003C RID: 60
		public Manufactory _manufactory;

		// Token: 0x0400003D RID: 61
		public BlockableObject _blockableObject;

		// Token: 0x0400003E RID: 62
		public Inventories _inventories;

		// Token: 0x0400003F RID: 63
		public FillInputWorkplaceBehavior _fillInputWorkplaceBehavior;

		// Token: 0x04000040 RID: 64
		public EmptyOutputWorkplaceBehavior _emptyOutputWorkplaceBehavior;
	}
}
