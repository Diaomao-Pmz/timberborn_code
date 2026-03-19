using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200007C RID: 124
	public class RoadNavMeshSource : NavMeshSource
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00008C76 File Offset: 0x00006E76
		public RoadNavMeshSource(NodeIdService nodeIdService, RoadNavMeshGraph roadNavMeshGraph) : base(nodeIdService, roadNavMeshGraph)
		{
		}

		// Token: 0x04000144 RID: 324
		public new readonly NodeIdService _nodeIdService;

		// Token: 0x04000145 RID: 325
		public readonly RoadNavMeshGraph _roadNavMeshGraph;

		// Token: 0x04000146 RID: 326
		public new NavMeshSourceNode[] _nodes;
	}
}
