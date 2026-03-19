using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000081 RID: 129
	public class RoadSpillFlowFieldGenerator
	{
		// Token: 0x060002BE RID: 702 RVA: 0x00008EBE File Offset: 0x000070BE
		public RoadSpillFlowFieldGenerator(NodeIdService nodeIdService)
		{
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008ED8 File Offset: 0x000070D8
		public void FillFlowFieldUpToDistance(TerrainNavMeshGraph terrainNavMeshGraph, AccessFlowField startRoadFlowField, int maxTerrainDistance, RoadSpillFlowField flowField)
		{
			this._flowField = flowField;
			this._maxTerrainDistance = maxTerrainDistance;
			this._doubledMaxTerrainDistance = maxTerrainDistance * 2;
			if (!this._flowField.IsFilled)
			{
				this._flowField.Clear();
				this._openSet.Clear();
				this.PushStartingNodes(startRoadFlowField);
				while (!this._openSet.IsEmpty<RoadSpillFlowFieldGenerator.Node>())
				{
					this.VisitNeighbors(terrainNavMeshGraph, this._openSet.Dequeue());
				}
				this._flowField.FinishFilling();
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008F54 File Offset: 0x00007154
		public void PushStartingNodes(AccessFlowField startRoadFlowField)
		{
			foreach (FlowFieldNode flowFieldNode in startRoadFlowField.GetAllNodes())
			{
				this.PushNode(flowFieldNode.Id, -1, flowFieldNode.Id, 0);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008FB0 File Offset: 0x000071B0
		public void VisitNeighbors(TerrainNavMeshGraph terrainNavMeshGraph, RoadSpillFlowFieldGenerator.Node node)
		{
			ReadOnlyList<int> cheapNeighbors = terrainNavMeshGraph.GetCheapNeighbors(node.NodeId);
			for (int i = 0; i < cheapNeighbors.Count; i++)
			{
				this.VisitNode(node, cheapNeighbors[i]);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008FEC File Offset: 0x000071EC
		public void VisitNode(RoadSpillFlowFieldGenerator.Node parentNode, int nodeId)
		{
			if (!this._flowField.HasNode(nodeId))
			{
				int roadParentNodeId = parentNode.RoadParentNodeId;
				Vector3Int vector3Int = this._nodeIdService.IdToGrid(nodeId);
				Vector3Int vector3Int2 = this._nodeIdService.IdToGrid(roadParentNodeId);
				if (Math.Abs(vector3Int.x - vector3Int2.x) < this._maxTerrainDistance && Math.Abs(vector3Int.y - vector3Int2.y) < this._maxTerrainDistance)
				{
					int num = parentNode.SimpleDistanceToRoad + 1;
					if (num < this._doubledMaxTerrainDistance)
					{
						this.PushNode(nodeId, parentNode.NodeId, roadParentNodeId, num);
					}
				}
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009084 File Offset: 0x00007284
		public void PushNode(int nodeId, int parentNodeId, int roadParentNodeId, int distanceToRoad)
		{
			this._openSet.Enqueue(new RoadSpillFlowFieldGenerator.Node(nodeId, roadParentNodeId, distanceToRoad));
			this._flowField.AddNode(nodeId, parentNodeId, roadParentNodeId, (float)distanceToRoad);
		}

		// Token: 0x04000151 RID: 337
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000152 RID: 338
		public readonly Queue<RoadSpillFlowFieldGenerator.Node> _openSet = new Queue<RoadSpillFlowFieldGenerator.Node>();

		// Token: 0x04000153 RID: 339
		public RoadSpillFlowField _flowField;

		// Token: 0x04000154 RID: 340
		public int _maxTerrainDistance;

		// Token: 0x04000155 RID: 341
		public int _doubledMaxTerrainDistance;

		// Token: 0x02000082 RID: 130
		public readonly struct Node
		{
			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060002C4 RID: 708 RVA: 0x000090AB File Offset: 0x000072AB
			public int NodeId { get; }

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060002C5 RID: 709 RVA: 0x000090B3 File Offset: 0x000072B3
			public int RoadParentNodeId { get; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060002C6 RID: 710 RVA: 0x000090BB File Offset: 0x000072BB
			public int SimpleDistanceToRoad { get; }

			// Token: 0x060002C7 RID: 711 RVA: 0x000090C3 File Offset: 0x000072C3
			public Node(int nodeId, int roadParentNodeId, int simpleDistanceToRoad)
			{
				this.NodeId = nodeId;
				this.RoadParentNodeId = roadParentNodeId;
				this.SimpleDistanceToRoad = simpleDistanceToRoad;
			}
		}
	}
}
