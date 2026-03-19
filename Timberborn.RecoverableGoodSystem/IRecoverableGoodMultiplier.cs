using System;
using Timberborn.InventorySystem;

namespace Timberborn.RecoverableGoodSystem
{
	// Token: 0x02000005 RID: 5
	public interface IRecoverableGoodMultiplier
	{
		// Token: 0x06000009 RID: 9
		float GetMultiplierForInventory(Inventory inventory);
	}
}
