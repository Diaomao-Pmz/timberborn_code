using System;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000004 RID: 4
	public readonly struct DisallowedGoodsChangedEventArgs
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public string GoodId { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public DisallowedGoodsChangedEventArgs(string goodId)
		{
			this.GoodId = goodId;
		}
	}
}
