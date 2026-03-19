using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000017 RID: 23
	public class DistrictService : IDistrictService
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000039D0 File Offset: 0x00001BD0
		public DistrictService(InstantDistrictMap instantDistrictMap, NodeIdService nodeIdService, DistrictUpdater districtUpdater, PreviewDistrictMap previewDistrictMap, DistrictConflictDetector districtConflictDetector, PreviewRoadNavMeshGraph previewRoadNavMeshGraph, PreviewDistrictObstacleService previewDistrictObstacleService, DistrictMap districtMap, GlobalReachabilityService globalReachabilityService, DistrictRandomDestinationPicker districtRandomDestinationPicker)
		{
			this._instantDistrictMap = instantDistrictMap;
			this._nodeIdService = nodeIdService;
			this._districtUpdater = districtUpdater;
			this._previewDistrictMap = previewDistrictMap;
			this._districtConflictDetector = districtConflictDetector;
			this._previewRoadNavMeshGraph = previewRoadNavMeshGraph;
			this._previewDistrictObstacleService = previewDistrictObstacleService;
			this._districtMap = districtMap;
			this._globalReachabilityService = globalReachabilityService;
			this._districtRandomDestinationPicker = districtRandomDestinationPicker;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003A30 File Offset: 0x00001C30
		public District AddDistrict(Vector3Int centerCoordinates)
		{
			if (this._nodeIdService.Contains(centerCoordinates))
			{
				District district = new District(this._nodeIdService.GridToId(centerCoordinates), centerCoordinates);
				this._districtUpdater.EnqueueChange(DistrictChange.AddDistrict(district));
				return district;
			}
			throw new ArgumentException(string.Format("{0} is out of map", centerCoordinates));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003A86 File Offset: 0x00001C86
		public void RemoveDistrict(District district)
		{
			this._districtUpdater.EnqueueChange(DistrictChange.RemoveDistrict(district));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003A9C File Offset: 0x00001C9C
		public District AddPreviewDistrict(Vector3Int centerCoordinates)
		{
			if (this._nodeIdService.Contains(centerCoordinates))
			{
				District district = new District(this._nodeIdService.GridToId(centerCoordinates), centerCoordinates);
				this._districtUpdater.ApplyPreviewChange(DistrictChange.AddDistrict(district));
				return district;
			}
			throw new ArgumentException(string.Format("{0} is out of map", centerCoordinates));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003AF2 File Offset: 0x00001CF2
		public void RemovePreviewDistrict(District district)
		{
			this._districtUpdater.ApplyPreviewChange(DistrictChange.RemoveDistrict(district));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003B08 File Offset: 0x00001D08
		public void SetObstacle(Vector3Int coordinates)
		{
			if (this._nodeIdService.Contains(coordinates))
			{
				int obstacle = this._nodeIdService.GridToId(coordinates);
				this._districtUpdater.EnqueueChange(DistrictChange.SetObstacle(obstacle));
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003B44 File Offset: 0x00001D44
		public void UnsetObstacle(Vector3Int coordinates)
		{
			if (this._nodeIdService.Contains(coordinates))
			{
				int nodeId = this._nodeIdService.GridToId(coordinates);
				this._districtUpdater.EnqueueChange(DistrictChange.UnsetObstacle(nodeId));
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003B80 File Offset: 0x00001D80
		public void SetPreviewObstacle(Vector3Int coordinates)
		{
			if (this._nodeIdService.Contains(coordinates))
			{
				int obstacle = this._nodeIdService.GridToId(coordinates);
				this._districtUpdater.ApplyPreviewChange(DistrictChange.SetObstacle(obstacle));
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003BBC File Offset: 0x00001DBC
		public void UnsetPreviewObstacle(Vector3Int coordinates)
		{
			if (this._nodeIdService.Contains(coordinates))
			{
				int nodeId = this._nodeIdService.GridToId(coordinates);
				this._districtUpdater.ApplyPreviewChange(DistrictChange.UnsetObstacle(nodeId));
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public bool IsPreviewDistrictInConflict(Vector3Int? previewDistrictCenter)
		{
			IReadOnlyCollection<int> readOnlyCollection = this._previewDistrictMap.DistrictCenterNodeIds();
			IEnumerable<int> enumerable2;
			if (previewDistrictCenter == null)
			{
				IEnumerable<int> enumerable = readOnlyCollection;
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = readOnlyCollection.Append(this._nodeIdService.GridToId(previewDistrictCenter.Value));
			}
			IEnumerable<int> districtCenters = enumerable2;
			return this._districtConflictDetector.AreDistrictsInConflict(this._previewRoadNavMeshGraph, this._previewDistrictObstacleService, districtCenters);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003C50 File Offset: 0x00001E50
		public bool DistrictIsGloballyReachable(District district, Vector3 start)
		{
			return district != null && this._instantDistrictMap.HasDistrictCenter(district) && this._globalReachabilityService.AreaReachable(start, district.CenterNodeId);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003C77 File Offset: 0x00001E77
		public bool IsOnDistrictRoad(District district, Vector3 road)
		{
			return this.IsOnDistrictRoad(this._districtMap, district, road);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003C87 File Offset: 0x00001E87
		public bool IsOnInstantDistrictRoad(District district, Vector3 road)
		{
			return this.IsOnDistrictRoad(this._instantDistrictMap, district, road);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003C97 File Offset: 0x00001E97
		public bool IsOnPreviewDistrictRoad(District district, Vector3 road)
		{
			return this.IsOnDistrictRoad(this._previewDistrictMap, district, road);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public bool IsOnInstantDistrictRoadSpill(Accessible accessible)
		{
			for (int i = 0; i < accessible.Accesses.Count; i++)
			{
				if (this.IsOnInstantDistrictRoadSpill(accessible.Accesses[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public bool IsOnInstantDistrictRoadSpill(Vector3 position)
		{
			return this._nodeIdService.Contains(position) && this._instantDistrictMap.NodeHasAnyDistrictRoadSpillFlowField(this._nodeIdService.WorldToId(position));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003D11 File Offset: 0x00001F11
		public Vector3 GetRandomDestinationInDistrict(District district, Vector3 coordinates)
		{
			return this._districtRandomDestinationPicker.GetRandomDestination(district, coordinates);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003D20 File Offset: 0x00001F20
		public bool IsOnDistrictRoad(DistrictMap districtMap, District district, Vector3 road)
		{
			if (this._nodeIdService.Contains(road))
			{
				int nodeId = this._nodeIdService.WorldToId(road);
				return districtMap.RoadNodeIsOccupiedByDistrict(district, nodeId);
			}
			return false;
		}

		// Token: 0x0400004E RID: 78
		public readonly InstantDistrictMap _instantDistrictMap;

		// Token: 0x0400004F RID: 79
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000050 RID: 80
		public readonly DistrictUpdater _districtUpdater;

		// Token: 0x04000051 RID: 81
		public readonly PreviewDistrictMap _previewDistrictMap;

		// Token: 0x04000052 RID: 82
		public readonly DistrictConflictDetector _districtConflictDetector;

		// Token: 0x04000053 RID: 83
		public readonly PreviewRoadNavMeshGraph _previewRoadNavMeshGraph;

		// Token: 0x04000054 RID: 84
		public readonly PreviewDistrictObstacleService _previewDistrictObstacleService;

		// Token: 0x04000055 RID: 85
		public readonly DistrictMap _districtMap;

		// Token: 0x04000056 RID: 86
		public readonly GlobalReachabilityService _globalReachabilityService;

		// Token: 0x04000057 RID: 87
		public readonly DistrictRandomDestinationPicker _districtRandomDestinationPicker;
	}
}
