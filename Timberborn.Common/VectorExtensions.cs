using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000032 RID: 50
	public static class VectorExtensions
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x0000390A File Offset: 0x00001B0A
		public static Vector2Int XY(this Vector3Int value)
		{
			return new Vector2Int(value.x, value.y);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000391F File Offset: 0x00001B1F
		public static Vector2 XY(this Vector3 value)
		{
			return new Vector2(value.x, value.y);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003932 File Offset: 0x00001B32
		public static IEnumerable<Vector2Int> XY(this IEnumerable<Vector3Int> vectors)
		{
			return vectors.Select(new Func<Vector3Int, Vector2Int>(VectorExtensions.XY));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003946 File Offset: 0x00001B46
		public static Vector3Int XYZ(this Vector2Int value)
		{
			return new Vector3Int(value.x, value.y, 0);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000395C File Offset: 0x00001B5C
		public static Vector3 XYZ(this Vector2 value)
		{
			return new Vector3(value.x, value.y, 0f);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003974 File Offset: 0x00001B74
		public static Vector2 XZ(this Vector3 value)
		{
			return new Vector2(value.x, value.z);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003987 File Offset: 0x00001B87
		public static Vector3Int Above(this Vector3Int value)
		{
			return new Vector3Int(value.x, value.y, value.z + 1);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000039A5 File Offset: 0x00001BA5
		public static Vector3Int Below(this Vector3Int value)
		{
			return new Vector3Int(value.x, value.y, value.z - 1);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000039C3 File Offset: 0x00001BC3
		public static Vector3Int FloorToInt(this Vector3 value)
		{
			return new Vector3Int(Mathf.FloorToInt(value.x), Mathf.FloorToInt(value.y), Mathf.FloorToInt(value.z));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000039EB File Offset: 0x00001BEB
		public static Vector3Int CeilToInt(this Vector3 value)
		{
			return new Vector3Int(Mathf.CeilToInt(value.x), Mathf.CeilToInt(value.y), Mathf.CeilToInt(value.z));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003A13 File Offset: 0x00001C13
		public static Vector2Int FloorToInt(this Vector2 value)
		{
			return new Vector2Int(Mathf.FloorToInt(value.x), Mathf.FloorToInt(value.y));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003A30 File Offset: 0x00001C30
		public static Vector3Int ToVector3Int(this Vector2Int coords2D, int z)
		{
			return new Vector3Int(coords2D.x, coords2D.y, z);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003A46 File Offset: 0x00001C46
		public static Vector3 ToVector3(this Vector2 coords2D, int z)
		{
			return new Vector3(coords2D.x, coords2D.y, (float)z);
		}
	}
}
