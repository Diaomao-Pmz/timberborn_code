using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.WindSystem
{
	// Token: 0x0200000D RID: 13
	public class WindRotationAnimator : BaseComponent, IAwakableComponent, IUpdatableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002549 File Offset: 0x00000749
		public WindRotationAnimator(WindService windService, NonlinearAnimationManager nonlinearAnimationManager)
		{
			this._windService = windService;
			this._nonlinearAnimationManager = nonlinearAnimationManager;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000256A File Offset: 0x0000076A
		public void Awake()
		{
			base.DisableComponent();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002574 File Offset: 0x00000774
		public void InitializeEntity()
		{
			WindRotationAnimatorSpec component = base.GetComponent<WindRotationAnimatorSpec>();
			foreach (WindRotatorSpec windRotatorSpec in component.WindRotators)
			{
				this._windRotators.Add(WindRotationAnimator.WindRotator.Create(windRotatorSpec, base.GameObject));
			}
			if (!string.IsNullOrWhiteSpace(component.Tower.TransformName))
			{
				this._tower = WindRotationAnimator.WindRotator.Create(component.Tower, base.GameObject);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000025EA File Offset: 0x000007EA
		public void OnEnterFinishedState()
		{
			this.RotateTowerInstantly();
			base.EnableComponent();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000256A File Offset: 0x0000076A
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000025F8 File Offset: 0x000007F8
		public void Update()
		{
			this.UpdateAnimation();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002600 File Offset: 0x00000800
		public void SuspendAnimation()
		{
			this._animationSuspended = true;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002609 File Offset: 0x00000809
		public void UnsuspendAnimation()
		{
			this._animationSuspended = false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002614 File Offset: 0x00000814
		public void UpdateAnimation()
		{
			if (!this._animationSuspended)
			{
				float deltaTime = Time.deltaTime * this._nonlinearAnimationManager.SpeedMultiplier * this._windService.WindStrength;
				this.RotateTower(deltaTime);
				for (int i = 0; i < this._windRotators.Count; i++)
				{
					WindRotationAnimator.RotateRotators(this._windRotators[i], deltaTime);
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002676 File Offset: 0x00000876
		public void RotateTowerInstantly()
		{
			if (this._tower != null)
			{
				this._tower.Transform.rotation = this.GetTargetTowerRotation();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002698 File Offset: 0x00000898
		public void RotateTower(float deltaTime)
		{
			if (this._tower != null)
			{
				this._tower.Transform.rotation = Quaternion.RotateTowards(this._tower.Transform.rotation, this.GetTargetTowerRotation(), deltaTime * this._tower.Spec.RotationSpeed);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000026EC File Offset: 0x000008EC
		public Quaternion GetTargetTowerRotation()
		{
			float num = Vector2.SignedAngle(Vector2.down, this._windService.WindDirection);
			return Quaternion.Euler(this._tower.Spec.RotationAxis * num);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000272A File Offset: 0x0000092A
		public static void RotateRotators(WindRotationAnimator.WindRotator windRotator, float deltaTime)
		{
			windRotator.Transform.Rotate(windRotator.Spec.RotationAxis, deltaTime * windRotator.Spec.RotationSpeed);
		}

		// Token: 0x0400000F RID: 15
		public readonly WindService _windService;

		// Token: 0x04000010 RID: 16
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x04000011 RID: 17
		public bool _animationSuspended;

		// Token: 0x04000012 RID: 18
		public readonly List<WindRotationAnimator.WindRotator> _windRotators = new List<WindRotationAnimator.WindRotator>();

		// Token: 0x04000013 RID: 19
		public WindRotationAnimator.WindRotator _tower;

		// Token: 0x0200000E RID: 14
		public class WindRotator
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000048 RID: 72 RVA: 0x0000274F File Offset: 0x0000094F
			public WindRotatorSpec Spec { get; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000049 RID: 73 RVA: 0x00002757 File Offset: 0x00000957
			public Transform Transform { get; }

			// Token: 0x0600004A RID: 74 RVA: 0x0000275F File Offset: 0x0000095F
			public WindRotator(WindRotatorSpec spec, Transform transform)
			{
				this.Spec = spec;
				this.Transform = transform;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00002778 File Offset: 0x00000978
			public static WindRotationAnimator.WindRotator Create(WindRotatorSpec windRotatorSpec, GameObject parent)
			{
				Transform transform = parent.FindChildTransform(windRotatorSpec.TransformName);
				return new WindRotationAnimator.WindRotator(windRotatorSpec, transform);
			}
		}
	}
}
