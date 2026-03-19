using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000073 RID: 115
	public class RestrictedNodeMap : ILoadableSingleton
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000828E File Offset: 0x0000648E
		public RestrictedNodeMap(NodeIdService nodeIdService)
		{
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000829D File Offset: 0x0000649D
		public void Load()
		{
			this._nodes = new bool[this._nodeIdService.NumberOfNodes];
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000082B5 File Offset: 0x000064B5
		public bool IsNodeRestricted(int nodeId)
		{
			return this._nodes[nodeId];
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000082BF File Offset: 0x000064BF
		public void RestrictNode(int nodeId)
		{
			this._nodes[nodeId] = true;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000082CA File Offset: 0x000064CA
		public void UnrestrictNode(int nodeId)
		{
			this._nodes[nodeId] = false;
		}

		// Token: 0x04000123 RID: 291
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000124 RID: 292
		public bool[] _nodes;
	}
}
