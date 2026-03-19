using System;

namespace Timberborn.GameCycleSystem
{
	// Token: 0x0200000A RID: 10
	public interface ICycleDuration
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26
		int DurationInDays { get; }

		// Token: 0x0600001B RID: 27
		void SetForCycle(int cycle);
	}
}
