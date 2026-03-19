using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000014 RID: 20
	public class DistrictRandomDestinationPicker
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000036A8 File Offset: 0x000018A8
		public DistrictRandomDestinationPicker(IRandomNumberGenerator randomNumberGenerator, RoadReachabilityService roadReachabilityService, TerrainReachabilityService terrainReachabilityService, DistrictMap districtMap, NodeIdService nodeIdService, TerrainNavMeshGraph terrainNavMeshGraph)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._roadReachabilityService = roadReachabilityService;
			this._terrainReachabilityService = terrainReachabilityService;
			this._districtMap = districtMap;
			this._nodeIdService = nodeIdService;
			this._terrainNavMeshGraph = terrainNavMeshGraph;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003700 File Offset: 0x00001900
		public Vector3 GetRandomDestination(District district, Vector3 coordinates)
		{
			int nodeId = this._nodeIdService.WorldToId(coordinates);
			int roadNode;
			if (!this._districtMap.TryGetParentRoadNode(district, nodeId, out roadNode))
			{
				return coordinates;
			}
			return this.GetRandomDestination(roadNode);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003734 File Offset: 0x00001934
		public Vector3 GetRandomDestination(int roadNode)
		{
			this._roadReachabilityService.GetReachableNeighborsInRange(roadNode, DistrictRandomDestinationPicker.RoadMaxDistance, this._reachableNodes);
			this.ValidateAndClearReachableNodes();
			int listElement = this._randomNumberGenerator.GetListElement<int>(this._validNodes);
			this._validNodes.Clear();
			this._terrainReachabilityService.GetReachableNeighborsInRange(listElement, DistrictRandomDestinationPicker.TerrainMaxDistance, this._reachableNodes);
			this.ValidateAndClearReachableNodes();
			int num;
			int nodeId = this._randomNumberGenerator.TryGetListElement<int>(this._validNodes, out num) ? num : listElement;
			this._validNodes.Clear();
			return this._nodeIdService.IdToWorld(nodeId);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000037CC File Offset: 0x000019CC
		public void ValidateAndClearReachableNodes()
		{
			foreach (int num in this._reachableNodes)
			{
				if (this._terrainNavMeshGraph.IsConnectedToDefaultGroup(num))
				{
					this._validNodes.Add(num);
				}
			}
			this._reachableNodes.Clear();
		}

		// Token: 0x0400003F RID: 63
		public static readonly int RoadMaxDistance = 10;

		// Token: 0x04000040 RID: 64
		public static readonly int TerrainMaxDistance = 5;

		// Token: 0x04000041 RID: 65
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000042 RID: 66
		public readonly RoadReachabilityService _roadReachabilityService;

		// Token: 0x04000043 RID: 67
		public readonly TerrainReachabilityService _terrainReachabilityService;

		// Token: 0x04000044 RID: 68
		public readonly DistrictMap _districtMap;

		// Token: 0x04000045 RID: 69
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000046 RID: 70
		public readonly TerrainNavMeshGraph _terrainNavMeshGraph;

		// Token: 0x04000047 RID: 71
		public readonly List<int> _reachableNodes = new List<int>();

		// Token: 0x04000048 RID: 72
		public readonly List<int> _validNodes = new List<int>();
	}
}
