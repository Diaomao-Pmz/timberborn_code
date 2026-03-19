using System;
using System.Collections.Generic;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.TerrainPhysics;
using UnityEngine;

namespace Timberborn.DeconstructionSystem
{
	// Token: 0x0200000A RID: 10
	public class DeconstructionParticleFactory : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000021D8 File Offset: 0x000003D8
		public DeconstructionParticleFactory(EventBus eventBus, IInstantiator instantiator, IAssetLoader assetLoader, RootObjectProvider rootObjectProvider, IRandomNumberGenerator randomNumberGenerator, ISpecService specService)
		{
			this._eventBus = eventBus;
			this._instantiator = instantiator;
			this._assetLoader = assetLoader;
			this._rootObjectProvider = rootObjectProvider;
			this._randomNumberGenerator = randomNumberGenerator;
			this._specService = specService;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002230 File Offset: 0x00000430
		public void Load()
		{
			this._spec = this._specService.GetSingleSpec<DeconstructionParticleFactorySpec>();
			this._root = this._rootObjectProvider.CreateRootObject("DeconstructionParticleFactory").transform;
			this._particlePrefab = this._assetLoader.Load<GameObject>(this._spec.ParticlePrefabPath);
			this._eventBus.Register(this);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002294 File Offset: 0x00000494
		public void AddPausableParticles(HashSet<Vector3Int> particlesCoordinates)
		{
			foreach (Vector3Int coordinates in particlesCoordinates)
			{
				this.AddParticles(coordinates, false);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E4 File Offset: 0x000004E4
		[OnEvent]
		public void OnBuildingDeconstructed(BuildingDeconstructedEvent buildingDeconstructedEvent)
		{
			foreach (Vector3Int coordinates in buildingDeconstructedEvent.Coordinates)
			{
				this.AddParticles(coordinates, true);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000233C File Offset: 0x0000053C
		[OnEvent]
		public void OnTerrainDestroyed(TerrainDestroyedEvent terrainDestroyedEvent)
		{
			this.AddParticles(terrainDestroyedEvent.Coordinates, true);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000234C File Offset: 0x0000054C
		public void LateUpdateSingleton()
		{
			if (this._particlesToSpawn.Count > 0)
			{
				if (this._particlesToSpawn.Count <= this._spec.MinParticlesForThreshold)
				{
					this.SpawnAllParticles();
				}
				else
				{
					this.SpawnLimitedParticles();
				}
				this._particlesToSpawn.Clear();
				this._particlesInNeighbours.Clear();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023A4 File Offset: 0x000005A4
		public void AddParticles(Vector3Int coordinates, bool useUnscaledTime = true)
		{
			if (this._particlesToSpawn.Add(new DeconstructionParticleFactory.ParticleSpawnParameters(coordinates, useUnscaledTime)))
			{
				foreach (Vector3Int vector3Int in Deltas.Neighbors26Vector3Int)
				{
					Vector3Int key = coordinates + vector3Int;
					byte b;
					if (this._particlesInNeighbours.TryGetValue(key, out b))
					{
						this._particlesInNeighbours[key] = b + 1;
					}
					else
					{
						this._particlesInNeighbours[key] = 1;
					}
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000241C File Offset: 0x0000061C
		public void SpawnAllParticles()
		{
			foreach (DeconstructionParticleFactory.ParticleSpawnParameters spawnParameters in this._particlesToSpawn)
			{
				this.SpawnParticle(spawnParameters);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002470 File Offset: 0x00000670
		public void SpawnLimitedParticles()
		{
			float countFactor = Mathf.Clamp01((float)(this._particlesToSpawn.Count - this._spec.MinParticlesForThreshold) / (float)(this._spec.MaxParticlesForThreshold - this._spec.MinParticlesForThreshold));
			foreach (DeconstructionParticleFactory.ParticleSpawnParameters spawnParameters in this._particlesToSpawn)
			{
				if (this.ShouldSpawnParticle(spawnParameters, countFactor))
				{
					this.SpawnParticle(spawnParameters);
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002504 File Offset: 0x00000704
		public bool ShouldSpawnParticle(DeconstructionParticleFactory.ParticleSpawnParameters spawnParameters, float countFactor)
		{
			byte b;
			if (this._particlesInNeighbours.TryGetValue(spawnParameters.Coordinates, out b))
			{
				float num = Mathf.Lerp(this._spec.MinParticleSpawnThreshold, this._spec.MaxParticleSpawnThreshold, Mathf.Sqrt((float)b / this._spec.MaxNeighboursCount));
				return this._randomNumberGenerator.Range(0f, 1f) > num * countFactor;
			}
			return true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002574 File Offset: 0x00000774
		public void SpawnParticle(DeconstructionParticleFactory.ParticleSpawnParameters spawnParameters)
		{
			GameObject gameObject = this._instantiator.Instantiate(this._particlePrefab, this._root);
			gameObject.transform.position = CoordinateSystem.GridToWorld(spawnParameters.Coordinates);
			if (!spawnParameters.UseUnscaledTime)
			{
				gameObject.GetComponent<ParticleSystem>().main.useUnscaledTime = false;
			}
		}

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly IInstantiator _instantiator;

		// Token: 0x0400000D RID: 13
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000E RID: 14
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400000F RID: 15
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000010 RID: 16
		public readonly ISpecService _specService;

		// Token: 0x04000011 RID: 17
		public DeconstructionParticleFactorySpec _spec;

		// Token: 0x04000012 RID: 18
		public readonly Dictionary<Vector3Int, byte> _particlesInNeighbours = new Dictionary<Vector3Int, byte>();

		// Token: 0x04000013 RID: 19
		public readonly HashSet<DeconstructionParticleFactory.ParticleSpawnParameters> _particlesToSpawn = new HashSet<DeconstructionParticleFactory.ParticleSpawnParameters>();

		// Token: 0x04000014 RID: 20
		public GameObject _particlePrefab;

		// Token: 0x04000015 RID: 21
		public Transform _root;

		// Token: 0x0200000B RID: 11
		public readonly struct ParticleSpawnParameters : IEquatable<DeconstructionParticleFactory.ParticleSpawnParameters>
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600001C RID: 28 RVA: 0x000025CD File Offset: 0x000007CD
			public Vector3Int Coordinates { get; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600001D RID: 29 RVA: 0x000025D5 File Offset: 0x000007D5
			public bool UseUnscaledTime { get; }

			// Token: 0x0600001E RID: 30 RVA: 0x000025DD File Offset: 0x000007DD
			public ParticleSpawnParameters(Vector3Int coordinates, bool useUnscaledTime)
			{
				this.Coordinates = coordinates;
				this.UseUnscaledTime = useUnscaledTime;
			}

			// Token: 0x0600001F RID: 31 RVA: 0x000025F0 File Offset: 0x000007F0
			public bool Equals(DeconstructionParticleFactory.ParticleSpawnParameters other)
			{
				return this.Coordinates.Equals(other.Coordinates) && this.UseUnscaledTime == other.UseUnscaledTime;
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00002628 File Offset: 0x00000828
			public override bool Equals(object obj)
			{
				if (obj is DeconstructionParticleFactory.ParticleSpawnParameters)
				{
					DeconstructionParticleFactory.ParticleSpawnParameters other = (DeconstructionParticleFactory.ParticleSpawnParameters)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06000021 RID: 33 RVA: 0x0000264D File Offset: 0x0000084D
			public override int GetHashCode()
			{
				return HashCode.Combine<Vector3Int, bool>(this.Coordinates, this.UseUnscaledTime);
			}
		}
	}
}
