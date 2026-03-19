using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.Hauling;
using Timberborn.InventorySystem;
using Timberborn.Stockpiles;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x02000007 RID: 7
	public class ObtainGoodHaulBehaviorProvider : BaseComponent, IAwakableComponent, IHaulBehaviorProvider
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000023CF File Offset: 0x000005CF
		public ObtainGoodHaulBehaviorProvider(InventoryFillCalculator inventoryFillCalculator)
		{
			this._inventoryFillCalculator = inventoryFillCalculator;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023DE File Offset: 0x000005DE
		public void Awake()
		{
			this._goodObtainer = base.GetComponent<GoodObtainer>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._inventory = base.GetComponent<Stockpile>().Inventory;
			this._obtainGoodWorkplaceBehavior = base.GetComponent<ObtainGoodWorkplaceBehavior>();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002418 File Offset: 0x00000618
		public void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors)
		{
			if (this._goodObtainer.IsObtaining && this._blockableObject.IsUnblocked)
			{
				float weight = 1f - this._inventoryFillCalculator.GetInputFillPercentage(this._inventory);
				weightedBehaviors.Add(new WeightedBehavior(weight, this._obtainGoodWorkplaceBehavior));
			}
		}

		// Token: 0x04000010 RID: 16
		public readonly InventoryFillCalculator _inventoryFillCalculator;

		// Token: 0x04000011 RID: 17
		public GoodObtainer _goodObtainer;

		// Token: 0x04000012 RID: 18
		public BlockableObject _blockableObject;

		// Token: 0x04000013 RID: 19
		public Inventory _inventory;

		// Token: 0x04000014 RID: 20
		public ObtainGoodWorkplaceBehavior _obtainGoodWorkplaceBehavior;
	}
}
