using System;

namespace Timberborn.Population
{
	// Token: 0x02000005 RID: 5
	public readonly struct ContaminationData : IEquatable<ContaminationData>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000217E File Offset: 0x0000037E
		public int ContaminatedAdults { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002186 File Offset: 0x00000386
		public int ContaminatedChildren { get; }

		// Token: 0x0600000E RID: 14 RVA: 0x0000218E File Offset: 0x0000038E
		public ContaminationData(int contaminatedAdults, int contaminatedChildren)
		{
			this.ContaminatedAdults = contaminatedAdults;
			this.ContaminatedChildren = contaminatedChildren;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000219E File Offset: 0x0000039E
		public int ContaminatedTotal
		{
			get
			{
				return this.ContaminatedAdults + this.ContaminatedChildren;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021AD File Offset: 0x000003AD
		public bool Equals(ContaminationData other)
		{
			return this.ContaminatedAdults == other.ContaminatedAdults && this.ContaminatedChildren == other.ContaminatedChildren;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021D0 File Offset: 0x000003D0
		public override bool Equals(object obj)
		{
			if (obj is ContaminationData)
			{
				ContaminationData other = (ContaminationData)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021F5 File Offset: 0x000003F5
		public override int GetHashCode()
		{
			return this.ContaminatedAdults * 397 ^ this.ContaminatedChildren;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000220A File Offset: 0x0000040A
		public static bool operator ==(ContaminationData left, ContaminationData right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002214 File Offset: 0x00000414
		public static bool operator !=(ContaminationData left, ContaminationData right)
		{
			return !left.Equals(right);
		}
	}
}
