using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200003A RID: 58
	public class InstantDistrictMap : DistrictMap
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00004F1B File Offset: 0x0000311B
		public InstantDistrictMap(InstantRoadNavMeshGraph instantRoadNavMeshGraph, InstantTerrainNavMeshGraph instantTerrainNavMeshGraph, DistrictRoadFlowFieldGenerator districtRoadFlowFieldGenerator, RoadSpillFlowFieldGenerator roadSpillFlowFieldGenerator, NavigationDistance navigationDistance, InstantDistrictObstacleService instantDistrictObstacleService) : base(instantRoadNavMeshGraph, instantTerrainNavMeshGraph, districtRoadFlowFieldGenerator, roadSpillFlowFieldGenerator, navigationDistance, instantDistrictObstacleService)
		{
		}
	}
}
