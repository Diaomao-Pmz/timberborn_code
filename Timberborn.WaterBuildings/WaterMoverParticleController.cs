using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using Timberborn.TickSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000037 RID: 55
	public class WaterMoverParticleController : TickableComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x0600028B RID: 651 RVA: 0x00007D31 File Offset: 0x00005F31
		public void Awake()
		{
			this._waterMover = base.GetComponent<WaterMover>();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007D40 File Offset: 0x00005F40
		public void InitializeEntity()
		{
			ImmutableArray<string> attachmentIds = base.GetComponent<WaterMoverParticleControllerSpec>().AttachmentIds;
			this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(attachmentIds);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007D70 File Offset: 0x00005F70
		public override void Tick()
		{
			if (this._waterMover.CanMoveWater)
			{
				this._particlesRunner.Play();
				return;
			}
			this._particlesRunner.Stop();
		}

		// Token: 0x04000108 RID: 264
		public WaterMover _waterMover;

		// Token: 0x04000109 RID: 265
		public ParticlesRunner _particlesRunner;
	}
}
