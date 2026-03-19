using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000026 RID: 38
	public class GlobalReachabilityService : ILoadableSingleton, IPrioritizedSingletonInstantNavMeshListener
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00004C90 File Offset: 0x00002E90
		public GlobalReachabilityService(NodeIdService nodeIdService, InstantTerrainNavMeshGraph instantTerrainNavMeshGraph)
		{
			this._nodeIdService = nodeIdService;
			this._instantTerrainNavMeshGraph = instantTerrainNavMeshGraph;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004CBC File Offset: 0x00002EBC
		public void Load()
		{
			this._visitedNodes = new bool[this._nodeIdService.NumberOfNodes];
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public bool AreaReachable(Vector3 a, int b)
		{
			return this._nodeIdService.Contains(a) && this.AreaReachable(this._nodeIdService.WorldToId(a), b);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004CF9 File Offset: 0x00002EF9
		public bool AreaReachable(Vector3 a, Vector3 b)
		{
			return this._nodeIdService.Contains(a) && this._nodeIdService.Contains(b) && this.AreaReachable(this._nodeIdService.WorldToId(a), this._nodeIdService.WorldToId(b));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004D37 File Offset: 0x00002F37
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.ClearAreas();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004D3F File Offset: 0x00002F3F
		public bool AreaReachable(int aNodeId, int bNodeId)
		{
			return this.GetAreaOfNode(aNodeId) == this.GetAreaOfNode(bNodeId);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004D54 File Offset: 0x00002F54
		public int GetAreaOfNode(int nodeId)
		{
			int result;
			if (!this._nodesWithAssignedArea.TryGetValue(nodeId, out result))
			{
				this.CreateAreaReachableFromNode(nodeId, this._areaCounter);
				int areaCounter = this._areaCounter;
				this._areaCounter = areaCounter + 1;
				result = areaCounter;
			}
			return result;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004D91 File Offset: 0x00002F91
		public void ClearAreas()
		{
			this._areaCounter = 0;
			this._nodesWithAssignedArea.Clear();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004DA5 File Offset: 0x00002FA5
		public void CreateAreaReachableFromNode(int startingNodeId, int area)
		{
			Array.Clear(this._visitedNodes, 0, this._visitedNodes.Length);
			this.VisitNode(startingNodeId, area);
			while (!this._nodesToVisit.IsEmpty<int>())
			{
				this.VisitNeighbors(this._nodesToVisit.Dequeue(), area);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public void VisitNeighbors(int nodeId, int area)
		{
			ReadOnlyList<NavMeshNode> neighbors = this._instantTerrainNavMeshGraph.GetNeighbors(nodeId);
			for (int i = 0; i < neighbors.Count; i++)
			{
				int id = neighbors[i].Id;
				if (!this._visitedNodes[id])
				{
					this.VisitNode(id, area);
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004E33 File Offset: 0x00003033
		public void VisitNode(int nodeId, int area)
		{
			this._visitedNodes[nodeId] = true;
			this._nodesToVisit.Enqueue(nodeId);
			this._nodesWithAssignedArea[nodeId] = area;
		}

		// Token: 0x0400007B RID: 123
		public readonly NodeIdService _nodeIdService;

		// Token: 0x0400007C RID: 124
		public readonly InstantTerrainNavMeshGraph _instantTerrainNavMeshGraph;

		// Token: 0x0400007D RID: 125
		public readonly Dictionary<int, int> _nodesWithAssignedArea = new Dictionary<int, int>();

		// Token: 0x0400007E RID: 126
		public readonly Queue<int> _nodesToVisit = new Queue<int>();

		// Token: 0x0400007F RID: 127
		public int _areaCounter;

		// Token: 0x04000080 RID: 128
		public bool[] _visitedNodes;
	}
}
