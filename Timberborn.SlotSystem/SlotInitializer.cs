using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000016 RID: 22
	public abstract class SlotInitializer : BaseComponent
	{
		// Token: 0x06000096 RID: 150
		public abstract IEnumerable<ISlot> InitializeSlots();

		// Token: 0x06000097 RID: 151 RVA: 0x000022C4 File Offset: 0x000004C4
		public SlotInitializer()
		{
		}
	}
}
