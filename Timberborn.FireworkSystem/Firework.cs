using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using Timberborn.Persistence;
using Timberborn.SoundSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.FireworkSystem
{
	// Token: 0x02000007 RID: 7
	public class Firework : BaseComponent, IAwakableComponent, IPostInitializableEntity, IUpdatableComponent, IPersistentEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public Firework(EntityService entityService, ISoundSystem soundSystem, IDayNightCycle dayNightCycle)
		{
			this._entityService = entityService;
			this._soundSystem = soundSystem;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211D File Offset: 0x0000031D
		public void Awake()
		{
			this._spec = base.GetComponent<FireworkSpec>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000212B File Offset: 0x0000032B
		public void PostInitializeEntity()
		{
			this.InitializeParticles();
			if (!this._isLoaded)
			{
				this.PlayTrailSound();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		public void Launch(Vector3 position, Quaternion quaternion, float flightDistance)
		{
			this._spawnTimestamp = this._dayNightCycle.FluidSecondsPassedToday;
			this._initialPosition = position;
			this._initialDirection = (quaternion * Vector3.forward).normalized;
			this._flightDistance = flightDistance;
			base.Transform.position = position;
			base.Transform.rotation = quaternion;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021A1 File Offset: 0x000003A1
		public void Update()
		{
			if (!this._isDeleted && Time.timeScale > 0f)
			{
				this.UpdateFinalization();
				this.UpdateMovement();
				this.UpdateDeletion();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021CC File Offset: 0x000003CC
		public void Save(IEntitySaver entitySaver)
		{
			if (!this._isFinalized)
			{
				IObjectSaver component = entitySaver.GetComponent(Firework.ComponentKey);
				component.Set(Firework.PositionKey, base.Transform.position);
				component.Set(Firework.RotationKey, base.Transform.rotation);
				component.Set(Firework.InitialPositionKey, this._initialPosition);
				component.Set(Firework.InitialDirectionKey, this._initialDirection);
				component.Set(Firework.FightDistanceKey, this._flightDistance);
				component.Set(Firework.DistanceFlownKey, this._distanceFlown);
				component.Set(Firework.SimulationTimeKey, this._simulationTime);
				component.Set(Firework.SpawnTimestampKey, this._spawnTimestamp);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002280 File Offset: 0x00000480
		[BackwardCompatible(2026, 3, 5, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Firework.ComponentKey, out objectLoader) && objectLoader.Has<Vector3>(Firework.InitialPositionKey))
			{
				base.Transform.position = objectLoader.Get(Firework.PositionKey);
				base.Transform.rotation = objectLoader.Get(Firework.RotationKey);
				this._initialPosition = objectLoader.Get(Firework.InitialPositionKey);
				this._initialDirection = objectLoader.Get(Firework.InitialDirectionKey);
				this._flightDistance = objectLoader.Get(Firework.FightDistanceKey);
				this._distanceFlown = objectLoader.Get(Firework.DistanceFlownKey);
				this._simulationTime = objectLoader.Get(Firework.SimulationTimeKey);
				this._spawnTimestamp = objectLoader.Get(Firework.SpawnTimestampKey);
				this._isLoaded = true;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002348 File Offset: 0x00000548
		public void InitializeParticles()
		{
			ParticlesCache component = base.GetComponent<ParticlesCache>();
			if (this._spec.HasBurst)
			{
				this._burstRunner = component.GetParticlesRunner("Burst");
				this._burstRunner.Disable();
			}
			this._trailRunner = component.GetParticlesRunner("Trail");
			this._trailRunner.Enable();
			if (this._isLoaded)
			{
				this._trailRunner.FastForward(Firework.LoadedParticlesFastForward);
			}
			this._trailRunner.Play();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023C4 File Offset: 0x000005C4
		public void UpdateFinalization()
		{
			if (!this._isFinalized && this._distanceFlown >= this._flightDistance)
			{
				this._trailRunner.DisableEmission();
				if (this._spec.HasBurst)
				{
					this.PlayBurstSound();
					this._burstRunner.Enable();
					this._burstRunner.Play();
				}
				this._isFinalized = true;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002424 File Offset: 0x00000624
		public void UpdateMovement()
		{
			if (this._distanceFlown <= this._flightDistance + Firework.DistanceMargin)
			{
				this._simulationTime = this._dayNightCycle.FluidSecondsPassedToday - this._spawnTimestamp;
				Vector3 vector = Firework.Gravity + Firework.Thrust * this._initialDirection;
				Vector3 vector2 = this._initialPosition + this._initialDirection * Firework.InitialVelocity * this._simulationTime + 0.5f * vector * this._simulationTime * this._simulationTime;
				this._distanceFlown += Vector3.Distance(base.Transform.position, vector2);
				base.Transform.position = vector2;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024F4 File Offset: 0x000006F4
		public void UpdateDeletion()
		{
			if (this._distanceFlown >= Firework.DistanceMargin)
			{
				ParticlesRunner burstRunner = this._burstRunner;
				if ((burstRunner == null || !burstRunner.HasParticles()) && !this._trailRunner.HasParticles())
				{
					this._isDeleted = true;
					this._entityService.Delete(this);
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002545 File Offset: 0x00000745
		public void PlayTrailSound()
		{
			if (!string.IsNullOrWhiteSpace(this._spec.TrailSound))
			{
				this._soundSystem.PlaySound3D(base.GameObject, this._spec.TrailSound, 5);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002576 File Offset: 0x00000776
		public void PlayBurstSound()
		{
			if (!string.IsNullOrWhiteSpace(this._spec.BurstSound))
			{
				this._soundSystem.PlaySound2D(base.GameObject, this._spec.BurstSound, 5);
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly float Thrust = 10f;

		// Token: 0x04000009 RID: 9
		public static readonly float InitialVelocity = 10f;

		// Token: 0x0400000A RID: 10
		public static readonly float DistanceMargin = 10f;

		// Token: 0x0400000B RID: 11
		public static readonly float LoadedParticlesFastForward = 0.1f;

		// Token: 0x0400000C RID: 12
		public static readonly Vector3 Gravity = new Vector3(0f, -9.81f, 0f);

		// Token: 0x0400000D RID: 13
		public static readonly ComponentKey ComponentKey = new ComponentKey("Firework");

		// Token: 0x0400000E RID: 14
		public static readonly PropertyKey<Vector3> PositionKey = new PropertyKey<Vector3>("Position");

		// Token: 0x0400000F RID: 15
		public static readonly PropertyKey<Quaternion> RotationKey = new PropertyKey<Quaternion>("Rotation");

		// Token: 0x04000010 RID: 16
		public static readonly PropertyKey<Vector3> InitialPositionKey = new PropertyKey<Vector3>("InitialPosition");

		// Token: 0x04000011 RID: 17
		public static readonly PropertyKey<Vector3> InitialDirectionKey = new PropertyKey<Vector3>("InitialDirection");

		// Token: 0x04000012 RID: 18
		public static readonly PropertyKey<float> FightDistanceKey = new PropertyKey<float>("FlightDistanceKey");

		// Token: 0x04000013 RID: 19
		public static readonly PropertyKey<float> DistanceFlownKey = new PropertyKey<float>("DistanceFlown");

		// Token: 0x04000014 RID: 20
		public static readonly PropertyKey<float> SimulationTimeKey = new PropertyKey<float>("SimulationTime");

		// Token: 0x04000015 RID: 21
		public static readonly PropertyKey<float> SpawnTimestampKey = new PropertyKey<float>("SpawnTimestamp");

		// Token: 0x04000016 RID: 22
		public readonly EntityService _entityService;

		// Token: 0x04000017 RID: 23
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000018 RID: 24
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000019 RID: 25
		public FireworkSpec _spec;

		// Token: 0x0400001A RID: 26
		public ParticlesRunner _trailRunner;

		// Token: 0x0400001B RID: 27
		public ParticlesRunner _burstRunner;

		// Token: 0x0400001C RID: 28
		public bool _isFinalized;

		// Token: 0x0400001D RID: 29
		public bool _isDeleted;

		// Token: 0x0400001E RID: 30
		public Vector3 _initialPosition;

		// Token: 0x0400001F RID: 31
		public Vector3 _initialDirection;

		// Token: 0x04000020 RID: 32
		public float _flightDistance;

		// Token: 0x04000021 RID: 33
		public float _distanceFlown;

		// Token: 0x04000022 RID: 34
		public float _simulationTime;

		// Token: 0x04000023 RID: 35
		public float _spawnTimestamp;

		// Token: 0x04000024 RID: 36
		public bool _isLoaded;
	}
}
