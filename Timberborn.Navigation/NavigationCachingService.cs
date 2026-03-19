using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200004A RID: 74
	public class NavigationCachingService : INavigationCachingService
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00004F5C File Offset: 0x0000315C
		public NavigationCachingService(RoadFlowFieldCache roadFlowFieldCache, TerrainFlowFieldCache terrainFlowFieldCache, NodeIdService nodeIdService)
		{
			this._roadFlowFieldCache = roadFlowFieldCache;
			this._terrainFlowFieldCache = terrainFlowFieldCache;
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004F79 File Offset: 0x00003179
		public void StartCachingRoadFlowField(Vector3Int coordinates)
		{
			this._roadFlowFieldCache.StartCachingAtNode(this._nodeIdService.GridToId(coordinates));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004F92 File Offset: 0x00003192
		public void StopCachingRoadFlowField(Vector3Int coordinates)
		{
			this._roadFlowFieldCache.StopCachingAtNode(this._nodeIdService.GridToId(coordinates));
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004FAB File Offset: 0x000031AB
		public void StartCachingTerrainFlowField(Vector3Int coordinates)
		{
			this._terrainFlowFieldCache.StartCachingAtNode(this._nodeIdService.GridToId(coordinates));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004FC4 File Offset: 0x000031C4
		public void StopCachingTerrainFlowField(Vector3Int coordinates)
		{
			this._terrainFlowFieldCache.StopCachingAtNode(this._nodeIdService.GridToId(coordinates));
		}

		// Token: 0x04000084 RID: 132
		public readonly RoadFlowFieldCache _roadFlowFieldCache;

		// Token: 0x04000085 RID: 133
		public readonly TerrainFlowFieldCache _terrainFlowFieldCache;

		// Token: 0x04000086 RID: 134
		public readonly NodeIdService _nodeIdService;
	}
}
