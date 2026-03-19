using System;
using System.Collections.Generic;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Brushes
{
	// Token: 0x02000008 RID: 8
	public class BrushShapeIterator
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021FD File Offset: 0x000003FD
		public BrushShapeIterator(ITerrainService terrainService)
		{
			this._terrainService = terrainService;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000220C File Offset: 0x0000040C
		public IEnumerable<Vector3Int> IterateShape(Vector3Int center, int size, BrushShape brushShape)
		{
			if (brushShape == BrushShape.Square)
			{
				return this.IterateSquare(center, size);
			}
			if (brushShape != BrushShape.Round)
			{
				throw new ArgumentException(string.Format("Unexpected {0}: {1}", "BrushShape", brushShape));
			}
			return this.IterateRound(center, size);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002243 File Offset: 0x00000443
		public IEnumerable<Vector3Int> IterateSquare(Vector3Int center, int size)
		{
			int num = size - 1;
			int minX = center.x - num;
			int maxX = center.x + num;
			int y = center.y - num;
			int maxY = center.y + num;
			Vector3Int coords = new Vector3Int(0, 0, center.z);
			coords.y = y;
			while (coords.y <= maxY)
			{
				coords.x = minX;
				int num2;
				while (coords.x <= maxX)
				{
					if (this._terrainService.Contains(coords))
					{
						yield return coords;
					}
					num2 = coords.x + 1;
					coords.x = num2;
				}
				num2 = coords.y + 1;
				coords.y = num2;
			}
			yield break;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002261 File Offset: 0x00000461
		public IEnumerable<Vector3Int> IterateRound(Vector3Int center, int size)
		{
			int num = size - 1;
			int minX = center.x - num;
			int maxX = center.x + num;
			int y = center.y - num;
			int maxY = center.y + num;
			Vector3Int coords = new Vector3Int(0, 0, center.z);
			coords.y = y;
			while (coords.y <= maxY)
			{
				coords.x = minX;
				int num2;
				while (coords.x <= maxX)
				{
					if (this._terrainService.Contains(coords) && Vector3.Distance(coords, center) + 0.7f <= (float)size)
					{
						yield return coords;
					}
					num2 = coords.x + 1;
					coords.x = num2;
				}
				num2 = coords.y + 1;
				coords.y = num2;
			}
			yield break;
		}

		// Token: 0x0400000F RID: 15
		public readonly ITerrainService _terrainService;
	}
}
