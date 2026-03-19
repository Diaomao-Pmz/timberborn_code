using System;
using System.Collections.Generic;
using Timberborn.Gathering;
using Timberborn.Growing;
using Timberborn.InputSystem;
using Timberborn.NaturalResources;
using UnityEngine;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000008 RID: 8
	public class DevModePlantableSpawner
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021F7 File Offset: 0x000003F7
		public DevModePlantableSpawner(InputService inputService, NaturalResourceFactory naturalResourceFactory)
		{
			this._inputService = inputService;
			this._naturalResourceFactory = naturalResourceFactory;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002210 File Offset: 0x00000410
		public void SpawnPlantables(IEnumerable<Vector3Int> blocks, string resourceId)
		{
			if (this._inputService.IsKeyHeld(DevModePlantableSpawner.PlantSpawnedKey))
			{
				foreach (Vector3Int vector3Int in blocks)
				{
					Vector3Int coordinates = vector3Int + new Vector3Int(0, 0, 1);
					NaturalResource naturalResource = this._naturalResourceFactory.SpawnIgnoringConstraints(resourceId, coordinates);
					if (naturalResource && this._inputService.IsKeyHeld(DevModePlantableSpawner.PlantGrownKey))
					{
						naturalResource.GetComponent<Growable>().IncreaseGrowthProgress(1f);
						if (this._inputService.IsKeyHeld(DevModePlantableSpawner.PlantWithYieldKey))
						{
							GatherableYieldGrower component = naturalResource.GetComponent<GatherableYieldGrower>();
							if (component != null)
							{
								component.FastForwardGrowth(1f);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400000E RID: 14
		public static readonly string PlantSpawnedKey = "PlantSpawned";

		// Token: 0x0400000F RID: 15
		public static readonly string PlantGrownKey = "PlantGrown";

		// Token: 0x04000010 RID: 16
		public static readonly string PlantWithYieldKey = "PlantWithYield";

		// Token: 0x04000011 RID: 17
		public readonly InputService _inputService;

		// Token: 0x04000012 RID: 18
		public readonly NaturalResourceFactory _naturalResourceFactory;
	}
}
