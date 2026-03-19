using System;
using Timberborn.BatchControl;
using Timberborn.EntitySystem;
using Timberborn.GoodConsumingBuildingSystem;
using Timberborn.InventorySystemBatchControl;

namespace Timberborn.AttractionsBatchControl
{
	// Token: 0x0200000A RID: 10
	public class GoodConsumingAttractionBatchControlRowItemFactory
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000024D3 File Offset: 0x000006D3
		public GoodConsumingAttractionBatchControlRowItemFactory(InventoryCapacityBatchControlRowItemFactory inventoryCapacityBatchControlRowItemFactory)
		{
			this._inventoryCapacityBatchControlRowItemFactory = inventoryCapacityBatchControlRowItemFactory;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024E4 File Offset: 0x000006E4
		public IBatchControlRowItem Create(EntityComponent entity)
		{
			GoodConsumingBuilding component = entity.GetComponent<GoodConsumingBuilding>();
			if (component != null)
			{
				return this._inventoryCapacityBatchControlRowItemFactory.Create(component.Inventory);
			}
			return null;
		}

		// Token: 0x0400001C RID: 28
		public readonly InventoryCapacityBatchControlRowItemFactory _inventoryCapacityBatchControlRowItemFactory;
	}
}
