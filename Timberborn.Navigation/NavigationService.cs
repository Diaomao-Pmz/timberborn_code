using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000050 RID: 80
	public class NavigationService : INavigationService
	{
		// Token: 0x0600017C RID: 380 RVA: 0x000055E4 File Offset: 0x000037E4
		public NavigationService(PathfindingService pathfindingService, NavMeshPositionService navMeshPositionService, INavMeshService navMeshService, GlobalReachabilityService globalReachabilityService, NavigationDistance navigationDistance, NavMeshGroupService navMeshGroupService)
		{
			this._pathfindingService = pathfindingService;
			this._navMeshPositionService = navMeshPositionService;
			this._navMeshService = navMeshService;
			this._globalReachabilityService = globalReachabilityService;
			this._navigationDistance = navigationDistance;
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005619 File Offset: 0x00003819
		public float HeuristicDistance(Vector3 start, Vector3 end)
		{
			return Vector3.Distance(start, end);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005622 File Offset: 0x00003822
		public bool DestinationIsReachable(Vector3 start, Vector3 end)
		{
			return this.FindPathLimitedRange(start, end, null);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000562D File Offset: 0x0000382D
		public bool DestinationIsReachableUnlimitedRange(Vector3 start, Vector3 end)
		{
			return this._globalReachabilityService.AreaReachable(start, end);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000563C File Offset: 0x0000383C
		public bool FindRoadPath(Vector3 start, Vector3 end, out float distance)
		{
			return this._pathfindingService.FindRoadPathCached(start, end, out distance, null);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000564D File Offset: 0x0000384D
		public bool FindInstantRoadPath(Vector3 start, Vector3 end, out float distance)
		{
			return this._pathfindingService.FindInstantRoadPath(start, end, out distance);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000565D File Offset: 0x0000385D
		public bool FindTerrainPath(Vector3 start, Vector3 end, out float distance)
		{
			return this._pathfindingService.FindTerrainPathCached(start, end, this._navigationDistance.ResourceBuildings, out distance, null);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005679 File Offset: 0x00003879
		public bool FindPath(Vector3 start, Vector3 end, List<PathCorner> pathCorners)
		{
			return this.FindPathLimitedRange(start, end, pathCorners);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005684 File Offset: 0x00003884
		public bool FindPathUnlimitedRange(Vector3 start, Vector3 end, List<PathCorner> pathCorners, out float distance)
		{
			return this.DestinationIsReachableUnlimitedRange(start, end, out distance, pathCorners) && this._pathfindingService.FindPathUncached(start, end, out distance, pathCorners);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000056A5 File Offset: 0x000038A5
		public bool FindPathUnlimitedRange(Vector3 start, IReadOnlyList<Vector3> ends, List<PathCorner> pathCorners, out float distance)
		{
			return this.FindPathUnlimitedRange(start, ends, pathCorners, true, out distance);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000056B3 File Offset: 0x000038B3
		public bool FindRoadSpillOrTerrainPathUnlimitedRange(Vector3 start, IReadOnlyList<Vector3> ends, List<PathCorner> pathCorners, out float distance)
		{
			return this.FindPathUnlimitedRange(start, ends, pathCorners, false, out distance);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000056C1 File Offset: 0x000038C1
		public bool FindRoadToTerrainPath(Vector3 roadStart, Vector3 terrainEnd, out Vector3 endOfRoad, out float distanceFromClosestRoad, out float totalDistance)
		{
			return this._pathfindingService.FindPathFromRoadToTerrainCached(roadStart, terrainEnd, out endOfRoad, out distanceFromClosestRoad, out totalDistance);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000056D8 File Offset: 0x000038D8
		public bool InStoppingProximity(Vector3 a, Vector3 b)
		{
			return (a - b).sqrMagnitude < NavigationService.StoppingDistanceSquared;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000056FB File Offset: 0x000038FB
		public bool IsOnNavMesh(Vector3 position)
		{
			return this._navMeshService.IsOnNavMesh(NavigationCoordinateSystem.WorldToGridInt(position));
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000570E File Offset: 0x0000390E
		public Vector3? ClosestPositionOnNavMesh(Vector3 position, float maxDistance)
		{
			return this._navMeshPositionService.ClosestPositionOnNavMesh(position, maxDistance);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005720 File Offset: 0x00003920
		public bool FindPathLimitedRange(Vector3 start, Vector3 end, List<PathCorner> pathCorners)
		{
			float num;
			return this.DestinationIsReachableUnlimitedRange(start, end, out num, pathCorners) && this._pathfindingService.FindPathUncached(start, end, out num, pathCorners);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000574C File Offset: 0x0000394C
		public bool DestinationIsReachableUnlimitedRange(Vector3 start, Vector3 end, out float distance, IList pathCorners)
		{
			distance = 0f;
			if (this.DestinationIsReachableUnlimitedRange(start, end))
			{
				return true;
			}
			if (pathCorners != null)
			{
				pathCorners.Clear();
			}
			return false;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005770 File Offset: 0x00003970
		public bool FindPathUnlimitedRange(Vector3 start, IReadOnlyList<Vector3> ends, List<PathCorner> pathCorners, bool findUncached, out float distance)
		{
			bool flag = false;
			foreach (Vector3 vector in ends)
			{
				if (this.InStoppingProximity(start, vector))
				{
					if (pathCorners != null)
					{
						pathCorners.Clear();
					}
					if (pathCorners != null)
					{
						pathCorners.Add(new PathCorner(vector, 1f, this._navMeshGroupService.GetDefaultGroupId()));
					}
					distance = 0f;
					return true;
				}
				if (!flag && this.DestinationIsReachableUnlimitedRange(start, vector))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				if (pathCorners != null)
				{
					pathCorners.Clear();
				}
				distance = 0f;
				return false;
			}
			if (findUncached)
			{
				return this._pathfindingService.FindPathUncached(start, ends, out distance, pathCorners);
			}
			return this._pathfindingService.FindRoadSpillOrTerrainPath(start, ends, out distance, pathCorners);
		}

		// Token: 0x04000092 RID: 146
		public static readonly float StoppingDistanceSquared = 0.010000001f;

		// Token: 0x04000093 RID: 147
		public readonly PathfindingService _pathfindingService;

		// Token: 0x04000094 RID: 148
		public readonly NavMeshPositionService _navMeshPositionService;

		// Token: 0x04000095 RID: 149
		public readonly INavMeshService _navMeshService;

		// Token: 0x04000096 RID: 150
		public readonly GlobalReachabilityService _globalReachabilityService;

		// Token: 0x04000097 RID: 151
		public readonly NavigationDistance _navigationDistance;

		// Token: 0x04000098 RID: 152
		public readonly NavMeshGroupService _navMeshGroupService;
	}
}
