using System;
using System.Collections.Generic;
using Timberborn.MapStateSystem;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200002F RID: 47
	public class WaterMapBoundaryService
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004ACC File Offset: 0x00002CCC
		public WaterMapBoundaryService(MapSize mapSize, WaterSimulator waterSimulator)
		{
			this._mapSize = mapSize;
			this._waterSimulator = waterSimulator;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004AED File Offset: 0x00002CED
		public void FullyBlockCell(Vector2Int coordinates)
		{
			if (!this._mapSize.ContainsInTerrain(coordinates))
			{
				if (!this._blockedCells.Contains(coordinates))
				{
					this._waterSimulator.FullyBlockCell(coordinates);
				}
				this._blockedCells.Add(coordinates);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004B23 File Offset: 0x00002D23
		public void FullyUnblockCell(Vector2Int coordinates)
		{
			if (!this._mapSize.ContainsInTerrain(coordinates))
			{
				this._blockedCells.Remove(coordinates);
				if (!this._blockedCells.Contains(coordinates))
				{
					this._waterSimulator.FullyUnblockCell(coordinates);
				}
			}
		}

		// Token: 0x040000AE RID: 174
		public readonly MapSize _mapSize;

		// Token: 0x040000AF RID: 175
		public readonly WaterSimulator _waterSimulator;

		// Token: 0x040000B0 RID: 176
		public readonly List<Vector2Int> _blockedCells = new List<Vector2Int>();
	}
}
