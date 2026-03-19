using System;
using Timberborn.PopulationStatisticsSystem;

namespace Timberborn.DwellingSystem
{
	// Token: 0x0200000E RID: 14
	public readonly struct DwellingChangedEventArgs
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F6F File Offset: 0x0000116F
		public DwellingStatistics OldDwellingStatistics { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002F77 File Offset: 0x00001177
		public DwellingStatistics NewDwellingStatistics { get; }

		// Token: 0x06000069 RID: 105 RVA: 0x00002F7F File Offset: 0x0000117F
		public DwellingChangedEventArgs(DwellingStatistics oldDwellingStatistics, DwellingStatistics newDwellingStatistics)
		{
			this.OldDwellingStatistics = oldDwellingStatistics;
			this.NewDwellingStatistics = newDwellingStatistics;
		}
	}
}
