using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.WindSystem
{
	// Token: 0x0200000B RID: 11
	public class WindParticleController : BaseComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002322 File Offset: 0x00000522
		public WindParticleController(EventBus eventBus, WindService windService)
		{
			this._eventBus = eventBus;
			this._windService = windService;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002338 File Offset: 0x00000538
		public void Awake()
		{
			this._particlesCache = base.GetComponent<ParticlesCache>();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002348 File Offset: 0x00000548
		public void InitializeEntity()
		{
			WindParticleControllerSpec component = base.GetComponent<WindParticleControllerSpec>();
			this._particlesRunner = this._particlesCache.GetParticlesRunner(component.AttachmentIds);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002378 File Offset: 0x00000578
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
			this.UpdateParticles();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000238C File Offset: 0x0000058C
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000239A File Offset: 0x0000059A
		[OnEvent]
		public void OnWindChanged(WindChangedEvent windChangedEvent)
		{
			this.UpdateParticles();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023A4 File Offset: 0x000005A4
		public void UpdateParticles()
		{
			Vector3 forceMultiplier;
			forceMultiplier..ctor(this._windService.WindDirection.x * this._windService.WindStrength, 1f, -this._windService.WindDirection.y * this._windService.WindStrength);
			this._particlesRunner.UpdateForceMultiplier(forceMultiplier);
		}

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;

		// Token: 0x0400000B RID: 11
		public readonly WindService _windService;

		// Token: 0x0400000C RID: 12
		public ParticlesCache _particlesCache;

		// Token: 0x0400000D RID: 13
		public ParticlesRunner _particlesRunner;
	}
}
