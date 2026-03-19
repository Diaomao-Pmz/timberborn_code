using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Forestry;
using Timberborn.Particles;
using Timberborn.WalkingSystem;

namespace Timberborn.ForestryEffects
{
	// Token: 0x02000008 RID: 8
	public class TreeCutterParticleController : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000218C File Offset: 0x0000038C
		public void Awake()
		{
			this._particlesCache = base.GetComponent<ParticlesCache>();
			this._swimmingAnimator = base.GetComponent<SwimmingAnimator>();
			this._treeCutterParticleControllerSpec = base.GetComponent<TreeCutterParticleControllerSpec>();
			TreeCutter component = base.GetComponent<TreeCutter>();
			component.CuttingStarted += this.StartCutting;
			component.CuttingStopped += this.StopCutting;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E6 File Offset: 0x000003E6
		public void StartCutting(object sender, EventArgs eventArgs)
		{
			this.CreateParticlesRunner();
			this.UpdateParticlesState();
			this._swimmingAnimator.SwimmingStateChanged += this.OnSwimmingStateChanged;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000220B File Offset: 0x0000040B
		public void StopCutting(object sender, EventArgs eventArgs)
		{
			this._particlesRunner.Stop();
			this._swimmingAnimator.SwimmingStateChanged -= this.OnSwimmingStateChanged;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222F File Offset: 0x0000042F
		public void OnSwimmingStateChanged(object sender, EventArgs e)
		{
			this.UpdateParticlesState();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002238 File Offset: 0x00000438
		public void CreateParticlesRunner()
		{
			if (this._particlesRunner == null)
			{
				string particlesAttachmentId = this._treeCutterParticleControllerSpec.ParticlesAttachmentId;
				this._particlesRunner = this._particlesCache.GetParticlesRunner(particlesAttachmentId);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000226B File Offset: 0x0000046B
		public void UpdateParticlesState()
		{
			if (this._swimmingAnimator.IsSwimming)
			{
				this._particlesRunner.Stop();
				return;
			}
			this._particlesRunner.Play();
		}

		// Token: 0x04000008 RID: 8
		public ParticlesCache _particlesCache;

		// Token: 0x04000009 RID: 9
		public SwimmingAnimator _swimmingAnimator;

		// Token: 0x0400000A RID: 10
		public TreeCutterParticleControllerSpec _treeCutterParticleControllerSpec;

		// Token: 0x0400000B RID: 11
		public ParticlesRunner _particlesRunner;
	}
}
