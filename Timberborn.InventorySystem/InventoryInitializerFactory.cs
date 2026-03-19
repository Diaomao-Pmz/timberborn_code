using System;
using Timberborn.Goods;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200001A RID: 26
	public class InventoryInitializerFactory
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x0000432F File Offset: 0x0000252F
		public InventoryInitializerFactory(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000433E File Offset: 0x0000253E
		public InventoryInitializer Create(Inventory inventory, int capacity, string componentName)
		{
			return new InventoryInitializer(this._goodService, inventory, capacity, componentName);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000434E File Offset: 0x0000254E
		public InventoryInitializer CreateWithUnlimitedCapacity(Inventory inventory, string componentName)
		{
			return this.Create(inventory, int.MaxValue, componentName);
		}

		// Token: 0x04000053 RID: 83
		public readonly IGoodService _goodService;
	}
}
