using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x0200000D RID: 13
	public class TerrainOnBlockObjectFinder
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000283F File Offset: 0x00000A3F
		public TerrainOnBlockObjectFinder(ITerrainService terrainService, StackableBlockService stackableBlockService)
		{
			this._terrainService = terrainService;
			this._stackableBlockService = stackableBlockService;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002858 File Offset: 0x00000A58
		public void Find(BlockObject blockObject, Queue<Vector3Int> terrainCoordinates)
		{
			foreach (Block block in blockObject.PositionedBlocks.GetOccupiedBlocks())
			{
				Vector3Int coordinates = block.Coordinates;
				Vector3Int vector3Int = coordinates.Above();
				if (this.IsUnderground(vector3Int) && blockObject.IsFinished && block.Stackable.IsStackable())
				{
					terrainCoordinates.Enqueue(vector3Int);
				}
				if (blockObject.IsUnfinished && block.Stackable.IsUnfinishedGround())
				{
					if (this.IsUnderground(vector3Int))
					{
						terrainCoordinates.Enqueue(vector3Int);
					}
					foreach (Vector3Int vector3Int2 in Deltas.Neighbors4Vector3Int)
					{
						Vector3Int vector3Int3 = coordinates + vector3Int2;
						if (this.IsUnderground(vector3Int3))
						{
							terrainCoordinates.Enqueue(vector3Int3);
						}
					}
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002948 File Offset: 0x00000B48
		public bool IsUnderground(Vector3Int coordinates)
		{
			return this._terrainService.Underground(coordinates) || this._stackableBlockService.IsUnfinishedGroundBlockAt(coordinates);
		}

		// Token: 0x04000017 RID: 23
		public readonly ITerrainService _terrainService;

		// Token: 0x04000018 RID: 24
		public readonly StackableBlockService _stackableBlockService;
	}
}
