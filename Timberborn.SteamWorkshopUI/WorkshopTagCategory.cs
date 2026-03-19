using System;

namespace Timberborn.SteamWorkshopUI
{
	// Token: 0x0200000E RID: 14
	public readonly struct WorkshopTagCategory : IEquatable<WorkshopTagCategory>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003072 File Offset: 0x00001272
		public string Name { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000307A File Offset: 0x0000127A
		public int Order { get; }

		// Token: 0x06000070 RID: 112 RVA: 0x00003082 File Offset: 0x00001282
		public WorkshopTagCategory(string name, int order)
		{
			this.Name = name;
			this.Order = order;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003092 File Offset: 0x00001292
		public bool Equals(WorkshopTagCategory other)
		{
			return this.Name == other.Name && this.Order == other.Order;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030BC File Offset: 0x000012BC
		public override bool Equals(object obj)
		{
			if (obj is WorkshopTagCategory)
			{
				WorkshopTagCategory other = (WorkshopTagCategory)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000030E1 File Offset: 0x000012E1
		public override int GetHashCode()
		{
			return ((this.Name != null) ? this.Name.GetHashCode() : 0) * 397 ^ this.Order;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003106 File Offset: 0x00001306
		public static bool operator ==(WorkshopTagCategory left, WorkshopTagCategory right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003110 File Offset: 0x00001310
		public static bool operator !=(WorkshopTagCategory left, WorkshopTagCategory right)
		{
			return !left.Equals(right);
		}
	}
}
