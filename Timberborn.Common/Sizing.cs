using System;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x0200002C RID: 44
	public static class Sizing
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x0000365F File Offset: 0x0000185F
		public static bool SizeContains(Vector2Int size, Vector2Int coordinates)
		{
			return coordinates.x >= 0 && coordinates.x < size.x && coordinates.y >= 0 && coordinates.y < size.y;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003697 File Offset: 0x00001897
		public static bool SizeContains(Vector3Int size, Vector2Int coordinates)
		{
			return coordinates.x >= 0 && coordinates.x < size.x && coordinates.y >= 0 && coordinates.y < size.y;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000036D0 File Offset: 0x000018D0
		public static bool SizeContains(Vector3Int size, Vector3Int coordinates)
		{
			return coordinates.x >= 0 && coordinates.x < size.x && coordinates.y >= 0 && coordinates.y < size.y && coordinates.z >= 0 && coordinates.z < size.z;
		}
	}
}
