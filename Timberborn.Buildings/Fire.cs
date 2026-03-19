using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreSound;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using Timberborn.SoundSystem;
using Timberborn.TemplateAttachmentSystem;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x0200001C RID: 28
	public class Fire : BaseComponent, IInitializableEntity, IParticlesSpeedMultiplier
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003E3D File Offset: 0x0000203D
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00003E45 File Offset: 0x00002045
		public ParticleSystem.MainModule SingleFlame { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003E4E File Offset: 0x0000204E
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00003E56 File Offset: 0x00002056
		public Light Light { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003E5F File Offset: 0x0000205F
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00003E67 File Offset: 0x00002067
		public float SpeedMultiplier { get; private set; } = 1f;

		// Token: 0x060000EF RID: 239 RVA: 0x00003E70 File Offset: 0x00002070
		public Fire(ISoundSystem soundSystem)
		{
			this._soundSystem = soundSystem;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003E8C File Offset: 0x0000208C
		public void InitializeEntity()
		{
			string attachmentId = base.GetComponent<FireSpec>().AttachmentId;
			this._fireRoot = base.GetComponent<TemplateAttachments>().GetOrCreateAttachment(attachmentId).GameObject;
			this.Light = this._fireRoot.GetComponentInChildren<Light>();
			ParticleSystem[] componentsInChildren = this._fireRoot.GetComponentsInChildren<ParticleSystem>();
			if (componentsInChildren.Length == 1)
			{
				this.SingleFlame = componentsInChildren[0].main;
			}
			this._fireRoot.SetActive(false);
			this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(attachmentId);
			this._particlesRunner.AddParticleSpeedMultiplier(this);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003F18 File Offset: 0x00002118
		public void Enable()
		{
			if (!this._fireStarted)
			{
				this._fireRoot.SetActive(true);
				this._soundSystem.LoopSingle3DSound(this._fireRoot, Fire.SoundEventKey, 128);
				this._soundSystem.SetCustomMixer(this._fireRoot, Fire.SoundEventKey, MixerNames.BuildingMixerNameKey);
				this._fireStarted = true;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003F76 File Offset: 0x00002176
		public void Disable()
		{
			this._fireRoot.SetActive(false);
			if (this._fireStarted)
			{
				this._soundSystem.StopSound(this._fireRoot, Fire.SoundEventKey);
			}
			this._fireStarted = false;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003FA9 File Offset: 0x000021A9
		public void SetSpeedMultiplier(float speedMultiplier)
		{
			this.SpeedMultiplier = speedMultiplier;
			this._particlesRunner.UpdateSimulationSpeed();
		}

		// Token: 0x04000048 RID: 72
		public static readonly string SoundEventKey = "Environment.Buildings.Fire";

		// Token: 0x0400004C RID: 76
		public readonly ISoundSystem _soundSystem;

		// Token: 0x0400004D RID: 77
		public GameObject _fireRoot;

		// Token: 0x0400004E RID: 78
		public ParticlesRunner _particlesRunner;

		// Token: 0x0400004F RID: 79
		public bool _fireStarted;
	}
}
