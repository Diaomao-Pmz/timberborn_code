using System;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200000D RID: 13
	public interface IInitializableGoodDisallower : IGoodDisallower
	{
		// Token: 0x06000043 RID: 67
		void Initialize(Inventory inventory);
	}
}
