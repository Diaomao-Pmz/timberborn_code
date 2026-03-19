using System;

namespace Timberborn.LevelVisibilitySystem
{
	// Token: 0x02000008 RID: 8
	public class MaxVisibleLevelChangedEvent
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000244F File Offset: 0x0000064F
		public int OldMaxVisibleLevel { get; }

		// Token: 0x06000027 RID: 39 RVA: 0x00002457 File Offset: 0x00000657
		public MaxVisibleLevelChangedEvent(int oldMaxVisibleLevel)
		{
			this.OldMaxVisibleLevel = oldMaxVisibleLevel;
		}
	}
}
