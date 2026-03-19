using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Wonders
{
	// Token: 0x0200001D RID: 29
	public class WonderParticleController : BaseComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener, IPersistentEntity, IFastForwardableParticles
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000033D1 File Offset: 0x000015D1
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000033D9 File Offset: 0x000015D9
		public float FastForwardDuration { get; private set; }

		// Token: 0x060000A9 RID: 169 RVA: 0x000033E2 File Offset: 0x000015E2
		public void Awake()
		{
			this._wonder = base.GetComponent<Wonder>();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000033F0 File Offset: 0x000015F0
		public void InitializeEntity()
		{
			ImmutableArray<string> attachmentIds = base.GetComponent<WonderParticleControllerSpec>().AttachmentIds;
			base.GetComponent<NonLinearParticlesSpeedMultiplier>().Disable();
			this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(attachmentIds);
			if (this.FastForwardDuration > 0f)
			{
				this._emissionStartTime = Time.time - this.FastForwardDuration;
				this._particlesRunner.Play();
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003455 File Offset: 0x00001655
		public void OnEnterFinishedState()
		{
			this._wonder.WonderActivated += this.OnWonderActivated;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000346E File Offset: 0x0000166E
		public void OnExitFinishedState()
		{
			this._wonder.WonderActivated -= this.OnWonderActivated;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003488 File Offset: 0x00001688
		public void Save(IEntitySaver entitySaver)
		{
			if (this._particlesRunner.IsPlaying)
			{
				IObjectSaver component = entitySaver.GetComponent(WonderParticleController.WonderParticleControllerKey);
				float value = Time.time - this._emissionStartTime;
				component.Set(WonderParticleController.EmissionDurationKey, value);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000034C8 File Offset: 0x000016C8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(WonderParticleController.WonderParticleControllerKey, out objectLoader))
			{
				this.FastForwardDuration = objectLoader.Get(WonderParticleController.EmissionDurationKey);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000034F5 File Offset: 0x000016F5
		public void OnWonderActivated(object sender, EventArgs e)
		{
			this._emissionStartTime = Time.time;
			this._particlesRunner.Play();
		}

		// Token: 0x04000049 RID: 73
		public static readonly ComponentKey WonderParticleControllerKey = new ComponentKey("WonderParticleController");

		// Token: 0x0400004A RID: 74
		public static readonly PropertyKey<float> EmissionDurationKey = new PropertyKey<float>("EmissionDuration");

		// Token: 0x0400004C RID: 76
		public Wonder _wonder;

		// Token: 0x0400004D RID: 77
		public ParticlesRunner _particlesRunner;

		// Token: 0x0400004E RID: 78
		public float _emissionStartTime;
	}
}
