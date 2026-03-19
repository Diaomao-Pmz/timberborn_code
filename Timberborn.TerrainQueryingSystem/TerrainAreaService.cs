using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.GridTraversing;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainQueryingSystem
{
	// Token: 0x02000004 RID: 4
	public class TerrainAreaService
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public TerrainAreaService(ITerrainService terrainService, TerrainPicker terrainPicker)
		{
			this._terrainService = terrainService;
			this._terrainPicker = terrainPicker;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public IEnumerable<Vector3Int> InMapCoordinates(IEnumerable<Vector2Int> blocks)
		{
			foreach (Vector2Int cellCoordinates in blocks)
			{
				foreach (Vector3Int vector3Int in this._terrainService.GetAllHeightsInCell(cellCoordinates))
				{
					yield return vector3Int;
				}
				IEnumerator<Vector3Int> enumerator2 = null;
			}
			IEnumerator<Vector2Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020EB File Offset: 0x000002EB
		public IEnumerable<Vector3Int> InMapLeveledCoordinates(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
			int startHeight = (traversedCoordinates != null) ? (traversedCoordinates.GetValueOrDefault().Coordinates.z + 1) : 0;
			foreach (Vector3Int value in inputBlocks)
			{
				if (this._terrainService.OnGround(value.Above()))
				{
					yield return new Vector3Int(value.x, value.y, startHeight);
				}
			}
			IEnumerator<Vector3Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000006 RID: 6
		public readonly ITerrainService _terrainService;

		// Token: 0x04000007 RID: 7
		public readonly TerrainPicker _terrainPicker;
	}
}
