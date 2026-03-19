using System;
using Timberborn.NaturalResources;
using Timberborn.SoilContaminationSystem;
using UnityEngine;

namespace Timberborn.NaturalResourcesContamination
{
	// Token: 0x02000008 RID: 8
	public class ContaminatedNaturalResourceSpawnValidator : ISpawnValidator
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023BE File Offset: 0x000005BE
		public ContaminatedNaturalResourceSpawnValidator(ISoilContaminationService soilContaminationService)
		{
			this._soilContaminationService = soilContaminationService;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023CD File Offset: 0x000005CD
		public bool CanSpawn(Vector3Int coordinates, string resourceTemplateName)
		{
			return !this._soilContaminationService.SoilIsContaminated(coordinates);
		}

		// Token: 0x04000012 RID: 18
		public readonly ISoilContaminationService _soilContaminationService;
	}
}
