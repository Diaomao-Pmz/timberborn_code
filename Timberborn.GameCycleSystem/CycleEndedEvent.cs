using System;

namespace Timberborn.GameCycleSystem
{
	// Token: 0x02000005 RID: 5
	public class CycleEndedEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020BE File Offset: 0x000002BE
		public int Cycle { get; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020C6 File Offset: 0x000002C6
		public CycleEndedEvent(int cycle)
		{
			this.Cycle = cycle;
		}
	}
}
