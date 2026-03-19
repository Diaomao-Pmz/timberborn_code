using System;

namespace Timberborn.TimeSystem
{
	// Token: 0x0200000F RID: 15
	public interface ITickProgressService
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000071 RID: 113
		float Progress { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000072 RID: 114
		float SecondsPassedThisTick { get; }
	}
}
