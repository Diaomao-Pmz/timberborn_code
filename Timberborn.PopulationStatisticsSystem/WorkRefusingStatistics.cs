using System;

namespace Timberborn.PopulationStatisticsSystem
{
	// Token: 0x0200000B RID: 11
	public readonly struct WorkRefusingStatistics
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002237 File Offset: 0x00000437
		public int RefusingWorkers { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000223F File Offset: 0x0000043F
		public int NotRefusingWorkers { get; }

		// Token: 0x06000018 RID: 24 RVA: 0x00002247 File Offset: 0x00000447
		public WorkRefusingStatistics(int refusingWorkers, int notRefusingWorkers)
		{
			this.RefusingWorkers = refusingWorkers;
			this.NotRefusingWorkers = notRefusingWorkers;
		}
	}
}
