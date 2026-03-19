using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200006D RID: 109
	public class PreviewDistrictMap : DistrictMap
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00004F1B File Offset: 0x0000311B
		public PreviewDistrictMap(PreviewRoadNavMeshGraph previewRoadNavMeshGraph, PreviewTerrainNavMeshGraph previewTerrainNavMeshGraph, DistrictRoadFlowFieldGenerator districtRoadFlowFieldGenerator, RoadSpillFlowFieldGenerator roadSpillFlowFieldGenerator, NavigationDistance navigationDistance, PreviewDistrictObstacleService previewDistrictObstacleService) : base(previewRoadNavMeshGraph, previewTerrainNavMeshGraph, districtRoadFlowFieldGenerator, roadSpillFlowFieldGenerator, navigationDistance, previewDistrictObstacleService)
		{
		}
	}
}
