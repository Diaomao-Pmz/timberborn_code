using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.Particles
{
	// Token: 0x02000013 RID: 19
	public class ParticlesRunner
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E2E File Offset: 0x0000102E
		public BaseComponent Owner { get; }

		// Token: 0x06000067 RID: 103 RVA: 0x00002E36 File Offset: 0x00001036
		public ParticlesRunner(BaseComponent owner, IEnumerable<ParticlesObject> particleObjects)
		{
			this.Owner = owner;
			this._particleObjects = particleObjects.ToImmutableArray<ParticlesObject>();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E5C File Offset: 0x0000105C
		public static ParticlesRunner Create(BaseComponent owner, IEnumerable<ParticleSystem> particleSystems)
		{
			return new ParticlesRunner(owner, particleSystems.Select(new Func<ParticleSystem, ParticlesObject>(ParticlesObject.Create)));
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002E76 File Offset: 0x00001076
		public bool IsPlaying
		{
			get
			{
				if (this._particleObjects.Length > 0)
				{
					return this._particleObjects.Any((ParticlesObject particleObject) => particleObject.IsPlaying);
				}
				return false;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002EB2 File Offset: 0x000010B2
		public void AddParticleSpeedMultiplier(IParticlesSpeedMultiplier particlesSpeedMultiplier)
		{
			this._particleSpeedMultipliers.Add(particlesSpeedMultiplier);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EC0 File Offset: 0x000010C0
		public void RemoveParticleSpeedMultiplier(IParticlesSpeedMultiplier particlesSpeedMultiplier)
		{
			this._particleSpeedMultipliers.Remove(particlesSpeedMultiplier);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002ED0 File Offset: 0x000010D0
		public void Play()
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].Play();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F08 File Offset: 0x00001108
		public void Stop()
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].Stop();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F40 File Offset: 0x00001140
		public void Enable()
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].Enable();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F78 File Offset: 0x00001178
		public void Disable()
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].Disable();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002FB0 File Offset: 0x000011B0
		public void EnableEmission()
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].EnableEmission();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002FE8 File Offset: 0x000011E8
		public void SetEmissionRate(float rate)
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].SetEmissionRate(rate);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003020 File Offset: 0x00001220
		public void DisableEmission()
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].DisableEmission();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003058 File Offset: 0x00001258
		public void UpdateSimulationSpeed()
		{
			float speedMultiplier = this.GetSpeedMultiplier();
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].UpdateSimulationSpeed(speedMultiplier);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003098 File Offset: 0x00001298
		public void FastForward(float simulationTime)
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].FastForward(simulationTime);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030D0 File Offset: 0x000012D0
		public bool HasParticles()
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				if (this._particleObjects[i].ParticleCount() > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003110 File Offset: 0x00001310
		public void UpdateForceMultiplier(Vector3 forceMultiplier)
		{
			for (int i = 0; i < this._particleObjects.Length; i++)
			{
				this._particleObjects[i].UpdateForceMultiplier(forceMultiplier);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003148 File Offset: 0x00001348
		public float GetSpeedMultiplier()
		{
			float num = 1f;
			for (int i = 0; i < this._particleSpeedMultipliers.Count; i++)
			{
				num *= this._particleSpeedMultipliers[i].SpeedMultiplier;
			}
			return num;
		}

		// Token: 0x04000026 RID: 38
		public readonly ImmutableArray<ParticlesObject> _particleObjects;

		// Token: 0x04000027 RID: 39
		public readonly List<IParticlesSpeedMultiplier> _particleSpeedMultipliers = new List<IParticlesSpeedMultiplier>();
	}
}
