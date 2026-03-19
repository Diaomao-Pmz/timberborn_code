using System;

namespace Timberborn.EntityNaming
{
	// Token: 0x0200000E RID: 14
	public readonly struct NamedEntitySortingKey : IEquatable<NamedEntitySortingKey>, IComparable<NamedEntitySortingKey>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002628 File Offset: 0x00000828
		private string SortableName { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002630 File Offset: 0x00000830
		private Guid EntityId { get; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002638 File Offset: 0x00000838
		public NamedEntitySortingKey(string sortableName, Guid entityId)
		{
			this.SortableName = sortableName;
			this.EntityId = entityId;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002648 File Offset: 0x00000848
		public bool Equals(NamedEntitySortingKey other)
		{
			return this.SortableName == other.SortableName && this.EntityId.Equals(other.EntityId);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002680 File Offset: 0x00000880
		public override bool Equals(object obj)
		{
			if (obj is NamedEntitySortingKey)
			{
				NamedEntitySortingKey other = (NamedEntitySortingKey)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026A5 File Offset: 0x000008A5
		public override int GetHashCode()
		{
			return HashCode.Combine<string, Guid>(this.SortableName, this.EntityId);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026B8 File Offset: 0x000008B8
		public int CompareTo(NamedEntitySortingKey other)
		{
			int num = string.Compare(this.SortableName, other.SortableName, StringComparison.InvariantCulture);
			if (num == 0)
			{
				return this.EntityId.CompareTo(other.EntityId);
			}
			return num;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026F3 File Offset: 0x000008F3
		public static bool operator ==(NamedEntitySortingKey left, NamedEntitySortingKey right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026FD File Offset: 0x000008FD
		public static bool operator !=(NamedEntitySortingKey left, NamedEntitySortingKey right)
		{
			return !left.Equals(right);
		}
	}
}
