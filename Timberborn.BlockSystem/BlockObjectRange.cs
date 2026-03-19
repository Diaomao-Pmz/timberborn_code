using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.TerrainQueryingSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000016 RID: 22
	public class BlockObjectRange : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000098 RID: 152 RVA: 0x000036D8 File Offset: 0x000018D8
		public BlockObjectRange(TerrainAreaService terrainAreaService, StackableBlockService stackableBlockService)
		{
			this._terrainAreaService = terrainAreaService;
			this._stackableBlockService = stackableBlockService;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000036EE File Offset: 0x000018EE
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003708 File Offset: 0x00001908
		public IEnumerable<Vector2Int> GetBlocksInRectangularRadius(int radius)
		{
			Vector2 vector = this._blockObjectCenter.GridCenter.XY();
			Vector2Int vector2Int = this.RotatedSize();
			ValueTuple<int, int> areaBounds = BlockObjectRange.GetAreaBounds(vector.x, vector2Int.x, radius);
			int item = areaBounds.Item1;
			int maxX = areaBounds.Item2;
			areaBounds = BlockObjectRange.GetAreaBounds(vector.y, vector2Int.y, radius);
			int minY = areaBounds.Item1;
			int maxY = areaBounds.Item2;
			int num;
			for (int x = item; x < maxX; x = num + 1)
			{
				for (int y = minY; y < maxY; y = num + 1)
				{
					yield return new Vector2Int(x, y);
					num = y;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003720 File Offset: 0x00001920
		public IEnumerable<Vector3Int> GetBlocksOnTerrainInRectangularRadius(int radius)
		{
			IEnumerable<Vector2Int> blocksInRectangularRadius = this.GetBlocksInRectangularRadius(radius);
			return this._terrainAreaService.InMapCoordinates(blocksInRectangularRadius);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003744 File Offset: 0x00001944
		public IEnumerable<Vector3Int> GetBlocksOnTerrainOrStackableInRectangularRadius(int radius, bool finishedOnly)
		{
			IEnumerable<Vector2Int> blocksInRectangularRadius = this.GetBlocksInRectangularRadius(radius);
			return this._stackableBlockService.GetGroundOrStackableBlocks(blocksInRectangularRadius, finishedOnly);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003768 File Offset: 0x00001968
		public Vector2Int RotatedSize()
		{
			Vector2Int result = this._blockObject.Blocks.Size.XY();
			Orientation orientation = this._blockObject.Orientation;
			if (orientation != Orientation.Cw90 && orientation != Orientation.Cw270)
			{
				return result;
			}
			return new Vector2Int(result.y, result.x);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000037B4 File Offset: 0x000019B4
		public static ValueTuple<int, int> GetAreaBounds(float center, int size, int radius)
		{
			int item = (int)(center - (float)size / 2f - (float)radius);
			int item2 = (int)(center + (float)size / 2f + (float)radius);
			return new ValueTuple<int, int>(item, item2);
		}

		// Token: 0x0400005E RID: 94
		public readonly TerrainAreaService _terrainAreaService;

		// Token: 0x0400005F RID: 95
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x04000060 RID: 96
		public BlockObject _blockObject;

		// Token: 0x04000061 RID: 97
		public BlockObjectCenter _blockObjectCenter;
	}
}
