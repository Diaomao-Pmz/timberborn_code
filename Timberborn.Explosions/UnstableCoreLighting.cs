using System;
using Timberborn.ActivatorSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Rendering;
using Timberborn.TemplateAttachmentSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x0200001D RID: 29
	public class UnstableCoreLighting : BaseComponent, IAwakableComponent, IInitializableEntity, IUpdatableComponent
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004499 File Offset: 0x00002699
		public UnstableCoreLighting(MaterialColorer materialColorer, NonlinearAnimationManager nonlinearAnimationManager)
		{
			this._materialColorer = materialColorer;
			this._nonlinearAnimationManager = nonlinearAnimationManager;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000044AF File Offset: 0x000026AF
		public void Awake()
		{
			this._timedComponentActivator = base.GetComponent<TimedComponentActivator>();
			this._spec = base.GetComponent<UnstableCoreLightingSpec>();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000044CC File Offset: 0x000026CC
		public void InitializeEntity()
		{
			this._light = base.GetComponent<TemplateAttachments>().GetOrCreateAttachment(this._spec.AttachmentId).Transform.GetComponentInChildren<Light>();
			this._light.intensity = this._spec.LightStrength;
			if (this._timedComponentActivator.CountdownIsActive)
			{
				this._lastStateChange = Time.time;
			}
			else
			{
				this._timedComponentActivator.CountdownActivated += delegate(object _, EventArgs _)
				{
					this._lastStateChange = Time.time;
				};
			}
			this.DisableLight();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000454C File Offset: 0x0000274C
		public void Update()
		{
			if (this._timedComponentActivator.CountdownIsActive && Time.timeScale > 0f)
			{
				float time = Time.time;
				float num = Mathf.Lerp(this._spec.MaxInterval, this._spec.MinInterval, this._timedComponentActivator.ActivationProgress) / this._nonlinearAnimationManager.SpeedMultiplier;
				if (time >= this._lastStateChange + num)
				{
					if (this._light.intensity > 0f)
					{
						this.DisableLight();
					}
					else
					{
						this.EnableLight();
					}
					this._lastStateChange = time;
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000045DD File Offset: 0x000027DD
		public void DisableLight()
		{
			this._materialColorer.DisableLighting(this);
			this._light.intensity = 0f;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000045FC File Offset: 0x000027FC
		public void EnableLight()
		{
			this._materialColorer.EnableLighting(this, null);
			this._light.intensity = this._spec.LightStrength;
		}

		// Token: 0x04000083 RID: 131
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000084 RID: 132
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x04000085 RID: 133
		public TimedComponentActivator _timedComponentActivator;

		// Token: 0x04000086 RID: 134
		public UnstableCoreLightingSpec _spec;

		// Token: 0x04000087 RID: 135
		public Light _light;

		// Token: 0x04000088 RID: 136
		public float _lastStateChange;
	}
}
