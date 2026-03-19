using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x0200000F RID: 15
	public class TerrainCutout
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002949 File Offset: 0x00000B49
		public TerrainCutout(ITerrainService terrainService)
		{
			this._terrainService = terrainService;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002958 File Offset: 0x00000B58
		public void SetCutout(IEnumerable<Vector3Int> positionedCutoutTiles)
		{
			foreach (Vector3Int cutout in positionedCutoutTiles)
			{
				this._terrainService.SetCutout(cutout);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000029A8 File Offset: 0x00000BA8
		public void UnsetCutout(IEnumerable<Vector3Int> positionedCutoutTiles)
		{
			foreach (Vector3Int coordinates in positionedCutoutTiles)
			{
				this._terrainService.UnsetCutout(coordinates);
			}
		}

		// Token: 0x04000017 RID: 23
		public readonly ITerrainService _terrainService;
	}
}
