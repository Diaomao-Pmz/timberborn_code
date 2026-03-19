using System;
using Timberborn.NaturalResources;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.NaturalResourcesMoisture
{
	// Token: 0x0200000D RID: 13
	public class WateredNaturalResourceSpawnValidator : ISpawnValidator
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002D03 File Offset: 0x00000F03
		public WateredNaturalResourceSpawnValidator(FloodableNaturalResourceService floodableNaturalResourceService, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._floodableNaturalResourceService = floodableNaturalResourceService;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D19 File Offset: 0x00000F19
		public bool CanSpawn(Vector3Int coordinates, string resourceTemplateName)
		{
			if (this._floodableNaturalResourceService.IsFloodableNaturalResource(resourceTemplateName))
			{
				return this._floodableNaturalResourceService.ConditionsAreMet(resourceTemplateName, coordinates);
			}
			return !this._threadSafeWaterMap.CellIsUnderwater(coordinates);
		}

		// Token: 0x04000028 RID: 40
		public readonly FloodableNaturalResourceService _floodableNaturalResourceService;

		// Token: 0x04000029 RID: 41
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;
	}
}
