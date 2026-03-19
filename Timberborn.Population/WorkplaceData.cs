using System;

namespace Timberborn.Population
{
	// Token: 0x0200000C RID: 12
	public readonly struct WorkplaceData : IEquatable<WorkplaceData>
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002889 File Offset: 0x00000A89
		public int OccupiedWorkslots { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002891 File Offset: 0x00000A91
		public int FreeWorkslots { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002899 File Offset: 0x00000A99
		public int Unemployed { get; }

		// Token: 0x06000054 RID: 84 RVA: 0x000028A1 File Offset: 0x00000AA1
		public WorkplaceData(int occupiedWorkslots, int freeWorkslots, int unemployed)
		{
			this.OccupiedWorkslots = occupiedWorkslots;
			this.FreeWorkslots = freeWorkslots;
			this.Unemployed = unemployed;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000028B8 File Offset: 0x00000AB8
		public int TotalWorkslots
		{
			get
			{
				return this.OccupiedWorkslots + this.FreeWorkslots;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000028C7 File Offset: 0x00000AC7
		public bool Equals(WorkplaceData other)
		{
			return this.OccupiedWorkslots == other.OccupiedWorkslots && this.FreeWorkslots == other.FreeWorkslots && this.Unemployed == other.Unemployed;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000028F8 File Offset: 0x00000AF8
		public override bool Equals(object obj)
		{
			if (obj is WorkplaceData)
			{
				WorkplaceData other = (WorkplaceData)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000291D File Offset: 0x00000B1D
		public override int GetHashCode()
		{
			return (this.OccupiedWorkslots * 397 ^ this.FreeWorkslots) * 397 ^ this.Unemployed;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000293F File Offset: 0x00000B3F
		public static bool operator ==(WorkplaceData left, WorkplaceData right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002949 File Offset: 0x00000B49
		public static bool operator !=(WorkplaceData left, WorkplaceData right)
		{
			return !left.Equals(right);
		}
	}
}
