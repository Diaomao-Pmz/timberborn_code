using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000012 RID: 18
	public class PrioritySlotRetriever : BaseComponent, IAwakableComponent, ICustomSlotRetriever
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002F1F File Offset: 0x0000111F
		public void Awake()
		{
			this._prioritySlotRetrieverSpec = base.GetComponent<PrioritySlotRetrieverSpec>();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F30 File Offset: 0x00001130
		public bool TryGetUnassignedSlot(IEnumerable<ISlot> slots, out ISlot slot)
		{
			List<ISlot> list = slots.ToList<ISlot>();
			foreach (string b in this._prioritySlotRetrieverSpec.PrioritySlotNames)
			{
				foreach (ISlot slot2 in list)
				{
					if (slot2.Name == b && !slot2.AssignedEnterer)
					{
						slot = slot2;
						return true;
					}
				}
			}
			slot = null;
			return false;
		}

		// Token: 0x04000026 RID: 38
		public PrioritySlotRetrieverSpec _prioritySlotRetrieverSpec;
	}
}
