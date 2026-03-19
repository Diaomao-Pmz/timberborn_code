using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000060 RID: 96
	public abstract class NavMeshSource : ILoadableSingleton
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000693D File Offset: 0x00004B3D
		public NavMeshSource(NodeIdService nodeIdService, INavMeshGraph navMeshGraph)
		{
			this._nodeIdService = nodeIdService;
			this._navMeshGraph = navMeshGraph;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006953 File Offset: 0x00004B53
		public void Load()
		{
			this._nodes = new NavMeshSourceNode[this._nodeIdService.NumberOfNodes];
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000696B File Offset: 0x00004B6B
		public void AddEdge(int startNodeId, int endNodeId, int groupId, float cost)
		{
			this.VerifyBeforeChange(startNodeId, endNodeId);
			this.GetNode(startNodeId).AddEdge(endNodeId, cost, groupId);
			this.UpdateConnectionBetweenNodes(startNodeId, endNodeId);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000698D File Offset: 0x00004B8D
		public void RemoveEdge(int startNodeId, int endNodeId, int group, float cost)
		{
			this.VerifyBeforeChange(startNodeId, endNodeId);
			this.GetNode(startNodeId).RemoveEdge(endNodeId, cost, group);
			this.UpdateConnectionBetweenNodes(startNodeId, endNodeId);
			this.VerifyAfterChange(startNodeId, endNodeId);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000069B7 File Offset: 0x00004BB7
		public void BlockEdge(int startNodeId, int endNodeId, int groupId)
		{
			this.VerifyBeforeChange(startNodeId, endNodeId);
			this.GetNode(startNodeId).BlockEdge(endNodeId, groupId);
			this.UpdateConnectionBetweenNodes(startNodeId, endNodeId);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000069D7 File Offset: 0x00004BD7
		public void UnblockEdge(int startNodeId, int endNodeId, int groupId)
		{
			this.VerifyBeforeChange(startNodeId, endNodeId);
			this.GetNode(startNodeId).UnblockEdge(endNodeId, groupId);
			this.UpdateConnectionBetweenNodes(startNodeId, endNodeId);
			this.VerifyAfterChange(startNodeId, endNodeId);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000069FF File Offset: 0x00004BFF
		public void VerifyBeforeChange(int startNodeId, int endNodeId)
		{
			this.VerifyBeforeChange(startNodeId);
			this.VerifyBeforeChange(endNodeId);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006A10 File Offset: 0x00004C10
		public void VerifyBeforeChange(int nodeId)
		{
			NavMeshSourceNode[] nodes = this._nodes;
			if (nodes[nodeId] == null)
			{
				nodes[nodeId] = new NavMeshSourceNode();
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006A33 File Offset: 0x00004C33
		public void VerifyAfterChange(int startNodeId, int endNodeId)
		{
			this.VerifyAfterChange(startNodeId);
			this.VerifyAfterChange(endNodeId);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006A43 File Offset: 0x00004C43
		public void VerifyAfterChange(int nodeId)
		{
			if (this._nodes[nodeId].IsEmpty)
			{
				this._nodes[nodeId] = null;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006A5D File Offset: 0x00004C5D
		public NavMeshSourceNode GetNode(int nodeId)
		{
			return this._nodes[nodeId];
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006A68 File Offset: 0x00004C68
		public void UpdateConnectionBetweenNodes(int aNodeId, int bNodeId)
		{
			NavMeshSourceNode node = this.GetNode(aNodeId);
			NavMeshSourceNode node2 = this.GetNode(bNodeId);
			int num;
			float val;
			int num2;
			float val2;
			if (node.IsConnectedTo(bNodeId, out num, out val) && node2.IsConnectedTo(aNodeId, out num2, out val2) && num == num2)
			{
				float cost = Math.Max(val, val2);
				this._navMeshGraph.ConnectNodes(aNodeId, bNodeId, num, cost);
				return;
			}
			this._navMeshGraph.DisconnectNodes(aNodeId, bNodeId);
		}

		// Token: 0x040000DC RID: 220
		public readonly NodeIdService _nodeIdService;

		// Token: 0x040000DD RID: 221
		public readonly INavMeshGraph _navMeshGraph;

		// Token: 0x040000DE RID: 222
		public NavMeshSourceNode[] _nodes;
	}
}
