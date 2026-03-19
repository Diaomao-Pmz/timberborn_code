using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000021 RID: 33
	public class BlockOccupancyService : IBlockOccupancyService
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000046BE File Offset: 0x000028BE
		public BlockOccupancyService(EntityComponentRegistry entityComponentRegistry)
		{
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000046D0 File Offset: 0x000028D0
		public bool OccupantPresentOnArea(BlockObject blockObject, float minDistanceFromArea)
		{
			IEnumerable<BlockOccupant> occupants = this._entityComponentRegistry.GetEnabled<BlockOccupant>();
			return blockObject.PositionedBlocks.GetOccupiedCoordinates().Any((Vector3Int coords) => occupants.Any((BlockOccupant beaver) => BlockOccupancyService.OccupantAtCoords(beaver, coords, minDistanceFromArea)));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004718 File Offset: 0x00002918
		public static bool OccupantAtCoords(BlockOccupant occupant, Vector3Int coordinates, float minDistanceFromTile)
		{
			float num = (float)coordinates.x - minDistanceFromTile;
			float num2 = (float)(coordinates.x + 1) + minDistanceFromTile;
			float num3 = (float)coordinates.y - minDistanceFromTile;
			float num4 = (float)(coordinates.y + 1) + minDistanceFromTile;
			Vector3 gridCoordinates = occupant.GridCoordinates;
			float x = gridCoordinates.x;
			float y = gridCoordinates.y;
			int num5 = Mathf.FloorToInt(gridCoordinates.z);
			return x >= num && x <= num2 && y >= num3 && y <= num4 && coordinates.z == num5;
		}

		// Token: 0x0400008D RID: 141
		public readonly EntityComponentRegistry _entityComponentRegistry;
	}
}
