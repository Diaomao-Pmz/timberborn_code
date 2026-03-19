using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000022 RID: 34
	public class FlowFieldPathFinder
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000042E6 File Offset: 0x000024E6
		public FlowFieldPathFinder(NodeIdService nodeIdService, FlowFieldPathBuilder flowFieldPathBuilder, FlowFieldPathTransformer flowFieldPathTransformer)
		{
			this._nodeIdService = nodeIdService;
			this._flowFieldPathBuilder = flowFieldPathBuilder;
			this._flowFieldPathTransformer = flowFieldPathTransformer;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000431C File Offset: 0x0000251C
		public bool FindPathInFlowField(PathFlowField flowField, PathRequest pathRequest, out float distance, List<PathCorner> pathCorners)
		{
			int startNodeId = this._nodeIdService.WorldToId(pathRequest.Start);
			int num = this._nodeIdService.WorldToId(pathRequest.Destination);
			if (flowField.FoundPath(startNodeId, num))
			{
				if (pathCorners != null)
				{
					this._flowFieldPathBuilder.BuildPath(flowField, pathRequest, this._pathCache);
					this.TransformPathIntoPathCorners(this._pathCache, pathCorners, pathRequest);
				}
				distance = flowField.GetDistance(num);
				return true;
			}
			if (pathCorners != null)
			{
				pathCorners.Clear();
			}
			distance = 0f;
			return false;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000043A0 File Offset: 0x000025A0
		public bool FindPathInFlowField(AccessFlowField flowField, PathRequest pathRequest, out float distance, List<PathCorner> pathCorners)
		{
			int num = this._nodeIdService.WorldToId(pathRequest.Destination);
			if (flowField.FoundPath(num))
			{
				if (pathCorners != null)
				{
					this._flowFieldPathBuilder.BuildPath(flowField, pathRequest, this._pathCache);
					this.TransformPathIntoPathCorners(this._pathCache, pathCorners, pathRequest);
				}
				distance = flowField.GetDistance(num);
				return true;
			}
			if (pathCorners != null)
			{
				pathCorners.Clear();
			}
			distance = 0f;
			return false;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004410 File Offset: 0x00002610
		public bool FindPathInFlowField(AccessFlowField accessFlowField, RoadSpillFlowField roadSpillFlowField, PathRequest pathRequest, out float distance, List<PathCorner> pathCorners = null)
		{
			int nodeId = this._nodeIdService.WorldToId(pathRequest.Destination);
			if (roadSpillFlowField.HasNode(nodeId))
			{
				int roadParentNodeId = roadSpillFlowField.GetRoadParentNodeId(nodeId);
				if (accessFlowField.FoundPath(roadParentNodeId))
				{
					if (pathCorners != null)
					{
						Vector3 vector = this._nodeIdService.IdToWorld(roadParentNodeId);
						this._flowFieldPathBuilder.BuildPath(accessFlowField, PathRequest.Create(pathRequest.Start, vector), this._pathCache);
						this._partialPathCache.Clear();
						this._flowFieldPathBuilder.BuildPath(roadSpillFlowField, PathRequest.Create(vector, pathRequest.Destination), this._partialPathCache);
						this._pathCache.RemoveLast();
						this._pathCache.AddRange(this._partialPathCache);
						this.TransformPathIntoPathCorners(this._pathCache, pathCorners, pathRequest);
					}
					distance = accessFlowField.GetDistance(roadParentNodeId) + roadSpillFlowField.GetDistanceToRoad(nodeId);
					return true;
				}
			}
			if (pathCorners != null)
			{
				pathCorners.Clear();
			}
			distance = 0f;
			return false;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004500 File Offset: 0x00002700
		public bool FindPathInFlowField(PathFlowField pathFlowField, RoadSpillFlowField roadSpillFlowField, PathRequest pathRequest, out float distance, List<PathCorner> pathCorners = null)
		{
			int startNodeId = this._nodeIdService.WorldToId(pathRequest.Start);
			int nodeId = this._nodeIdService.WorldToId(pathRequest.Destination);
			if (roadSpillFlowField.HasNode(nodeId))
			{
				int roadParentNodeId = roadSpillFlowField.GetRoadParentNodeId(nodeId);
				if (pathFlowField.FoundPath(startNodeId, roadParentNodeId))
				{
					if (pathCorners != null)
					{
						Vector3 vector = this._nodeIdService.IdToWorld(roadParentNodeId);
						this._flowFieldPathBuilder.BuildPath(pathFlowField, PathRequest.Create(pathRequest.Start, vector), this._pathCache);
						this._partialPathCache.Clear();
						this._flowFieldPathBuilder.BuildPath(roadSpillFlowField, PathRequest.Create(vector, pathRequest.Destination), this._partialPathCache);
						this._pathCache.RemoveLast();
						this._pathCache.AddRange(this._partialPathCache);
						this.TransformPathIntoPathCorners(this._pathCache, pathCorners, pathRequest);
					}
					distance = pathFlowField.GetDistance(roadParentNodeId) + roadSpillFlowField.GetDistanceToRoad(nodeId);
					return true;
				}
			}
			if (pathCorners != null)
			{
				pathCorners.Clear();
			}
			distance = 0f;
			return false;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004604 File Offset: 0x00002804
		public bool FindPathInFlowField(Vector3 start, IReadOnlyList<Vector3> destinations, AccessFlowField flowField, out float distance, List<PathCorner> pathCorners = null)
		{
			Vector3? vector = null;
			float num = float.PositiveInfinity;
			for (int i = 0; i < destinations.Count; i++)
			{
				Vector3 vector2 = destinations[i];
				int num2 = this._nodeIdService.WorldToId(vector2);
				if (flowField.FoundPath(num2))
				{
					float distance2 = flowField.GetDistance(num2);
					if (distance2 < num)
					{
						vector = new Vector3?(vector2);
						num = distance2;
					}
				}
			}
			if (vector != null)
			{
				return this.FindPathInFlowField(flowField, PathRequest.Create(start, vector.Value), out distance, pathCorners);
			}
			distance = 0f;
			return false;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004695 File Offset: 0x00002895
		public void TransformPathIntoPathCorners(List<FlowFieldPathNode> flowFieldPath, List<PathCorner> pathCorners, PathRequest pathRequest)
		{
			if (pathRequest.Reversed)
			{
				this._flowFieldPathTransformer.TransformReversedPath(flowFieldPath, pathCorners);
				return;
			}
			this._flowFieldPathTransformer.TransformPath(flowFieldPath, pathCorners);
		}

		// Token: 0x04000067 RID: 103
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000068 RID: 104
		public readonly FlowFieldPathBuilder _flowFieldPathBuilder;

		// Token: 0x04000069 RID: 105
		public readonly FlowFieldPathTransformer _flowFieldPathTransformer;

		// Token: 0x0400006A RID: 106
		public readonly List<FlowFieldPathNode> _pathCache = new List<FlowFieldPathNode>();

		// Token: 0x0400006B RID: 107
		public readonly List<FlowFieldPathNode> _partialPathCache = new List<FlowFieldPathNode>();
	}
}
