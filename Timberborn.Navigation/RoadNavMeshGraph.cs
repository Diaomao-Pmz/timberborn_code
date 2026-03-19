using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x0200007B RID: 123
	public class RoadNavMeshGraph : INavMeshGraph, ILoadableSingleton
	{
		// Token: 0x06000298 RID: 664 RVA: 0x00008AAD File Offset: 0x00006CAD
		public RoadNavMeshGraph(NodeIdService nodeIdService)
		{
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00008ABC File Offset: 0x00006CBC
		public void Load()
		{
			this._neighbors = new List<NavMeshNode>[this._nodeIdService.NumberOfNodes];
			for (int i = 0; i < this._neighbors.Length; i++)
			{
				this._neighbors[i] = RoadNavMeshGraph.EmptyNeighbors;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008AFF File Offset: 0x00006CFF
		public void ConnectNodes(int aNodeId, int bNodeId, int groupId, float cost)
		{
			this.VerifyBeforeChange(aNodeId, bNodeId);
			this.RemoveOneWayConnections(aNodeId, bNodeId);
			this.AddOneWayConnection(aNodeId, bNodeId, groupId, cost);
			this.AddOneWayConnection(bNodeId, aNodeId, groupId, cost);
			this.VerifyAfterChange(aNodeId, bNodeId);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008B2F File Offset: 0x00006D2F
		public void DisconnectNodes(int aNodeId, int bNodeId)
		{
			this.RemoveOneWayConnections(aNodeId, bNodeId);
			this.VerifyAfterChange(aNodeId, bNodeId);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008B41 File Offset: 0x00006D41
		public ReadOnlyList<NavMeshNode> GetNeighbors(int nodeId)
		{
			return this._neighbors[nodeId].AsReadOnlyList<NavMeshNode>();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008B50 File Offset: 0x00006D50
		public bool IsOnNavMesh(int nodeId)
		{
			return !this._neighbors[nodeId].IsEmpty<NavMeshNode>();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00008B64 File Offset: 0x00006D64
		public bool AreConnected(int nodeIdA, int nodeIdB)
		{
			List<NavMeshNode> list = this._neighbors[nodeIdA];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Id == nodeIdB)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00008BA0 File Offset: 0x00006DA0
		public void RemoveOneWayConnections(int aNodeId, int bNodeId)
		{
			this.RemoveOneWayConnection(aNodeId, bNodeId);
			this.RemoveOneWayConnection(bNodeId, aNodeId);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00008BB2 File Offset: 0x00006DB2
		public void AddOneWayConnection(int aNodeId, int bNodeId, int groupId, float cost)
		{
			this._neighbors[aNodeId].Add(new NavMeshNode(bNodeId, groupId, cost));
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00008BCC File Offset: 0x00006DCC
		public void RemoveOneWayConnection(int aNodeId, int bNodeId)
		{
			List<NavMeshNode> list = this._neighbors[aNodeId];
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].Id == bNodeId)
				{
					list.RemoveAt(i);
				}
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00008C0E File Offset: 0x00006E0E
		public void VerifyBeforeChange(int aNodeId, int bNodeId)
		{
			this.VerifyBeforeChange(aNodeId);
			this.VerifyBeforeChange(bNodeId);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00008C1E File Offset: 0x00006E1E
		public void VerifyBeforeChange(int nodeId)
		{
			if (this._neighbors[nodeId] == RoadNavMeshGraph.EmptyNeighbors)
			{
				this._neighbors[nodeId] = new List<NavMeshNode>();
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00008C3C File Offset: 0x00006E3C
		public void VerifyAfterChange(int aNodeId, int bNodeId)
		{
			this.VerifyAfterChange(aNodeId);
			this.VerifyAfterChange(bNodeId);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00008C4C File Offset: 0x00006E4C
		public void VerifyAfterChange(int nodeId)
		{
			if (this._neighbors[nodeId].Count == 0)
			{
				this._neighbors[nodeId] = RoadNavMeshGraph.EmptyNeighbors;
			}
		}

		// Token: 0x04000141 RID: 321
		public static readonly List<NavMeshNode> EmptyNeighbors = new List<NavMeshNode>();

		// Token: 0x04000142 RID: 322
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000143 RID: 323
		public List<NavMeshNode>[] _neighbors;
	}
}
