using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200000D RID: 13
	public readonly struct DistrictChange
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002DE1 File Offset: 0x00000FE1
		public DistrictChange(District district, int nodeId, DistrictChangeType districtChangeType)
		{
			this._district = district;
			this._nodeId = nodeId;
			this._districtChangeType = districtChangeType;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public static DistrictChange AddDistrict(District district)
		{
			return new DistrictChange(district, 0, DistrictChangeType.AddDistrict);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E02 File Offset: 0x00001002
		public static DistrictChange RemoveDistrict(District district)
		{
			return new DistrictChange(district, 0, DistrictChangeType.RemoveDistrict);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E0C File Offset: 0x0000100C
		public static DistrictChange SetObstacle(int nodeId)
		{
			return new DistrictChange(null, nodeId, DistrictChangeType.SetObstacle);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E16 File Offset: 0x00001016
		public static DistrictChange UnsetObstacle(int nodeId)
		{
			return new DistrictChange(null, nodeId, DistrictChangeType.UnsetObstacle);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E20 File Offset: 0x00001020
		public void ApplyChange(DistrictMap districtMap, DistrictObstacleService districtObstacleService, NavMeshUpdate.Builder navMeshUpdateBuilder = null)
		{
			switch (this._districtChangeType)
			{
			case DistrictChangeType.AddDistrict:
				districtMap.AddDistrictCenter(this._district);
				if (navMeshUpdateBuilder != null)
				{
					navMeshUpdateBuilder.AddRoadNode(this._district.CenterNodeId);
					return;
				}
				break;
			case DistrictChangeType.RemoveDistrict:
				districtMap.RemoveDistrictCenter(this._district);
				if (navMeshUpdateBuilder != null)
				{
					navMeshUpdateBuilder.AddRoadNode(this._district.CenterNodeId);
					return;
				}
				break;
			case DistrictChangeType.SetObstacle:
				districtObstacleService.SetObstacle(this._nodeId);
				districtMap.OnObstacleChanged();
				if (navMeshUpdateBuilder != null)
				{
					navMeshUpdateBuilder.AddRoadNode(this._nodeId);
					return;
				}
				break;
			case DistrictChangeType.UnsetObstacle:
				districtObstacleService.UnsetObstacle(this._nodeId);
				districtMap.OnObstacleChanged();
				if (navMeshUpdateBuilder != null)
				{
					navMeshUpdateBuilder.AddRoadNode(this._nodeId);
					return;
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x04000021 RID: 33
		public readonly District _district;

		// Token: 0x04000022 RID: 34
		public readonly int _nodeId;

		// Token: 0x04000023 RID: 35
		public readonly DistrictChangeType _districtChangeType;
	}
}
