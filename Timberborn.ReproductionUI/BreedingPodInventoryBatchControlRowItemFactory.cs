using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.InventorySystemBatchControl;
using Timberborn.Reproduction;

namespace Timberborn.ReproductionUI
{
	// Token: 0x0200000A RID: 10
	public class BreedingPodInventoryBatchControlRowItemFactory
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000251D File Offset: 0x0000071D
		public BreedingPodInventoryBatchControlRowItemFactory(InventoryCapacityBatchControlRowItemFactory inventoryCapacityBatchControlRowItemFactory)
		{
			this._inventoryCapacityBatchControlRowItemFactory = inventoryCapacityBatchControlRowItemFactory;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000252C File Offset: 0x0000072C
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			BreedingPod component = entity.GetComponent<BreedingPod>();
			if (!component)
			{
				return null;
			}
			return this._inventoryCapacityBatchControlRowItemFactory.Create(component.Inventory);
		}

		// Token: 0x0400001E RID: 30
		public readonly InventoryCapacityBatchControlRowItemFactory _inventoryCapacityBatchControlRowItemFactory;
	}
}
