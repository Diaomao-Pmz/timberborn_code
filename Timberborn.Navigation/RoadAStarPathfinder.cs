using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000076 RID: 118
	public class RoadAStarPathfinder : ILoadableSingleton
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00008437 File Offset: 0x00006637
		public RoadAStarPathfinder(HeuristicsCalculator heuristicsCalculator, BinaryHeapFactory binaryHeapFactory)
		{
			this._heuristicsCalculator = heuristicsCalculator;
			this._binaryHeapFactory = binaryHeapFactory;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00008458 File Offset: 0x00006658
		public void Load()
		{
			this._openSet = this._binaryHeapFactory.Create<AStarNode>();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000846C File Offset: 0x0000666C
		public void FillFlowFieldWithPath(RoadNavMeshGraph roadNavMeshGraph, PathFlowField flowField, int startNodeId, int destinationNodeId)
		{
			this._flowField = flowField;
			this._heuristicsCalculator.SetDestinationNode(destinationNodeId);
			if (!this._flowField.CheckedPath(startNodeId, destinationNodeId) && RoadAStarPathfinder.HeuristicallyReachable(roadNavMeshGraph, startNodeId, destinationNodeId))
			{
				this._flowField.Clear(startNodeId);
				this._openSet.Clear();
				this.PushStartingNode(startNodeId);
				while (!this._openSet.IsEmpty())
				{
					AStarNode node = this._openSet.Pop();
					int nodeId = node.NodeId;
					if (!this._flowField.HasNode(nodeId))
					{
						this._flowField.AddNode(nodeId, node.ParentNodeId, node.GScore);
						if (nodeId == destinationNodeId)
						{
							this._flowField.MarkAsPartiallyFilled();
							return;
						}
						this.VisitNeighbors(roadNavMeshGraph, node);
					}
				}
				this._flowField.MarkAsFullyFilled();
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000853C File Offset: 0x0000673C
		public bool FillFlowFieldWithPath(RoadNavMeshGraph roadNavMeshGraph, PathFlowField flowField, int startNodeId, IReadOnlyList<int> destinationNodeIds, out int destinationNodeId)
		{
			this._flowField = flowField;
			this._heuristicsCalculator.SetDestinationNodes(destinationNodeIds);
			this._destinationNodes.Clear();
			this._destinationNodes.AddRange(destinationNodeIds);
			this._flowField.Clear(startNodeId);
			this._openSet.Clear();
			this.PushStartingNode(startNodeId);
			while (!this._openSet.IsEmpty())
			{
				AStarNode node = this._openSet.Pop();
				int nodeId = node.NodeId;
				if (!this._flowField.HasNode(nodeId))
				{
					this._flowField.AddNode(nodeId, node.ParentNodeId, node.GScore);
					if (this._destinationNodes.Contains(nodeId))
					{
						this._flowField.MarkAsPartiallyFilled();
						destinationNodeId = nodeId;
						return true;
					}
					this.VisitNeighbors(roadNavMeshGraph, node);
				}
			}
			this._flowField.MarkAsFullyFilled();
			destinationNodeId = 0;
			return false;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008615 File Offset: 0x00006815
		public static bool HeuristicallyReachable(RoadNavMeshGraph roadNavMeshGraph, int startNodeId, int destinationNodeId)
		{
			return roadNavMeshGraph.IsOnNavMesh(startNodeId) && roadNavMeshGraph.IsOnNavMesh(destinationNodeId);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000862C File Offset: 0x0000682C
		public void VisitNeighbors(RoadNavMeshGraph roadNavMeshGraph, AStarNode node)
		{
			ReadOnlyList<NavMeshNode> neighbors = roadNavMeshGraph.GetNeighbors(node.NodeId);
			for (int i = 0; i < neighbors.Count; i++)
			{
				this.VisitNode(node, neighbors[i]);
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008668 File Offset: 0x00006868
		public void VisitNode(AStarNode parentNode, NavMeshNode node)
		{
			int id = node.Id;
			if (!this._flowField.HasNode(id))
			{
				float gScore = parentNode.GScore + node.Cost;
				this.PushNode(id, parentNode.NodeId, gScore);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000086AA File Offset: 0x000068AA
		public void PushStartingNode(int nodeId)
		{
			this.PushNode(nodeId, -1, 0f);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000086BC File Offset: 0x000068BC
		public void PushNode(int nodeId, int parentNodeId, float gScore)
		{
			float num = this._heuristicsCalculator.H(nodeId);
			float fScore = gScore + num;
			this._openSet.Push(new AStarNode(nodeId, parentNodeId, gScore, fScore));
		}

		// Token: 0x0400012A RID: 298
		public readonly HeuristicsCalculator _heuristicsCalculator;

		// Token: 0x0400012B RID: 299
		public readonly BinaryHeapFactory _binaryHeapFactory;

		// Token: 0x0400012C RID: 300
		public BinaryHeap<AStarNode> _openSet;

		// Token: 0x0400012D RID: 301
		public readonly HashSet<int> _destinationNodes = new HashSet<int>();

		// Token: 0x0400012E RID: 302
		public PathFlowField _flowField;
	}
}
