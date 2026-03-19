using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WonderPlanes
{
	// Token: 0x0200000D RID: 13
	public class PlaneLauncherRotator : BaseComponent, IAwakableComponent, IUpdatableComponent, IPersistentEntity, IPostInitializableEntity
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600004A RID: 74 RVA: 0x00002EF0 File Offset: 0x000010F0
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x00002F28 File Offset: 0x00001128
		public event EventHandler RotationFinished;

		// Token: 0x0600004C RID: 76 RVA: 0x00002F60 File Offset: 0x00001160
		public void Awake()
		{
			this._planeLauncherRotatorSpec = base.GetComponent<PlaneLauncherRotatorSpec>();
			this._rotationCurve = this._planeLauncherRotatorSpec.RotationCurve.ToAnimationCurve();
			this._rotatedElement = base.GameObject.FindChildTransform(this._planeLauncherRotatorSpec.RotatedElementName);
			base.DisableComponent();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FB1 File Offset: 0x000011B1
		public void PostInitializeEntity()
		{
			if (this._loadedRotation > 0f)
			{
				this._rotatedElement.Rotate(Vector3.up, this._loadedRotation);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002FD6 File Offset: 0x000011D6
		public void Update()
		{
			if (this._remainingRotation > 0f)
			{
				this.UpdateRotation();
				return;
			}
			EventHandler rotationFinished = this.RotationFinished;
			if (rotationFinished != null)
			{
				rotationFinished(this, EventArgs.Empty);
			}
			base.DisableComponent();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003009 File Offset: 0x00001209
		public void StartRotation(float rotationAngle)
		{
			this._remainingRotation = rotationAngle;
			this._rotationDuration = this._planeLauncherRotatorSpec.FullRotationDuration * rotationAngle / 360f;
			this._rotationTime = 0f;
			base.EnableComponent();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000303C File Offset: 0x0000123C
		public void RotateToOriginalPosition()
		{
			this.StartRotation(360f - this.CurrentRotation);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003050 File Offset: 0x00001250
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(PlaneLauncherRotator.PlaneLauncherRotatorKey);
			component.Set(PlaneLauncherRotator.RemainingRotationKey, this._remainingRotation);
			component.Set(PlaneLauncherRotator.LoadedRotationKey, this.CurrentRotation);
			component.Set(PlaneLauncherRotator.RotationTimeKey, this._rotationTime);
			component.Set(PlaneLauncherRotator.RotationDurationKey, this._rotationDuration);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000030AC File Offset: 0x000012AC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(PlaneLauncherRotator.PlaneLauncherRotatorKey);
			this._loadedRotation = component.Get(PlaneLauncherRotator.LoadedRotationKey);
			this._remainingRotation = component.Get(PlaneLauncherRotator.RemainingRotationKey);
			this._rotationTime = component.Get(PlaneLauncherRotator.RotationTimeKey);
			this._rotationDuration = component.Get(PlaneLauncherRotator.RotationDurationKey);
			if (this._remainingRotation > 0f || this._loadedRotation > 0f)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000312C File Offset: 0x0000132C
		public float CurrentRotation
		{
			get
			{
				return this._rotatedElement.localRotation.eulerAngles.y;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003154 File Offset: 0x00001354
		public void UpdateRotation()
		{
			this._rotationTime += Time.deltaTime;
			float num = this._rotationCurve.Evaluate(this._rotationTime / this._rotationDuration) * Time.deltaTime;
			if (num > this._remainingRotation)
			{
				num = this._remainingRotation;
			}
			this._rotatedElement.Rotate(Vector3.up, num);
			this._remainingRotation -= num;
		}

		// Token: 0x04000045 RID: 69
		public static readonly ComponentKey PlaneLauncherRotatorKey = new ComponentKey("PlaneLauncherRotator");

		// Token: 0x04000046 RID: 70
		public static readonly PropertyKey<float> RemainingRotationKey = new PropertyKey<float>("RemainingRotation");

		// Token: 0x04000047 RID: 71
		public static readonly PropertyKey<float> LoadedRotationKey = new PropertyKey<float>("LoadedRotation");

		// Token: 0x04000048 RID: 72
		public static readonly PropertyKey<float> RotationTimeKey = new PropertyKey<float>("RotationTime");

		// Token: 0x04000049 RID: 73
		public static readonly PropertyKey<float> RotationDurationKey = new PropertyKey<float>("RotationDuration");

		// Token: 0x0400004B RID: 75
		public PlaneLauncherRotatorSpec _planeLauncherRotatorSpec;

		// Token: 0x0400004C RID: 76
		public AnimationCurve _rotationCurve;

		// Token: 0x0400004D RID: 77
		public Transform _rotatedElement;

		// Token: 0x0400004E RID: 78
		public float _remainingRotation;

		// Token: 0x0400004F RID: 79
		public float _loadedRotation;

		// Token: 0x04000050 RID: 80
		public float _rotationDuration;

		// Token: 0x04000051 RID: 81
		public float _rotationTime;
	}
}
