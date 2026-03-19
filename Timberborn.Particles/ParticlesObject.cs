using System;
using UnityEngine;

namespace Timberborn.Particles
{
	// Token: 0x02000012 RID: 18
	public readonly struct ParticlesObject
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002C66 File Offset: 0x00000E66
		public ParticlesObject(ParticleSystem particleSystem, float initialSpeed)
		{
			this._particleSystem = particleSystem;
			this._initialSpeed = initialSpeed;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C78 File Offset: 0x00000E78
		public static ParticlesObject Create(ParticleSystem particleSystem)
		{
			ParticleSystem.MainModule main = particleSystem.main;
			float simulationSpeed = main.simulationSpeed;
			main.simulationSpeed = 0f;
			particleSystem.Stop();
			return new ParticlesObject(particleSystem, simulationSpeed);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002CAD File Offset: 0x00000EAD
		public bool IsPlaying
		{
			get
			{
				return this._particleSystem.isPlaying;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002CBA File Offset: 0x00000EBA
		public void Play()
		{
			this._particleSystem.Play();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002CC7 File Offset: 0x00000EC7
		public void Stop()
		{
			this._particleSystem.Stop();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public void Enable()
		{
			this._particleSystem.gameObject.SetActive(true);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002CE7 File Offset: 0x00000EE7
		public void Disable()
		{
			this._particleSystem.gameObject.SetActive(false);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CFC File Offset: 0x00000EFC
		public void EnableEmission()
		{
			this._particleSystem.emission.enabled = true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D20 File Offset: 0x00000F20
		public void SetEmissionRate(float rate)
		{
			this._particleSystem.emission.rateOverTime = rate;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D48 File Offset: 0x00000F48
		public void DisableEmission()
		{
			this._particleSystem.emission.enabled = false;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D6C File Offset: 0x00000F6C
		public void UpdateSimulationSpeed(float speedMultiplier)
		{
			this._particleSystem.main.simulationSpeed = this._initialSpeed * speedMultiplier;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D94 File Offset: 0x00000F94
		public void UpdateForceMultiplier(Vector3 forceMultiplier)
		{
			ParticleSystem.ForceOverLifetimeModule forceOverLifetime = this._particleSystem.forceOverLifetime;
			forceOverLifetime.xMultiplier = forceMultiplier.x;
			forceOverLifetime.yMultiplier = forceMultiplier.y;
			forceOverLifetime.zMultiplier = forceMultiplier.z;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public void FastForward(float time)
		{
			float simulationSpeed = this._particleSystem.main.simulationSpeed;
			this.UpdateSimulationSpeed(1f);
			this._particleSystem.Play();
			this._particleSystem.Simulate(time, true, false, false);
			this.UpdateSimulationSpeed(simulationSpeed);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E21 File Offset: 0x00001021
		public int ParticleCount()
		{
			return this._particleSystem.particleCount;
		}

		// Token: 0x04000023 RID: 35
		public readonly ParticleSystem _particleSystem;

		// Token: 0x04000024 RID: 36
		public readonly float _initialSpeed;
	}
}
