using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using Timberborn.TickSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200001A RID: 26
	public class MechanicalNodeParticlesController : TickableComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener, IParticlesSpeedMultiplier
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003988 File Offset: 0x00001B88
		public float SpeedMultiplier
		{
			get
			{
				if (!this.ShouldPlayParticles)
				{
					return 1f;
				}
				return this._mechanicalNode.PowerEfficiency;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000039A3 File Offset: 0x00001BA3
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._mechanicalNodeParticlesControllerSpec = base.GetComponent<MechanicalNodeParticlesControllerSpec>();
			base.DisableComponent();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000039C3 File Offset: 0x00001BC3
		public void InitializeEntity()
		{
			this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(this._mechanicalNodeParticlesControllerSpec.AttachmentIds);
			this._particlesRunner.AddParticleSpeedMultiplier(this);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002482 File Offset: 0x00000682
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000248A File Offset: 0x0000068A
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000039F2 File Offset: 0x00001BF2
		public override void Tick()
		{
			if (this.ShouldPlayParticles)
			{
				this._particlesRunner.Play();
				this._particlesRunner.UpdateSimulationSpeed();
				return;
			}
			this._particlesRunner.Stop();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003A1E File Offset: 0x00001C1E
		public bool ShouldPlayParticles
		{
			get
			{
				return this._mechanicalNode.ActiveAndPowered && this._mechanicalNode.PowerEfficiency > this._mechanicalNodeParticlesControllerSpec.MinEfficiency;
			}
		}

		// Token: 0x0400004F RID: 79
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000050 RID: 80
		public MechanicalNodeParticlesControllerSpec _mechanicalNodeParticlesControllerSpec;

		// Token: 0x04000051 RID: 81
		public ParticlesRunner _particlesRunner;
	}
}
