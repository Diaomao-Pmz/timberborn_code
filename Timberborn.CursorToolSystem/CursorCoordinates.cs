using System;
using UnityEngine;

namespace Timberborn.CursorToolSystem
{
	// Token: 0x02000006 RID: 6
	public readonly struct CursorCoordinates
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002203 File Offset: 0x00000403
		public Vector3 Coordinates { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000220B File Offset: 0x0000040B
		public Vector3Int TileCoordinates { get; }

		// Token: 0x06000010 RID: 16 RVA: 0x00002213 File Offset: 0x00000413
		public CursorCoordinates(Vector3 coordinates, Vector3Int tileCoordinates)
		{
			this.Coordinates = coordinates;
			this.TileCoordinates = tileCoordinates;
		}
	}
}
