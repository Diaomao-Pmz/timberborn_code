using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200000D RID: 13
	public class DistrictBuilding : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600002B RID: 43 RVA: 0x00002730 File Offset: 0x00000930
		// (remove) Token: 0x0600002C RID: 44 RVA: 0x00002768 File Offset: 0x00000968
		public event EventHandler ReassignedDistrict;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600002D RID: 45 RVA: 0x000027A0 File Offset: 0x000009A0
		// (remove) Token: 0x0600002E RID: 46 RVA: 0x000027D8 File Offset: 0x000009D8
		public event EventHandler ReassignedInstantDistrict;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600002F RID: 47 RVA: 0x00002810 File Offset: 0x00000A10
		// (remove) Token: 0x06000030 RID: 48 RVA: 0x00002848 File Offset: 0x00000A48
		public event EventHandler ReassignedConstructionDistrict;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000287D File Offset: 0x00000A7D
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002885 File Offset: 0x00000A85
		public DistrictCenter District { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000288E File Offset: 0x00000A8E
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002896 File Offset: 0x00000A96
		public DistrictCenter InstantDistrict { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000289F File Offset: 0x00000A9F
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000028A7 File Offset: 0x00000AA7
		public DistrictCenter ConstructionDistrict { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000028B0 File Offset: 0x00000AB0
		public bool IsEnabledDistrictCenter
		{
			get
			{
				return this._districtCenter && this._districtCenter.Enabled;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028CC File Offset: 0x00000ACC
		public void Awake()
		{
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
			this._districtCenter = base.GetComponent<DistrictCenter>();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028E6 File Offset: 0x00000AE6
		public void AssignDistrict(DistrictCenter district)
		{
			if (district != this.District)
			{
				this.District = district;
				EventHandler reassignedDistrict = this.ReassignedDistrict;
				if (reassignedDistrict == null)
				{
					return;
				}
				reassignedDistrict(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000290E File Offset: 0x00000B0E
		public void UnassignDistrict()
		{
			this.AssignDistrict(null);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002917 File Offset: 0x00000B17
		public void AssignInstantDistrict(DistrictCenter district)
		{
			if (district != this.InstantDistrict)
			{
				this.InstantDistrict = district;
				EventHandler reassignedInstantDistrict = this.ReassignedInstantDistrict;
				if (reassignedInstantDistrict == null)
				{
					return;
				}
				reassignedInstantDistrict(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000293F File Offset: 0x00000B3F
		public void UnassignInstantDistrict()
		{
			this.AssignInstantDistrict(null);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002948 File Offset: 0x00000B48
		public void AssignConstructionDistrict(DistrictCenter district)
		{
			if (district != this.ConstructionDistrict)
			{
				this.ConstructionDistrict = district;
				EventHandler reassignedConstructionDistrict = this.ReassignedConstructionDistrict;
				if (reassignedConstructionDistrict == null)
				{
					return;
				}
				reassignedConstructionDistrict(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002970 File Offset: 0x00000B70
		public void UnassignConstructionDistrict()
		{
			this.AssignConstructionDistrict(null);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002979 File Offset: 0x00000B79
		public bool ShouldBeAssignedToDistrict(DistrictCenter district)
		{
			return (this.IsEnabledDistrictCenter && this._districtCenter == district) || district.AccessibleIsOnDistrictRoad(this._buildingAccessible.Accessible);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000299F File Offset: 0x00000B9F
		public bool ShouldBeAssignedToInstantDistrict(DistrictCenter district)
		{
			return (this.IsEnabledDistrictCenter && this._districtCenter == district) || district.AccessibleIsOnInstantDistrictRoad(this._buildingAccessible.Accessible);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029C5 File Offset: 0x00000BC5
		public bool ShouldBeAssignedToConstructionDistrict(DistrictCenter district)
		{
			return district.IsOnInstantDistrictRoad(this._buildingAccessible.CalculateAccess());
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029D8 File Offset: 0x00000BD8
		public DistrictCenter GetInstantOrConstructionDistrict()
		{
			if (!this.InstantDistrict)
			{
				return this.ConstructionDistrict;
			}
			return this.InstantDistrict;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029F4 File Offset: 0x00000BF4
		public DistrictCenter GetDistrictOrConstructionDistrict()
		{
			if (!this.District)
			{
				return this.ConstructionDistrict;
			}
			return this.District;
		}

		// Token: 0x04000022 RID: 34
		public BuildingAccessible _buildingAccessible;

		// Token: 0x04000023 RID: 35
		public DistrictCenter _districtCenter;
	}
}
