using System;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000016 RID: 22
	public readonly struct InventoryChangedEventArgs
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004058 File Offset: 0x00002258
		public string GoodId { get; }

		// Token: 0x060000BE RID: 190 RVA: 0x00004060 File Offset: 0x00002260
		public InventoryChangedEventArgs(string goodId)
		{
			this.GoodId = goodId;
		}
	}
}
