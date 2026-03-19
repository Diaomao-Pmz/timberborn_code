using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using UnityEngine;

namespace Timberborn.ActivatorSystem
{
	// Token: 0x02000007 RID: 7
	public class ActivationProgressParticles : BaseComponent, IAwakableComponent, IInitializableEntity, IUpdatableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public void Awake()
		{
			this._particlesCache = base.GetComponent<ParticlesCache>();
			this._timedComponentActivator = base.GetComponent<TimedComponentActivator>();
			this._spec = base.GetComponent<ActivationProgressParticlesSpec>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002124 File Offset: 0x00000324
		public void InitializeEntity()
		{
			this._particlesRunner = this._particlesCache.GetParticlesRunner(this._spec.AttachmentIds);
			this._particlesRunner.Disable();
			if (!this._timedComponentActivator.CountdownIsActive)
			{
				this._timedComponentActivator.CountdownActivated += this.OnCountdownActivated;
			}
			else if (!this._timedComponentActivator.IsPastActivationTime)
			{
				this.PlayParticles();
			}
			if (!this._timedComponentActivator.IsPastActivationTime)
			{
				this._timedComponentActivator.Activated += this.OnActivated;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021BC File Offset: 0x000003BC
		public void Update()
		{
			if (this._particlesRunner != null)
			{
				float emissionRate = Mathf.Lerp((float)this._spec.MinEmission, (float)this._spec.MaxEmission, this._timedComponentActivator.ActivationProgress);
				this._particlesRunner.SetEmissionRate(emissionRate);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002206 File Offset: 0x00000406
		public void OnCountdownActivated(object sender, EventArgs e)
		{
			this.PlayParticles();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000220E File Offset: 0x0000040E
		public void OnActivated(object sender, EventArgs e)
		{
			this._particlesRunner.Disable();
			this._particlesRunner = null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002222 File Offset: 0x00000422
		public void PlayParticles()
		{
			this._particlesRunner.Enable();
			this._particlesRunner.Play();
		}

		// Token: 0x04000008 RID: 8
		public ParticlesCache _particlesCache;

		// Token: 0x04000009 RID: 9
		public TimedComponentActivator _timedComponentActivator;

		// Token: 0x0400000A RID: 10
		public ActivationProgressParticlesSpec _spec;

		// Token: 0x0400000B RID: 11
		public ParticlesRunner _particlesRunner;
	}
}
