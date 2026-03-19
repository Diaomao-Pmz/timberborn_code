using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000089 RID: 137
	public class TerrainNavMeshGraph : INavMeshGraph, ILoadableSingleton
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x000097E9 File Offset: 0x000079E9
		public TerrainNavMeshGraph(NodeIdService nodeIdService, NavMeshGroupService navMeshGroupService)
		{
			this._nodeIdService = nodeIdService;
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009800 File Offset: 0x00007A00
		public void Load()
		{
			this._allNeighbors = new List<NavMeshNode>[this._nodeIdService.NumberOfNodes];
			this._cheapNeighbors = new List<int>[this._nodeIdService.NumberOfNodes];
			for (int i = 0; i < this._allNeighbors.Length; i++)
			{
				this._allNeighbors[i] = TerrainNavMeshGraph.EmptyAllNeighbors;
				this._cheapNeighbors[i] = TerrainNavMeshGraph.EmptyCheapNeighbors;
			}
			this._defaultGroupId = this._navMeshGroupService.GetDefaultGroupId();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009877 File Offset: 0x00007A77
		public void ConnectNodes(int aNodeId, int bNodeId, int groupId, float cost)
		{
			this.VerifyBeforeChange(aNodeId, bNodeId);
			this.RemoveOneWayConnections(aNodeId, bNodeId);
			this.AddOneWayConnection(aNodeId, bNodeId, groupId, cost);
			this.AddOneWayConnection(bNodeId, aNodeId, groupId, cost);
			this.VerifyAfterChange(aNodeId, bNodeId);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000098A7 File Offset: 0x00007AA7
		public void DisconnectNodes(int aNodeId, int bNodeId)
		{
			this.RemoveOneWayConnections(aNodeId, bNodeId);
			this.VerifyAfterChange(aNodeId, bNodeId);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000098B9 File Offset: 0x00007AB9
		public ReadOnlyList<NavMeshNode> GetNeighbors(int nodeId)
		{
			return this._allNeighbors[nodeId].AsReadOnlyList<NavMeshNode>();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000098C8 File Offset: 0x00007AC8
		public ReadOnlyList<int> GetCheapNeighbors(int nodeId)
		{
			return this._cheapNeighbors[nodeId].AsReadOnlyList<int>();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000098D7 File Offset: 0x00007AD7
		public bool IsOnNavMesh(int nodeId)
		{
			return !this._allNeighbors[nodeId].IsEmpty<NavMeshNode>();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000098EC File Offset: 0x00007AEC
		public bool AreConnected(int nodeIdA, int nodeIdB)
		{
			List<NavMeshNode> list = this._allNeighbors[nodeIdA];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Id == nodeIdB)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009928 File Offset: 0x00007B28
		public float GetConnectionCost(int nodeIdA, int nodeIdB)
		{
			List<NavMeshNode> list = this._allNeighbors[nodeIdA];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Id == nodeIdB)
				{
					return list[i].Cost;
				}
			}
			return TerrainNavMeshGraph.DefaultConnectionCost;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009978 File Offset: 0x00007B78
		public int GetGroupId(int nodeIdA, int nodeIdB)
		{
			List<NavMeshNode> list = this._allNeighbors[nodeIdA];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Id == nodeIdB)
				{
					return list[i].GroupId;
				}
			}
			return this._defaultGroupId;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000099C8 File Offset: 0x00007BC8
		public bool IsConnectedToDefaultGroup(int nodeId)
		{
			List<NavMeshNode> list = this._allNeighbors[nodeId];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GroupId == this._defaultGroupId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009A09 File Offset: 0x00007C09
		public void RemoveOneWayConnections(int aNodeId, int bNodeId)
		{
			this.RemoveOneWayConnection(aNodeId, bNodeId);
			this.RemoveOneWayConnection(bNodeId, aNodeId);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00009A1B File Offset: 0x00007C1B
		public void AddOneWayConnection(int aNodeId, int bNodeId, int groupId, float cost)
		{
			this._allNeighbors[aNodeId].Add(new NavMeshNode(bNodeId, groupId, cost));
			if (cost <= 1f)
			{
				this._cheapNeighbors[aNodeId].Add(bNodeId);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00009A4C File Offset: 0x00007C4C
		public void RemoveOneWayConnection(int aNodeId, int bNodeId)
		{
			List<NavMeshNode> list = this._allNeighbors[aNodeId];
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].Id == bNodeId)
				{
					list.RemoveAt(i);
				}
			}
			this._cheapNeighbors[aNodeId].Remove(bNodeId);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00009A9D File Offset: 0x00007C9D
		public void VerifyBeforeChange(int aNodeId, int bNodeId)
		{
			this.VerifyBeforeChange(aNodeId);
			this.VerifyBeforeChange(bNodeId);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00009AAD File Offset: 0x00007CAD
		public void VerifyBeforeChange(int nodeId)
		{
			if (this._allNeighbors[nodeId] == TerrainNavMeshGraph.EmptyAllNeighbors)
			{
				this._allNeighbors[nodeId] = new List<NavMeshNode>();
			}
			if (this._cheapNeighbors[nodeId] == TerrainNavMeshGraph.EmptyCheapNeighbors)
			{
				this._cheapNeighbors[nodeId] = new List<int>();
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00009AE7 File Offset: 0x00007CE7
		public void VerifyAfterChange(int aNodeId, int bNodeId)
		{
			this.VerifyAfterChange(aNodeId);
			this.VerifyAfterChange(bNodeId);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00009AF7 File Offset: 0x00007CF7
		public void VerifyAfterChange(int nodeId)
		{
			if (this._allNeighbors[nodeId].Count == 0)
			{
				this._allNeighbors[nodeId] = TerrainNavMeshGraph.EmptyAllNeighbors;
			}
			if (this._cheapNeighbors[nodeId].Count == 0)
			{
				this._cheapNeighbors[nodeId] = TerrainNavMeshGraph.EmptyCheapNeighbors;
			}
		}

		// Token: 0x04000174 RID: 372
		public static readonly float DefaultConnectionCost = 1f;

		// Token: 0x04000175 RID: 373
		public static readonly List<NavMeshNode> EmptyAllNeighbors = new List<NavMeshNode>();

		// Token: 0x04000176 RID: 374
		public static readonly List<int> EmptyCheapNeighbors = new List<int>();

		// Token: 0x04000177 RID: 375
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000178 RID: 376
		public readonly NavMeshGroupService _navMeshGroupService;

		// Token: 0x04000179 RID: 377
		public List<NavMeshNode>[] _allNeighbors;

		// Token: 0x0400017A RID: 378
		public List<int>[] _cheapNeighbors;

		// Token: 0x0400017B RID: 379
		public int _defaultGroupId;
	}
}
