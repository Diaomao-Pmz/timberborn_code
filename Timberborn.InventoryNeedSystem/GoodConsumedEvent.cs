using System;

namespace Timberborn.InventoryNeedSystem
{
	// Token: 0x02000004 RID: 4
	public struct GoodConsumedEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public readonly string GoodId { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public GoodConsumedEvent(string goodId)
		{
			this.GoodId = goodId;
		}
	}
}
