using System;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000015 RID: 21
	public class DistrictRoadFlowFieldGenerator : ILoadableSingleton
	{
		// Token: 0x06000082 RID: 130 RVA: 0x0000384F File Offset: 0x00001A4F
		public DistrictRoadFlowFieldGenerator(BinaryHeapFactory binaryHeapFactory)
		{
			this._binaryHeapFactory = binaryHeapFactory;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000385E File Offset: 0x00001A5E
		public void Load()
		{
			this._openSet = this._binaryHeapFactory.Create<DistrictRoadFlowFieldGenerator.Node>();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003874 File Offset: 0x00001A74
		public void FillFlowFieldUpToDistance(RoadNavMeshGraph roadNavMeshGraph, DistrictObstacleService districtObstacleService, AccessFlowField flowField, int startNodeId)
		{
			this._flowField = flowField;
			if (!this._flowField.IsFilled)
			{
				this._flowField.Clear();
				if (roadNavMeshGraph.IsOnNavMesh(startNodeId))
				{
					this._openSet.Clear();
					this.PushNode(startNodeId, 0f, -1);
					while (!this._openSet.IsEmpty())
					{
						this.VisitNeighbors(roadNavMeshGraph, districtObstacleService, this._openSet.Pop());
					}
					this._flowField.MarkAsFilled();
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000038F0 File Offset: 0x00001AF0
		public void VisitNeighbors(RoadNavMeshGraph roadNavMeshGraph, DistrictObstacleService districtObstacleService, DistrictRoadFlowFieldGenerator.Node node)
		{
			ReadOnlyList<NavMeshNode> neighbors = roadNavMeshGraph.GetNeighbors(node.NodeId);
			for (int i = 0; i < neighbors.Count; i++)
			{
				this.VisitNode(districtObstacleService, node, neighbors[i]);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003930 File Offset: 0x00001B30
		public void VisitNode(DistrictObstacleService districtObstacleService, DistrictRoadFlowFieldGenerator.Node parentNode, NavMeshNode node)
		{
			int id = node.Id;
			if (!this._flowField.HasNode(id) && !districtObstacleService.IsSetObstacle(id))
			{
				float distance = parentNode.Distance + node.Cost;
				this.PushNode(id, distance, parentNode.NodeId);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000397B File Offset: 0x00001B7B
		public void PushNode(int nodeId, float distance, int parentNodeId = -1)
		{
			this._openSet.Push(new DistrictRoadFlowFieldGenerator.Node(nodeId, distance));
			this._flowField.AddNode(nodeId, parentNodeId, distance);
		}

		// Token: 0x04000049 RID: 73
		public readonly BinaryHeapFactory _binaryHeapFactory;

		// Token: 0x0400004A RID: 74
		public BinaryHeap<DistrictRoadFlowFieldGenerator.Node> _openSet;

		// Token: 0x0400004B RID: 75
		public AccessFlowField _flowField;

		// Token: 0x02000016 RID: 22
		public readonly struct Node : IOrderable<DistrictRoadFlowFieldGenerator.Node>
		{
			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000088 RID: 136 RVA: 0x0000399D File Offset: 0x00001B9D
			public int NodeId { get; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000089 RID: 137 RVA: 0x000039A5 File Offset: 0x00001BA5
			public float Distance { get; }

			// Token: 0x0600008A RID: 138 RVA: 0x000039AD File Offset: 0x00001BAD
			public Node(int nodeId, float distance)
			{
				this.NodeId = nodeId;
				this.Distance = distance;
			}

			// Token: 0x0600008B RID: 139 RVA: 0x000039BD File Offset: 0x00001BBD
			public bool IsLessThan(DistrictRoadFlowFieldGenerator.Node other)
			{
				return this.Distance < other.Distance;
			}
		}
	}
}
