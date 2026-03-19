using System;

namespace Timberborn.Debugging
{
	// Token: 0x0200000C RID: 12
	public class DevModeToggledEvent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023A9 File Offset: 0x000005A9
		public bool Enabled { get; }

		// Token: 0x06000025 RID: 37 RVA: 0x000023B1 File Offset: 0x000005B1
		public DevModeToggledEvent(bool enabled)
		{
			this.Enabled = enabled;
		}
	}
}
