using System;
using Timberborn.PopulationStatisticsSystem;

namespace Timberborn.PopulationWorkStatistics
{
	// Token: 0x02000009 RID: 9
	public readonly struct WorkerCountChangedEventArgs
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000252C File Offset: 0x0000072C
		public EmploymentStatistics OldEmploymentStatistics { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002534 File Offset: 0x00000734
		public EmploymentStatistics NewEmploymentStatistics { get; }

		// Token: 0x06000020 RID: 32 RVA: 0x0000253C File Offset: 0x0000073C
		public WorkerCountChangedEventArgs(EmploymentStatistics oldEmploymentStatistics, EmploymentStatistics newEmploymentStatistics)
		{
			this.OldEmploymentStatistics = oldEmploymentStatistics;
			this.NewEmploymentStatistics = newEmploymentStatistics;
		}
	}
}
