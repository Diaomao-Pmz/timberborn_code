using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.TerrainPhysics;
using Timberborn.WorldPersistence;

namespace Timberborn.BlockAndTerrainLoadValidation
{
	// Token: 0x02000004 RID: 4
	public class BlockAndTerrainBatchLoader : IEntityBatchLoader
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BlockAndTerrainBatchLoader(BlockObjectBatchLoader blockObjectBatchLoader, TerrainPhysicsPostLoader terrainPhysicsPostLoader)
		{
			this._blockObjectBatchLoader = blockObjectBatchLoader;
			this._terrainPhysicsPostLoader = terrainPhysicsPostLoader;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public void BatchLoadEntities(IEnumerable<EntityComponent> entities)
		{
			this._blockObjectBatchLoader.AddToServices(entities);
			this._terrainPhysicsPostLoader.ValidateAll();
		}

		// Token: 0x04000006 RID: 6
		public readonly BlockObjectBatchLoader _blockObjectBatchLoader;

		// Token: 0x04000007 RID: 7
		public readonly TerrainPhysicsPostLoader _terrainPhysicsPostLoader;
	}
}
