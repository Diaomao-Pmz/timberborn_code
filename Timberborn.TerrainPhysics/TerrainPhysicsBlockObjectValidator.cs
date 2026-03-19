using System;
using Timberborn.BlockSystem;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x0200000E RID: 14
	public class TerrainPhysicsBlockObjectValidator : IBlockObjectValidator
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002966 File Offset: 0x00000B66
		public TerrainPhysicsBlockObjectValidator(ITerrainPhysicsService terrainPhysicsService, TerrainPhysicsValidationEnabler terrainPhysicsValidationEnabler)
		{
			this._terrainPhysicsService = terrainPhysicsService;
			this._terrainPhysicsValidationEnabler = terrainPhysicsValidationEnabler;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000297C File Offset: 0x00000B7C
		public bool IsValid(BlockObject blockObject, out string errorMessage)
		{
			errorMessage = null;
			return !blockObject.HasComponent<TerrainPhysicsBlockObjectValidatorSpec>() || !this._terrainPhysicsValidationEnabler.Enabled || this._terrainPhysicsService.ValidateBlockObjectPreview(blockObject);
		}

		// Token: 0x04000019 RID: 25
		public readonly ITerrainPhysicsService _terrainPhysicsService;

		// Token: 0x0400001A RID: 26
		public readonly TerrainPhysicsValidationEnabler _terrainPhysicsValidationEnabler;
	}
}
