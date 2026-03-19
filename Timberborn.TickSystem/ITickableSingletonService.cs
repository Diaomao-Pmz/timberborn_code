using System;

namespace Timberborn.TickSystem
{
	// Token: 0x0200000B RID: 11
	public interface ITickableSingletonService
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000F RID: 15
		// (remove) Token: 0x06000010 RID: 16
		event EventHandler ForcedParallelTickFinished;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17
		TimeSpan LastParallelTickDuration { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18
		bool ParalleTicklIsFinished { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19
		bool IsStartingParallelTick { get; }

		// Token: 0x06000014 RID: 20
		void TickAll();

		// Token: 0x06000015 RID: 21
		void ForceFinishParallelTick();
	}
}
