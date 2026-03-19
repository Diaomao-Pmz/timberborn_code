using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Navigation;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x02000007 RID: 7
	public class BlockableEntranceBuilding : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public BlockableEntranceBuilding(IBlockService blockService, PreviewBlockService previewBlockService, ITerrainService terrainService, INavMeshService navMeshService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
			this._terrainService = terrainService;
			this._navMeshService = navMeshService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002125 File Offset: 0x00000325
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002133 File Offset: 0x00000333
		public bool IsEntranceBlocked()
		{
			return this._blockObject.HasEntrance && (this.IsBlockedByTerrain() || (this.IsBlockedByNavMesh() && this.IsBlockedByOtherObject()));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000215E File Offset: 0x0000035E
		public bool IsEntranceBlockedByCoordinates(IEnumerable<Vector3Int> coordinates)
		{
			return this.IsEntranceBlocked() && coordinates.Contains(this.EntranceCoordinates);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002176 File Offset: 0x00000376
		public bool IsEntranceInaccessible()
		{
			return this._blockObject.HasEntrance && (this.IsBlockedByTerrain() || this.IsBlockedByNavMesh());
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002197 File Offset: 0x00000397
		public Vector3Int EntranceCoordinates
		{
			get
			{
				return this._blockObject.PositionedEntrance.Coordinates;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A9 File Offset: 0x000003A9
		public bool IsBlockedByTerrain()
		{
			return this._terrainService.Underground(this.EntranceCoordinates);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021BC File Offset: 0x000003BC
		public bool IsBlockedByNavMesh()
		{
			Vector3Int doorstepCoordinates = this._blockObject.PositionedEntrance.DoorstepCoordinates;
			return !this._navMeshService.AreConnectedPreview(doorstepCoordinates, this.EntranceCoordinates);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021F0 File Offset: 0x000003F0
		public bool IsBlockedByOtherObject()
		{
			BlockObject blockObject = this._previewBlockService.GetBottomPreviewAt(this.EntranceCoordinates);
			if (blockObject == null || blockObject.Overridable)
			{
				blockObject = this._blockService.GetBottomObjectAt(this.EntranceCoordinates);
				return blockObject != null && !blockObject.Overridable;
			}
			return true;
		}

		// Token: 0x04000008 RID: 8
		public readonly IBlockService _blockService;

		// Token: 0x04000009 RID: 9
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400000A RID: 10
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000B RID: 11
		public readonly INavMeshService _navMeshService;

		// Token: 0x0400000C RID: 12
		public BlockObject _blockObject;
	}
}
