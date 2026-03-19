using System;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200000B RID: 11
	public interface IAmountProvider
	{
		// Token: 0x0600003E RID: 62
		int UnreservedAmountInStock(string goodId);

		// Token: 0x0600003F RID: 63
		int UnreservedCapacity(string goodId);
	}
}
