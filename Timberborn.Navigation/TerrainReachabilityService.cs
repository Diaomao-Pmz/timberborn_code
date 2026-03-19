using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x0200008B RID: 139
	public class TerrainReachabilityService
	{
		// Token: 0x06000306 RID: 774 RVA: 0x00009B51 File Offset: 0x00007D51
		public TerrainReachabilityService(TerrainNavMeshGraph terrainNavMeshGraph, RestrictedNodeMap restrictedNodeMap)
		{
			this._terrainNavMeshGraph = terrainNavMeshGraph;
			this._restrictedNodeMap = restrictedNodeMap;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009B7D File Offset: 0x00007D7D
		public void GetReachableNeighborsInRange(int startingNodeId, int range, List<int> reachableRoadNodes)
		{
			this.VisitNode(startingNodeId, 0f);
			this.TraverseNodes(range);
			this.CopyNodes(reachableRoadNodes);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009B9C File Offset: 0x00007D9C
		public void TraverseNodes(int range)
		{
			while (!this._nodesToVisit.IsEmpty<TerrainReachabilityService.NodeToVisit>())
			{
				TerrainReachabilityService.NodeToVisit nodeToVisit = this._nodesToVisit.Dequeue();
				if (nodeToVisit.Distance < (float)range)
				{
					this.VisitNeighbors(nodeToVisit);
				}
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public void CopyNodes(List<int> reachableRoadNodes)
		{
			foreach (int num in this._visitedNodes)
			{
				if (!this._restrictedNodeMap.IsNodeRestricted(num))
				{
					reachableRoadNodes.Add(num);
				}
			}
			this._visitedNodes.Clear();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009C44 File Offset: 0x00007E44
		public void VisitNeighbors(TerrainReachabilityService.NodeToVisit nodeToVisit)
		{
			ReadOnlyList<NavMeshNode> neighbors = this._terrainNavMeshGraph.GetNeighbors(nodeToVisit.Id);
			for (int i = 0; i < neighbors.Count; i++)
			{
				NavMeshNode navMeshNode = neighbors[i];
				int id = navMeshNode.Id;
				if (!this._visitedNodes.Contains(id))
				{
					this.VisitNode(id, nodeToVisit.Distance + navMeshNode.Cost);
				}
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00009CAB File Offset: 0x00007EAB
		public void VisitNode(int nodeId, float distance)
		{
			this._visitedNodes.Add(nodeId);
			this._nodesToVisit.Enqueue(new TerrainReachabilityService.NodeToVisit(nodeId, distance));
		}

		// Token: 0x0400017F RID: 383
		public readonly TerrainNavMeshGraph _terrainNavMeshGraph;

		// Token: 0x04000180 RID: 384
		public readonly RestrictedNodeMap _restrictedNodeMap;

		// Token: 0x04000181 RID: 385
		public readonly Queue<TerrainReachabilityService.NodeToVisit> _nodesToVisit = new Queue<TerrainReachabilityService.NodeToVisit>();

		// Token: 0x04000182 RID: 386
		public readonly HashSet<int> _visitedNodes = new HashSet<int>();

		// Token: 0x0200008C RID: 140
		public readonly struct NodeToVisit
		{
			// Token: 0x17000052 RID: 82
			// (get) Token: 0x0600030C RID: 780 RVA: 0x00009CCC File Offset: 0x00007ECC
			public int Id { get; }

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x0600030D RID: 781 RVA: 0x00009CD4 File Offset: 0x00007ED4
			public float Distance { get; }

			// Token: 0x0600030E RID: 782 RVA: 0x00009CDC File Offset: 0x00007EDC
			public NodeToVisit(int id, float distance)
			{
				this.Id = id;
				this.Distance = distance;
			}
		}
	}
}
