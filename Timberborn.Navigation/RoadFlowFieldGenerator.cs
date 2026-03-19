using System;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000078 RID: 120
	public class RoadFlowFieldGenerator : ILoadableSingleton
	{
		// Token: 0x06000285 RID: 645 RVA: 0x000087BC File Offset: 0x000069BC
		public RoadFlowFieldGenerator(BinaryHeapFactory binaryHeapFactory)
		{
			this._binaryHeapFactory = binaryHeapFactory;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000087D7 File Offset: 0x000069D7
		public void Load()
		{
			this._openSet = this._binaryHeapFactory.Create<RoadFlowFieldGenerator.Node>();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000087EC File Offset: 0x000069EC
		public void FillFlowField(RoadNavMeshGraph roadNavMeshGraph, AccessFlowField flowField, AccessFlowField limitingFlowField, int startNodeId)
		{
			this._flowField = flowField;
			this._limitingFlowField = limitingFlowField;
			if (!this._flowField.IsFilled)
			{
				this._flowField.Clear();
				if (roadNavMeshGraph.IsOnNavMesh(startNodeId) && limitingFlowField.HasNode(startNodeId))
				{
					this._openSet.Clear();
					this.PushNode(startNodeId, 0f, -1);
					while (!this._openSet.IsEmpty())
					{
						this.VisitNeighbors(roadNavMeshGraph, this._openSet.Pop());
					}
					this._flowField.MarkAsFilled();
				}
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00008878 File Offset: 0x00006A78
		public void VisitNeighbors(RoadNavMeshGraph roadNavMeshGraph, RoadFlowFieldGenerator.Node node)
		{
			ReadOnlyList<NavMeshNode> neighbors = roadNavMeshGraph.GetNeighbors(node.NodeId);
			for (int i = 0; i < neighbors.Count; i++)
			{
				this.VisitNode(node, neighbors[i]);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000088B4 File Offset: 0x00006AB4
		public void VisitNode(RoadFlowFieldGenerator.Node parentNode, NavMeshNode node)
		{
			int id = node.Id;
			if (!this._flowField.HasNode(id) && this._limitingFlowField.HasNode(id))
			{
				float distance = parentNode.Distance + node.Cost;
				this.PushNode(id, distance, parentNode.NodeId);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008904 File Offset: 0x00006B04
		public void PushNode(int nodeId, float distance, int parentNodeId = -1)
		{
			this._openSet.Push(new RoadFlowFieldGenerator.Node(nodeId, distance));
			this._flowField.AddNode(nodeId, parentNodeId, distance);
		}

		// Token: 0x04000132 RID: 306
		public readonly BinaryHeapFactory _binaryHeapFactory;

		// Token: 0x04000133 RID: 307
		public BinaryHeap<RoadFlowFieldGenerator.Node> _openSet = new BinaryHeap<RoadFlowFieldGenerator.Node>(0);

		// Token: 0x04000134 RID: 308
		public AccessFlowField _flowField;

		// Token: 0x04000135 RID: 309
		public AccessFlowField _limitingFlowField;

		// Token: 0x02000079 RID: 121
		public readonly struct Node : IOrderable<RoadFlowFieldGenerator.Node>
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x0600028B RID: 651 RVA: 0x00008926 File Offset: 0x00006B26
			public int NodeId { get; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600028C RID: 652 RVA: 0x0000892E File Offset: 0x00006B2E
			public float Distance { get; }

			// Token: 0x0600028D RID: 653 RVA: 0x00008936 File Offset: 0x00006B36
			public Node(int nodeId, float distance)
			{
				this.NodeId = nodeId;
				this.Distance = distance;
			}

			// Token: 0x0600028E RID: 654 RVA: 0x00008946 File Offset: 0x00006B46
			public bool IsLessThan(RoadFlowFieldGenerator.Node other)
			{
				return this.Distance < other.Distance;
			}
		}
	}
}
