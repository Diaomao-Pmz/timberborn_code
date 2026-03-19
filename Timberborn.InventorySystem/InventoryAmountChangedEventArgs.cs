using System;
using Timberborn.Goods;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000015 RID: 21
	public readonly struct InventoryAmountChangedEventArgs
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004047 File Offset: 0x00002247
		public GoodAmount GoodAmount { get; }

		// Token: 0x060000BC RID: 188 RVA: 0x0000404F File Offset: 0x0000224F
		public InventoryAmountChangedEventArgs(GoodAmount goodAmount)
		{
			this.GoodAmount = goodAmount;
		}
	}
}
