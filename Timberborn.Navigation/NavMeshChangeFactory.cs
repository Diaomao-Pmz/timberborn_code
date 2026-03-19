using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000053 RID: 83
	public class NavMeshChangeFactory
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00005BC2 File Offset: 0x00003DC2
		public NavMeshChangeFactory(NodeIdService nodeIdService)
		{
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public NavMeshChange Create(in NavMeshChangeSpecification navMeshChangeSpecification)
		{
			NavMeshEdge navMeshEdge = navMeshChangeSpecification.NavMeshEdge;
			if (this.EdgeIsInBounds(navMeshEdge))
			{
				NavMeshChangeType navMeshChangeType = navMeshChangeSpecification.NavMeshChangeType;
				int startNodeId = this._nodeIdService.GridToId(navMeshEdge.Start);
				int endNodeId = this._nodeIdService.GridToId(navMeshEdge.End);
				int groupId = navMeshChangeSpecification.NavMeshEdge.GroupId;
				float cost = NavMeshChangeFactory.EdgeCost(navMeshChangeType, navMeshEdge);
				return new NavMeshChange(navMeshChangeType, startNodeId, endNodeId, groupId, cost);
			}
			return default(NavMeshChange);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005C4D File Offset: 0x00003E4D
		public bool EdgeIsInBounds(in NavMeshEdge edge)
		{
			return this._nodeIdService.Contains(edge.Start) && this._nodeIdService.Contains(edge.End);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005C75 File Offset: 0x00003E75
		public static float EdgeCost(NavMeshChangeType navMeshChangeType, in NavMeshEdge navMeshEdge)
		{
			if (navMeshChangeType != NavMeshChangeType.BlockEdge && navMeshChangeType != NavMeshChangeType.UnblockEdge)
			{
				return navMeshEdge.Cost;
			}
			return 0f;
		}

		// Token: 0x040000A6 RID: 166
		public readonly NodeIdService _nodeIdService;
	}
}
