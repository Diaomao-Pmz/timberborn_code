using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000008 RID: 8
	public readonly struct AStarNode : IOrderable<AStarNode>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A50 File Offset: 0x00000C50
		public int NodeId { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A58 File Offset: 0x00000C58
		public int ParentNodeId { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A60 File Offset: 0x00000C60
		public float GScore { get; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002A68 File Offset: 0x00000C68
		public AStarNode(int nodeId, int parentNodeId, float gScore, float fScore)
		{
			this.NodeId = nodeId;
			this.ParentNodeId = parentNodeId;
			this.GScore = gScore;
			this._fScore = fScore;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A87 File Offset: 0x00000C87
		public bool IsLessThan(AStarNode other)
		{
			return this._fScore < other._fScore;
		}

		// Token: 0x0400001A RID: 26
		public readonly float _fScore;
	}
}
