using System;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000086 RID: 134
	public class TerrainFlowFieldGenerator : ILoadableSingleton
	{
		// Token: 0x060002DD RID: 733 RVA: 0x000094A4 File Offset: 0x000076A4
		public TerrainFlowFieldGenerator(BinaryHeapFactory binaryHeapFactory, NavMeshGroupService navMeshGroupService)
		{
			this._binaryHeapFactory = binaryHeapFactory;
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000094BA File Offset: 0x000076BA
		public void Load()
		{
			this._openSet = this._binaryHeapFactory.Create<TerrainFlowFieldGenerator.Node>();
			this._defaultGroupId = this._navMeshGroupService.GetDefaultGroupId();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000094E0 File Offset: 0x000076E0
		public void FillFlowFieldUpToDistance(TerrainNavMeshGraph terrainNavMeshGraph, AccessFlowField flowField, float maxDistance, int startNodeId)
		{
			this._flowField = flowField;
			this._maxDistance = maxDistance;
			if (!this._flowField.IsFilled)
			{
				this._flowField.Clear();
				if (terrainNavMeshGraph.IsOnNavMesh(startNodeId))
				{
					this._openSet.Clear();
					this.PushNode(startNodeId, 0f, 0f, -1);
					while (!this._openSet.IsEmpty())
					{
						TerrainFlowFieldGenerator.Node node = this._openSet.Pop();
						int nodeId = node.NodeId;
						if (!this._flowField.HasNode(nodeId))
						{
							this._flowField.AddNode(nodeId, node.ParentNodeId, node.Distance);
							this.VisitNeighbors(terrainNavMeshGraph, node);
						}
					}
					this._flowField.MarkAsFilled();
				}
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000959C File Offset: 0x0000779C
		public void VisitNeighbors(TerrainNavMeshGraph terrainNavMeshGraph, TerrainFlowFieldGenerator.Node node)
		{
			ReadOnlyList<NavMeshNode> neighbors = terrainNavMeshGraph.GetNeighbors(node.NodeId);
			for (int i = 0; i < neighbors.Count; i++)
			{
				this.VisitNode(node, neighbors[i]);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000095D8 File Offset: 0x000077D8
		public void VisitNode(TerrainFlowFieldGenerator.Node parentNode, NavMeshNode node)
		{
			int id = node.Id;
			if (!this._flowField.HasNode(id))
			{
				float num = (node.GroupId == this._defaultGroupId) ? ((float)((node.Cost > 0f) ? 1 : 0)) : node.Cost;
				float num2 = parentNode.SimpleDistance + num;
				if (num2 <= this._maxDistance)
				{
					float distance = parentNode.Distance + node.Cost;
					this.PushNode(id, distance, num2, parentNode.NodeId);
				}
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000965A File Offset: 0x0000785A
		public void PushNode(int nodeId, float distance, float simpleDistance, int parentNodeId = -1)
		{
			this._openSet.Push(new TerrainFlowFieldGenerator.Node(nodeId, parentNodeId, distance, simpleDistance));
		}

		// Token: 0x04000163 RID: 355
		public readonly BinaryHeapFactory _binaryHeapFactory;

		// Token: 0x04000164 RID: 356
		public readonly NavMeshGroupService _navMeshGroupService;

		// Token: 0x04000165 RID: 357
		public BinaryHeap<TerrainFlowFieldGenerator.Node> _openSet;

		// Token: 0x04000166 RID: 358
		public AccessFlowField _flowField;

		// Token: 0x04000167 RID: 359
		public float _maxDistance;

		// Token: 0x04000168 RID: 360
		public int _defaultGroupId;

		// Token: 0x02000087 RID: 135
		public readonly struct Node : IOrderable<TerrainFlowFieldGenerator.Node>
		{
			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060002E3 RID: 739 RVA: 0x00009671 File Offset: 0x00007871
			public int NodeId { get; }

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060002E4 RID: 740 RVA: 0x00009679 File Offset: 0x00007879
			public int ParentNodeId { get; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060002E5 RID: 741 RVA: 0x00009681 File Offset: 0x00007881
			public float Distance { get; }

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009689 File Offset: 0x00007889
			public float SimpleDistance { get; }

			// Token: 0x060002E7 RID: 743 RVA: 0x00009691 File Offset: 0x00007891
			public Node(int nodeId, int parentNodeId, float distance, float simpleDistance)
			{
				this.NodeId = nodeId;
				this.ParentNodeId = parentNodeId;
				this.Distance = distance;
				this.SimpleDistance = simpleDistance;
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x000096B0 File Offset: 0x000078B0
			public bool IsLessThan(TerrainFlowFieldGenerator.Node other)
			{
				return this.Distance < other.Distance;
			}
		}
	}
}
