using System;
using Timberborn.BlockSystem;
using Timberborn.NaturalResources;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x0200001A RID: 26
	public class PlantingAreaValidator
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003380 File Offset: 0x00001580
		public PlantingAreaValidator(IBlockService blockService, SpawnValidationService spawnValidationService)
		{
			this._blockService = blockService;
			this._spawnValidationService = spawnValidationService;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003396 File Offset: 0x00001596
		public bool CanPlant(Vector3Int coordinates, string name)
		{
			return this.IsSamePlantable(coordinates, name) || this._spawnValidationService.IsUnobstructed(coordinates, name);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000033B4 File Offset: 0x000015B4
		public bool IsSamePlantable(Vector3Int coordinates, string name)
		{
			PlantableSpec bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<PlantableSpec>(coordinates);
			return bottomObjectComponentAt != null && bottomObjectComponentAt.TemplateName == name;
		}

		// Token: 0x04000048 RID: 72
		public readonly IBlockService _blockService;

		// Token: 0x04000049 RID: 73
		public readonly SpawnValidationService _spawnValidationService;
	}
}
