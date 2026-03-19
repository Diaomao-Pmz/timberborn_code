using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Coordinates
{
	// Token: 0x02000016 RID: 22
	public static class OrientationExtensions
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003424 File Offset: 0x00001624
		public static float ToAngle(this Orientation orientation)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return 0f;
			case Orientation.Cw90:
				return 90f;
			case Orientation.Cw180:
				return 180f;
			case Orientation.Cw270:
				return 270f;
			default:
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "Orientation", orientation));
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000347B File Offset: 0x0000167B
		public static Quaternion ToWorldSpaceRotation(this Orientation orientation)
		{
			return Quaternion.AngleAxis(orientation.ToAngle(), CoordinateSystem.GridToWorld(new Vector3(0f, 0f, 1f)));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000034A4 File Offset: 0x000016A4
		public static Vector3 ToPivotOffset(this Orientation orientation)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return new Vector3(0f, 0f, 0f);
			case Orientation.Cw90:
				return new Vector3(0f, 1f, 0f);
			case Orientation.Cw180:
				return new Vector3(1f, 1f, 0f);
			case Orientation.Cw270:
				return new Vector3(1f, 0f, 0f);
			default:
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "Orientation", orientation));
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003537 File Offset: 0x00001737
		public static Orientation RotateClockwise(this Orientation orientation)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return Orientation.Cw90;
			case Orientation.Cw90:
				return Orientation.Cw180;
			case Orientation.Cw180:
				return Orientation.Cw270;
			case Orientation.Cw270:
				return Orientation.Cw0;
			default:
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "Orientation", orientation));
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003573 File Offset: 0x00001773
		public static Orientation RotateCounterclockwise(this Orientation orientation)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return Orientation.Cw270;
			case Orientation.Cw90:
				return Orientation.Cw0;
			case Orientation.Cw180:
				return Orientation.Cw90;
			case Orientation.Cw270:
				return Orientation.Cw180;
			default:
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "Orientation", orientation));
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000035AF File Offset: 0x000017AF
		public static Orientation Flip(this Orientation orientation)
		{
			return orientation.RotateClockwise().RotateClockwise();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000035BC File Offset: 0x000017BC
		public static Vector3 Transform(this Orientation orientation, Vector3 vector)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return vector;
			case Orientation.Cw90:
				return new Vector3(vector.y, -vector.x, vector.z);
			case Orientation.Cw180:
				return new Vector3(-vector.x, -vector.y, vector.z);
			case Orientation.Cw270:
				return new Vector3(-vector.y, vector.x, vector.z);
			default:
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "Orientation", orientation));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000364C File Offset: 0x0000184C
		public static Vector3 TransformInWorldSpace(this Orientation orientation, Vector3 vector)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return vector;
			case Orientation.Cw90:
				return new Vector3(vector.z, vector.y, -vector.x);
			case Orientation.Cw180:
				return new Vector3(-vector.x, vector.y, -vector.z);
			case Orientation.Cw270:
				return new Vector3(-vector.z, vector.y, vector.x);
			default:
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "Orientation", orientation));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000036DC File Offset: 0x000018DC
		public static Vector3Int Transform(this Orientation orientation, Vector3Int vector)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return vector;
			case Orientation.Cw90:
				return new Vector3Int(vector.y, -vector.x, vector.z);
			case Orientation.Cw180:
				return new Vector3Int(-vector.x, -vector.y, vector.z);
			case Orientation.Cw270:
				return new Vector3Int(-vector.y, vector.x, vector.z);
			default:
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "Orientation", orientation));
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003774 File Offset: 0x00001974
		public static Vector2Int Transform(this Orientation orientation, Vector2Int vector)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return vector;
			case Orientation.Cw90:
				return new Vector2Int(vector.y, -vector.x);
			case Orientation.Cw180:
				return new Vector2Int(-vector.x, -vector.y);
			case Orientation.Cw270:
				return new Vector2Int(-vector.y, vector.x);
			default:
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "Orientation", orientation));
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000037F8 File Offset: 0x000019F8
		public static Direction2D Transform(this Orientation orientation, Direction2D direction2D)
		{
			switch (orientation)
			{
			case Orientation.Cw0:
				return direction2D;
			case Orientation.Cw90:
				return direction2D.Next();
			case Orientation.Cw180:
				return direction2D.Next().Next();
			case Orientation.Cw270:
				return direction2D.Next().Next().Next();
			default:
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "Orientation", orientation));
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000385D File Offset: 0x00001A5D
		public static IEnumerable<Orientation> AllValues()
		{
			return (Orientation[])Enum.GetValues(typeof(Orientation));
		}
	}
}
