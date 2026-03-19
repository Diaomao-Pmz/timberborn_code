using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000032 RID: 50
	public interface INavigationService
	{
		// Token: 0x0600012E RID: 302
		float HeuristicDistance(Vector3 start, Vector3 end);

		// Token: 0x0600012F RID: 303
		bool DestinationIsReachable(Vector3 start, Vector3 end);

		// Token: 0x06000130 RID: 304
		bool DestinationIsReachableUnlimitedRange(Vector3 start, Vector3 end);

		// Token: 0x06000131 RID: 305
		bool FindRoadPath(Vector3 start, Vector3 end, out float distance);

		// Token: 0x06000132 RID: 306
		bool FindInstantRoadPath(Vector3 access, Vector3 end, out float distance);

		// Token: 0x06000133 RID: 307
		bool FindTerrainPath(Vector3 start, Vector3 end, out float distance);

		// Token: 0x06000134 RID: 308
		bool FindPath(Vector3 start, Vector3 end, List<PathCorner> pathCorners);

		// Token: 0x06000135 RID: 309
		bool FindPathUnlimitedRange(Vector3 start, Vector3 end, List<PathCorner> pathCorners, out float distance);

		// Token: 0x06000136 RID: 310
		bool FindPathUnlimitedRange(Vector3 start, IReadOnlyList<Vector3> ends, List<PathCorner> pathCorners, out float distance);

		// Token: 0x06000137 RID: 311
		bool FindRoadSpillOrTerrainPathUnlimitedRange(Vector3 start, IReadOnlyList<Vector3> ends, List<PathCorner> pathCorners, out float distance);

		// Token: 0x06000138 RID: 312
		bool FindRoadToTerrainPath(Vector3 roadStart, Vector3 terrainEnd, out Vector3 endOfRoad, out float distanceFromClosestRoad, out float totalDistance);

		// Token: 0x06000139 RID: 313
		bool InStoppingProximity(Vector3 a, Vector3 b);

		// Token: 0x0600013A RID: 314
		bool IsOnNavMesh(Vector3 position);

		// Token: 0x0600013B RID: 315
		Vector3? ClosestPositionOnNavMesh(Vector3 position, float maxDistance);
	}
}
