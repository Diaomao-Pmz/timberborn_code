using System;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x0200000C RID: 12
	public class TerrainDestroyer
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002809 File Offset: 0x00000A09
		public TerrainDestroyer(ITerrainService terrainService, EventBus eventBus)
		{
			this._terrainService = terrainService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000281F File Offset: 0x00000A1F
		public void DestroyTerrain(Vector3Int coordinates)
		{
			this._terrainService.UnsetTerrain(coordinates, 1);
			this._eventBus.Post(new TerrainDestroyedEvent(coordinates));
		}

		// Token: 0x04000015 RID: 21
		public readonly ITerrainService _terrainService;

		// Token: 0x04000016 RID: 22
		public readonly EventBus _eventBus;
	}
}
