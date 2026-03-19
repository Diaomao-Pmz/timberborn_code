using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.CharacterMovementSystem;
using Timberborn.EntitySystem;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x02000007 RID: 7
	public class ZiplineCharacterAnimator : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._ziplineVisitor = base.GetComponent<ZiplineVisitor>();
			this._movementAnimator = base.GetComponent<MovementAnimator>();
			this._walkingEnforcerToggle = base.GetComponent<WalkingEnforcer>().GetWalkingEnforcerToggle();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public void InitializeEntity()
		{
			if (this._characterAnimator.HasParameter(ZiplineCharacterAnimator.AnimationName))
			{
				this._ziplineVisitor.EnteredZipline += this.OnZiplineEntered;
				this._ziplineVisitor.ExitedZipline += this.OnZiplineExited;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002185 File Offset: 0x00000385
		public void OnZiplineEntered(object sender, EventArgs e)
		{
			this._walkingEnforcerToggle.EnableForcedWalking();
			this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationName, true);
			this._movementAnimator.XRotationUpdated += this.OnXRotationUpdated;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BC File Offset: 0x000003BC
		public void OnZiplineExited(object sender, EventArgs e)
		{
			this._walkingEnforcerToggle.DisableForcedWalking();
			this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationName, false);
			this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationUpName, false);
			this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationDownName, false);
			this._movementAnimator.XRotationUpdated -= this.OnXRotationUpdated;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002220 File Offset: 0x00000420
		public void OnXRotationUpdated(object sender, float xRotation)
		{
			float num = (Mathf.Abs(xRotation) > 180f) ? (xRotation - Mathf.Sign(xRotation) * 360f) : xRotation;
			if (num > ZiplineCharacterAnimator.AngleThreshold)
			{
				this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationUpName, false);
				this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationDownName, true);
				return;
			}
			if (num < -ZiplineCharacterAnimator.AngleThreshold)
			{
				this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationUpName, true);
				this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationDownName, false);
				return;
			}
			this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationUpName, false);
			this._characterAnimator.SetBool(ZiplineCharacterAnimator.AnimationDownName, false);
		}

		// Token: 0x04000008 RID: 8
		public static readonly float AngleThreshold = 20f;

		// Token: 0x04000009 RID: 9
		public static readonly string AnimationName = "Zipline";

		// Token: 0x0400000A RID: 10
		public static readonly string AnimationUpName = "ZiplineUp";

		// Token: 0x0400000B RID: 11
		public static readonly string AnimationDownName = "ZiplineDown";

		// Token: 0x0400000C RID: 12
		public CharacterAnimator _characterAnimator;

		// Token: 0x0400000D RID: 13
		public ZiplineVisitor _ziplineVisitor;

		// Token: 0x0400000E RID: 14
		public MovementAnimator _movementAnimator;

		// Token: 0x0400000F RID: 15
		public WalkingEnforcerToggle _walkingEnforcerToggle;
	}
}
