using System;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200001D RID: 29
	public class NullGoodDisallower : IGoodDisallower
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060000E1 RID: 225 RVA: 0x00004800 File Offset: 0x00002A00
		// (remove) Token: 0x060000E2 RID: 226 RVA: 0x00004800 File Offset: 0x00002A00
		public event EventHandler<DisallowedGoodsChangedEventArgs> DisallowedGoodsChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004802 File Offset: 0x00002A02
		public int AllowedAmount(string goodId)
		{
			return int.MaxValue;
		}
	}
}
