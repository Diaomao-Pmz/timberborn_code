using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.TerrainPhysics;
using UnityEngine;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000028 RID: 40
	public class PhysicallySupportedConstructionSiteUpdater : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00004739 File Offset: 0x00002939
		public PhysicallySupportedConstructionSiteUpdater(IBlockService blockService, ITerrainPhysicsService terrainPhysicsService)
		{
			this._blockService = blockService;
			this._terrainPhysicsService = terrainPhysicsService;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000474F File Offset: 0x0000294F
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000475D File Offset: 0x0000295D
		public void OnEnterFinishedState()
		{
			this.UpdateNeighbours();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003A19 File Offset: 0x00001C19
		public void OnExitFinishedState()
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004768 File Offset: 0x00002968
		public void UpdateNeighbours()
		{
			if (this._blockObject.Solid)
			{
				foreach (Block block in this._blockObject.PositionedBlocks.GetAllBlocks())
				{
					if (block.Stackable.IsStackable())
					{
						Vector3Int coordinates = block.Coordinates;
						foreach (Vector3Int vector3Int in this._terrainPhysicsService.PhysicsSupportDeltas)
						{
							this.UpdateNeighbour(coordinates + vector3Int);
						}
					}
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004820 File Offset: 0x00002A20
		public void UpdateNeighbour(Vector3Int coordinates)
		{
			foreach (PhysicallySupportedConstructionSite physicallySupportedConstructionSite in this._blockService.GetObjectsWithComponentAt<PhysicallySupportedConstructionSite>(coordinates))
			{
				physicallySupportedConstructionSite.Validate();
			}
		}

		// Token: 0x04000075 RID: 117
		public readonly IBlockService _blockService;

		// Token: 0x04000076 RID: 118
		public readonly ITerrainPhysicsService _terrainPhysicsService;

		// Token: 0x04000077 RID: 119
		public BlockObject _blockObject;
	}
}
