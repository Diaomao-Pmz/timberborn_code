using System;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000009 RID: 9
	public class CurrentSpeedChangedEvent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023A5 File Offset: 0x000005A5
		public float CurrentSpeed { get; }

		// Token: 0x0600001E RID: 30 RVA: 0x000023AD File Offset: 0x000005AD
		public CurrentSpeedChangedEvent(float currentSpeed)
		{
			this.CurrentSpeed = currentSpeed;
		}
	}
}
