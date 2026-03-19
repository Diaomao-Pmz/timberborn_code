using System;
using System.Collections.Generic;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200002E RID: 46
	public class PreviewTerrainCutoutService
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00004ACF File Offset: 0x00002CCF
		public PreviewTerrainCutoutService(ITerrainService terrainService)
		{
			this._terrainService = terrainService;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004AEC File Offset: 0x00002CEC
		public void SetCutout(IEnumerable<Vector3Int> positionedCutoutTiles)
		{
			foreach (Vector3Int cutout in positionedCutoutTiles)
			{
				this.SetCutout(cutout);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004B34 File Offset: 0x00002D34
		public void UnsetCutout(IEnumerable<Vector3Int> positionedCutoutTiles)
		{
			foreach (Vector3Int positionedCutoutTile in positionedCutoutTiles)
			{
				this.UnsetCutout(positionedCutoutTile);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004B7C File Offset: 0x00002D7C
		public void SetCutout(Vector3Int positionedCutoutTile)
		{
			if (this._cutoutTiles.Add(positionedCutoutTile))
			{
				this._terrainService.SetCutout(positionedCutoutTile);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004B98 File Offset: 0x00002D98
		public void UnsetCutout(Vector3Int positionedCutoutTile)
		{
			if (this._cutoutTiles.Remove(positionedCutoutTile))
			{
				this._terrainService.UnsetCutout(positionedCutoutTile);
			}
		}

		// Token: 0x0400009D RID: 157
		public readonly ITerrainService _terrainService;

		// Token: 0x0400009E RID: 158
		public readonly HashSet<Vector3Int> _cutoutTiles = new HashSet<Vector3Int>();
	}
}
