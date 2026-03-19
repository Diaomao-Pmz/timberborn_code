using System;

namespace Timberborn.PopulationStatisticsSystem
{
	// Token: 0x02000005 RID: 5
	public readonly struct DwellingStatistics
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020ED File Offset: 0x000002ED
		public int OccupiedBeds { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020F5 File Offset: 0x000002F5
		public int FreeBeds { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x000020FD File Offset: 0x000002FD
		public DwellingStatistics(int occupiedBeds, int freeBeds)
		{
			this.OccupiedBeds = occupiedBeds;
			this.FreeBeds = freeBeds;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000210D File Offset: 0x0000030D
		public static DwellingStatistics operator +(DwellingStatistics left, DwellingStatistics right)
		{
			return new DwellingStatistics(left.OccupiedBeds + right.OccupiedBeds, left.FreeBeds + right.FreeBeds);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002132 File Offset: 0x00000332
		public static DwellingStatistics operator -(DwellingStatistics left, DwellingStatistics right)
		{
			return new DwellingStatistics(left.OccupiedBeds - right.OccupiedBeds, left.FreeBeds - right.FreeBeds);
		}
	}
}
