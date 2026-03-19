using System;

namespace Timberborn.Debugging
{
	// Token: 0x02000007 RID: 7
	public class DebugModeToggledEvent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000220C File Offset: 0x0000040C
		public bool Enabled { get; }

		// Token: 0x06000010 RID: 16 RVA: 0x00002214 File Offset: 0x00000414
		public DebugModeToggledEvent(bool enabled)
		{
			this.Enabled = enabled;
		}
	}
}
