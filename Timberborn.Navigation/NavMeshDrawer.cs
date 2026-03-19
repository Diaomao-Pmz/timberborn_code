using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000056 RID: 86
	public class NavMeshDrawer : INavMeshDrawer, IPrioritizedSingletonNavMeshListener
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00005CAB File Offset: 0x00003EAB
		public NavMeshDrawer(NodeIdService nodeIdService, TerrainNavMeshGraph terrainNavMeshGraph, RestrictedNodeMap restrictedNodeMap)
		{
			this._nodeIdService = nodeIdService;
			this._terrainNavMeshGraph = terrainNavMeshGraph;
			this._restrictedNodeMap = restrictedNodeMap;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005CD3 File Offset: 0x00003ED3
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.UpdateNodes(navMeshUpdate.TerrainNodeIds);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005CE4 File Offset: 0x00003EE4
		public void DrawForOneFrameAroundCoordinates(Vector3Int coordinates)
		{
			Vector3 center = NavigationCoordinateSystem.GridToWorld(coordinates);
			foreach (int nodeId in this._nodesWithNeighbors)
			{
				this.DrawNodeAndItsEdges(center, nodeId);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005D40 File Offset: 0x00003F40
		public void UpdateNodes(ReadOnlyList<int> nodeIds)
		{
			for (int i = 0; i < nodeIds.Count; i++)
			{
				this.UpdateNode(nodeIds[i]);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005D70 File Offset: 0x00003F70
		public void UpdateNode(int nodeId)
		{
			if (this._terrainNavMeshGraph.GetNeighbors(nodeId).IsEmpty())
			{
				this._nodesWithNeighbors.Remove(nodeId);
				return;
			}
			this._nodesWithNeighbors.Add(nodeId);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public void DrawNodeAndItsEdges(Vector3 center, int nodeId)
		{
			Vector3 vector = this._nodeIdService.IdToWorld(nodeId);
			if (Vector3.Distance(center, vector) < 30f)
			{
				Color color = this._restrictedNodeMap.IsNodeRestricted(nodeId) ? Color.yellow : Color.cyan;
				NavMeshDrawer.DrawNode(vector, color);
				this.DrawEdges(nodeId, vector);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005E02 File Offset: 0x00004002
		public static void DrawNode(Vector3 nodePosition, Color color)
		{
			Debug.DrawRay(nodePosition, Vector3.up / 3f, color);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005E1C File Offset: 0x0000401C
		public void DrawEdges(int nodeId, Vector3 nodePosition)
		{
			foreach (NavMeshNode navMeshNode in this._terrainNavMeshGraph.GetNeighbors(nodeId))
			{
				Vector3 vector = this._nodeIdService.IdToWorld(navMeshNode.Id);
				Debug.DrawLine(nodePosition, vector, Color.red);
			}
		}

		// Token: 0x040000AF RID: 175
		public readonly NodeIdService _nodeIdService;

		// Token: 0x040000B0 RID: 176
		public readonly TerrainNavMeshGraph _terrainNavMeshGraph;

		// Token: 0x040000B1 RID: 177
		public readonly RestrictedNodeMap _restrictedNodeMap;

		// Token: 0x040000B2 RID: 178
		public readonly HashSet<int> _nodesWithNeighbors = new HashSet<int>();
	}
}
