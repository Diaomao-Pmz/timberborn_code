using System;
using UnityEngine;

namespace Timberborn.Coordinates
{
	// Token: 0x0200000A RID: 10
	public static class Direction3DExtensions
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002794 File Offset: 0x00000994
		public static Vector3Int ToOffset(this Direction3D direction3D)
		{
			switch (direction3D)
			{
			case Direction3D.Down:
				return Vector3Int.down;
			case Direction3D.Left:
				return Vector3Int.left;
			case Direction3D.Up:
				return Vector3Int.up;
			case Direction3D.Right:
				return Vector3Int.right;
			case Direction3D.Bottom:
				return Direction3DExtensions.BottomVector;
			case Direction3D.Top:
				return Direction3DExtensions.TopVector;
			default:
				throw new ArgumentOutOfRangeException("direction3D", direction3D, null);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027F8 File Offset: 0x000009F8
		public static Direction3D FromOffset(Vector3Int offset)
		{
			if (offset == Vector3Int.down)
			{
				return Direction3D.Down;
			}
			if (offset == Vector3Int.left)
			{
				return Direction3D.Left;
			}
			if (offset == Vector3Int.up)
			{
				return Direction3D.Up;
			}
			if (offset == Vector3Int.right)
			{
				return Direction3D.Right;
			}
			if (offset == Direction3DExtensions.BottomVector)
			{
				return Direction3D.Bottom;
			}
			if (offset == Direction3DExtensions.TopVector)
			{
				return Direction3D.Top;
			}
			throw new ArgumentException("Can't create Direction3D " + string.Format("from {0} {1}", "offset", offset));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002884 File Offset: 0x00000A84
		public static Directions3D ToDirections3D(this Direction3D direction3D)
		{
			switch (direction3D)
			{
			case Direction3D.Down:
				return Directions3D.Down;
			case Direction3D.Left:
				return Directions3D.Left;
			case Direction3D.Up:
				return Directions3D.Up;
			case Direction3D.Right:
				return Directions3D.Right;
			case Direction3D.Bottom:
				return Directions3D.Bottom;
			case Direction3D.Top:
				return Directions3D.Top;
			default:
				throw new ArgumentOutOfRangeException("direction3D", direction3D, null);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000028D0 File Offset: 0x00000AD0
		public static Direction3D RotateHorizontally(this Direction3D direction3D, Orientation orientation)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return direction3D;
			case Orientation.Cw90:
				return direction3D.NextHorizontally();
			case Orientation.Cw180:
				return direction3D.NextHorizontally().NextHorizontally();
			case Orientation.Cw270:
				return direction3D.NextHorizontally().NextHorizontally().NextHorizontally();
			default:
				throw new ArgumentOutOfRangeException("orientation", orientation, null);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000292C File Offset: 0x00000B2C
		public static float ToHorizontalAngle(this Direction3D direction3D)
		{
			switch (direction3D)
			{
			case Direction3D.Down:
			case Direction3D.Bottom:
			case Direction3D.Top:
				return 0f;
			case Direction3D.Left:
				return 90f;
			case Direction3D.Up:
				return 180f;
			case Direction3D.Right:
				return 270f;
			default:
				throw new ArgumentOutOfRangeException("direction3D", direction3D, null);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002982 File Offset: 0x00000B82
		public static bool IsHorizontal(this Direction3D direction3D)
		{
			return direction3D == Direction3D.Down || direction3D == Direction3D.Up || direction3D == Direction3D.Left || direction3D == Direction3D.Right;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002995 File Offset: 0x00000B95
		public static Direction3D Across(this Direction3D direction3D)
		{
			switch (direction3D)
			{
			case Direction3D.Down:
				return Direction3D.Up;
			case Direction3D.Left:
				return Direction3D.Right;
			case Direction3D.Up:
				return Direction3D.Down;
			case Direction3D.Right:
				return Direction3D.Left;
			case Direction3D.Bottom:
				return Direction3D.Top;
			case Direction3D.Top:
				return Direction3D.Bottom;
			default:
				throw new ArgumentOutOfRangeException("direction3D", direction3D, null);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000029D4 File Offset: 0x00000BD4
		public static Quaternion ToRotation(this Direction3D direction)
		{
			if (direction.IsHorizontal())
			{
				return Quaternion.AngleAxis(direction.ToHorizontalAngle(), Vector3.up);
			}
			if (direction != Direction3D.Top)
			{
				return Quaternion.AngleAxis(-90f, Vector3.right);
			}
			return Quaternion.AngleAxis(90f, Vector3.right);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A12 File Offset: 0x00000C12
		public static Direction3D NextHorizontally(this Direction3D direction3D)
		{
			switch (direction3D)
			{
			case Direction3D.Down:
				return Direction3D.Left;
			case Direction3D.Left:
				return Direction3D.Up;
			case Direction3D.Up:
				return Direction3D.Right;
			case Direction3D.Right:
				return Direction3D.Down;
			case Direction3D.Bottom:
				return Direction3D.Bottom;
			case Direction3D.Top:
				return Direction3D.Top;
			default:
				throw new ArgumentOutOfRangeException("direction3D", direction3D, null);
			}
		}

		// Token: 0x0400001E RID: 30
		public static readonly Vector3Int BottomVector = new Vector3Int(0, 0, -1);

		// Token: 0x0400001F RID: 31
		public static readonly Vector3Int TopVector = new Vector3Int(0, 0, 1);
	}
}
