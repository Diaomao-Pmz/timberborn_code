using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using Timberborn.Workshops;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000014 RID: 20
	public class WorkshopParticleController : BaseComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x06000095 RID: 149 RVA: 0x000032BB File Offset: 0x000014BB
		public void Awake()
		{
			this._workshop = base.GetComponent<Workshop>();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000032CC File Offset: 0x000014CC
		public void InitializeEntity()
		{
			ImmutableArray<string> attachmentIds = base.GetComponent<WorkshopParticleControllerSpec>().AttachmentIds;
			this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(attachmentIds);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000032FC File Offset: 0x000014FC
		public void OnEnterFinishedState()
		{
			this._workshop.WorkshopStateChanged += this.OnWorkshopStateChanged;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003315 File Offset: 0x00001515
		public void OnExitFinishedState()
		{
			this._workshop.WorkshopStateChanged -= this.OnWorkshopStateChanged;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000332E File Offset: 0x0000152E
		public void OnWorkshopStateChanged(object sender, WorkshopStateChangedEventArgs e)
		{
			if (this._workshop.CurrentlyWorking)
			{
				this._particlesRunner.Play();
				return;
			}
			this._particlesRunner.Stop();
		}

		// Token: 0x04000035 RID: 53
		public Workshop _workshop;

		// Token: 0x04000036 RID: 54
		public ParticlesRunner _particlesRunner;
	}
}
