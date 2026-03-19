using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BlockSystemNavigation;
using Timberborn.TerrainSystem;

namespace Timberborn.BlockObstacles
{
	// Token: 0x0200000A RID: 10
	public class BlockOccupier : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002492 File Offset: 0x00000692
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000249A File Offset: 0x0000069A
		public BlockObject BlockObject { get; private set; }

		// Token: 0x0600001A RID: 26 RVA: 0x000024A3 File Offset: 0x000006A3
		public BlockOccupier(ITerrainService terrainService, IBlockService blockService)
		{
			this._terrainService = terrainService;
			this._blockService = blockService;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024C4 File Offset: 0x000006C4
		public void Awake()
		{
			this.BlockObject = base.GetComponent<BlockObject>();
			this._blockObjectNavMesh = base.GetComponent<IBlockObjectNavMesh>();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024DE File Offset: 0x000006DE
		public bool CanBeAddedToServices()
		{
			return !this.IsUnderground() && this.CoordinatesHaveNoOtherObjectsExceptFloor();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024F0 File Offset: 0x000006F0
		public void AddToServices()
		{
			if (!this._isAddedToServices)
			{
				this.BlockObject.MarkAsFinishedAndAddToServices();
				this._blockObjectNavMesh.RecalculateNavMeshObject();
				this._blockObjectNavMesh.NavMeshObject.EnqueueAddToRegularNavMesh();
				this._isAddedToServices = true;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002527 File Offset: 0x00000727
		public void RemoveFromServices()
		{
			if (this._isAddedToServices)
			{
				this.BlockObject.MarkAsPreview();
				this._blockObjectNavMesh.NavMeshObject.EnqueueRemoveFromRegularNavMesh();
				this._isAddedToServices = false;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002553 File Offset: 0x00000753
		public bool IsUnderground()
		{
			return this._terrainService.Underground(this.BlockObject.Coordinates);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000256B File Offset: 0x0000076B
		public bool CoordinatesHaveNoOtherObjectsExceptFloor()
		{
			this._blockService.GetIntersectingObjectsAt(this.BlockObject.Coordinates, ~BlockOccupations.Floor, this._blockObjectCache);
			bool result = this.HasNoOtherObject();
			this._blockObjectCache.Clear();
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000259C File Offset: 0x0000079C
		public bool HasNoOtherObject()
		{
			foreach (BlockObject blockObject in this._blockObjectCache)
			{
				if (blockObject != this.BlockObject && !blockObject.Overridable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400000E RID: 14
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000F RID: 15
		public readonly IBlockService _blockService;

		// Token: 0x04000010 RID: 16
		public IBlockObjectNavMesh _blockObjectNavMesh;

		// Token: 0x04000011 RID: 17
		public bool _isAddedToServices;

		// Token: 0x04000012 RID: 18
		public readonly List<BlockObject> _blockObjectCache = new List<BlockObject>();
	}
}
