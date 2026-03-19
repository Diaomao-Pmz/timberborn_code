using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000016 RID: 22
	public class TerrainPhysicsUpdater : ITickableSingleton, IPostLoadableSingleton
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000033A4 File Offset: 0x000015A4
		public TerrainPhysicsUpdater(ITerrainService terrainService, EntityService entityService, TerrainDestroyer terrainDestroyer, TerrainAndBlockObjectsToDeleteFinder terrainAndBlockObjectsToDeleteFinder)
		{
			this._terrainService = terrainService;
			this._entityService = entityService;
			this._terrainDestroyer = terrainDestroyer;
			this._terrainAndBlockObjectsToDeleteFinder = terrainAndBlockObjectsToDeleteFinder;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033F5 File Offset: 0x000015F5
		public void PostLoad()
		{
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003410 File Offset: 0x00001610
		public void Tick()
		{
			if (!this._terrainToCheck.IsEmpty<Vector3Int>())
			{
				this._terrainAndBlockObjectsToDeleteFinder.FindAll(this._terrainToCheck, this._terrainQueuedForDestruction, this._blockObjectsToDelete);
				this._terrainToCheck.Clear();
				foreach (Vector3Int coordinates in this._terrainQueuedForDestruction)
				{
					this._terrainDestroyer.DestroyTerrain(coordinates);
				}
				foreach (BlockObject entity in this._blockObjectsToDelete)
				{
					this._entityService.Delete(entity);
				}
				this._terrainQueuedForDestruction.Clear();
				this._blockObjectsToDelete.Clear();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003500 File Offset: 0x00001700
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs preTerrainColumnChangedEventArgs)
		{
			TerrainHeightChange change = preTerrainColumnChangedEventArgs.Change;
			if (!change.SetTerrain)
			{
				for (int i = change.From; i <= change.To; i++)
				{
					Vector3Int item = change.Coordinates.ToVector3Int(i);
					this._terrainToCheck.Enqueue(item);
				}
			}
		}

		// Token: 0x04000035 RID: 53
		public readonly ITerrainService _terrainService;

		// Token: 0x04000036 RID: 54
		public readonly EntityService _entityService;

		// Token: 0x04000037 RID: 55
		public readonly TerrainDestroyer _terrainDestroyer;

		// Token: 0x04000038 RID: 56
		public readonly TerrainAndBlockObjectsToDeleteFinder _terrainAndBlockObjectsToDeleteFinder;

		// Token: 0x04000039 RID: 57
		public readonly HashSet<Vector3Int> _terrainQueuedForDestruction = new HashSet<Vector3Int>();

		// Token: 0x0400003A RID: 58
		public readonly HashSet<BlockObject> _blockObjectsToDelete = new HashSet<BlockObject>();

		// Token: 0x0400003B RID: 59
		public readonly Queue<Vector3Int> _terrainToCheck = new Queue<Vector3Int>();
	}
}
