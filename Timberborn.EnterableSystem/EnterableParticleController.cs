using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Particles;

namespace Timberborn.EnterableSystem
{
	// Token: 0x0200000C RID: 12
	public class EnterableParticleController : BaseComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x0600004B RID: 75 RVA: 0x0000290F File Offset: 0x00000B0F
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002920 File Offset: 0x00000B20
		public void InitializeEntity()
		{
			ImmutableArray<string> attachmentIds = base.GetComponent<EnterableParticleControllerSpec>().AttachmentIds;
			this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(attachmentIds);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002950 File Offset: 0x00000B50
		public void OnEnterFinishedState()
		{
			this._enterable.EntererAdded += this.OnEntererAdded;
			this._enterable.EntererRemoved += this.OnEntererRemoved;
			this.UpdateParticles();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002986 File Offset: 0x00000B86
		public void OnExitFinishedState()
		{
			this._enterable.EntererAdded -= this.OnEntererAdded;
			this._enterable.EntererRemoved -= this.OnEntererRemoved;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029B6 File Offset: 0x00000BB6
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			this.UpdateParticles();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029B6 File Offset: 0x00000BB6
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			this.UpdateParticles();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029BE File Offset: 0x00000BBE
		public void UpdateParticles()
		{
			if (this._enterable.NumberOfEnterersInside > 0)
			{
				this._particlesRunner.Play();
				return;
			}
			this._particlesRunner.Stop();
		}

		// Token: 0x04000017 RID: 23
		public Enterable _enterable;

		// Token: 0x04000018 RID: 24
		public ParticlesRunner _particlesRunner;
	}
}
