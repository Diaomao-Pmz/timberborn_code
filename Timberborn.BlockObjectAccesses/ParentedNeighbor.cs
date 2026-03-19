using System;
using UnityEngine;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x02000015 RID: 21
	public readonly struct ParentedNeighbor : IEquatable<ParentedNeighbor>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000034FF File Offset: 0x000016FF
		public Vector3Int Neighbor { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003507 File Offset: 0x00001707
		public Vector3Int Parent { get; }

		// Token: 0x06000079 RID: 121 RVA: 0x0000350F File Offset: 0x0000170F
		public ParentedNeighbor(Vector3Int neighbor, Vector3Int parent)
		{
			this.Neighbor = neighbor;
			this.Parent = parent;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003520 File Offset: 0x00001720
		public bool Equals(ParentedNeighbor other)
		{
			return this.Neighbor.Equals(other.Neighbor) && this.Parent.Equals(other.Parent);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000355C File Offset: 0x0000175C
		public override bool Equals(object obj)
		{
			if (obj is ParentedNeighbor)
			{
				ParentedNeighbor other = (ParentedNeighbor)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003584 File Offset: 0x00001784
		public override int GetHashCode()
		{
			return this.Neighbor.GetHashCode() * 397 ^ this.Parent.GetHashCode();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000035C0 File Offset: 0x000017C0
		public static bool operator ==(ParentedNeighbor left, ParentedNeighbor right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000035CA File Offset: 0x000017CA
		public static bool operator !=(ParentedNeighbor left, ParentedNeighbor right)
		{
			return !left.Equals(right);
		}
	}
}
