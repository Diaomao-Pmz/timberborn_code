using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Particles;
using Timberborn.TemplateAttachmentSystem;
using Timberborn.TimeSystem;
using Timberborn.Workshops;
using UnityEngine;

namespace Timberborn.BotUpkeep
{
	// Token: 0x02000008 RID: 8
	public class BotManufactoryAnimationController : BaseComponent, IAwakableComponent, IUpdatableComponent, IInitializableEntity
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021BE File Offset: 0x000003BE
		public BotManufactoryAnimationController(IRandomNumberGenerator randomNumberGenerator, NonlinearAnimationManager nonlinearAnimationManager)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._nonlinearAnimationManager = nonlinearAnimationManager;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D4 File Offset: 0x000003D4
		public void Awake()
		{
			this._botManufactoryAnimationControllerSpec = base.GetComponent<BotManufactoryAnimationControllerSpec>();
			this.FindComponents();
			base.GetComponent<Workshop>().WorkshopStateChanged += this.OnWorkshopStateChanged;
			base.DisableComponent();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002208 File Offset: 0x00000408
		public void InitializeEntity()
		{
			if (this._botManufactoryAnimationControllerSpec.AttachmentIds.Length > 0)
			{
				this._particlesRunner = base.GetComponent<ParticlesCache>().GetParticlesRunner(this._botManufactoryAnimationControllerSpec.AttachmentIds);
				this._particlesRunner.Play();
				this._particlesRunner.DisableEmission();
				this._light = this.GetLightAttachment();
				Light light = this._light;
				this._initialLightIntensity = ((light != null) ? light.intensity : 0f);
			}
			this.StopAssembling();
			this.ResetRingRotation();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002296 File Offset: 0x00000496
		public void Update()
		{
			if (this._remainingAssemblyDuration > 0f)
			{
				this.UpdateAssembling();
				return;
			}
			if (this._remainingRingRotation > 0f)
			{
				this.UpdateRingRotation();
				return;
			}
			this.ResetAll();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C8 File Offset: 0x000004C8
		public void FindComponents()
		{
			this._ring = base.GameObject.FindChildTransform(this._botManufactoryAnimationControllerSpec.RingName);
			if (!string.IsNullOrWhiteSpace(this._botManufactoryAnimationControllerSpec.DrillName))
			{
				this._drill = base.GameObject.FindChildTransform(this._botManufactoryAnimationControllerSpec.DrillName);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000231F File Offset: 0x0000051F
		public void OnWorkshopStateChanged(object sender, WorkshopStateChangedEventArgs e)
		{
			if (!e.CurrentlyProducing)
			{
				base.DisableComponent();
				this.StopAssembling();
				return;
			}
			base.EnableComponent();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000233C File Offset: 0x0000053C
		public Light GetLightAttachment()
		{
			TemplateAttachments component = base.GetComponent<TemplateAttachments>();
			foreach (string id in this._botManufactoryAnimationControllerSpec.AttachmentIds)
			{
				Light componentInChildren = component.GetOrCreateAttachment(id).Transform.GetComponentInChildren<Light>(true);
				if (componentInChildren)
				{
					return componentInChildren;
				}
			}
			return null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002398 File Offset: 0x00000598
		public void StopAssembling()
		{
			ParticlesRunner particlesRunner = this._particlesRunner;
			if (particlesRunner != null)
			{
				particlesRunner.DisableEmission();
			}
			if (this._light)
			{
				this._light.intensity = 0f;
			}
			this._remainingAssemblyDuration = 0f;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023D4 File Offset: 0x000005D4
		public void UpdateAssembling()
		{
			this._remainingAssemblyDuration -= Time.deltaTime;
			if (this._light)
			{
				this._light.intensity = this._initialLightIntensity;
			}
			if (this._drill)
			{
				float num = Time.deltaTime * this._botManufactoryAnimationControllerSpec.DrillRotationSpeed * this._nonlinearAnimationManager.SpeedMultiplier;
				this._drill.Rotate(Vector3.forward, num * this._currentDirection);
			}
			ParticlesRunner particlesRunner = this._particlesRunner;
			if (particlesRunner == null)
			{
				return;
			}
			particlesRunner.EnableEmission();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002464 File Offset: 0x00000664
		public void UpdateRingRotation()
		{
			this.StopAssembling();
			float num = Time.deltaTime * this._botManufactoryAnimationControllerSpec.RingRotationSpeed * this._nonlinearAnimationManager.SpeedMultiplier;
			this._ring.Rotate(Vector3.up, num * this._currentDirection);
			this._remainingRingRotation -= num;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024BB File Offset: 0x000006BB
		public void ResetAll()
		{
			this.ResetRingRotation();
			this.ResetAssembling();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024CC File Offset: 0x000006CC
		public void ResetRingRotation()
		{
			this._remainingRingRotation = (float)this._randomNumberGenerator.Range(BotManufactoryAnimationController.MinRingRotationAngle, BotManufactoryAnimationController.MaxRingRotationAngle);
			this._currentDirection = (float)((this._randomNumberGenerator.Range(0f, 1f) > 0.5f) ? 1 : -1);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000251C File Offset: 0x0000071C
		public void ResetAssembling()
		{
			this._remainingAssemblyDuration = this._botManufactoryAnimationControllerSpec.AssemblyDuration;
		}

		// Token: 0x0400000D RID: 13
		public static readonly int MinRingRotationAngle = 90;

		// Token: 0x0400000E RID: 14
		public static readonly int MaxRingRotationAngle = 270;

		// Token: 0x0400000F RID: 15
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000010 RID: 16
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x04000011 RID: 17
		public BotManufactoryAnimationControllerSpec _botManufactoryAnimationControllerSpec;

		// Token: 0x04000012 RID: 18
		public ParticlesRunner _particlesRunner;

		// Token: 0x04000013 RID: 19
		public Transform _ring;

		// Token: 0x04000014 RID: 20
		public Transform _drill;

		// Token: 0x04000015 RID: 21
		public Light _light;

		// Token: 0x04000016 RID: 22
		public float _initialLightIntensity;

		// Token: 0x04000017 RID: 23
		public float _currentDirection;

		// Token: 0x04000018 RID: 24
		public float _remainingRingRotation;

		// Token: 0x04000019 RID: 25
		public float _remainingAssemblyDuration;
	}
}
