using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000021 RID: 33
	public class FlowFieldPathBuilder
	{
		// Token: 0x060000DB RID: 219 RVA: 0x000040A2 File Offset: 0x000022A2
		public FlowFieldPathBuilder(TerrainNavMeshGraph terrainNavMeshGraph, NodeIdService nodeIdService)
		{
			this._terrainNavMeshGraph = terrainNavMeshGraph;
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000040B8 File Offset: 0x000022B8
		public void BuildPath(IFlowField flowField, PathRequest pathRequest, List<FlowFieldPathNode> flowFieldPath)
		{
			flowFieldPath.Clear();
			if (pathRequest.Destination == pathRequest.Start)
			{
				flowFieldPath.Add(new FlowFieldPathNode(pathRequest.Destination, 0f, 0f, 0));
				return;
			}
			this.BuildPathInternal(flowField, pathRequest, flowFieldPath);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004108 File Offset: 0x00002308
		public void BuildPathInternal(IFlowField flowField, PathRequest pathRequest, List<FlowFieldPathNode> flowFieldPath)
		{
			flowFieldPath.Add(new FlowFieldPathNode(pathRequest.Destination, 0f, 0f, 0));
			int num = this._nodeIdService.WorldToId(pathRequest.Start);
			int num2 = this._nodeIdService.WorldToId(pathRequest.Destination);
			int previousNodeId = num2;
			if (num != num2)
			{
				int parentId;
				for (int num3 = flowField.GetParentId(num2); num3 != num; num3 = parentId)
				{
					this.AddEdgeNode(num3, previousNodeId, flowFieldPath);
					parentId = flowField.GetParentId(num3);
					if (parentId == num3)
					{
						throw new InvalidOperationException(string.Format("Infinite loop at {0} {1},", num3, this._nodeIdService.IdToGrid(num3)) + string.Format(" start: {0} {1},", num, this._nodeIdService.IdToGrid(num)) + string.Format(" destination: {0} {1}", num2, this._nodeIdService.IdToGrid(num2)));
					}
					previousNodeId = num3;
				}
			}
			this.AppendStartPoint(pathRequest.Start, num, previousNodeId, flowFieldPath);
			flowFieldPath.Reverse();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004210 File Offset: 0x00002410
		public void AddEdgeNode(int nodeId, int previousNodeId, List<FlowFieldPathNode> flowFieldPath)
		{
			float connectionCost = this._terrainNavMeshGraph.GetConnectionCost(nodeId, previousNodeId);
			float distanceToNext = this._nodeIdService.Distance(nodeId, previousNodeId);
			int groupId = this._terrainNavMeshGraph.GetGroupId(nodeId, previousNodeId);
			flowFieldPath.Add(new FlowFieldPathNode(this._nodeIdService.IdToWorld(nodeId), connectionCost, distanceToNext, groupId));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004264 File Offset: 0x00002464
		public void AppendStartPoint(Vector3 start, int startNodeId, int previousNodeId, List<FlowFieldPathNode> flowFieldPath)
		{
			FlowFieldPathNode flowFieldPathNode = flowFieldPath[flowFieldPath.Count - 1];
			float num = Vector3.Distance(start, flowFieldPathNode.Position);
			bool flag = flowFieldPath.Count > 1;
			float cost = flag ? this._terrainNavMeshGraph.GetConnectionCost(startNodeId, previousNodeId) : Math.Min(1f, 1f / num);
			int groupId = flag ? this._terrainNavMeshGraph.GetGroupId(startNodeId, previousNodeId) : flowFieldPathNode.GroupId;
			flowFieldPath.Add(new FlowFieldPathNode(start, cost, num, groupId));
		}

		// Token: 0x04000065 RID: 101
		public readonly TerrainNavMeshGraph _terrainNavMeshGraph;

		// Token: 0x04000066 RID: 102
		public readonly NodeIdService _nodeIdService;
	}
}
