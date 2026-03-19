using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.MechanicalSystem;
using Timberborn.Particles;
using Timberborn.TickSystem;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x0200000C RID: 12
	public class PowerGeneratorParticleController : TickableComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002518 File Offset: 0x00000718
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002528 File Offset: 0x00000728
		public void InitializeEntity()
		{
			ImmutableArray<string> attachmentIds = base.GetComponent<PowerGeneratorParticleControllerSpec>().AttachmentIds;
			this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(attachmentIds);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002558 File Offset: 0x00000758
		public override void Tick()
		{
			if (this._mechanicalNode.ActiveAndPowered && this._mechanicalNode.OutputMultiplier > 0f)
			{
				this._particlesRunner.Play();
				return;
			}
			this._particlesRunner.Stop();
		}

		// Token: 0x04000012 RID: 18
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000013 RID: 19
		public ParticlesRunner _particlesRunner;
	}
}
