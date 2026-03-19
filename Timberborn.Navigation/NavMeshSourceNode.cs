using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x02000061 RID: 97
	public class NavMeshSourceNode
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00006AC9 File Offset: 0x00004CC9
		public bool IsEmpty
		{
			get
			{
				return this._edges == NavMeshSourceNode.EmptyEdges && this._blockages == NavMeshSourceNode.EmptyBlockages;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006AE7 File Offset: 0x00004CE7
		public void AddEdge(int nodeId, float cost, int groupId)
		{
			if (this._edges == NavMeshSourceNode.EmptyEdges)
			{
				this._edges = new List<NavMeshNode>();
			}
			this._edges.Add(new NavMeshNode(nodeId, groupId, cost));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006B14 File Offset: 0x00004D14
		public void RemoveEdge(int nodeId, float cost, int groupId)
		{
			this._edges.Remove(new NavMeshNode(nodeId, groupId, cost));
			if (this._edges.Count == 0)
			{
				this._edges = NavMeshSourceNode.EmptyEdges;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006B42 File Offset: 0x00004D42
		public void BlockEdge(int nodeId, int groupId)
		{
			if (this._blockages == NavMeshSourceNode.EmptyBlockages)
			{
				this._blockages = new List<int>();
			}
			this._blockages.Add(NavMeshSourceNode.GetBlockageKey(nodeId, groupId));
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006B70 File Offset: 0x00004D70
		public void UnblockEdge(int nodeId, int groupId)
		{
			if (!this._blockages.Remove(NavMeshSourceNode.GetBlockageKey(nodeId, groupId)))
			{
				throw new InvalidOperationException(string.Format("Can't unblock edge to {0}, it wasn't blocked", nodeId));
			}
			if (this._blockages.Count == 0)
			{
				this._blockages = NavMeshSourceNode.EmptyBlockages;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006BC0 File Offset: 0x00004DC0
		public bool IsConnectedTo(int nodeId, out int groupId, out float cost)
		{
			cost = float.MaxValue;
			groupId = 0;
			bool result = false;
			NavMeshSourceNode.DistinctBlockages.Clear();
			NavMeshSourceNode.DistinctBlockages.AddRange(this._blockages);
			for (int i = 0; i < this._edges.Count; i++)
			{
				NavMeshNode navMeshNode = this._edges[i];
				int blockageKey = NavMeshSourceNode.GetBlockageKey(navMeshNode.Id, navMeshNode.GroupId);
				if (navMeshNode.Id == nodeId && navMeshNode.Cost < cost && !NavMeshSourceNode.DistinctBlockages.Remove(blockageKey))
				{
					result = true;
					cost = navMeshNode.Cost;
					groupId = navMeshNode.GroupId;
				}
			}
			return result;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006C61 File Offset: 0x00004E61
		public static int GetBlockageKey(int nodeId, int groupId)
		{
			return nodeId * 397 ^ groupId;
		}

		// Token: 0x040000DF RID: 223
		public static readonly List<NavMeshNode> EmptyEdges = new List<NavMeshNode>();

		// Token: 0x040000E0 RID: 224
		public static readonly List<int> EmptyBlockages = new List<int>();

		// Token: 0x040000E1 RID: 225
		public static readonly HashSet<int> DistinctBlockages = new HashSet<int>();

		// Token: 0x040000E2 RID: 226
		public List<NavMeshNode> _edges = NavMeshSourceNode.EmptyEdges;

		// Token: 0x040000E3 RID: 227
		public List<int> _blockages = NavMeshSourceNode.EmptyBlockages;
	}
}
