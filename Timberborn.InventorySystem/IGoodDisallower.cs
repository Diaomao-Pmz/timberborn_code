using System;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200000C RID: 12
	public interface IGoodDisallower
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000040 RID: 64
		// (remove) Token: 0x06000041 RID: 65
		event EventHandler<DisallowedGoodsChangedEventArgs> DisallowedGoodsChanged;

		// Token: 0x06000042 RID: 66
		int AllowedAmount(string goodId);
	}
}
