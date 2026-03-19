using System;

namespace Timberborn.PopulationStatisticsSystem
{
	// Token: 0x02000004 RID: 4
	public readonly struct BeaverContaminationStatistics
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public int ContaminatedAdults { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public int ContaminatedChildren { get; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public BeaverContaminationStatistics(int contaminatedAdults, int contaminatedChildren)
		{
			this.ContaminatedAdults = contaminatedAdults;
			this.ContaminatedChildren = contaminatedChildren;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020DE File Offset: 0x000002DE
		public int Total
		{
			get
			{
				return this.ContaminatedAdults + this.ContaminatedChildren;
			}
		}
	}
}
