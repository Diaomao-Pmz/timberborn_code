using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200000F RID: 15
	public class DistrictBuildingDistance : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002EBC File Offset: 0x000010BC
		public DistrictBuildingDistance(NavigationDistance navigationDistance, DistanceToDistrictDescriber distanceToDistrictDescriber)
		{
			this._navigationDistance = navigationDistance;
			this._distanceToDistrictDescriber = distanceToDistrictDescriber;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002ED2 File Offset: 0x000010D2
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void Start()
		{
			this._access = base.GetComponent<BuildingAccessible>().CalculateAccess();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EF4 File Offset: 0x000010F4
		public bool TryGetDistanceToDistrict(out int distance)
		{
			if (!this._districtBuilding.IsEnabledDistrictCenter)
			{
				DistrictCenter instantOrConstructionDistrict = this._districtBuilding.GetInstantOrConstructionDistrict();
				if (instantOrConstructionDistrict != null)
				{
					Accessible accessible = instantOrConstructionDistrict.GetComponent<BuildingAccessible>().Accessible;
					float num;
					if (!accessible.FindRoadPath(this._access, out num))
					{
						accessible.FindInstantRoadPath(this._access, out num);
					}
					distance = Mathf.RoundToInt(num);
					return true;
				}
			}
			distance = 0;
			return false;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F58 File Offset: 0x00001158
		public string DescribeDistance()
		{
			int num;
			if (!this.TryGetDistanceToDistrict(out num))
			{
				return null;
			}
			return this._distanceToDistrictDescriber.DescribeDistance((float)num);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F80 File Offset: 0x00001180
		public bool IsAboveThreshold()
		{
			int num;
			return this.TryGetDistanceToDistrict(out num) && (float)num > this._navigationDistance.LargeDistrictThreshold;
		}

		// Token: 0x04000029 RID: 41
		public readonly NavigationDistance _navigationDistance;

		// Token: 0x0400002A RID: 42
		public readonly DistanceToDistrictDescriber _distanceToDistrictDescriber;

		// Token: 0x0400002B RID: 43
		public DistrictBuilding _districtBuilding;

		// Token: 0x0400002C RID: 44
		public Vector3 _access;
	}
}
