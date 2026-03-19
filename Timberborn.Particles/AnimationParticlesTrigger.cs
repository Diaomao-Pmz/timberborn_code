using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.TimbermeshAnimations;

namespace Timberborn.Particles
{
	// Token: 0x02000008 RID: 8
	public class AnimationParticlesTrigger : BaseComponent, IAwakableComponent, IUpdatableComponent
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002330 File Offset: 0x00000530
		public void Awake()
		{
			this._particlesCache = base.GetComponent<ParticlesCache>();
			this._animator = base.GetComponentInChildren<IAnimator>(true);
			this._animationParticlesTriggerSpec = base.GetComponent<AnimationParticlesTriggerSpec>();
			this._animator.AnimationChanged += delegate(object _, EventArgs _)
			{
				this.UpdateState();
			};
			base.DisableComponent();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002380 File Offset: 0x00000580
		public void Update()
		{
			float repeatedTime = this._animator.RepeatedTime;
			foreach (AnimationParticle animationParticle in this._runningParticles)
			{
				for (int i = 0; i < animationParticle.TriggerTimes.Length; i++)
				{
					this.UpdateTrigger(i, repeatedTime, animationParticle);
				}
			}
			this._lastFrameTime = repeatedTime;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002404 File Offset: 0x00000604
		public void UpdateTrigger(int index, float animatorTime, AnimationParticle animationParticle)
		{
			float num = animationParticle.TriggerTimes[index];
			if (this._lastFrameTime <= num && (animatorTime > num || animatorTime < this._lastFrameTime))
			{
				this._particleRunners[animationParticle].Play();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002448 File Offset: 0x00000648
		public void UpdateState()
		{
			this.FindCurrentAnimationParticle();
			if (this._runningParticles.Count > 0)
			{
				base.EnableComponent();
				this.CreateParticlesRunner();
				this._lastFrameTime = this._animator.RepeatedTime;
				return;
			}
			base.DisableComponent();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002484 File Offset: 0x00000684
		public void FindCurrentAnimationParticle()
		{
			this._runningParticles.Clear();
			foreach (AnimationParticle animationParticle in this._animationParticlesTriggerSpec.AnimationParticles)
			{
				if (animationParticle.AnimationName == this._animator.AnimationName)
				{
					this._runningParticles.Add(animationParticle);
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024E8 File Offset: 0x000006E8
		public void CreateParticlesRunner()
		{
			foreach (AnimationParticle animationParticle in this._runningParticles)
			{
				if (!this._particleRunners.ContainsKey(animationParticle))
				{
					string particlesAttachmentId = animationParticle.ParticlesAttachmentId;
					this._particleRunners[animationParticle] = this._particlesCache.GetParticlesRunner(particlesAttachmentId);
				}
			}
		}

		// Token: 0x0400000B RID: 11
		public ParticlesCache _particlesCache;

		// Token: 0x0400000C RID: 12
		public IAnimator _animator;

		// Token: 0x0400000D RID: 13
		public AnimationParticlesTriggerSpec _animationParticlesTriggerSpec;

		// Token: 0x0400000E RID: 14
		public float _lastFrameTime;

		// Token: 0x0400000F RID: 15
		public readonly List<AnimationParticle> _runningParticles = new List<AnimationParticle>();

		// Token: 0x04000010 RID: 16
		public readonly Dictionary<AnimationParticle, ParticlesRunner> _particleRunners = new Dictionary<AnimationParticle, ParticlesRunner>();
	}
}
