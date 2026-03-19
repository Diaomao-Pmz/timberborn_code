using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000025 RID: 37
	public class GateConflictDetector
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004974 File Offset: 0x00002B74
		public GateConflictDetector(NodeIdService nodeIdService, PreviewDistrictMap previewDistrictMap, PreviewRoadNavMeshGraph previewRoadNavMeshGraph, PreviewDistrictObstacleService previewDistrictObstacleService)
		{
			this._nodeIdService = nodeIdService;
			this._previewDistrictMap = previewDistrictMap;
			this._previewRoadNavMeshGraph = previewRoadNavMeshGraph;
			this._previewDistrictObstacleService = previewDistrictObstacleService;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000049DC File Offset: 0x00002BDC
		public bool CanOpenGateWithoutConflict(Vector3Int from, Vector3Int to, Vector3Int center, Dictionary<Vector3Int, Vector3Int> openGateCrossings)
		{
			int num = this._nodeIdService.GridToId(from);
			int num2 = this._nodeIdService.GridToId(to);
			int item = this._nodeIdService.GridToId(center);
			this._ignorableNodes.Add(num);
			this._ignorableNodes.Add(num2);
			this._ignorableNodes.Add(item);
			int num3 = 0;
			foreach (int key in this._previewDistrictMap.DistrictCenterNodeIds())
			{
				this._nodeToDistrict.Add(key, num3++);
			}
			int? num4 = this.FindDistrictId(num, openGateCrossings);
			int? num5 = this.FindDistrictId(num2, openGateCrossings);
			this._ignorableNodes.Clear();
			this._nodeToDistrict.Clear();
			return num4 == null || num5 == null || num4.Value == num5.Value;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public int? FindDistrictId(int startNodeId, Dictionary<Vector3Int, Vector3Int> openGateCrossings)
		{
			this._nodesToVisit.Clear();
			this._visitedNodes.Clear();
			if (this._previewDistrictObstacleService.IsSetObstacle(startNodeId))
			{
				return null;
			}
			this._visitedNodes.Add(startNodeId);
			this._nodesToVisit.Enqueue(startNodeId);
			while (this._nodesToVisit.Count > 0)
			{
				int num = this._nodesToVisit.Dequeue();
				int value;
				if (this._nodeToDistrict.TryGetValue(num, out value))
				{
					return new int?(value);
				}
				this.VisitNode(num, openGateCrossings);
			}
			return null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004B78 File Offset: 0x00002D78
		public void VisitNode(int nodeId, Dictionary<Vector3Int, Vector3Int> openGateCrossings)
		{
			foreach (NavMeshNode navMeshNode in this._previewRoadNavMeshGraph.GetNeighbors(nodeId))
			{
				this._neighborNodes.Add(navMeshNode.Id);
			}
			Vector3Int key = this._nodeIdService.IdToGrid(nodeId);
			Vector3Int coordinates;
			if (openGateCrossings.TryGetValue(key, out coordinates))
			{
				int item = this._nodeIdService.GridToId(coordinates);
				this._neighborNodes.Add(item);
			}
			for (int i = 0; i < this._neighborNodes.Count; i++)
			{
				int num = this._neighborNodes[i];
				if (!this._visitedNodes.Contains(num) && !this._ignorableNodes.Contains(num) && !this._previewDistrictObstacleService.IsSetObstacle(num))
				{
					this._visitedNodes.Add(num);
					this._nodesToVisit.Enqueue(num);
				}
			}
			this._neighborNodes.Clear();
		}

		// Token: 0x04000072 RID: 114
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000073 RID: 115
		public readonly PreviewDistrictMap _previewDistrictMap;

		// Token: 0x04000074 RID: 116
		public readonly PreviewRoadNavMeshGraph _previewRoadNavMeshGraph;

		// Token: 0x04000075 RID: 117
		public readonly PreviewDistrictObstacleService _previewDistrictObstacleService;

		// Token: 0x04000076 RID: 118
		public readonly Queue<int> _nodesToVisit = new Queue<int>();

		// Token: 0x04000077 RID: 119
		public readonly HashSet<int> _visitedNodes = new HashSet<int>();

		// Token: 0x04000078 RID: 120
		public readonly List<int> _neighborNodes = new List<int>();

		// Token: 0x04000079 RID: 121
		public readonly HashSet<int> _ignorableNodes = new HashSet<int>();

		// Token: 0x0400007A RID: 122
		public readonly Dictionary<int, int> _nodeToDistrict = new Dictionary<int, int>();
	}
}
