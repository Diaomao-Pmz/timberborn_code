using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.Hauling;
using Timberborn.InventorySystem;

namespace Timberborn.Reproduction
{
	// Token: 0x0200000D RID: 13
	public class BringNutrientHaulBehaviorProvider : BaseComponent, IAwakableComponent, IHaulBehaviorProvider
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public BringNutrientHaulBehaviorProvider(InventoryFillCalculator inventoryFillCalculator)
		{
			this._inventoryFillCalculator = inventoryFillCalculator;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BB6 File Offset: 0x00000DB6
		public void Awake()
		{
			this._breedingPod = base.GetComponent<BreedingPod>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._bringNutrientWorkplaceBehavior = base.GetComponent<BringNutrientWorkplaceBehavior>();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors)
		{
			if (this._breedingPod && this._blockableObject.IsUnblocked)
			{
				Inventory inventory = this._breedingPod.Inventory;
				float weight = 1f - this._inventoryFillCalculator.GetInputFillPercentage(inventory);
				weightedBehaviors.Add(new WeightedBehavior(weight, this._bringNutrientWorkplaceBehavior));
			}
		}

		// Token: 0x04000022 RID: 34
		public readonly InventoryFillCalculator _inventoryFillCalculator;

		// Token: 0x04000023 RID: 35
		public BreedingPod _breedingPod;

		// Token: 0x04000024 RID: 36
		public BlockableObject _blockableObject;

		// Token: 0x04000025 RID: 37
		public BringNutrientWorkplaceBehavior _bringNutrientWorkplaceBehavior;
	}
}
