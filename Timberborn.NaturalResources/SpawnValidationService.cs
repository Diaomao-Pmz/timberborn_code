using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.SoilMoistureSystem;
using Timberborn.TemplateSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.NaturalResources
{
	// Token: 0x0200000F RID: 15
	public class SpawnValidationService
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002619 File Offset: 0x00000819
		public SpawnValidationService(ITerrainService terrainService, BlockValidator blockValidator, ISoilMoistureService soilMoistureService, TemplateNameMapper templateNameMapper, IEnumerable<ISpawnValidator> spawnValidators)
		{
			this._terrainService = terrainService;
			this._blockValidator = blockValidator;
			this._soilMoistureService = soilMoistureService;
			this._templateNameMapper = templateNameMapper;
			this._spawnValidators = spawnValidators.ToImmutableArray<ISpawnValidator>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000264B File Offset: 0x0000084B
		public bool CanSpawnIgnoringConstraints(Vector3Int coordinates, BlockObjectSpec blockObjectSpec)
		{
			return this._terrainService.OnGround(coordinates) && this.IsUnobstructed(coordinates, blockObjectSpec);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002665 File Offset: 0x00000865
		public bool CanSpawn(Vector3Int coordinates, BlockObjectSpec blockObjectSpec, string resourceId)
		{
			return this.IsSuitableTerrain(coordinates) && this.SpotIsValid(coordinates, resourceId) && this.IsUnobstructed(coordinates, blockObjectSpec);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002684 File Offset: 0x00000884
		public bool IsUnobstructed(Vector3Int coordinates, string resourceId)
		{
			BlockObjectSpec spec = this._templateNameMapper.GetTemplate(resourceId).GetSpec<BlockObjectSpec>();
			return this.IsUnobstructed(coordinates, spec);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026AB File Offset: 0x000008AB
		public bool IsSuitableTerrain(Vector3Int coordinates)
		{
			return this._terrainService.OnGround(coordinates) && this._soilMoistureService.SoilIsMoist(coordinates) && !this._terrainService.CellIsField(coordinates);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026DC File Offset: 0x000008DC
		public bool SpotIsValid(Vector3Int coordinates, string resourceId)
		{
			for (int i = 0; i < this._spawnValidators.Length; i++)
			{
				if (!this._spawnValidators[i].CanSpawn(coordinates, resourceId))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002717 File Offset: 0x00000917
		public bool IsUnobstructed(Vector3Int coordinates, BlockObjectSpec blockObjectSpec)
		{
			return this._blockValidator.BlocksValid(blockObjectSpec, new Placement(coordinates));
		}

		// Token: 0x04000016 RID: 22
		public readonly ITerrainService _terrainService;

		// Token: 0x04000017 RID: 23
		public readonly BlockValidator _blockValidator;

		// Token: 0x04000018 RID: 24
		public readonly ISoilMoistureService _soilMoistureService;

		// Token: 0x04000019 RID: 25
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x0400001A RID: 26
		public readonly ImmutableArray<ISpawnValidator> _spawnValidators;
	}
}
