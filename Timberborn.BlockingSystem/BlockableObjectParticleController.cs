using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Particles;

namespace Timberborn.BlockingSystem
{
	// Token: 0x0200000A RID: 10
	public class BlockableObjectParticleController : BaseComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002428 File Offset: 0x00000628
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002438 File Offset: 0x00000638
		public void InitializeEntity()
		{
			ImmutableArray<string> attachmentIds = base.GetComponent<BlockableObjectParticleControllerSpec>().AttachmentIds;
			this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(attachmentIds);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002468 File Offset: 0x00000668
		public void OnEnterFinishedState()
		{
			this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
			if (this._blockableObject.IsUnblocked)
			{
				this._particlesRunner.Play();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024BB File Offset: 0x000006BB
		public void OnExitFinishedState()
		{
			this._blockableObject.ObjectUnblocked -= this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked -= this.OnObjectBlocked;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024EB File Offset: 0x000006EB
		public void OnObjectUnblocked(object sender, EventArgs e)
		{
			this._particlesRunner.Play();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024F8 File Offset: 0x000006F8
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this._particlesRunner.Stop();
		}

		// Token: 0x0400000F RID: 15
		public BlockableObject _blockableObject;

		// Token: 0x04000010 RID: 16
		public ParticlesRunner _particlesRunner;
	}
}
