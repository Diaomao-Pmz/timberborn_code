using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainLevelValidation
{
	// Token: 0x02000008 RID: 8
	public class ContinuousTerrainConstraint : BaseComponent, IAwakableComponent, IInfiniteUndergroundModel
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002157 File Offset: 0x00000357
		public ContinuousTerrainConstraint(ITerrainService terrainService, MapIndexService mapIndexService)
		{
			this._terrainService = terrainService;
			this._mapIndexService = mapIndexService;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002170 File Offset: 0x00000370
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			if (this._blockObject.Blocks.GetAllBlocks().Any((Block block) => block.MatterBelow == MatterBelow.Ground && !block.OccupyAllBelow))
			{
				throw new NotSupportedException(string.Format("Some of the ground blocks of {0} ", this) + "are not set to occupy all below.");
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021DC File Offset: 0x000003DC
		public bool IsNotOnFirstColumnOfTerrain()
		{
			foreach (Vector3Int vector3Int in this._blockObject.PositionedBlocks.GetFoundationCoordinates())
			{
				if (this._terrainService.Contains(vector3Int))
				{
					int index3D = this._mapIndexService.CellToIndex(vector3Int.XY());
					if (this._terrainService.GetColumnCeiling(index3D) < vector3Int.z)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400000A RID: 10
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000B RID: 11
		public readonly MapIndexService _mapIndexService;

		// Token: 0x0400000C RID: 12
		public BlockObject _blockObject;
	}
}
