using System;
using Timberborn.SoilContaminationSystem;
using Timberborn.SoilMoistureSystem;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000025 RID: 37
	public class PlantingSoilValidator
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00004231 File Offset: 0x00002431
		public PlantingSoilValidator(ISoilMoistureService soilMoistureService, ISoilContaminationService soilContaminationService)
		{
			this._soilMoistureService = soilMoistureService;
			this._soilContaminationService = soilContaminationService;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004248 File Offset: 0x00002448
		public bool Validate(PlantingSpot plantingSpot)
		{
			Vector3Int coordinates = plantingSpot.Coordinates;
			return this._soilMoistureService.SoilIsMoist(coordinates) && !this._soilContaminationService.SoilIsContaminated(coordinates);
		}

		// Token: 0x04000075 RID: 117
		public readonly ISoilMoistureService _soilMoistureService;

		// Token: 0x04000076 RID: 118
		public readonly ISoilContaminationService _soilContaminationService;
	}
}
