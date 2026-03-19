using System;

namespace Timberborn.Automation
{
	// Token: 0x0200000C RID: 12
	public class AutomationPlanVersioner
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002F54 File Offset: 0x00001154
		public long AcquirePlanVersion()
		{
			long num = this._planVersion + 1L;
			this._planVersion = num;
			return num;
		}

		// Token: 0x04000022 RID: 34
		public long _planVersion;
	}
}
