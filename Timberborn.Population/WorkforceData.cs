using System;

namespace Timberborn.Population
{
	// Token: 0x0200000B RID: 11
	public readonly struct WorkforceData : IEquatable<WorkforceData>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000027E7 File Offset: 0x000009E7
		public int Employable { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000027EF File Offset: 0x000009EF
		public int Unemployable { get; }

		// Token: 0x0600004A RID: 74 RVA: 0x000027F7 File Offset: 0x000009F7
		public WorkforceData(int employable, int unemployable)
		{
			this.Employable = employable;
			this.Unemployable = unemployable;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002807 File Offset: 0x00000A07
		public int Total
		{
			get
			{
				return this.Employable + this.Unemployable;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002816 File Offset: 0x00000A16
		public bool Equals(WorkforceData other)
		{
			return this.Employable == other.Employable && this.Unemployable == other.Unemployable;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002838 File Offset: 0x00000A38
		public override bool Equals(object obj)
		{
			if (obj is WorkforceData)
			{
				WorkforceData other = (WorkforceData)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000285D File Offset: 0x00000A5D
		public override int GetHashCode()
		{
			return this.Employable * 397 ^ this.Unemployable;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002872 File Offset: 0x00000A72
		public static bool operator ==(WorkforceData left, WorkforceData right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000287C File Offset: 0x00000A7C
		public static bool operator !=(WorkforceData left, WorkforceData right)
		{
			return !left.Equals(right);
		}
	}
}
