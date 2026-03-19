using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200000E RID: 14
	public class DistrictBuildingAssigner : ILoadableSingleton, ISingletonNavMeshListener, ISingletonInstantNavMeshListener
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002A18 File Offset: 0x00000C18
		public DistrictBuildingAssigner(EventBus eventBus, DistrictCenterRegistry districtCenterRegistry)
		{
			this._eventBus = eventBus;
			this._districtCenterRegistry = districtCenterRegistry;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A4F File Offset: 0x00000C4F
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A60 File Offset: 0x00000C60
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			DistrictBuilding districtBuilding;
			if (enteredFinishedStateEvent.BlockObject.TryGetComponent<DistrictBuilding>(out districtBuilding))
			{
				this.RegisterBuilding(districtBuilding);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A84 File Offset: 0x00000C84
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			DistrictBuilding districtBuilding;
			if (exitedFinishedStateEvent.BlockObject.TryGetComponent<DistrictBuilding>(out districtBuilding))
			{
				this.UnregisterBuilding(districtBuilding);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002AA7 File Offset: 0x00000CA7
		[OnEvent]
		public void OnDistrictCenterRegistryChanged(DistrictCenterRegistryChangedEvent districtCenterRegistryChangedEvent)
		{
			this.ReassignAllBuildings();
			this.ReassignAllInstantBuildings();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AB5 File Offset: 0x00000CB5
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			if (navMeshUpdate.UpdatedRoads)
			{
				this.ReassignAllBuildings();
			}
			else
			{
				this.AssignNewBuildings();
			}
			this._newBuildingsToAssign.Clear();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			if (navMeshUpdate.UpdatedRoads)
			{
				this.ReassignAllInstantBuildings();
			}
			else
			{
				this.AssignNewInstantBuildings();
			}
			this._newInstantBuildingsToAssign.Clear();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B00 File Offset: 0x00000D00
		public void RegisterBuilding(DistrictBuilding districtBuilding)
		{
			this._allBuildings.Add(districtBuilding);
			this.AssignDistrict(districtBuilding);
			this.AssignInstantDistrict(districtBuilding);
			if (!districtBuilding.District)
			{
				this._newBuildingsToAssign.Add(districtBuilding);
			}
			if (!districtBuilding.InstantDistrict)
			{
				this._newInstantBuildingsToAssign.Add(districtBuilding);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B5C File Offset: 0x00000D5C
		public void UnregisterBuilding(DistrictBuilding districtBuilding)
		{
			this._allBuildings.Remove(districtBuilding);
			EntityComponent component = districtBuilding.GetComponent<EntityComponent>();
			DistrictCenter district = districtBuilding.District;
			if (district)
			{
				district.DistrictBuildingRegistry.UnregisterFinishedBuilding(component);
			}
			districtBuilding.UnassignDistrict();
			DistrictCenter instantDistrict = districtBuilding.InstantDistrict;
			if (instantDistrict)
			{
				instantDistrict.DistrictBuildingRegistry.UnregisterInstantFinishedBuilding(component);
			}
			districtBuilding.UnassignInstantDistrict();
			this._newBuildingsToAssign.Remove(districtBuilding);
			this._newInstantBuildingsToAssign.Remove(districtBuilding);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void ReassignAllBuildings()
		{
			foreach (DistrictBuilding districtBuilding in this._allBuildings)
			{
				DistrictCenter district = districtBuilding.District;
				if (district && !districtBuilding.ShouldBeAssignedToDistrict(district))
				{
					districtBuilding.UnassignDistrict();
					district.DistrictBuildingRegistry.UnregisterFinishedBuilding(districtBuilding.GetComponent<EntityComponent>());
				}
				if (!districtBuilding.District)
				{
					this.AssignDistrict(districtBuilding);
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C6C File Offset: 0x00000E6C
		public void AssignNewBuildings()
		{
			if (!this._newBuildingsToAssign.IsEmpty<DistrictBuilding>())
			{
				foreach (DistrictBuilding building in this._newBuildingsToAssign)
				{
					this.AssignDistrict(building);
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CCC File Offset: 0x00000ECC
		public void AssignDistrict(DistrictBuilding building)
		{
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				if (building.ShouldBeAssignedToDistrict(districtCenter))
				{
					building.AssignDistrict(districtCenter);
					districtCenter.DistrictBuildingRegistry.RegisterFinishedBuilding(building.GetComponent<EntityComponent>());
					return;
				}
			}
			building.UnassignDistrict();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D4C File Offset: 0x00000F4C
		public void ReassignAllInstantBuildings()
		{
			foreach (DistrictBuilding districtBuilding in this._allBuildings)
			{
				DistrictCenter instantDistrict = districtBuilding.InstantDistrict;
				if (instantDistrict && !districtBuilding.ShouldBeAssignedToInstantDistrict(instantDistrict))
				{
					instantDistrict.DistrictBuildingRegistry.UnregisterInstantFinishedBuilding(districtBuilding.GetComponent<EntityComponent>());
					districtBuilding.UnassignInstantDistrict();
				}
				if (!districtBuilding.InstantDistrict)
				{
					this.AssignInstantDistrict(districtBuilding);
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DDC File Offset: 0x00000FDC
		public void AssignNewInstantBuildings()
		{
			if (!this._newInstantBuildingsToAssign.IsEmpty<DistrictBuilding>())
			{
				foreach (DistrictBuilding building in this._newInstantBuildingsToAssign)
				{
					this.AssignInstantDistrict(building);
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E3C File Offset: 0x0000103C
		public void AssignInstantDistrict(DistrictBuilding building)
		{
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				if (building.ShouldBeAssignedToInstantDistrict(districtCenter))
				{
					building.AssignInstantDistrict(districtCenter);
					districtCenter.DistrictBuildingRegistry.RegisterInstantFinishedBuilding(building.GetComponent<EntityComponent>());
					return;
				}
			}
			building.UnassignInstantDistrict();
		}

		// Token: 0x04000024 RID: 36
		public readonly EventBus _eventBus;

		// Token: 0x04000025 RID: 37
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000026 RID: 38
		public readonly List<DistrictBuilding> _allBuildings = new List<DistrictBuilding>();

		// Token: 0x04000027 RID: 39
		public readonly List<DistrictBuilding> _newBuildingsToAssign = new List<DistrictBuilding>();

		// Token: 0x04000028 RID: 40
		public readonly List<DistrictBuilding> _newInstantBuildingsToAssign = new List<DistrictBuilding>();
	}
}
