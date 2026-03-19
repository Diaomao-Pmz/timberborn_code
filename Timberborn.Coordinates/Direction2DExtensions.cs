using System;
using UnityEngine;

namespace Timberborn.Coordinates
{
	// Token: 0x02000008 RID: 8
	public static class Direction2DExtensions
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002604 File Offset: 0x00000804
		public static Vector3Int ToOffset(this Direction2D direction2D)
		{
			switch (direction2D)
			{
			case Direction2D.Down:
				return Vector3Int.down;
			case Direction2D.Left:
				return Vector3Int.left;
			case Direction2D.Up:
				return Vector3Int.up;
			case Direction2D.Right:
				return Vector3Int.right;
			default:
				throw new ArgumentOutOfRangeException("direction2D", direction2D, null);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002652 File Offset: 0x00000852
		public static Direction2D Next(this Direction2D direction2D)
		{
			switch (direction2D)
			{
			case Direction2D.Down:
				return Direction2D.Left;
			case Direction2D.Left:
				return Direction2D.Up;
			case Direction2D.Up:
				return Direction2D.Right;
			case Direction2D.Right:
				return Direction2D.Down;
			default:
				throw new ArgumentOutOfRangeException("direction2D", direction2D, null);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002685 File Offset: 0x00000885
		public static Direction2D Across(this Direction2D direction2D)
		{
			switch (direction2D)
			{
			case Direction2D.Down:
				return Direction2D.Up;
			case Direction2D.Left:
				return Direction2D.Right;
			case Direction2D.Up:
				return Direction2D.Down;
			case Direction2D.Right:
				return Direction2D.Left;
			default:
				throw new ArgumentOutOfRangeException("direction2D", direction2D, null);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026B8 File Offset: 0x000008B8
		public static Directions2D ToDirections(this Direction2D direction2D)
		{
			switch (direction2D)
			{
			case Direction2D.Down:
				return Directions2D.Down;
			case Direction2D.Left:
				return Directions2D.Left;
			case Direction2D.Up:
				return Directions2D.Up;
			case Direction2D.Right:
				return Directions2D.Right;
			default:
				throw new ArgumentOutOfRangeException("direction2D", direction2D, null);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026B8 File Offset: 0x000008B8
		public static Directions3D ToDirections3D(this Direction2D direction2D)
		{
			switch (direction2D)
			{
			case Direction2D.Down:
				return Directions3D.Down;
			case Direction2D.Left:
				return Directions3D.Left;
			case Direction2D.Up:
				return Directions3D.Up;
			case Direction2D.Right:
				return Directions3D.Right;
			default:
				throw new ArgumentOutOfRangeException("direction2D", direction2D, null);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026EC File Offset: 0x000008EC
		public static float ToAngle(this Direction2D direction2D)
		{
			switch (direction2D)
			{
			case Direction2D.Down:
				return 0f;
			case Direction2D.Left:
				return 90f;
			case Direction2D.Up:
				return 180f;
			case Direction2D.Right:
				return 270f;
			default:
				throw new ArgumentOutOfRangeException("direction2D", direction2D, null);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000273A File Offset: 0x0000093A
		public static Orientation ToOrientation(this Direction2D direction2D)
		{
			switch (direction2D)
			{
			case Direction2D.Down:
				return Orientation.Cw0;
			case Direction2D.Left:
				return Orientation.Cw90;
			case Direction2D.Up:
				return Orientation.Cw180;
			case Direction2D.Right:
				return Orientation.Cw270;
			default:
				throw new ArgumentOutOfRangeException("direction2D", direction2D, null);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000276D File Offset: 0x0000096D
		public static Quaternion ToWorldSpaceRotation(this Direction2D direction2D)
		{
			return Quaternion.AngleAxis(direction2D.ToAngle(), CoordinateSystem.GridToWorld(new Vector3(0f, 0f, 1f)));
		}
	}
}
