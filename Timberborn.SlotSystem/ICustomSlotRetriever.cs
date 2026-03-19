using System;
using System.Collections.Generic;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000009 RID: 9
	public interface ICustomSlotRetriever
	{
		// Token: 0x0600001E RID: 30
		bool TryGetUnassignedSlot(IEnumerable<ISlot> slots, out ISlot slot);
	}
}
