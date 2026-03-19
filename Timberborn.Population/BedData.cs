using System;

namespace Timberborn.Population
{
	// Token: 0x02000004 RID: 4
	public readonly struct BedData : IEquatable<BedData>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public int OccupiedBeds { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public int FreeBeds { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public int Homeless { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		public BedData(int occupiedBeds, int freeBeds, int homeless)
		{
			this.OccupiedBeds = occupiedBeds;
			this.FreeBeds = freeBeds;
			this.Homeless = homeless;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EF File Offset: 0x000002EF
		public bool Equals(BedData other)
		{
			return this.OccupiedBeds == other.OccupiedBeds && this.FreeBeds == other.FreeBeds && this.Homeless == other.Homeless;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public override bool Equals(object obj)
		{
			if (obj is BedData)
			{
				BedData other = (BedData)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002145 File Offset: 0x00000345
		public override int GetHashCode()
		{
			return (this.OccupiedBeds * 397 ^ this.FreeBeds) * 397 ^ this.Homeless;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002167 File Offset: 0x00000367
		public static bool operator ==(BedData left, BedData right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002171 File Offset: 0x00000371
		public static bool operator !=(BedData left, BedData right)
		{
			return !left.Equals(right);
		}
	}
}
