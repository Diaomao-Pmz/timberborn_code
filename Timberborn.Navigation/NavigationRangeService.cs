using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200004F RID: 79
	public class NavigationRangeService : INavigationRangeService
	{
		// Token: 0x06000175 RID: 373 RVA: 0x000054D5 File Offset: 0x000036D5
		public NavigationRangeService(NodeIdService nodeIdService, NavigationDistance navigationDistance, TerrainNavigationRangeService terrainNavigationRangeService, RoadNavigationRangeService roadNavigationRangeService, RoadSpillNavigationRangeService roadSpillNavigationRangeService)
		{
			this._terrainNavigationRangeService = terrainNavigationRangeService;
			this._nodeIdService = nodeIdService;
			this._navigationDistance = navigationDistance;
			this._roadNavigationRangeService = roadNavigationRangeService;
			this._roadSpillNavigationRangeService = roadSpillNavigationRangeService;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005502 File Offset: 0x00003702
		public IEnumerable<WeightedCoordinates> GetRoadNodesInRange(Vector3 position)
		{
			if (!this._nodeIdService.Contains(position))
			{
				return Enumerable.Empty<WeightedCoordinates>();
			}
			return this._roadNavigationRangeService.GetNodesInRange(position);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005524 File Offset: 0x00003724
		public IEnumerable<WeightedCoordinates> GetRoadPreviewNodesInRange(Vector3 position)
		{
			if (!this._nodeIdService.Contains(position))
			{
				return Enumerable.Empty<WeightedCoordinates>();
			}
			return this._roadNavigationRangeService.GetPreviewNodesInRange(position);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005546 File Offset: 0x00003746
		public IEnumerable<Vector3Int> GetTerrainNodesInRange(Vector3 position)
		{
			if (!this._nodeIdService.Contains(position))
			{
				return Enumerable.Empty<Vector3Int>();
			}
			return this._terrainNavigationRangeService.GetNodesInRange(position, this._navigationDistance.ResourceBuildings);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005573 File Offset: 0x00003773
		public IEnumerable<Vector3Int> GetTerrainPreviewNodesInRange(Vector3 position)
		{
			if (!this._nodeIdService.Contains(position))
			{
				return Enumerable.Empty<Vector3Int>();
			}
			return this._terrainNavigationRangeService.GetPreviewNodesInRange(position, this._navigationDistance.ResourceBuildings);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000055A0 File Offset: 0x000037A0
		public IEnumerable<Vector3Int> GetRoadSpillNodesInRange(Vector3 position)
		{
			if (!this._nodeIdService.Contains(position))
			{
				return Enumerable.Empty<Vector3Int>();
			}
			return this._roadSpillNavigationRangeService.GetNodesInRange(position);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000055C2 File Offset: 0x000037C2
		public IEnumerable<Vector3Int> GetRoadSpillPreviewNodesInRange(Vector3 position)
		{
			if (!this._nodeIdService.Contains(position))
			{
				return Enumerable.Empty<Vector3Int>();
			}
			return this._roadSpillNavigationRangeService.GetPreviewNodesInRange(position);
		}

		// Token: 0x0400008D RID: 141
		public readonly NodeIdService _nodeIdService;

		// Token: 0x0400008E RID: 142
		public readonly NavigationDistance _navigationDistance;

		// Token: 0x0400008F RID: 143
		public readonly TerrainNavigationRangeService _terrainNavigationRangeService;

		// Token: 0x04000090 RID: 144
		public readonly RoadNavigationRangeService _roadNavigationRangeService;

		// Token: 0x04000091 RID: 145
		public readonly RoadSpillNavigationRangeService _roadSpillNavigationRangeService;
	}
}
