using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200001B RID: 27
	public class DummyNavigationService : INavigationService
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003E7D File Offset: 0x0000207D
		public float HeuristicDistance(Vector3 start, Vector3 end)
		{
			return 0f;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003E84 File Offset: 0x00002084
		public bool DestinationIsReachable(Vector3 start, Vector3 end)
		{
			return false;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003E84 File Offset: 0x00002084
		public bool DestinationIsReachableUnlimitedRange(Vector3 start, Vector3 end)
		{
			return false;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003E87 File Offset: 0x00002087
		public bool FindRoadPath(Vector3 start, Vector3 end, out float distance)
		{
			distance = 0f;
			return false;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003E87 File Offset: 0x00002087
		public bool FindInstantRoadPath(Vector3 start, Vector3 end, out float distance)
		{
			distance = 0f;
			return false;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003E87 File Offset: 0x00002087
		public bool FindTerrainPath(Vector3 start, Vector3 end, out float distance)
		{
			distance = 0f;
			return false;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003E91 File Offset: 0x00002091
		public bool FindPathUnlimitedRange(Vector3 start, Vector3 end, List<PathCorner> pathCorners, out float distance)
		{
			distance = 0f;
			return false;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003E91 File Offset: 0x00002091
		public bool FindPathUnlimitedRange(Vector3 start, IReadOnlyList<Vector3> ends, List<PathCorner> pathCorners, out float distance)
		{
			distance = 0f;
			return false;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003E91 File Offset: 0x00002091
		public bool FindRoadSpillOrTerrainPathUnlimitedRange(Vector3 start, IReadOnlyList<Vector3> ends, List<PathCorner> pathCorners, out float distance)
		{
			distance = 0f;
			return false;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003E84 File Offset: 0x00002084
		public bool FindPath(Vector3 start, Vector3 end, List<PathCorner> pathCorners)
		{
			return false;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003E9C File Offset: 0x0000209C
		public bool FindRoadToTerrainPath(Vector3 roadStart, Vector3 terrainEnd, out Vector3 endOfRoad, out float distanceFromClosestRoad, out float totalDistance)
		{
			endOfRoad = default(Vector3);
			distanceFromClosestRoad = 0f;
			totalDistance = 0f;
			return false;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003E84 File Offset: 0x00002084
		public bool InStoppingProximity(Vector3 a, Vector3 b)
		{
			return false;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003E84 File Offset: 0x00002084
		public bool IsOnNavMesh(Vector3 position)
		{
			return false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003EB8 File Offset: 0x000020B8
		public Vector3? ClosestPositionOnNavMesh(Vector3 position, float maxDistance)
		{
			return null;
		}
	}
}
