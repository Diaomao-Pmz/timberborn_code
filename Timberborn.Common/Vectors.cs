using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000033 RID: 51
	public static class Vectors
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00003A5B File Offset: 0x00001C5B
		[return: TupleElementNames(new string[]
		{
			"min",
			"max"
		})]
		public static ValueTuple<Vector3Int, Vector3Int> MinMax(Vector3Int a, Vector3Int b)
		{
			return new ValueTuple<Vector3Int, Vector3Int>(Vectors.Min(a, b), Vectors.Max(a, b));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003A70 File Offset: 0x00001C70
		[return: TupleElementNames(new string[]
		{
			"min",
			"max"
		})]
		public static ValueTuple<Vector2Int, Vector2Int> MinMax(Vector2Int a, Vector2Int b)
		{
			return new ValueTuple<Vector2Int, Vector2Int>(Vectors.Min(a, b), Vectors.Max(a, b));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003A88 File Offset: 0x00001C88
		public static Vector3Int Min(Vector3Int a, Vector3Int b)
		{
			int num = Math.Min(a.x, b.x);
			int num2 = Math.Min(a.y, b.y);
			int num3 = Math.Min(a.z, b.z);
			return new Vector3Int(num, num2, num3);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public static Vector3Int Max(Vector3Int a, Vector3Int b)
		{
			int num = Math.Max(a.x, b.x);
			int num2 = Math.Max(a.y, b.y);
			int num3 = Math.Max(a.z, b.z);
			return new Vector3Int(num, num2, num3);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003B28 File Offset: 0x00001D28
		public static Vector2Int Min(Vector2Int a, Vector2Int b)
		{
			int num = Math.Min(a.x, b.x);
			int num2 = Math.Min(a.y, b.y);
			return new Vector2Int(num, num2);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003B64 File Offset: 0x00001D64
		public static Vector2Int Max(Vector2Int a, Vector2Int b)
		{
			int num = Math.Max(a.x, b.x);
			int num2 = Math.Max(a.y, b.y);
			return new Vector2Int(num, num2);
		}
	}
}
