using System;
using UnityEngine;

namespace Timberborn.Coordinates
{
	// Token: 0x0200000F RID: 15
	public readonly struct FlipMode : IEquatable<FlipMode>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002BCA File Offset: 0x00000DCA
		public bool IsFlipped { get; }

		// Token: 0x06000026 RID: 38 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public FlipMode(bool isFlipped)
		{
			this.IsFlipped = isFlipped;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002BDB File Offset: 0x00000DDB
		public bool IsUnflipped
		{
			get
			{
				return !this.IsFlipped;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002BE6 File Offset: 0x00000DE6
		public FlipMode Flip()
		{
			return new FlipMode(!this.IsFlipped);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002BF6 File Offset: 0x00000DF6
		public Vector3Int Transform(Vector3Int coordinates, int width)
		{
			if (!this.IsFlipped)
			{
				return coordinates;
			}
			return new Vector3Int(width - coordinates.x - 1, coordinates.y, coordinates.z);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C20 File Offset: 0x00000E20
		public Vector2Int Transform(Vector2Int coordinates, int width)
		{
			if (!this.IsFlipped)
			{
				return coordinates;
			}
			return new Vector2Int(width - coordinates.x - 1, coordinates.y);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C43 File Offset: 0x00000E43
		public Vector3 Transform(Vector3 coordinates, int width)
		{
			if (!this.IsFlipped)
			{
				return coordinates;
			}
			return new Vector3((float)width - coordinates.x, coordinates.y, coordinates.z);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C69 File Offset: 0x00000E69
		public Direction2D Transform(Direction2D direction2D)
		{
			if (!this.IsFlipped || (direction2D != Direction2D.Left && direction2D != Direction2D.Right))
			{
				return direction2D;
			}
			return direction2D.Across();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C83 File Offset: 0x00000E83
		public Direction3D Transform(Direction3D direction3D)
		{
			if (!this.IsFlipped || (direction3D != Direction3D.Left && direction3D != Direction3D.Right))
			{
				return direction3D;
			}
			return direction3D.Across();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C9D File Offset: 0x00000E9D
		public bool Equals(FlipMode other)
		{
			return this.IsFlipped == other.IsFlipped;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public override bool Equals(object obj)
		{
			if (obj is FlipMode)
			{
				FlipMode other = (FlipMode)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public override int GetHashCode()
		{
			return this.IsFlipped.GetHashCode();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002CF3 File Offset: 0x00000EF3
		public static bool operator ==(FlipMode left, FlipMode right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002CFD File Offset: 0x00000EFD
		public static bool operator !=(FlipMode left, FlipMode right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000033 RID: 51
		public static readonly FlipMode Unflipped = new FlipMode(false);

		// Token: 0x04000034 RID: 52
		public static readonly FlipMode Flipped = new FlipMode(true);
	}
}
