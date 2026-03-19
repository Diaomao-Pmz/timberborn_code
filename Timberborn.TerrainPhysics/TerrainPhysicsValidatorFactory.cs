using System;
using Timberborn.BlockSystem;
using Timberborn.TerrainSystem;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000019 RID: 25
	public class TerrainPhysicsValidatorFactory
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003E0D File Offset: 0x0000200D
		public TerrainPhysicsValidatorFactory(ITerrainService terrainService, StackableBlockService stackableBlockService, PreviewBlockService previewBlockService, SupportsToBeDeleted supportsToBeDeleted)
		{
			this._terrainService = terrainService;
			this._stackableBlockService = stackableBlockService;
			this._previewBlockService = previewBlockService;
			this._supportsToBeDeleted = supportsToBeDeleted;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003E32 File Offset: 0x00002032
		public TerrainPhysicsValidator CreateValidator()
		{
			TerrainPhysicsValidator terrainPhysicsValidator = new TerrainPhysicsValidator(this._terrainService, this._stackableBlockService, this._previewBlockService, this._supportsToBeDeleted, false);
			terrainPhysicsValidator.Initialize();
			return terrainPhysicsValidator;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003E58 File Offset: 0x00002058
		public TerrainPhysicsValidator CreatePreviewValidator()
		{
			TerrainPhysicsValidator terrainPhysicsValidator = new TerrainPhysicsValidator(this._terrainService, this._stackableBlockService, this._previewBlockService, this._supportsToBeDeleted, true);
			terrainPhysicsValidator.Initialize();
			return terrainPhysicsValidator;
		}

		// Token: 0x0400004C RID: 76
		public readonly ITerrainService _terrainService;

		// Token: 0x0400004D RID: 77
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x0400004E RID: 78
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400004F RID: 79
		public readonly SupportsToBeDeleted _supportsToBeDeleted;
	}
}
