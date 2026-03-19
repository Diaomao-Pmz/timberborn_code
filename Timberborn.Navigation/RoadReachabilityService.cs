using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x0200007D RID: 125
	public class RoadReachabilityService
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x00008C80 File Offset: 0x00006E80
		public RoadReachabilityService(RoadNavMeshGraph roadNavMeshGraph)
		{
			this._roadNavMeshGraph = roadNavMeshGraph;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public void GetReachableNeighborsInRange(int startingNodeId, int range, List<int> reachableRoadNodes)
		{
			this.VisitNode(startingNodeId, 0f);
			while (!this._nodesToVisit.IsEmpty<RoadReachabilityService.NodeToVisit>())
			{
				RoadReachabilityService.NodeToVisit nodeToVisit = this._nodesToVisit.Dequeue();
				if (nodeToVisit.Distance < (float)range)
				{
					reachableRoadNodes.Add(nodeToVisit.Id);
					this.VisitNeighbors(nodeToVisit);
				}
			}
			this._visitedNodes.Clear();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00008D08 File Offset: 0x00006F08
		public void VisitNeighbors(RoadReachabilityService.NodeToVisit nodeToVisit)
		{
			ReadOnlyList<NavMeshNode> neighbors = this._roadNavMeshGraph.GetNeighbors(nodeToVisit.Id);
			for (int i = 0; i < neighbors.Count; i++)
			{
				NavMeshNode navMeshNode = neighbors[i];
				int id = navMeshNode.Id;
				if (!this._visitedNodes.Contains(id))
				{
					this.VisitNode(id, nodeToVisit.Distance + navMeshNode.Cost);
				}
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00008D6F File Offset: 0x00006F6F
		public void VisitNode(int nodeId, float distance)
		{
			this._nodesToVisit.Enqueue(new RoadReachabilityService.NodeToVisit(nodeId, distance));
			this._visitedNodes.Add(nodeId);
		}

		// Token: 0x04000147 RID: 327
		public readonly RoadNavMeshGraph _roadNavMeshGraph;

		// Token: 0x04000148 RID: 328
		public readonly Queue<RoadReachabilityService.NodeToVisit> _nodesToVisit = new Queue<RoadReachabilityService.NodeToVisit>();

		// Token: 0x04000149 RID: 329
		public readonly HashSet<int> _visitedNodes = new HashSet<int>();

		// Token: 0x0200007E RID: 126
		public readonly struct NodeToVisit
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060002AC RID: 684 RVA: 0x00008D90 File Offset: 0x00006F90
			public int Id { get; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060002AD RID: 685 RVA: 0x00008D98 File Offset: 0x00006F98
			public float Distance { get; }

			// Token: 0x060002AE RID: 686 RVA: 0x00008DA0 File Offset: 0x00006FA0
			public NodeToVisit(int id, float distance)
			{
				this.Id = id;
				this.Distance = distance;
			}
		}
	}
}
