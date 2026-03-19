using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x02000011 RID: 17
	public class DistrictMap
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003034 File Offset: 0x00001234
		public DistrictMap(RoadNavMeshGraph roadNavMeshGraph, TerrainNavMeshGraph terrainNavMeshGraph, DistrictRoadFlowFieldGenerator districtRoadFlowFieldGenerator, RoadSpillFlowFieldGenerator roadSpillFlowFieldGenerator, NavigationDistance navigationDistance, DistrictObstacleService districtObstacleService)
		{
			this._roadNavMeshGraph = roadNavMeshGraph;
			this._terrainNavMeshGraph = terrainNavMeshGraph;
			this._districtRoadFlowFieldGenerator = districtRoadFlowFieldGenerator;
			this._roadSpillFlowFieldGenerator = roadSpillFlowFieldGenerator;
			this._navigationDistance = navigationDistance;
			this._districtObstacleService = districtObstacleService;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030C0 File Offset: 0x000012C0
		public void AddDistrictCenter(District district)
		{
			int centerNodeId = district.CenterNodeId;
			if (this._districtCenters.ContainsKey(centerNodeId))
			{
				throw new InvalidOperationException(string.Format("There's already district center at {0}", centerNodeId));
			}
			this._districtCenters[centerNodeId] = district;
			this._districtRoadFlowFields[district] = new AccessFlowField();
			this._districtRoadSpillFlowFields[district] = new RoadSpillFlowField();
			this._anyRoadFlowFieldDirty = true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000312E File Offset: 0x0000132E
		public void RemoveDistrictCenter(District district)
		{
			this._districtCenters.Remove(district.CenterNodeId);
			this._districtRoadFlowFields.Remove(district);
			this._districtRoadSpillFlowFields.Remove(district);
			this._anyRoadFlowFieldDirty = true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003164 File Offset: 0x00001364
		public bool HasDistrictCenter(District district)
		{
			District district2;
			return this._districtCenters.TryGetValue(district.CenterNodeId, out district2) && district == district2;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000318C File Offset: 0x0000138C
		public AccessFlowField GetDistrictRoadFlowFieldByRoadNodeId(int nodeId)
		{
			this.RecalculateRoadFlowFields();
			District key;
			AccessFlowField result;
			if (!this._districtsOnRoads.TryGetValue(nodeId, out key) || !this._districtRoadFlowFields.TryGetValue(key, out result))
			{
				return this._emptyRoadFlowField;
			}
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000031C8 File Offset: 0x000013C8
		public RoadSpillFlowField GetDistrictRoadSpillFlowFieldByRoadNodeId(int nodeId)
		{
			this.RecalculateRoadFlowFields();
			District district;
			if (!this._districtsOnRoads.TryGetValue(nodeId, out district))
			{
				return this._emptyRoadSpillFlowField;
			}
			return this.GetDistrictRoadSpillFlowField(district);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000031F9 File Offset: 0x000013F9
		public IReadOnlyCollection<int> DistrictCenterNodeIds()
		{
			return this._districtCenters.Keys;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003208 File Offset: 0x00001408
		public void OnObstacleChanged()
		{
			foreach (AccessFlowField accessFlowField in this._districtRoadFlowFields.Values)
			{
				accessFlowField.Clear();
			}
			this._anyRoadFlowFieldDirty = true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003264 File Offset: 0x00001464
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			ReadOnlyList<int> roadNodeIds = navMeshUpdate.RoadNodeIds;
			for (int i = 0; i < roadNodeIds.Count; i++)
			{
				District districtId;
				if (this._districtsOnRoads.TryGetValue(roadNodeIds[i], out districtId))
				{
					this.ClearDistrictRoadFlowFieldIfNotAlreadyRemoved(districtId);
					this._anyRoadFlowFieldDirty = true;
				}
			}
			ReadOnlyList<int> terrainNodeIds = navMeshUpdate.TerrainNodeIds;
			foreach (RoadSpillFlowField roadSpillFlowField in this._districtRoadSpillFlowFields.Values)
			{
				for (int j = 0; j < terrainNodeIds.Count; j++)
				{
					if (roadSpillFlowField.HasNode(terrainNodeIds[j]))
					{
						roadSpillFlowField.Clear();
						break;
					}
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003330 File Offset: 0x00001530
		public bool RoadNodeIsOccupiedByDistrict(District district, int nodeId)
		{
			this.RecalculateRoadFlowFields();
			District district2;
			return this._districtsOnRoads.TryGetValue(nodeId, out district2) && district2 == district;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000335C File Offset: 0x0000155C
		public bool NodeHasAnyDistrictRoadSpillFlowField(int nodeId)
		{
			foreach (District district in this._districtCenters.Values)
			{
				if (this.GetDistrictRoadSpillFlowField(district).HasNode(nodeId))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000033C4 File Offset: 0x000015C4
		public bool TryGetParentRoadNode(District district, int nodeId, out int parentNode)
		{
			RoadSpillFlowField districtRoadSpillFlowField = this.GetDistrictRoadSpillFlowField(district);
			if (districtRoadSpillFlowField.HasNode(nodeId))
			{
				parentNode = districtRoadSpillFlowField.GetRoadParentNodeId(nodeId);
				return true;
			}
			parentNode = 0;
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000033F4 File Offset: 0x000015F4
		public void ClearDistrictRoadFlowFieldIfNotAlreadyRemoved(District districtId)
		{
			AccessFlowField accessFlowField;
			if (this._districtRoadFlowFields.TryGetValue(districtId, out accessFlowField))
			{
				accessFlowField.Clear();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003418 File Offset: 0x00001618
		public void RecalculateRoadFlowFields()
		{
			if (this._anyRoadFlowFieldDirty)
			{
				this._districtsOnRoads.Clear();
				foreach (KeyValuePair<int, District> keyValuePair in this._districtCenters)
				{
					int num;
					District district;
					keyValuePair.Deconstruct(ref num, ref district);
					int districtCenterNodeId = num;
					District district2 = district;
					AccessFlowField accessFlowField = this._districtRoadFlowFields[district2];
					if (!accessFlowField.IsFilled)
					{
						this.RecalculateRoadFlowField(accessFlowField, districtCenterNodeId);
						this._districtRoadSpillFlowFields[district2].Clear();
					}
					this.AssignDistrictToRoadMap(district2);
				}
				this._anyRoadFlowFieldDirty = false;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000034CC File Offset: 0x000016CC
		public void RecalculateRoadFlowField(AccessFlowField roadFlowField, int districtCenterNodeId)
		{
			this._districtRoadFlowFieldGenerator.FillFlowFieldUpToDistance(this._roadNavMeshGraph, this._districtObstacleService, roadFlowField, districtCenterNodeId);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000034E8 File Offset: 0x000016E8
		public void AssignDistrictToRoadMap(District district)
		{
			foreach (int key in this._districtRoadFlowFields[district].GetAllNodeIds())
			{
				if (this._districtsOnRoads.ContainsKey(key))
				{
					throw new InvalidOperationException(string.Format("District {0} conflicts with district {1}", district, this._districtsOnRoads[key]));
				}
				this._districtsOnRoads[key] = district;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003574 File Offset: 0x00001774
		public RoadSpillFlowField GetDistrictRoadSpillFlowField(District district)
		{
			RoadSpillFlowField roadSpillFlowField;
			if (this._districtRoadSpillFlowFields.TryGetValue(district, out roadSpillFlowField))
			{
				if (!roadSpillFlowField.IsFilled)
				{
					this.RecalculateRoadFlowFields();
					AccessFlowField roadFlowField = this._districtRoadFlowFields[district];
					this.RecalculateRoadSpillFlowField(roadFlowField, roadSpillFlowField);
				}
				return roadSpillFlowField;
			}
			return this._emptyRoadSpillFlowField;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000035BC File Offset: 0x000017BC
		public void RecalculateRoadSpillFlowField(AccessFlowField roadFlowField, RoadSpillFlowField roadSpillFlowField)
		{
			this._roadSpillFlowFieldGenerator.FillFlowFieldUpToDistance(this._terrainNavMeshGraph, roadFlowField, this._navigationDistance.DistrictTerrain, roadSpillFlowField);
		}

		// Token: 0x0400002D RID: 45
		public readonly RoadNavMeshGraph _roadNavMeshGraph;

		// Token: 0x0400002E RID: 46
		public readonly TerrainNavMeshGraph _terrainNavMeshGraph;

		// Token: 0x0400002F RID: 47
		public readonly DistrictRoadFlowFieldGenerator _districtRoadFlowFieldGenerator;

		// Token: 0x04000030 RID: 48
		public readonly RoadSpillFlowFieldGenerator _roadSpillFlowFieldGenerator;

		// Token: 0x04000031 RID: 49
		public readonly NavigationDistance _navigationDistance;

		// Token: 0x04000032 RID: 50
		public readonly DistrictObstacleService _districtObstacleService;

		// Token: 0x04000033 RID: 51
		public readonly Dictionary<int, District> _districtCenters = new Dictionary<int, District>();

		// Token: 0x04000034 RID: 52
		public readonly Dictionary<District, AccessFlowField> _districtRoadFlowFields = new Dictionary<District, AccessFlowField>();

		// Token: 0x04000035 RID: 53
		public readonly Dictionary<District, RoadSpillFlowField> _districtRoadSpillFlowFields = new Dictionary<District, RoadSpillFlowField>();

		// Token: 0x04000036 RID: 54
		public readonly Dictionary<int, District> _districtsOnRoads = new Dictionary<int, District>();

		// Token: 0x04000037 RID: 55
		public readonly AccessFlowField _emptyRoadFlowField = new AccessFlowField();

		// Token: 0x04000038 RID: 56
		public readonly RoadSpillFlowField _emptyRoadSpillFlowField = new RoadSpillFlowField();

		// Token: 0x04000039 RID: 57
		public bool _anyRoadFlowFieldDirty = true;
	}
}
