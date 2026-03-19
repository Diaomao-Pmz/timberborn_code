using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200001D RID: 29
	public class DistrictConstructionAssigner : ILoadableSingleton, ISingletonInstantNavMeshListener
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00003EB0 File Offset: 0x000020B0
		public DistrictConstructionAssigner(EventBus eventBus, DistrictCenterRegistry districtCenterRegistry)
		{
			this._eventBus = eventBus;
			this._districtCenterRegistry = districtCenterRegistry;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003EDC File Offset: 0x000020DC
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003EEC File Offset: 0x000020EC
		[OnEvent]
		public void OnEnteredUnfinishedState(EnteredUnfinishedStateEvent enteredUnfinishedStateEvent)
		{
			DistrictBuilding districtBuilding;
			if (enteredUnfinishedStateEvent.BlockObject.TryGetComponent<DistrictBuilding>(out districtBuilding))
			{
				this.RegisterConstruction(districtBuilding);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003F10 File Offset: 0x00002110
		[OnEvent]
		public void OnExitedUnfinishedState(ExitedUnfinishedStateEvent exitedUnfinishedStateEvent)
		{
			DistrictBuilding districtBuilding;
			if (exitedUnfinishedStateEvent.BlockObject.TryGetComponent<DistrictBuilding>(out districtBuilding))
			{
				this.UnregisterConstruction(districtBuilding);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003F33 File Offset: 0x00002133
		[OnEvent]
		public void OnDistrictCenterRegistryChanged(DistrictCenterRegistryChangedEvent districtCenterRegistryChangedEvent)
		{
			this.ReassignAllConstructions();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003F3B File Offset: 0x0000213B
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			if (navMeshUpdate.UpdatedRoads)
			{
				this.ReassignAllConstructions();
			}
			else
			{
				this.AssignNewConstructions();
			}
			this._newConstructionsToAssign.Clear();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003F5F File Offset: 0x0000215F
		public void RegisterConstruction(DistrictBuilding districtBuilding)
		{
			this._constructions.Add(districtBuilding);
			this.AssignConstructionDistrict(districtBuilding);
			if (!districtBuilding.ConstructionDistrict)
			{
				this._newConstructionsToAssign.Add(districtBuilding);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003F8D File Offset: 0x0000218D
		public void UnregisterConstruction(DistrictBuilding districtBuilding)
		{
			this._constructions.Remove(districtBuilding);
			districtBuilding.UnassignConstructionDistrict();
			this._newConstructionsToAssign.Remove(districtBuilding);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003FB0 File Offset: 0x000021B0
		public void ReassignAllConstructions()
		{
			foreach (DistrictBuilding districtBuilding in this._constructions)
			{
				DistrictCenter constructionDistrict = districtBuilding.ConstructionDistrict;
				if (constructionDistrict && !districtBuilding.ShouldBeAssignedToConstructionDistrict(constructionDistrict))
				{
					districtBuilding.UnassignConstructionDistrict();
				}
				if (!districtBuilding.ConstructionDistrict)
				{
					this.AssignConstructionDistrict(districtBuilding);
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004030 File Offset: 0x00002230
		public void AssignNewConstructions()
		{
			if (!this._newConstructionsToAssign.IsEmpty<DistrictBuilding>())
			{
				foreach (DistrictBuilding building in this._newConstructionsToAssign)
				{
					this.AssignConstructionDistrict(building);
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004090 File Offset: 0x00002290
		public void AssignConstructionDistrict(DistrictBuilding building)
		{
			foreach (DistrictCenter district in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				if (building.ShouldBeAssignedToConstructionDistrict(district))
				{
					building.AssignConstructionDistrict(district);
					return;
				}
			}
			building.UnassignConstructionDistrict();
		}

		// Token: 0x04000052 RID: 82
		public readonly EventBus _eventBus;

		// Token: 0x04000053 RID: 83
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000054 RID: 84
		public readonly List<DistrictBuilding> _constructions = new List<DistrictBuilding>();

		// Token: 0x04000055 RID: 85
		public readonly List<DistrictBuilding> _newConstructionsToAssign = new List<DistrictBuilding>();
	}
}
