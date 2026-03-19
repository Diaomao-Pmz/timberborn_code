using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000069 RID: 105
	public class PathfindingService
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00007758 File Offset: 0x00005958
		public PathfindingService(TerrainFlowFieldCache terrainFlowFieldCache, RoadFlowFieldCache roadFlowFieldCache, TerrainAStarPathfinder terrainAStarPathfinder, TerrainFlowFieldGenerator terrainFlowFieldGenerator, RoadFlowFieldGenerator roadFlowFieldGenerator, RoadAStarPathfinder roadAStarPathfinder, NodeIdService nodeIdService, TerrainNavMeshGraph terrainNavMeshGraph, RoadNavMeshGraph roadNavMeshGraph, InstantRoadNavMeshGraph instantRoadNavMeshGraph, DistrictMap districtMap, InstantDistrictMap instantDistrictMap, FlowFieldPathFinder flowFieldPathFinder)
		{
			this._terrainFlowFieldCache = terrainFlowFieldCache;
			this._roadFlowFieldCache = roadFlowFieldCache;
			this._terrainAStarPathfinder = terrainAStarPathfinder;
			this._terrainFlowFieldGenerator = terrainFlowFieldGenerator;
			this._roadFlowFieldGenerator = roadFlowFieldGenerator;
			this._roadAStarPathfinder = roadAStarPathfinder;
			this._nodeIdService = nodeIdService;
			this._terrainNavMeshGraph = terrainNavMeshGraph;
			this._roadNavMeshGraph = roadNavMeshGraph;
			this._instantRoadNavMeshGraph = instantRoadNavMeshGraph;
			this._districtMap = districtMap;
			this._instantDistrictMap = instantDistrictMap;
			this._flowFieldPathFinder = flowFieldPathFinder;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000077DC File Offset: 0x000059DC
		public bool FindPathUncached(Vector3 start, Vector3 destination, out float distance, List<PathCorner> pathCorners = null)
		{
			if (this.FindRoadPathIfCachedAndFillIfNeeded(PathRequest.Create(start, destination), out distance, pathCorners))
			{
				return true;
			}
			if (this.FindRoadPathIfCachedAndFillIfNeeded(PathRequest.CreateReversed(destination, start), out distance, pathCorners))
			{
				return true;
			}
			if (this.FindTerrainPathIfCached(PathRequest.Create(start, destination), out distance, pathCorners))
			{
				return true;
			}
			if (this.FindTerrainPathIfCached(PathRequest.CreateReversed(destination, start), out distance, pathCorners))
			{
				return true;
			}
			if (this.FindRoadPathUncached(this._roadNavMeshGraph, this._roadFlowFieldCache.GetDefaultFlowField(), start, destination, out distance, pathCorners))
			{
				return true;
			}
			if (this.FindRoadSpillPathIfCached(PathRequest.Create(start, destination), out distance, pathCorners))
			{
				return true;
			}
			if (this.FindRoadSpillPathIfCached(PathRequest.CreateReversed(destination, start), out distance, pathCorners))
			{
				return true;
			}
			if (this.FindTerrainPathUncached(start, destination, out distance, pathCorners))
			{
				return true;
			}
			distance = 0f;
			if (pathCorners != null)
			{
				pathCorners.Clear();
			}
			return false;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000078A4 File Offset: 0x00005AA4
		public bool FindPathUncached(Vector3 start, IReadOnlyList<Vector3> destinations, out float distance, List<PathCorner> pathCorners = null)
		{
			if (destinations.Count == 1)
			{
				return this.FindPathUncached(start, destinations[0], out distance, pathCorners);
			}
			if (this.FindRoadPathIfCached(start, destinations, out distance, pathCorners))
			{
				return true;
			}
			if (this.FindTerrainPathIfCached(start, destinations, out distance, pathCorners))
			{
				return true;
			}
			if (this.FindRoadSpillPathIfCached(start, destinations, out distance, pathCorners))
			{
				return true;
			}
			if (this.FindRoadOrTerrainPathUncached(start, destinations, out distance, pathCorners))
			{
				return true;
			}
			distance = 0f;
			if (pathCorners != null)
			{
				pathCorners.Clear();
			}
			return false;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000791C File Offset: 0x00005B1C
		public bool FindRoadPathCached(Vector3 start, Vector3 destination, out float distance, List<PathCorner> pathCorners = null)
		{
			int num = this._nodeIdService.WorldToId(start);
			AccessFlowField flowFieldAtNode = this._roadFlowFieldCache.GetFlowFieldAtNode(num);
			AccessFlowField districtRoadFlowFieldByRoadNodeId = this._districtMap.GetDistrictRoadFlowFieldByRoadNodeId(num);
			this._roadFlowFieldGenerator.FillFlowField(this._roadNavMeshGraph, flowFieldAtNode, districtRoadFlowFieldByRoadNodeId, num);
			return this._flowFieldPathFinder.FindPathInFlowField(flowFieldAtNode, PathRequest.Create(start, destination), out distance, pathCorners);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000797C File Offset: 0x00005B7C
		public bool FindInstantRoadPath(Vector3 start, Vector3 destination, out float distance)
		{
			int num = this._nodeIdService.WorldToId(start);
			AccessFlowField instantFlowField = this._roadFlowFieldCache.GetInstantFlowField(num);
			AccessFlowField districtRoadFlowFieldByRoadNodeId = this._instantDistrictMap.GetDistrictRoadFlowFieldByRoadNodeId(num);
			this._roadFlowFieldGenerator.FillFlowField(this._instantRoadNavMeshGraph, instantFlowField, districtRoadFlowFieldByRoadNodeId, num);
			return this._flowFieldPathFinder.FindPathInFlowField(instantFlowField, PathRequest.Create(start, destination), out distance, null);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000079DC File Offset: 0x00005BDC
		public bool FindTerrainPathCached(Vector3 start, Vector3 destination, float maxDistance, out float distance, List<PathCorner> pathCorners = null)
		{
			int num = this._nodeIdService.WorldToId(start);
			AccessFlowField flowFieldAtNode = this._terrainFlowFieldCache.GetFlowFieldAtNode(num);
			this._terrainFlowFieldGenerator.FillFlowFieldUpToDistance(this._terrainNavMeshGraph, flowFieldAtNode, maxDistance, num);
			return this._flowFieldPathFinder.FindPathInFlowField(flowFieldAtNode, PathRequest.Create(start, destination), out distance, pathCorners);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007A30 File Offset: 0x00005C30
		public bool FindPathFromRoadToTerrainCached(Vector3 roadStart, Vector3 terrainDestination, out Vector3 endOfRoad, out float distanceFromClosestRoad, out float totalDistance)
		{
			int nodeId = this._nodeIdService.WorldToId(roadStart);
			int nodeId2 = this._nodeIdService.WorldToId(terrainDestination);
			RoadSpillFlowField districtRoadSpillFlowFieldByRoadNodeId = this._districtMap.GetDistrictRoadSpillFlowFieldByRoadNodeId(nodeId);
			if (districtRoadSpillFlowFieldByRoadNodeId.HasNode(nodeId2))
			{
				int roadParentNodeId = districtRoadSpillFlowFieldByRoadNodeId.GetRoadParentNodeId(nodeId2);
				endOfRoad = this._nodeIdService.IdToWorld(roadParentNodeId);
				float num;
				if (this.FindRoadPathCached(roadStart, endOfRoad, out num, null))
				{
					distanceFromClosestRoad = districtRoadSpillFlowFieldByRoadNodeId.GetDistanceToRoad(nodeId2);
					totalDistance = distanceFromClosestRoad + num;
					return true;
				}
			}
			endOfRoad = default(Vector3);
			distanceFromClosestRoad = 0f;
			totalDistance = 0f;
			return false;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007AC8 File Offset: 0x00005CC8
		public bool FindRoadSpillOrTerrainPath(Vector3 start, IReadOnlyList<Vector3> destinations, out float distance, List<PathCorner> pathCorners)
		{
			if (this.FindRoadSpillPathIfCached(start, destinations, out distance, pathCorners))
			{
				return true;
			}
			if (!this.FindTerrainPathIfCached(start, destinations, out distance, pathCorners))
			{
				this._destinationNodeIds.Clear();
				for (int i = 0; i < destinations.Count; i++)
				{
					this._destinationNodeIds.Add(this._nodeIdService.WorldToId(destinations[i]));
				}
				return this.FindTerrainPathUncached(start, this._destinationNodeIds, out distance, pathCorners);
			}
			distance = 0f;
			if (pathCorners != null)
			{
				pathCorners.Clear();
			}
			return false;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007B50 File Offset: 0x00005D50
		public bool FindRoadPathIfCachedAndFillIfNeeded(PathRequest pathRequest, out float distance, List<PathCorner> pathCorners)
		{
			int num = this._nodeIdService.WorldToId(pathRequest.Start);
			distance = 0f;
			AccessFlowField accessFlowField;
			return this._roadFlowFieldCache.TryGetFlowFieldAtNode(num, out accessFlowField) && (accessFlowField.IsFilled || this.TryFillRoadFlowField(num, accessFlowField)) && this._flowFieldPathFinder.FindPathInFlowField(accessFlowField, pathRequest, out distance, pathCorners);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007BAC File Offset: 0x00005DAC
		public bool TryFillRoadFlowField(int startNodeId, AccessFlowField flowField)
		{
			AccessFlowField districtRoadFlowFieldByRoadNodeId = this._districtMap.GetDistrictRoadFlowFieldByRoadNodeId(startNodeId);
			if (districtRoadFlowFieldByRoadNodeId.IsFilled)
			{
				this._roadFlowFieldGenerator.FillFlowField(this._roadNavMeshGraph, flowField, districtRoadFlowFieldByRoadNodeId, startNodeId);
				return true;
			}
			return false;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007BE8 File Offset: 0x00005DE8
		public bool FindTerrainPathIfCached(PathRequest pathRequest, out float distance, List<PathCorner> pathCorners)
		{
			int nodeId = this._nodeIdService.WorldToId(pathRequest.Start);
			distance = 0f;
			AccessFlowField flowField;
			return this._terrainFlowFieldCache.TryGetFlowFieldAtNode(nodeId, out flowField) && this._flowFieldPathFinder.FindPathInFlowField(flowField, pathRequest, out distance, pathCorners);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007C30 File Offset: 0x00005E30
		public bool FindRoadSpillPathIfCached(PathRequest pathRequest, out float distance, List<PathCorner> pathCorners = null)
		{
			int num = this._nodeIdService.WorldToId(pathRequest.Start);
			int nodeId = this._nodeIdService.WorldToId(pathRequest.Destination);
			RoadSpillFlowField districtRoadSpillFlowFieldByRoadNodeId = this._districtMap.GetDistrictRoadSpillFlowFieldByRoadNodeId(num);
			if (districtRoadSpillFlowFieldByRoadNodeId != null && districtRoadSpillFlowFieldByRoadNodeId.HasNode(nodeId))
			{
				int roadParentNodeId = districtRoadSpillFlowFieldByRoadNodeId.GetRoadParentNodeId(nodeId);
				AccessFlowField accessFlowField;
				if (this._roadFlowFieldCache.TryGetFlowFieldAtNode(num, out accessFlowField) && accessFlowField.FoundPath(roadParentNodeId))
				{
					return this._flowFieldPathFinder.FindPathInFlowField(accessFlowField, districtRoadSpillFlowFieldByRoadNodeId, pathRequest, out distance, pathCorners);
				}
				PathFlowField defaultFlowField = this._roadFlowFieldCache.GetDefaultFlowField();
				this._roadAStarPathfinder.FillFlowFieldWithPath(this._roadNavMeshGraph, defaultFlowField, num, roadParentNodeId);
				if (this._flowFieldPathFinder.FindPathInFlowField(defaultFlowField, districtRoadSpillFlowFieldByRoadNodeId, pathRequest, out distance, pathCorners))
				{
					return true;
				}
			}
			distance = 0f;
			return false;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007CF0 File Offset: 0x00005EF0
		public bool FindRoadPathUncached(RoadNavMeshGraph roadNavMeshGraph, PathFlowField flowField, Vector3 start, Vector3 destination, out float distance, List<PathCorner> pathCorners = null)
		{
			int startNodeId = this._nodeIdService.WorldToId(start);
			int destinationNodeId = this._nodeIdService.WorldToId(destination);
			this._roadAStarPathfinder.FillFlowFieldWithPath(roadNavMeshGraph, flowField, startNodeId, destinationNodeId);
			distance = 0f;
			return this._flowFieldPathFinder.FindPathInFlowField(flowField, PathRequest.Create(start, destination), out distance, pathCorners);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007D48 File Offset: 0x00005F48
		public bool FindTerrainPathUncached(Vector3 start, Vector3 destination, out float distance, List<PathCorner> pathCorners = null)
		{
			int startNodeId = this._nodeIdService.WorldToId(start);
			int destinationNodeId = this._nodeIdService.WorldToId(destination);
			PathFlowField defaultFlowField = this._terrainFlowFieldCache.GetDefaultFlowField();
			this._terrainAStarPathfinder.FillFlowFieldWithPath(this._terrainNavMeshGraph, defaultFlowField, startNodeId, destinationNodeId);
			distance = 0f;
			return this._flowFieldPathFinder.FindPathInFlowField(defaultFlowField, PathRequest.Create(start, destination), out distance, pathCorners);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007DAC File Offset: 0x00005FAC
		public bool FindRoadPathIfCached(Vector3 start, IReadOnlyList<Vector3> destinations, out float distance, List<PathCorner> pathCorners)
		{
			int nodeId = this._nodeIdService.WorldToId(start);
			distance = 0f;
			AccessFlowField flowField;
			return this._roadFlowFieldCache.TryGetFlowFieldAtNode(nodeId, out flowField) && this._flowFieldPathFinder.FindPathInFlowField(start, destinations, flowField, out distance, pathCorners);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public bool FindTerrainPathIfCached(Vector3 start, IReadOnlyList<Vector3> destinations, out float distance, List<PathCorner> pathCorners)
		{
			int nodeId = this._nodeIdService.WorldToId(start);
			distance = 0f;
			AccessFlowField flowField;
			return this._terrainFlowFieldCache.TryGetFlowFieldAtNode(nodeId, out flowField) && this._flowFieldPathFinder.FindPathInFlowField(start, destinations, flowField, out distance, pathCorners);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007E34 File Offset: 0x00006034
		public bool FindRoadSpillPathIfCached(Vector3 start, IReadOnlyList<Vector3> destinations, out float distance, List<PathCorner> pathCorners = null)
		{
			int num = this._nodeIdService.WorldToId(start);
			RoadSpillFlowField districtRoadSpillFlowFieldByRoadNodeId = this._districtMap.GetDistrictRoadSpillFlowFieldByRoadNodeId(num);
			if (districtRoadSpillFlowFieldByRoadNodeId != null && districtRoadSpillFlowFieldByRoadNodeId.IsFilled)
			{
				Vector3? vector = null;
				float num2 = float.PositiveInfinity;
				for (int i = 0; i < destinations.Count; i++)
				{
					Vector3 vector2 = destinations[i];
					int nodeId = this._nodeIdService.WorldToId(vector2);
					if (districtRoadSpillFlowFieldByRoadNodeId.HasNode(nodeId))
					{
						float? num3 = null;
						AccessFlowField accessFlowField;
						if (this._roadFlowFieldCache.TryGetFlowFieldAtNode(num, out accessFlowField) && this._flowFieldPathFinder.FindPathInFlowField(accessFlowField, districtRoadSpillFlowFieldByRoadNodeId, PathRequest.Create(start, vector2), out distance, null))
						{
							num3 = new float?(distance);
						}
						else
						{
							PathFlowField defaultFlowField = this._roadFlowFieldCache.GetDefaultFlowField();
							int roadParentNodeId = districtRoadSpillFlowFieldByRoadNodeId.GetRoadParentNodeId(nodeId);
							this._roadAStarPathfinder.FillFlowFieldWithPath(this._roadNavMeshGraph, defaultFlowField, num, roadParentNodeId);
							if (this._flowFieldPathFinder.FindPathInFlowField(defaultFlowField, districtRoadSpillFlowFieldByRoadNodeId, PathRequest.Create(start, vector2), out distance, null))
							{
								num3 = new float?(distance);
							}
						}
						float? num4 = num3;
						float num5 = num2;
						if (num4.GetValueOrDefault() < num5 & num4 != null)
						{
							vector = new Vector3?(vector2);
							num2 = num3.Value;
						}
					}
				}
				if (vector != null)
				{
					return this.FindRoadSpillPathIfCached(PathRequest.Create(start, vector.Value), out distance, pathCorners);
				}
			}
			distance = 0f;
			return false;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007FA0 File Offset: 0x000061A0
		public bool FindRoadOrTerrainPathUncached(Vector3 start, IReadOnlyList<Vector3> destinations, out float distance, List<PathCorner> pathCorners = null)
		{
			this._destinationNodeIds.Clear();
			for (int i = 0; i < destinations.Count; i++)
			{
				this._destinationNodeIds.Add(this._nodeIdService.WorldToId(destinations[i]));
			}
			if (this.FindRoadPathUncached(start, this._destinationNodeIds, out distance, pathCorners))
			{
				return true;
			}
			if (this.FindTerrainPathUncached(start, this._destinationNodeIds, out distance, pathCorners))
			{
				return true;
			}
			distance = 0f;
			return false;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008018 File Offset: 0x00006218
		public bool FindRoadPathUncached(Vector3 start, IReadOnlyList<int> destinationNodeIds, out float distance, List<PathCorner> pathCorners = null)
		{
			int startNodeId = this._nodeIdService.WorldToId(start);
			PathFlowField defaultFlowField = this._roadFlowFieldCache.GetDefaultFlowField();
			int nodeId;
			if (this._roadAStarPathfinder.FillFlowFieldWithPath(this._roadNavMeshGraph, defaultFlowField, startNodeId, destinationNodeIds, out nodeId))
			{
				Vector3 destination = this._nodeIdService.IdToWorld(nodeId);
				return this._flowFieldPathFinder.FindPathInFlowField(defaultFlowField, PathRequest.Create(start, destination), out distance, pathCorners);
			}
			distance = 0f;
			return false;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008084 File Offset: 0x00006284
		public bool FindTerrainPathUncached(Vector3 start, IReadOnlyList<int> destinationNodeIds, out float distance, List<PathCorner> pathCorners = null)
		{
			int startNodeId = this._nodeIdService.WorldToId(start);
			PathFlowField defaultFlowField = this._terrainFlowFieldCache.GetDefaultFlowField();
			int nodeId;
			if (this._terrainAStarPathfinder.FillFlowFieldWithPath(this._terrainNavMeshGraph, defaultFlowField, startNodeId, destinationNodeIds, out nodeId))
			{
				Vector3 destination = this._nodeIdService.IdToWorld(nodeId);
				return this._flowFieldPathFinder.FindPathInFlowField(defaultFlowField, PathRequest.Create(start, destination), out distance, pathCorners);
			}
			distance = 0f;
			return false;
		}

		// Token: 0x0400010C RID: 268
		public readonly TerrainFlowFieldCache _terrainFlowFieldCache;

		// Token: 0x0400010D RID: 269
		public readonly RoadFlowFieldCache _roadFlowFieldCache;

		// Token: 0x0400010E RID: 270
		public readonly TerrainAStarPathfinder _terrainAStarPathfinder;

		// Token: 0x0400010F RID: 271
		public readonly TerrainFlowFieldGenerator _terrainFlowFieldGenerator;

		// Token: 0x04000110 RID: 272
		public readonly RoadFlowFieldGenerator _roadFlowFieldGenerator;

		// Token: 0x04000111 RID: 273
		public readonly RoadAStarPathfinder _roadAStarPathfinder;

		// Token: 0x04000112 RID: 274
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000113 RID: 275
		public readonly TerrainNavMeshGraph _terrainNavMeshGraph;

		// Token: 0x04000114 RID: 276
		public readonly RoadNavMeshGraph _roadNavMeshGraph;

		// Token: 0x04000115 RID: 277
		public readonly InstantRoadNavMeshGraph _instantRoadNavMeshGraph;

		// Token: 0x04000116 RID: 278
		public readonly DistrictMap _districtMap;

		// Token: 0x04000117 RID: 279
		public readonly InstantDistrictMap _instantDistrictMap;

		// Token: 0x04000118 RID: 280
		public readonly FlowFieldPathFinder _flowFieldPathFinder;

		// Token: 0x04000119 RID: 281
		public readonly List<int> _destinationNodeIds = new List<int>();
	}
}
