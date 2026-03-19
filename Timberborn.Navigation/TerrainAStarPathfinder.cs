using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000084 RID: 132
	public class TerrainAStarPathfinder : ILoadableSingleton
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000915D File Offset: 0x0000735D
		public TerrainAStarPathfinder(HeuristicsCalculator heuristicsCalculator, BinaryHeapFactory binaryHeapFactory)
		{
			this._heuristicsCalculator = heuristicsCalculator;
			this._binaryHeapFactory = binaryHeapFactory;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000917E File Offset: 0x0000737E
		public void Load()
		{
			this._openSet = this._binaryHeapFactory.Create<AStarNode>();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00009194 File Offset: 0x00007394
		public void FillFlowFieldWithPath(TerrainNavMeshGraph terrainNavMeshGraph, PathFlowField flowField, int startNodeId, int destinationNodeId)
		{
			this._flowField = flowField;
			this._heuristicsCalculator.SetDestinationNode(destinationNodeId);
			if (!this._flowField.CheckedPath(startNodeId, destinationNodeId) && TerrainAStarPathfinder.HeuristicallyReachable(terrainNavMeshGraph, startNodeId, destinationNodeId))
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
						this.VisitNeighbors(terrainNavMeshGraph, node);
					}
				}
				this._flowField.MarkAsFullyFilled();
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00009264 File Offset: 0x00007464
		public bool FillFlowFieldWithPath(TerrainNavMeshGraph terrainNavMeshGraph, PathFlowField flowField, int startNodeId, IReadOnlyList<int> destinationNodeIds, out int destinationNodeId)
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
					this.VisitNeighbors(terrainNavMeshGraph, node);
				}
			}
			this._flowField.MarkAsFullyFilled();
			destinationNodeId = 0;
			return false;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000933D File Offset: 0x0000753D
		public static bool HeuristicallyReachable(TerrainNavMeshGraph terrainNavMeshGraph, int startNodeId, int destinationNodeId)
		{
			return terrainNavMeshGraph.IsOnNavMesh(startNodeId) && terrainNavMeshGraph.IsOnNavMesh(destinationNodeId);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00009354 File Offset: 0x00007554
		public void VisitNeighbors(TerrainNavMeshGraph terrainNavMeshGraph, AStarNode node)
		{
			ReadOnlyList<NavMeshNode> neighbors = terrainNavMeshGraph.GetNeighbors(node.NodeId);
			for (int i = 0; i < neighbors.Count; i++)
			{
				this.VisitNode(node, neighbors[i]);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00009390 File Offset: 0x00007590
		public void VisitNode(AStarNode parentNode, NavMeshNode node)
		{
			int id = node.Id;
			if (!this._flowField.HasNode(id))
			{
				float gScore = parentNode.GScore + node.Cost;
				this.PushNode(id, parentNode.NodeId, gScore);
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000093D2 File Offset: 0x000075D2
		public void PushStartingNode(int nodeId)
		{
			this.PushNode(nodeId, -1, 0f);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000093E4 File Offset: 0x000075E4
		public void PushNode(int nodeId, int parentNodeId, float gScore)
		{
			float num = this._heuristicsCalculator.H(nodeId);
			float fScore = gScore + num;
			this._openSet.Push(new AStarNode(nodeId, parentNodeId, gScore, fScore));
		}

		// Token: 0x0400015C RID: 348
		public readonly HeuristicsCalculator _heuristicsCalculator;

		// Token: 0x0400015D RID: 349
		public readonly BinaryHeapFactory _binaryHeapFactory;

		// Token: 0x0400015E RID: 350
		public BinaryHeap<AStarNode> _openSet;

		// Token: 0x0400015F RID: 351
		public readonly HashSet<int> _destinationNodes = new HashSet<int>();

		// Token: 0x04000160 RID: 352
		public PathFlowField _flowField;
	}
}
