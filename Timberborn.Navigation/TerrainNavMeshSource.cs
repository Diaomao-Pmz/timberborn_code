using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200008A RID: 138
	public class TerrainNavMeshSource : NavMeshSource
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00008C76 File Offset: 0x00006E76
		public TerrainNavMeshSource(NodeIdService nodeIdService, TerrainNavMeshGraph terrainNavMeshGraph) : base(nodeIdService, terrainNavMeshGraph)
		{
		}

		// Token: 0x0400017C RID: 380
		public new readonly NodeIdService _nodeIdService;

		// Token: 0x0400017D RID: 381
		public readonly TerrainNavMeshGraph _terrainNavMeshGraph;

		// Token: 0x0400017E RID: 382
		public new NavMeshSourceNode[] _nodes;
	}
}
