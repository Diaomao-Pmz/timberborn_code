using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200005E RID: 94
	public class NavMeshPositionService
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00006608 File Offset: 0x00004808
		public NavMeshPositionService(INavMeshService navMeshService, TerrainNavMeshGraph terrainNavMeshGraph, NodeIdService nodeIdService)
		{
			this._navMeshService = navMeshService;
			this._terrainNavMeshGraph = terrainNavMeshGraph;
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000663C File Offset: 0x0000483C
		public Vector3? ClosestPositionOnNavMesh(Vector3 originWorld, float maxDistance)
		{
			Vector3Int vector3Int = NavigationCoordinateSystem.WorldToGridInt(originWorld);
			this._nodeQueue.Clear();
			this._visitedNodes.Clear();
			this.EnqueueNode(vector3Int);
			while (!this._nodeQueue.IsEmpty<Vector3Int>())
			{
				Vector3Int vector3Int2 = this._nodeQueue.Dequeue();
				int nodeId = this._nodeIdService.GridToId(vector3Int2);
				if (this._navMeshService.IsOnNavMesh(vector3Int2) && this._terrainNavMeshGraph.IsConnectedToDefaultGroup(nodeId))
				{
					return new Vector3?(NavigationCoordinateSystem.GridToWorld(vector3Int2));
				}
				this.EnqueueNeighbors(vector3Int, vector3Int2, maxDistance);
			}
			return null;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000066D0 File Offset: 0x000048D0
		public void EnqueueNeighbors(Vector3Int origin, Vector3Int navMeshCoords, float maxDistance)
		{
			foreach (Vector3Int vector3Int in Deltas.Neighbors6Vector3Int)
			{
				Vector3Int vector3Int2 = navMeshCoords + vector3Int;
				if (Vector3Int.Distance(origin, vector3Int2) <= maxDistance)
				{
					this.EnqueueNode(vector3Int2);
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006712 File Offset: 0x00004912
		public void EnqueueNode(Vector3Int nodeCoords)
		{
			if (!this._visitedNodes.Contains(nodeCoords))
			{
				this._nodeQueue.Enqueue(nodeCoords);
				this._visitedNodes.Add(nodeCoords);
			}
		}

		// Token: 0x040000D0 RID: 208
		public readonly INavMeshService _navMeshService;

		// Token: 0x040000D1 RID: 209
		public readonly TerrainNavMeshGraph _terrainNavMeshGraph;

		// Token: 0x040000D2 RID: 210
		public readonly NodeIdService _nodeIdService;

		// Token: 0x040000D3 RID: 211
		public readonly Queue<Vector3Int> _nodeQueue = new Queue<Vector3Int>();

		// Token: 0x040000D4 RID: 212
		public readonly HashSet<Vector3Int> _visitedNodes = new HashSet<Vector3Int>();
	}
}
