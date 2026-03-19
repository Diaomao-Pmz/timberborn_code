using System;

namespace Timberborn.BatchControl
{
	// Token: 0x0200001C RID: 28
	public class BatchControlTabShownEvent
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003BFA File Offset: 0x00001DFA
		public BatchControlTab BatchControlTab { get; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00003C02 File Offset: 0x00001E02
		public BatchControlTabShownEvent(BatchControlTab batchControlTab)
		{
			this.BatchControlTab = batchControlTab;
		}
	}
}
