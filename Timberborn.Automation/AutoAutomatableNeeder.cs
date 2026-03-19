using System;

namespace Timberborn.Automation
{
	// Token: 0x02000004 RID: 4
	public class AutoAutomatableNeeder : IAutomatableNeeder
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public AutoAutomatableNeeder(AutomatorRegistry automatorRegistry)
		{
			this._automatorRegistry = automatorRegistry;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CF File Offset: 0x000002CF
		public bool NeedsAutomatable
		{
			get
			{
				return this._automatorRegistry.AnyTransmitters();
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly AutomatorRegistry _automatorRegistry;
	}
}
