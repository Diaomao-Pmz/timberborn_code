using System;
using UnityEngine;

namespace Timberborn.Coordinates
{
	// Token: 0x02000006 RID: 6
	public static class Deltas
	{
		// Token: 0x0400000A RID: 10
		public static readonly Vector3Int[] Corners4Vector3Int = new Vector3Int[]
		{
			new Vector3Int(-1, -1, 0),
			new Vector3Int(-1, 1, 0),
			new Vector3Int(1, 1, 0),
			new Vector3Int(1, -1, 0)
		};

		// Token: 0x0400000B RID: 11
		public static readonly Vector3Int[] Neighbors4Vector3Int = new Vector3Int[]
		{
			new Vector3Int(-1, 0, 0),
			new Vector3Int(0, 1, 0),
			new Vector3Int(1, 0, 0),
			new Vector3Int(0, -1, 0)
		};

		// Token: 0x0400000C RID: 12
		public static readonly Vector2Int[] Neighbors4Vector2Int = new Vector2Int[]
		{
			new Vector2Int(-1, 0),
			new Vector2Int(0, 1),
			new Vector2Int(1, 0),
			new Vector2Int(0, -1)
		};

		// Token: 0x0400000D RID: 13
		public static readonly Vector3Int[] Neighbors8AndSelfVector3Int = new Vector3Int[]
		{
			new Vector3Int(-1, 0, 0),
			new Vector3Int(-1, 1, 0),
			new Vector3Int(0, 1, 0),
			new Vector3Int(1, 1, 0),
			new Vector3Int(1, 0, 0),
			new Vector3Int(1, -1, 0),
			new Vector3Int(0, -1, 0),
			new Vector3Int(-1, -1, 0),
			new Vector3Int(0, 0, 0)
		};

		// Token: 0x0400000E RID: 14
		public static readonly Vector2Int[] Neighbors8AndSelfVector2Int = new Vector2Int[]
		{
			new Vector2Int(-1, 0),
			new Vector2Int(-1, 1),
			new Vector2Int(0, 1),
			new Vector2Int(1, 1),
			new Vector2Int(1, 0),
			new Vector2Int(1, -1),
			new Vector2Int(0, -1),
			new Vector2Int(-1, -1),
			new Vector2Int(0, 0)
		};

		// Token: 0x0400000F RID: 15
		public static readonly Vector3Int[] Neighbors8Vector3IntOrdered = new Vector3Int[]
		{
			new Vector3Int(-1, 0, 0),
			new Vector3Int(0, 1, 0),
			new Vector3Int(1, 0, 0),
			new Vector3Int(0, -1, 0),
			new Vector3Int(-1, -1, 0),
			new Vector3Int(-1, 1, 0),
			new Vector3Int(1, 1, 0),
			new Vector3Int(1, -1, 0)
		};

		// Token: 0x04000010 RID: 16
		public static readonly Vector3Int[] Neighbors6Vector3Int = new Vector3Int[]
		{
			new Vector3Int(-1, 0, 0),
			new Vector3Int(1, 0, 0),
			new Vector3Int(0, -1, 0),
			new Vector3Int(0, 1, 0),
			new Vector3Int(0, 0, -1),
			new Vector3Int(0, 0, 1)
		};

		// Token: 0x04000011 RID: 17
		public static readonly Vector3Int[] Neighbors26Vector3Int = new Vector3Int[]
		{
			new Vector3Int(-1, -1, -1),
			new Vector3Int(0, -1, -1),
			new Vector3Int(1, -1, -1),
			new Vector3Int(-1, 0, -1),
			new Vector3Int(0, 0, -1),
			new Vector3Int(1, 0, -1),
			new Vector3Int(-1, 1, -1),
			new Vector3Int(0, 1, -1),
			new Vector3Int(1, 1, -1),
			new Vector3Int(-1, -1, 0),
			new Vector3Int(0, -1, 0),
			new Vector3Int(1, -1, 0),
			new Vector3Int(-1, 0, 0),
			new Vector3Int(1, 0, 0),
			new Vector3Int(-1, 1, 0),
			new Vector3Int(0, 1, 0),
			new Vector3Int(1, 1, 0),
			new Vector3Int(-1, -1, 1),
			new Vector3Int(0, -1, 1),
			new Vector3Int(1, -1, 1),
			new Vector3Int(-1, 0, 1),
			new Vector3Int(0, 0, 1),
			new Vector3Int(1, 0, 1),
			new Vector3Int(-1, 1, 1),
			new Vector3Int(0, 1, 1),
			new Vector3Int(1, 1, 1)
		};
	}
}
