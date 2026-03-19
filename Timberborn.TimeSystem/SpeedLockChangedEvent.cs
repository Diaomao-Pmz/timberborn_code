using System;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000014 RID: 20
	public class SpeedLockChangedEvent
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public bool IsLocked { get; }

		// Token: 0x06000083 RID: 131 RVA: 0x00002BAF File Offset: 0x00000DAF
		public SpeedLockChangedEvent(bool isLocked)
		{
			this.IsLocked = isLocked;
		}
	}
}
