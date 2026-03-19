using System;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000010 RID: 16
	public readonly struct TerrainHeightChange
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000029F8 File Offset: 0x00000BF8
		public Vector2Int Coordinates { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002A00 File Offset: 0x00000C00
		public int From { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002A08 File Offset: 0x00000C08
		public int To { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002A10 File Offset: 0x00000C10
		public bool SetTerrain { get; }

		// Token: 0x06000069 RID: 105 RVA: 0x00002A18 File Offset: 0x00000C18
		public TerrainHeightChange(Vector2Int coordinates, int from, int to, bool setTerrain)
		{
			this.Coordinates = coordinates;
			this.From = from;
			this.To = to;
			this.SetTerrain = setTerrain;
		}
	}
}
