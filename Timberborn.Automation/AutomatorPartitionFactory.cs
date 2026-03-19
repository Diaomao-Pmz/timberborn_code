using System;

namespace Timberborn.Automation
{
	// Token: 0x02000014 RID: 20
	public class AutomatorPartitionFactory
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000042FF File Offset: 0x000024FF
		public AutomatorPartitionFactory(AutomationPlanVersioner automationPlanVersioner, AutomationDebugger automationDebugger)
		{
			this._automationPlanVersioner = automationPlanVersioner;
			this._automationDebugger = automationDebugger;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004315 File Offset: 0x00002515
		public AutomatorPartition Create()
		{
			return new AutomatorPartition(new AutomationPlan(this._automationPlanVersioner), this._automationDebugger);
		}

		// Token: 0x0400005B RID: 91
		public readonly AutomationPlanVersioner _automationPlanVersioner;

		// Token: 0x0400005C RID: 92
		public readonly AutomationDebugger _automationDebugger;
	}
}
