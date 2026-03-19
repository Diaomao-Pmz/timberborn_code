using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using Timberborn.WorkSystem;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000016 RID: 22
	public class PlanterBuildingStatusUpdater : BaseComponent, IAwakableComponent, IFinishedStateListener, INavMeshListener
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002C98 File Offset: 0x00000E98
		public PlanterBuildingStatusUpdater(EventBus eventBus, PlantingService plantingService, INavMeshListenerEntityRegistry navMeshListenerEntityRegistry)
		{
			this._eventBus = eventBus;
			this._plantingService = plantingService;
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public void Awake()
		{
			this._nothingToDoInRangeStatus = base.GetComponent<NothingToDoInRangeStatus>();
			this._planterBuilding = base.GetComponent<PlanterBuilding>();
			this._buildingTerrainRange = base.GetComponent<BuildingTerrainRange>();
			this._workplace = base.GetComponent<Workplace>();
			this._workplace.WorkerAssigned += delegate(object _, WorkerChangedEventArgs _)
			{
				this.OnWorkerAssigned();
			};
			base.DisableComponent();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002D12 File Offset: 0x00000F12
		public void DeactivateStatus()
		{
			this._nothingToDoInRangeStatus.DeactivateStatus();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002D1F File Offset: 0x00000F1F
		public void UpdateStatus()
		{
			if (this._shouldUpdateStatus)
			{
				this._shouldUpdateStatus = false;
				this.UpdateStatusInternal();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D36 File Offset: 0x00000F36
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._eventBus.Register(this);
			this._navMeshListenerEntityRegistry.RegisterNavMeshListener(this);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002D56 File Offset: 0x00000F56
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._eventBus.Unregister(this);
			this._navMeshListenerEntityRegistry.UnregisterNavMeshListener(this);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D76 File Offset: 0x00000F76
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._shouldUpdateStatus = true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D7F File Offset: 0x00000F7F
		[OnEvent]
		public void OnPlantingAreaMarked(PlantingAreaMarkedEvent plantingAreaMarkedEvent)
		{
			this.UpdateStatusInternal();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D76 File Offset: 0x00000F76
		[OnEvent]
		public void OnPlantingCoordinatesUnset(PlantingCoordinatesUnsetEvent plantingCoordinatesUnsetEvent)
		{
			this._shouldUpdateStatus = true;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002D87 File Offset: 0x00000F87
		public void OnWorkerAssigned()
		{
			if (this._workplace.NumberOfAssignedWorkers == 1)
			{
				this._shouldUpdateStatus = true;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002D9E File Offset: 0x00000F9E
		public void UpdateStatusInternal()
		{
			if (this.HasValidSpot())
			{
				this._nothingToDoInRangeStatus.DeactivateStatus();
				return;
			}
			this._nothingToDoInRangeStatus.ActivateStatus();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public bool HasValidSpot()
		{
			foreach (Vector3Int coords in this._buildingTerrainRange.GetRange())
			{
				if (this.IsValidSpot(coords))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002E24 File Offset: 0x00001024
		public bool IsValidSpot(Vector3Int coords)
		{
			string resourceAt = this._plantingService.GetResourceAt(coords);
			return resourceAt != null && this._planterBuilding.CanPlant(resourceAt);
		}

		// Token: 0x0400002B RID: 43
		public readonly EventBus _eventBus;

		// Token: 0x0400002C RID: 44
		public readonly PlantingService _plantingService;

		// Token: 0x0400002D RID: 45
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;

		// Token: 0x0400002E RID: 46
		public NothingToDoInRangeStatus _nothingToDoInRangeStatus;

		// Token: 0x0400002F RID: 47
		public PlanterBuilding _planterBuilding;

		// Token: 0x04000030 RID: 48
		public BuildingTerrainRange _buildingTerrainRange;

		// Token: 0x04000031 RID: 49
		public Workplace _workplace;

		// Token: 0x04000032 RID: 50
		public bool _shouldUpdateStatus;
	}
}
