using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.LevelVisibilitySystem;
using Timberborn.MapStateSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000066 RID: 102
	public class StackableBlockService
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x00007FC6 File Offset: 0x000061C6
		public StackableBlockService(IBlockService blockService, ITerrainService terrainService, MapSize mapSize, ILevelVisibilityService levelVisibilityService)
		{
			this._blockService = blockService;
			this._terrainService = terrainService;
			this._mapSize = mapSize;
			this._levelVisibilityService = levelVisibilityService;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007FEC File Offset: 0x000061EC
		public bool IsStackableBlockAt(Vector3Int coords, bool finishedOnly = false)
		{
			ReadOnlyList<BlockObject> objectsAt = this._blockService.GetObjectsAt(coords);
			for (int i = 0; i < objectsAt.Count; i++)
			{
				BlockObject blockObject = objectsAt[i];
				if ((!finishedOnly || blockObject.IsFinished) && blockObject.PositionedBlocks.GetBlock(coords).Stackable.IsStackable())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000804C File Offset: 0x0000624C
		public bool IsFinishedStackableBlockAt(Vector3Int coords)
		{
			ReadOnlyList<BlockObject> objectsAt = this._blockService.GetObjectsAt(coords);
			for (int i = 0; i < objectsAt.Count; i++)
			{
				BlockObject blockObject = objectsAt[i];
				if (blockObject.IsFinished && blockObject.PositionedBlocks.GetBlock(coords).Stackable.IsStackable())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000080A8 File Offset: 0x000062A8
		public bool IsUnfinishedGroundBlockAt(Vector3Int coords)
		{
			ReadOnlyList<BlockObject> objectsAt = this._blockService.GetObjectsAt(coords);
			for (int i = 0; i < objectsAt.Count; i++)
			{
				if (objectsAt[i].PositionedBlocks.GetBlock(coords).Stackable.IsUnfinishedGround())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000080F9 File Offset: 0x000062F9
		public IEnumerable<Vector3Int> GetGroundOrStackableBlocks(IEnumerable<Vector2Int> coordinates, bool finishedOnly = false)
		{
			foreach (Vector2Int coordinate in coordinates)
			{
				int num;
				for (int z = 0; z < this._mapSize.TotalSize.z; z = num + 1)
				{
					Vector3Int vector3Int = coordinate.ToVector3Int(z);
					if ((this._terrainService.OnGround(vector3Int) && z <= this._levelVisibilityService.MaxVisibleLevel) || this.IsVisibleStackableAt(vector3Int, finishedOnly))
					{
						yield return vector3Int;
					}
					num = z;
				}
				coordinate = default(Vector2Int);
			}
			IEnumerator<Vector2Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00008117 File Offset: 0x00006317
		public bool IsVisibleStackableAt(Vector3Int coords, bool finishedOnly)
		{
			return this._levelVisibilityService.BlockIsVisible(coords) && this.IsStackableBlockAt(new Vector3Int(coords.x, coords.y, coords.z - 1), finishedOnly);
		}

		// Token: 0x0400013F RID: 319
		public readonly IBlockService _blockService;

		// Token: 0x04000140 RID: 320
		public readonly ITerrainService _terrainService;

		// Token: 0x04000141 RID: 321
		public readonly MapSize _mapSize;

		// Token: 0x04000142 RID: 322
		public readonly ILevelVisibilityService _levelVisibilityService;
	}
}
