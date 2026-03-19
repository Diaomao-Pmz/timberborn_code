using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x02000016 RID: 22
	public readonly struct ParentedNeighbor2D : IEquatable<ParentedNeighbor2D>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000035D7 File Offset: 0x000017D7
		public Vector2Int Neighbor { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000035DF File Offset: 0x000017DF
		public Vector2Int Parent { get; }

		// Token: 0x06000081 RID: 129 RVA: 0x000035E7 File Offset: 0x000017E7
		public ParentedNeighbor2D(Vector2Int neighbor, Vector2Int parent)
		{
			this.Neighbor = neighbor;
			this.Parent = parent;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000035F7 File Offset: 0x000017F7
		public static ParentedNeighbor2D From3D(ParentedNeighbor parentedNeighbor)
		{
			return new ParentedNeighbor2D(parentedNeighbor.Neighbor.XY(), parentedNeighbor.Parent.XY());
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003616 File Offset: 0x00001816
		public static ParentedNeighbor2D FromVectors(Vector3Int neighbor, Vector3Int parent)
		{
			return new ParentedNeighbor2D(neighbor.XY(), parent.XY());
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000362C File Offset: 0x0000182C
		public bool Equals(ParentedNeighbor2D other)
		{
			return this.Neighbor.Equals(other.Neighbor) && this.Parent.Equals(other.Parent);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003668 File Offset: 0x00001868
		public override bool Equals(object obj)
		{
			if (obj is ParentedNeighbor2D)
			{
				ParentedNeighbor2D other = (ParentedNeighbor2D)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003690 File Offset: 0x00001890
		public override int GetHashCode()
		{
			return this.Neighbor.GetHashCode() * 397 ^ this.Parent.GetHashCode();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000036CC File Offset: 0x000018CC
		public static bool operator ==(ParentedNeighbor2D left, ParentedNeighbor2D right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000036D6 File Offset: 0x000018D6
		public static bool operator !=(ParentedNeighbor2D left, ParentedNeighbor2D right)
		{
			return !left.Equals(right);
		}
	}
}
