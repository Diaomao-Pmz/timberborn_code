using System;

namespace Timberborn.GameCycleSystem
{
	// Token: 0x02000006 RID: 6
	public class CycleStartedEvent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D5 File Offset: 0x000002D5
		public int Cycle { get; }

		// Token: 0x06000007 RID: 7 RVA: 0x000020DD File Offset: 0x000002DD
		public CycleStartedEvent(int cycle)
		{
			this.Cycle = cycle;
		}
	}
}
