using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000007 RID: 7
	public class InRangePlantingCoordinates : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public InRangePlantingCoordinates(EventBus eventBus, PlantingService plantingService)
		{
			this._eventBus = eventBus;
			this._plantingService = plantingService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002121 File Offset: 0x00000321
		public void Awake()
		{
			this._buildingTerrainRange = base.GetComponent<BuildingTerrainRange>();
			this._planterBuilding = base.GetComponent<PlanterBuilding>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213B File Offset: 0x0000033B
		public void OnEnterFinishedState()
		{
			this._buildingTerrainRange.RangeChanged += this.OnRangeChanged;
			this._eventBus.Register(this);
			this._dirty = true;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002167 File Offset: 0x00000367
		public void OnExitFinishedState()
		{
			this._buildingTerrainRange.RangeChanged -= this.OnRangeChanged;
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000218C File Offset: 0x0000038C
		[OnEvent]
		public void OnPlantingCoordinatesSet(PlantingCoordinatesSetEvent plantingCoordinatesSetEvent)
		{
			Vector3Int coordinates = plantingCoordinatesSetEvent.Coordinates;
			string resource = plantingCoordinatesSetEvent.Resource;
			if (this._buildingTerrainRange.GetRange().Contains(coordinates) && this.IsAllowed(resource))
			{
				this._coordinatesInRange.Add(coordinates);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D3 File Offset: 0x000003D3
		[OnEvent]
		public void OnPlantingCoordinatesUnset(PlantingCoordinatesUnsetEvent plantingCoordinatesUnsetEvent)
		{
			this._coordinatesInRange.Remove(plantingCoordinatesUnsetEvent.Coordinates);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E7 File Offset: 0x000003E7
		public ReadOnlyHashSet<Vector3Int> GetCoordinates()
		{
			this.UpdateCoordinates();
			return this._coordinatesInRange.AsReadOnlyHashSet<Vector3Int>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021FA File Offset: 0x000003FA
		public bool AreCoordinatesInRange(Vector3Int coordinates)
		{
			this.UpdateCoordinates();
			return this._coordinatesInRange.Contains(coordinates);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000220E File Offset: 0x0000040E
		public void OnRangeChanged(object sender, RangeChangedEventArgs rangeChangedEventArgs)
		{
			this._dirty = true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002218 File Offset: 0x00000418
		public void UpdateCoordinates()
		{
			if (this._dirty)
			{
				this._coordinatesInRange.Clear();
				foreach (Vector3Int vector3Int in this._buildingTerrainRange.GetRange())
				{
					PlantingSpot? spotAt = this._plantingService.GetSpotAt(vector3Int);
					if (spotAt != null && this.IsAllowed(spotAt.GetValueOrDefault().ResourceToPlant))
					{
						this._coordinatesInRange.Add(vector3Int);
					}
				}
				this._dirty = false;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C4 File Offset: 0x000004C4
		public bool IsAllowed(string resource)
		{
			return this._planterBuilding.CanPlant(resource);
		}

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly PlantingService _plantingService;

		// Token: 0x0400000A RID: 10
		public BuildingTerrainRange _buildingTerrainRange;

		// Token: 0x0400000B RID: 11
		public PlanterBuilding _planterBuilding;

		// Token: 0x0400000C RID: 12
		public readonly HashSet<Vector3Int> _coordinatesInRange = new HashSet<Vector3Int>();

		// Token: 0x0400000D RID: 13
		public bool _dirty;
	}
}
