using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.TimeSystem;
using Timberborn.Workshops;
using UnityEngine;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x0200000C RID: 12
	public class ObservatoryAnimator : BaseComponent, IAwakableComponent, IUpdatableComponent, IFinishedStateListener
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002795 File Offset: 0x00000995
		public ObservatoryAnimator(NonlinearAnimationManager nonlinearAnimationManager, IRandomNumberGenerator randomNumberGenerator, IDayNightCycle dayNightCycle)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._randomNumberGenerator = randomNumberGenerator;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027B4 File Offset: 0x000009B4
		public void Awake()
		{
			ObservatoryAnimatorSpec component = base.GetComponent<ObservatoryAnimatorSpec>();
			this._dome = base.GameObject.FindChildTransform(component.DomeName);
			this._telescope = base.GameObject.FindChildTransform(component.TelescopeName);
			this._workshop = base.GetComponent<Workshop>();
			base.DisableComponent();
			this.GenerateRandomAngles();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000280E File Offset: 0x00000A0E
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002816 File Offset: 0x00000A16
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000281E File Offset: 0x00000A1E
		public void Update()
		{
			if (this._dayNightCycle.PartialDayNumber >= this._nextGenerationTime)
			{
				this.GenerateRandomAngles();
			}
			this.UpdateAnimation();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002840 File Offset: 0x00000A40
		public void GenerateRandomAngles()
		{
			float num = this._randomNumberGenerator.Range(ObservatoryAnimator.MinDomeRotationAngle, ObservatoryAnimator.MaxDomeRotationAngle);
			float num2 = this._randomNumberGenerator.Range(ObservatoryAnimator.MinTelescopeRotationAngle, ObservatoryAnimator.MaxTelescopeRotationAngle);
			this._targetDomeRotation = Quaternion.Euler(0f, num, 0f);
			this._targetTelescopeRotation = Quaternion.Euler(-num2, 0f, 0f);
			this._nextGenerationTime = this._dayNightCycle.DayNumberHoursFromNow(this._randomNumberGenerator.Range(ObservatoryAnimator.MinGenerationInterval, ObservatoryAnimator.MaxGenerationInterval));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000028CC File Offset: 0x00000ACC
		public void UpdateAnimation()
		{
			if (this._workshop.CurrentlyWorking)
			{
				float deltaTime = Time.deltaTime * this._nonlinearAnimationManager.SpeedMultiplier;
				this.RotateDome(deltaTime);
				this.RotateTelescope(deltaTime);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002906 File Offset: 0x00000B06
		public void RotateDome(float deltaTime)
		{
			this._dome.localRotation = Quaternion.RotateTowards(this._dome.localRotation, this._targetDomeRotation, deltaTime * ObservatoryAnimator.DomeRotationSpeed);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002930 File Offset: 0x00000B30
		public void RotateTelescope(float deltaTime)
		{
			this._telescope.localRotation = Quaternion.RotateTowards(this._telescope.localRotation, this._targetTelescopeRotation, deltaTime * ObservatoryAnimator.TelescopeRotationSpeed);
		}

		// Token: 0x04000012 RID: 18
		public static readonly float DomeRotationSpeed = 40f;

		// Token: 0x04000013 RID: 19
		public static readonly float TelescopeRotationSpeed = 25f;

		// Token: 0x04000014 RID: 20
		public static readonly float MinDomeRotationAngle = 0f;

		// Token: 0x04000015 RID: 21
		public static readonly float MaxDomeRotationAngle = 360f;

		// Token: 0x04000016 RID: 22
		public static readonly float MinTelescopeRotationAngle = 5f;

		// Token: 0x04000017 RID: 23
		public static readonly float MaxTelescopeRotationAngle = 70f;

		// Token: 0x04000018 RID: 24
		public static readonly float MinGenerationInterval = 1f;

		// Token: 0x04000019 RID: 25
		public static readonly float MaxGenerationInterval = 3f;

		// Token: 0x0400001A RID: 26
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x0400001B RID: 27
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400001C RID: 28
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400001D RID: 29
		public Workshop _workshop;

		// Token: 0x0400001E RID: 30
		public Transform _dome;

		// Token: 0x0400001F RID: 31
		public Transform _telescope;

		// Token: 0x04000020 RID: 32
		public Quaternion _targetDomeRotation;

		// Token: 0x04000021 RID: 33
		public Quaternion _targetTelescopeRotation;

		// Token: 0x04000022 RID: 34
		public float _nextGenerationTime;
	}
}
