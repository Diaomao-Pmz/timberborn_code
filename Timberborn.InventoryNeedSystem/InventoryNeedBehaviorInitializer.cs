using System;
using Timberborn.Goods;
using Timberborn.InventorySystem;

namespace Timberborn.InventoryNeedSystem
{
	// Token: 0x02000008 RID: 8
	public class InventoryNeedBehaviorInitializer
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002677 File Offset: 0x00000877
		public InventoryNeedBehaviorInitializer(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002686 File Offset: 0x00000886
		public void AddNeedBehavior(Inventory inventory)
		{
			if (!inventory.GetComponent<InventoryGoodConsumptionBlocker>() && this.AllowsTakeableConsumableGood(inventory))
			{
				inventory.GetComponent<InventoryNeedBehavior>().Initialize(inventory);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026AC File Offset: 0x000008AC
		public bool AllowsTakeableConsumableGood(Inventory inventory)
		{
			foreach (StorableGoodAmount storableGoodAmount in inventory.AllowedGoods)
			{
				if (storableGoodAmount.StorableGood.Takeable && this._goodService.GetGood(storableGoodAmount.StorableGood.GoodId).HasConsumptionEffects)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000010 RID: 16
		public readonly IGoodService _goodService;
	}
}
