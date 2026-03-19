using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using Timberborn.Planting;
using Timberborn.WalkingSystem;

namespace Timberborn.PlantingEffects
{
	// Token: 0x02000009 RID: 9
	public class PlantingParticleController : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021CC File Offset: 0x000003CC
		public void Awake()
		{
			this._particlesCache = base.GetComponent<ParticlesCache>();
			this._swimmingAnimator = base.GetComponent<SwimmingAnimator>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E8 File Offset: 0x000003E8
		public void InitializeEntity()
		{
			this._plantExecutor = base.GetComponent<PlantExecutor>();
			this._plantExecutor.PlantingStarted += this.OnPlantingStarted;
			this._plantExecutor.PlantingFinished += this.OnPlantingFinished;
			if (this._plantExecutor.IsPlanting)
			{
				this.InitializePlantingParticles();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002242 File Offset: 0x00000442
		public void OnPlantingStarted(object sender, EventArgs eventArgs)
		{
			this.InitializePlantingParticles();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000224A File Offset: 0x0000044A
		public void OnPlantingFinished(object sender, EventArgs eventArgs)
		{
			this._particlesRunner.Stop();
			this._swimmingAnimator.UnderwaterStateChanged -= this.OnSwimmingStateChanged;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000226E File Offset: 0x0000046E
		public void OnSwimmingStateChanged(object sender, EventArgs e)
		{
			this.UpdateParticlesState();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002276 File Offset: 0x00000476
		public void InitializePlantingParticles()
		{
			this.CreateParticlesRunner();
			this.UpdateParticlesState();
			this._swimmingAnimator.UnderwaterStateChanged += this.OnSwimmingStateChanged;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000229C File Offset: 0x0000049C
		public void CreateParticlesRunner()
		{
			if (this._particlesRunner == null)
			{
				string particlesAttachmentId = base.GetComponent<PlantingParticleControllerSpec>().ParticlesAttachmentId;
				this._particlesRunner = this._particlesCache.GetParticlesRunner(particlesAttachmentId);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022CF File Offset: 0x000004CF
		public void UpdateParticlesState()
		{
			if (this._swimmingAnimator.IsUnderwater)
			{
				this._particlesRunner.Stop();
				return;
			}
			this._particlesRunner.Play();
		}

		// Token: 0x0400000A RID: 10
		public ParticlesCache _particlesCache;

		// Token: 0x0400000B RID: 11
		public SwimmingAnimator _swimmingAnimator;

		// Token: 0x0400000C RID: 12
		public ParticlesRunner _particlesRunner;

		// Token: 0x0400000D RID: 13
		public PlantExecutor _plantExecutor;
	}
}
