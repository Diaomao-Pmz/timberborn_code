using System;
using Timberborn.Goods;
using Timberborn.InventorySystem;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x02000008 RID: 8
	public interface IGoodProcessor
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33
		Inventory Inventory { get; }

		// Token: 0x06000022 RID: 34
		GoodRegistry GetProcessingGoods();
	}
}
