using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200005F RID: 95
	public class NavMeshService : INavMeshService
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x0000673B File Offset: 0x0000493B
		public NavMeshService(TerrainNavMeshGraph terrainNavMeshGraph, PreviewTerrainNavMeshGraph previewTerrainNavMeshGraph, NodeIdService nodeIdService, NavMeshUpdater navMeshUpdater, InstantRoadNavMeshGraph instantRoadNavMeshGraph, InstantTerrainNavMeshGraph instantTerrainNavMeshGraph, PreviewRoadNavMeshGraph previewRoadNavMeshGraph)
		{
			this._terrainNavMeshGraph = terrainNavMeshGraph;
			this._previewTerrainNavMeshGraph = previewTerrainNavMeshGraph;
			this._nodeIdService = nodeIdService;
			this._navMeshUpdater = navMeshUpdater;
			this._instantRoadNavMeshGraph = instantRoadNavMeshGraph;
			this._instantTerrainNavMeshGraph = instantTerrainNavMeshGraph;
			this._previewRoadNavMeshGraph = previewRoadNavMeshGraph;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006778 File Offset: 0x00004978
		public void AddEdge(NavMeshEdge navMeshEdge)
		{
			NavMeshUpdater navMeshUpdater = this._navMeshUpdater;
			NavMeshChangeSpecification navMeshChangeSpecification = new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.AddEdge);
			navMeshUpdater.EnqueueRegularChange(navMeshChangeSpecification);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000679C File Offset: 0x0000499C
		public void RemoveEdge(NavMeshEdge navMeshEdge)
		{
			NavMeshUpdater navMeshUpdater = this._navMeshUpdater;
			NavMeshChangeSpecification navMeshChangeSpecification = new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.RemoveEdge);
			navMeshUpdater.EnqueueRegularChange(navMeshChangeSpecification);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000067C0 File Offset: 0x000049C0
		public void BlockEdge(NavMeshEdge navMeshEdge)
		{
			NavMeshUpdater navMeshUpdater = this._navMeshUpdater;
			NavMeshChangeSpecification navMeshChangeSpecification = new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.BlockEdge);
			navMeshUpdater.EnqueueRegularChange(navMeshChangeSpecification);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000067E4 File Offset: 0x000049E4
		public void UnblockEdge(NavMeshEdge navMeshEdge)
		{
			NavMeshUpdater navMeshUpdater = this._navMeshUpdater;
			NavMeshChangeSpecification navMeshChangeSpecification = new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.UnblockEdge);
			navMeshUpdater.EnqueueRegularChange(navMeshChangeSpecification);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006808 File Offset: 0x00004A08
		public void AddPreviewEdge(NavMeshEdge navMeshEdge)
		{
			NavMeshUpdater navMeshUpdater = this._navMeshUpdater;
			NavMeshChangeSpecification navMeshChangeSpecification = new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.AddEdge);
			navMeshUpdater.EnqueuePreviewChange(navMeshChangeSpecification);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000682C File Offset: 0x00004A2C
		public void RemovePreviewEdge(NavMeshEdge navMeshEdge)
		{
			NavMeshUpdater navMeshUpdater = this._navMeshUpdater;
			NavMeshChangeSpecification navMeshChangeSpecification = new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.RemoveEdge);
			navMeshUpdater.EnqueuePreviewChange(navMeshChangeSpecification);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000684E File Offset: 0x00004A4E
		public bool IsOnNavMesh(Vector3Int coordinates)
		{
			return this._nodeIdService.Contains(coordinates) && this._terrainNavMeshGraph.IsOnNavMesh(this._nodeIdService.GridToId(coordinates));
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006877 File Offset: 0x00004A77
		public bool AreConnected(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return this.AreConnected(this._terrainNavMeshGraph, coordinatesA, coordinatesB);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006887 File Offset: 0x00004A87
		public bool AreConnectedPreview(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return this.AreConnected(this._previewTerrainNavMeshGraph, coordinatesA, coordinatesB);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006897 File Offset: 0x00004A97
		public bool AreConnectedRoadPreview(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return this.AreConnected(this._previewRoadNavMeshGraph, coordinatesA, coordinatesB);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000068A7 File Offset: 0x00004AA7
		public bool AreConnectedInstant(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return this.AreConnected(this._instantTerrainNavMeshGraph, coordinatesA, coordinatesB);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000068B7 File Offset: 0x00004AB7
		public bool AreConnectedRoadInstant(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return this.AreConnected(this._instantRoadNavMeshGraph, coordinatesA, coordinatesB);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000068C7 File Offset: 0x00004AC7
		public bool AreConnected(TerrainNavMeshGraph terrainNavMeshGraph, Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return this.EdgeIsInMap(coordinatesA, coordinatesB) && terrainNavMeshGraph.AreConnected(this._nodeIdService.GridToId(coordinatesA), this._nodeIdService.GridToId(coordinatesB));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000068F3 File Offset: 0x00004AF3
		public bool AreConnected(RoadNavMeshGraph roadNavMeshGraph, Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return this.EdgeIsInMap(coordinatesA, coordinatesB) && roadNavMeshGraph.AreConnected(this._nodeIdService.GridToId(coordinatesA), this._nodeIdService.GridToId(coordinatesB));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000691F File Offset: 0x00004B1F
		public bool EdgeIsInMap(Vector3Int start, Vector3Int end)
		{
			return this._nodeIdService.Contains(start) && this._nodeIdService.Contains(end);
		}

		// Token: 0x040000D5 RID: 213
		public readonly TerrainNavMeshGraph _terrainNavMeshGraph;

		// Token: 0x040000D6 RID: 214
		public readonly PreviewTerrainNavMeshGraph _previewTerrainNavMeshGraph;

		// Token: 0x040000D7 RID: 215
		public readonly NodeIdService _nodeIdService;

		// Token: 0x040000D8 RID: 216
		public readonly NavMeshUpdater _navMeshUpdater;

		// Token: 0x040000D9 RID: 217
		public readonly InstantRoadNavMeshGraph _instantRoadNavMeshGraph;

		// Token: 0x040000DA RID: 218
		public readonly InstantTerrainNavMeshGraph _instantTerrainNavMeshGraph;

		// Token: 0x040000DB RID: 219
		public readonly PreviewRoadNavMeshGraph _previewRoadNavMeshGraph;
	}
}
