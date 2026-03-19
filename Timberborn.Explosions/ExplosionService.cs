using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DeconstructionSystem;
using Timberborn.EntitySystem;
using Timberborn.MapEditorTickSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TickSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x02000010 RID: 16
	[MapEditorTickable]
	public class ExplosionService : ILoadableSingleton, ISaveableSingleton, ITickableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000057 RID: 87 RVA: 0x00003218 File Offset: 0x00001418
		// (remove) Token: 0x06000058 RID: 88 RVA: 0x00003250 File Offset: 0x00001450
		public event EventHandler<ReadOnlyHashSet<Vector3Int>> TilesExplosion;

		// Token: 0x06000059 RID: 89 RVA: 0x00003288 File Offset: 0x00001488
		public ExplosionService(ISingletonLoader singletonLoader, ExplosionDataValueSerializer explosionDataValueSerializer, ExplosionOutcomeGatherer explosionOutcomeGatherer, ITerrainService terrainService, EntityService entityService, CharacterExploder characterExploder, DeconstructionParticleFactory deconstructionParticleFactory)
		{
			this._singletonLoader = singletonLoader;
			this._explosionDataValueSerializer = explosionDataValueSerializer;
			this._explosionOutcomeGatherer = explosionOutcomeGatherer;
			this._terrainService = terrainService;
			this._entityService = entityService;
			this._characterExploder = characterExploder;
			this._deconstructionParticleFactory = deconstructionParticleFactory;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000032FC File Offset: 0x000014FC
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(ExplosionService.ExplosionServiceKey, out objectLoader))
			{
				foreach (ExplosionData explosionData in objectLoader.Get<ExplosionData>(ExplosionService.ExplosionsKey, this._explosionDataValueSerializer))
				{
					explosionData.InitializeAffectedTiles(this._explosionOutcomeGatherer);
					this._explosions.Add(explosionData);
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003380 File Offset: 0x00001580
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this._explosions.Count > 0)
			{
				singletonSaver.GetSingleton(ExplosionService.ExplosionServiceKey).Set<ExplosionData>(ExplosionService.ExplosionsKey, this._explosions, this._explosionDataValueSerializer);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000033B4 File Offset: 0x000015B4
		public void Tick()
		{
			for (int i = this._explosions.Count - 1; i >= 0; i--)
			{
				ExplosionData explosionData = this._explosions[i];
				ReadOnlyHashSet<Vector3Int> readOnlyHashSet;
				if (explosionData.TryGetExplosionOutcomeForCurrentRadius(out readOnlyHashSet))
				{
					this.ProcessAffectedTiles(readOnlyHashSet);
					EventHandler<ReadOnlyHashSet<Vector3Int>> tilesExplosion = this.TilesExplosion;
					if (tilesExplosion != null)
					{
						tilesExplosion(this, readOnlyHashSet);
					}
					if (!explosionData.MoveToNextRadius())
					{
						this._explosions.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000341E File Offset: 0x0000161E
		public void Register(UnstableCore unstableCore)
		{
			this._registeredCores.Remove(unstableCore);
			this._registeredCores.Add(unstableCore, this.GenerateExplosionData(unstableCore));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003440 File Offset: 0x00001640
		public void Explode(UnstableCore unstableCore)
		{
			ExplosionData item;
			if (this._registeredCores.TryGetValue(unstableCore, out item))
			{
				this._explosions.Add(item);
				this._registeredCores.Remove(unstableCore);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003478 File Offset: 0x00001678
		public void ProcessAffectedTiles(ReadOnlyHashSet<Vector3Int> affectedTiles)
		{
			this._explosionOutcomeGatherer.GetAffectedTerrainAndObjects(affectedTiles, this._terrainTiles, this._blockObjects);
			this._characterExploder.ExplodeCharactersAt(affectedTiles, null);
			this.DestroyEverythingAffected();
			this._terrainTiles.Clear();
			this._blockObjects.Clear();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000034C6 File Offset: 0x000016C6
		public ExplosionData GenerateExplosionData(UnstableCore unstableCore)
		{
			ExplosionData explosionData = new ExplosionData((float)unstableCore.ExplosionRadius + unstableCore.InnerRadius, unstableCore.ExplosionCenter, 0);
			explosionData.InitializeAffectedTiles(this._explosionOutcomeGatherer);
			return explosionData;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000034F0 File Offset: 0x000016F0
		public void DestroyEverythingAffected()
		{
			this._terrainService.UnsetTerrain(this._terrainTiles);
			foreach (BlockObject entity in this._blockObjects)
			{
				this._entityService.Delete(entity);
			}
			this._deconstructionParticleFactory.AddPausableParticles(this._terrainTiles);
		}

		// Token: 0x0400003B RID: 59
		public static readonly SingletonKey ExplosionServiceKey = new SingletonKey("ExplosionService");

		// Token: 0x0400003C RID: 60
		public static readonly ListKey<ExplosionData> ExplosionsKey = new ListKey<ExplosionData>("Explosions");

		// Token: 0x0400003E RID: 62
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400003F RID: 63
		public readonly ExplosionDataValueSerializer _explosionDataValueSerializer;

		// Token: 0x04000040 RID: 64
		public readonly ExplosionOutcomeGatherer _explosionOutcomeGatherer;

		// Token: 0x04000041 RID: 65
		public readonly ITerrainService _terrainService;

		// Token: 0x04000042 RID: 66
		public readonly EntityService _entityService;

		// Token: 0x04000043 RID: 67
		public readonly CharacterExploder _characterExploder;

		// Token: 0x04000044 RID: 68
		public readonly DeconstructionParticleFactory _deconstructionParticleFactory;

		// Token: 0x04000045 RID: 69
		public readonly Dictionary<UnstableCore, ExplosionData> _registeredCores = new Dictionary<UnstableCore, ExplosionData>();

		// Token: 0x04000046 RID: 70
		public readonly List<ExplosionData> _explosions = new List<ExplosionData>();

		// Token: 0x04000047 RID: 71
		public readonly HashSet<Vector3Int> _terrainTiles = new HashSet<Vector3Int>();

		// Token: 0x04000048 RID: 72
		public readonly HashSet<BlockObject> _blockObjects = new HashSet<BlockObject>();
	}
}
