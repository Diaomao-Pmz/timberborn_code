using System;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainUndoSystem
{
	// Token: 0x02000008 RID: 8
	public class UndoableTerrain
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021B9 File Offset: 0x000003B9
		public UndoableTerrain(ITerrainService terrainService, TerrainHeightChange terrainHeightChange)
		{
			this._terrainService = terrainService;
			this._terrainHeightChange = terrainHeightChange;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021D0 File Offset: 0x000003D0
		public void SetTerrain()
		{
			Vector3Int coordinates;
			coordinates..ctor(this._terrainHeightChange.Coordinates.x, this._terrainHeightChange.Coordinates.y, this._terrainHeightChange.From);
			this._terrainService.SetTerrain(coordinates, this._terrainHeightChange.To - this._terrainHeightChange.From + 1);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000223C File Offset: 0x0000043C
		public void UnsetTerrain()
		{
			Vector3Int coordinates;
			coordinates..ctor(this._terrainHeightChange.Coordinates.x, this._terrainHeightChange.Coordinates.y, this._terrainHeightChange.To);
			this._terrainService.UnsetTerrain(coordinates, this._terrainHeightChange.To - this._terrainHeightChange.From + 1);
		}

		// Token: 0x0400000A RID: 10
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000B RID: 11
		public readonly TerrainHeightChange _terrainHeightChange;
	}
}
