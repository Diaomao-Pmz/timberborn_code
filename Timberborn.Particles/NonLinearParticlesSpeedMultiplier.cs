using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.Particles
{
	// Token: 0x0200000D RID: 13
	public class NonLinearParticlesSpeedMultiplier : BaseComponent, IDeletableEntity, IParticlesSpeedMultiplier
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000027BB File Offset: 0x000009BB
		public NonLinearParticlesSpeedMultiplier(EventBus eventBus, NonlinearAnimationManager nonlinearAnimationManager)
		{
			this._eventBus = eventBus;
			this._nonlinearAnimationManager = nonlinearAnimationManager;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000027DC File Offset: 0x000009DC
		public float SpeedMultiplier
		{
			get
			{
				return this._nonlinearAnimationManager.SpeedMultiplier;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027E9 File Offset: 0x000009E9
		public void AddParticlesRunner(ParticlesRunner particlesRunner)
		{
			if (base.Enabled)
			{
				this._particlesRunner.Add(particlesRunner);
				particlesRunner.AddParticleSpeedMultiplier(this);
				this.Register();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000280C File Offset: 0x00000A0C
		public void DeleteEntity()
		{
			this.Unregister();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002814 File Offset: 0x00000A14
		public void Disable()
		{
			if (base.Enabled)
			{
				base.DisableComponent();
				foreach (ParticlesRunner particlesRunner in this._particlesRunner)
				{
					particlesRunner.RemoveParticleSpeedMultiplier(this);
				}
				this._particlesRunner.Clear();
				this.Unregister();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002884 File Offset: 0x00000A84
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateSimulationSpeed();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000288C File Offset: 0x00000A8C
		public void Register()
		{
			if (!this._registered)
			{
				this._registered = true;
				this._eventBus.Register(this);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028A9 File Offset: 0x00000AA9
		public void Unregister()
		{
			if (this._registered)
			{
				this._registered = false;
				this._eventBus.Unregister(this);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028C8 File Offset: 0x00000AC8
		public void UpdateSimulationSpeed()
		{
			foreach (ParticlesRunner particlesRunner in this._particlesRunner)
			{
				particlesRunner.UpdateSimulationSpeed();
			}
		}

		// Token: 0x04000015 RID: 21
		public readonly EventBus _eventBus;

		// Token: 0x04000016 RID: 22
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x04000017 RID: 23
		public readonly List<ParticlesRunner> _particlesRunner = new List<ParticlesRunner>();

		// Token: 0x04000018 RID: 24
		public bool _registered;
	}
}
