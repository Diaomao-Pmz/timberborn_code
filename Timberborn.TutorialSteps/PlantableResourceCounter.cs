using System;
using System.Collections.Generic;
using Timberborn.Planting;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200003C RID: 60
	public class PlantableResourceCounter : ILoadableSingleton
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x000056AA File Offset: 0x000038AA
		public PlantableResourceCounter(PlantingService plantingService, EventBus eventBus)
		{
			this._plantingService = plantingService;
			this._eventBus = eventBus;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000056CC File Offset: 0x000038CC
		public void Load()
		{
			foreach (Vector3Int coordinates in this._plantingService.PlantingCoordinates)
			{
				string resourceAt = this._plantingService.GetResourceAt(coordinates);
				if (resourceAt != null)
				{
					this.ModifyNumberOfResources(resourceAt, 1);
				}
			}
			this._eventBus.Register(this);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000573C File Offset: 0x0000393C
		[OnEvent]
		public void OnPlantingCoordinatesSet(PlantingCoordinatesSetEvent plantingCoordinatesSetEvent)
		{
			this.ModifyNumberOfResources(plantingCoordinatesSetEvent.Resource, 1);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000574B File Offset: 0x0000394B
		[OnEvent]
		public void OnPlantingCoordinatesUnset(PlantingCoordinatesUnsetEvent plantingCoordinatesUnsetEvent)
		{
			if (plantingCoordinatesUnsetEvent.Resource != null)
			{
				this.ModifyNumberOfResources(plantingCoordinatesUnsetEvent.Resource, -1);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005764 File Offset: 0x00003964
		public int GetNumberOfResources(string resource)
		{
			int result;
			if (!this._resources.TryGetValue(resource, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005784 File Offset: 0x00003984
		public void ModifyNumberOfResources(string resource, int change)
		{
			this._resources[resource] = this.GetNumberOfResources(resource) + change;
		}

		// Token: 0x040000C0 RID: 192
		public readonly PlantingService _plantingService;

		// Token: 0x040000C1 RID: 193
		public readonly EventBus _eventBus;

		// Token: 0x040000C2 RID: 194
		public readonly Dictionary<string, int> _resources = new Dictionary<string, int>();
	}
}
