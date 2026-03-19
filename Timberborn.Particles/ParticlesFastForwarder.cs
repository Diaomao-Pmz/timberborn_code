using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;

namespace Timberborn.Particles
{
	// Token: 0x02000011 RID: 17
	public class ParticlesFastForwarder : ILateUpdatableSingleton
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002B6C File Offset: 0x00000D6C
		public void LateUpdateSingleton()
		{
			if (this._isEnabled)
			{
				this.FastForwardAllParticles();
				this._isEnabled = false;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B83 File Offset: 0x00000D83
		public void Register(ParticlesRunner particlesRunner)
		{
			if (this._isEnabled)
			{
				this._particlesRunners.Add(particlesRunner);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B99 File Offset: 0x00000D99
		public void Unregister(ParticlesRunner particlesRunner)
		{
			if (this._isEnabled)
			{
				this._particlesRunners.Remove(particlesRunner);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public void FastForwardAllParticles()
		{
			foreach (ParticlesRunner particlesRunner in this._particlesRunners)
			{
				if (particlesRunner != null && particlesRunner.IsPlaying)
				{
					particlesRunner.FastForward(ParticlesFastForwarder.GetDuration(particlesRunner));
					particlesRunner.Play();
				}
			}
			this._particlesRunners.Clear();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C24 File Offset: 0x00000E24
		public static float GetDuration(ParticlesRunner particlesRunner)
		{
			IFastForwardableParticles component = particlesRunner.Owner.GetComponent<IFastForwardableParticles>();
			if (component == null)
			{
				return ParticlesFastForwarder.FastForwardDuration;
			}
			return component.FastForwardDuration;
		}

		// Token: 0x04000020 RID: 32
		public static readonly float FastForwardDuration = 5f;

		// Token: 0x04000021 RID: 33
		public readonly List<ParticlesRunner> _particlesRunners = new List<ParticlesRunner>();

		// Token: 0x04000022 RID: 34
		public bool _isEnabled = true;
	}
}
