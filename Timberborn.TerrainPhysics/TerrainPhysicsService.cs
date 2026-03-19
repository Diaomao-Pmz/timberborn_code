using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000015 RID: 21
	public class TerrainPhysicsService : ITerrainPhysicsService, ILoadableSingleton
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000327D File Offset: 0x0000147D
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003285 File Offset: 0x00001485
		public ReadOnlyList<Vector3Int> PhysicsSupportDeltas { get; private set; }

		// Token: 0x0600005B RID: 91 RVA: 0x0000328E File Offset: 0x0000148E
		public TerrainPhysicsService(TerrainPhysicsValidatorFactory terrainPhysicsValidatorFactory, TerrainAndBlockObjectsToDeleteFinder terrainAndBlockObjectsToDeleteFinder)
		{
			this._terrainPhysicsValidatorFactory = terrainPhysicsValidatorFactory;
			this._terrainAndBlockObjectsToDeleteFinder = terrainAndBlockObjectsToDeleteFinder;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000032A4 File Offset: 0x000014A4
		public void Load()
		{
			this._terrainPhysicsValidator = this._terrainPhysicsValidatorFactory.CreateValidator();
			this._previewTerrainPhysicsValidator = this._terrainPhysicsValidatorFactory.CreatePreviewValidator();
			this.PhysicsSupportDeltas = TerrainPhysicsService.GetCheckAreaCoordinates().AsReadOnlyList<Vector3Int>();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000032D8 File Offset: 0x000014D8
		public void GetTerrainAndBlockObjectStack(IEnumerable<BlockObject> inputBlockObjects, HashSet<Vector3Int> outputTerrain, HashSet<BlockObject> outputBlockObjects)
		{
			this._terrainAndBlockObjectsToDeleteFinder.FindAll(inputBlockObjects, outputTerrain, outputBlockObjects);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000032E8 File Offset: 0x000014E8
		public void GetTerrainAndBlockObjectStack(IEnumerable<Vector3Int> inputTerrain, IEnumerable<BlockObject> inputBlockObjects, HashSet<Vector3Int> outputTerrain, HashSet<BlockObject> outputBlockObjects)
		{
			this._terrainAndBlockObjectsToDeleteFinder.FindAllMarkInputAsDeleted(inputTerrain, inputBlockObjects, outputTerrain, outputBlockObjects);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000032FA File Offset: 0x000014FA
		public void GetValidTerrainToAdd(ICollection<Vector3Int> inputTerrain, HashSet<Vector3Int> terrainToAdd)
		{
			this._terrainPhysicsValidator.GetValidTerrainToAdd(inputTerrain, terrainToAdd);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003309 File Offset: 0x00001509
		public bool CanBeDestroyed(BlockObject blockObject)
		{
			return this._previewTerrainPhysicsValidator.CanBeDestroyed(blockObject);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003317 File Offset: 0x00001517
		public bool ValidateBlockObjectPreview(BlockObject blockObject)
		{
			return this._previewTerrainPhysicsValidator.ValidateBlockObjectPreview(blockObject);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003325 File Offset: 0x00001525
		public bool CanTerrainBeAdded(Vector3Int coordinates)
		{
			return this._terrainPhysicsValidator.CanTerrainBeAdded(coordinates);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003334 File Offset: 0x00001534
		public static List<Vector3Int> GetCheckAreaCoordinates()
		{
			List<Vector3Int> list = new List<Vector3Int>();
			int maxSupportDistance = TerrainPhysicsValidator.MaxSupportDistance;
			for (int i = -maxSupportDistance; i <= maxSupportDistance; i++)
			{
				int num = Mathf.Abs(i);
				for (int j = -maxSupportDistance; j <= maxSupportDistance; j++)
				{
					if (Mathf.Abs(j) + num <= maxSupportDistance)
					{
						if (i != 0 || j != 0)
						{
							list.Add(new Vector3Int(j, i, 0));
						}
						list.Add(new Vector3Int(j, i, 1));
					}
				}
			}
			return list;
		}

		// Token: 0x04000031 RID: 49
		public readonly TerrainPhysicsValidatorFactory _terrainPhysicsValidatorFactory;

		// Token: 0x04000032 RID: 50
		public readonly TerrainAndBlockObjectsToDeleteFinder _terrainAndBlockObjectsToDeleteFinder;

		// Token: 0x04000033 RID: 51
		public TerrainPhysicsValidator _terrainPhysicsValidator;

		// Token: 0x04000034 RID: 52
		public TerrainPhysicsValidator _previewTerrainPhysicsValidator;
	}
}
