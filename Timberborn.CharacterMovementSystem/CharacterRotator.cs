using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x0200000B RID: 11
	public class CharacterRotator : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000024F0 File Offset: 0x000006F0
		public CharacterRotator(NavMeshGroupService navMeshGroupService)
		{
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024FF File Offset: 0x000006FF
		public void Awake()
		{
			this._characterModel = base.GetComponent<CharacterModel>();
			this._runningProhibitor = base.GetComponent<RunningProhibitor>();
			this._movementSpeedAffector = base.GetComponent<IMovementSpeedAffector>();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002525 File Offset: 0x00000725
		public void Initialize(AnimatedPathFollower animatedPathFollower)
		{
			this._animatedPathFollower = animatedPathFollower;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000252E File Offset: 0x0000072E
		public Quaternion GetCharacterRotation(float deltaTime)
		{
			return Quaternion.Euler(this.GetXRotation(deltaTime), this.GetYRotation(deltaTime), 0f);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002548 File Offset: 0x00000748
		public void ResetXRotation()
		{
			this._characterModel.Rotation = Quaternion.Euler(0f, this._characterModel.Rotation.eulerAngles.y, 0f);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002588 File Offset: 0x00000788
		public float GetXRotation(float deltaTime)
		{
			float num = this.IsRunning() ? this._animatedPathFollower.CurrentXRotation : 0f;
			float x = this._characterModel.Rotation.eulerAngles.x;
			float num2 = this._animatedPathFollower.CurrentSpeed / CharacterRotator.MovementSpeedInfluence;
			float num3 = CharacterRotator.XRotationSpeed * Mathf.Max(1f, num2);
			return Mathf.MoveTowardsAngle(x, num, deltaTime * num3);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025F8 File Offset: 0x000007F8
		public float GetYRotation(float deltaTime)
		{
			float y = this._characterModel.Rotation.eulerAngles.y;
			Vector3 currentDirection = this._animatedPathFollower.CurrentDirection;
			if (!currentDirection.Equals(Vector3.zero) && !currentDirection.Equals(Vector3.down) && !currentDirection.Equals(Vector3.up))
			{
				float num = CharacterRotator.MinimizeRotation(Quaternion.LookRotation(currentDirection).eulerAngles.y - y);
				float num2 = this.CalculateYRotationSpeed(num) * deltaTime;
				float num3 = Mathf.Clamp(num, -num2, num2);
				return y + num3;
			}
			return y;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002690 File Offset: 0x00000890
		public float CalculateYRotationSpeed(float angleToTarget)
		{
			float num = Mathf.Abs(angleToTarget);
			float num2 = this._animatedPathFollower.CurrentSpeed / CharacterRotator.MovementSpeedInfluence;
			float num3 = CharacterRotator.YRotationSpeed * Mathf.Max(1f, num2);
			float currentDistanceToPathCorner = this._animatedPathFollower.CurrentDistanceToPathCorner;
			float num4 = num / num3;
			float num5 = currentDistanceToPathCorner / this._animatedPathFollower.CurrentSpeed;
			if (currentDistanceToPathCorner <= CharacterRotator.CornerSnapMaxDistance && num <= CharacterRotator.CornerSnapMaxAngle && num5 > 0f && this._animatedPathFollower.CurrentGroupId == this._navMeshGroupService.GetDefaultGroupId())
			{
				return num / num5;
			}
			if (num5 < num4 && num5 > 0f)
			{
				return Mathf.Max(num3, num3 * (num4 / num5));
			}
			return num3;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002738 File Offset: 0x00000938
		public bool IsRunning()
		{
			IMovementSpeedAffector movementSpeedAffector = this._movementSpeedAffector;
			bool flag = movementSpeedAffector != null && movementSpeedAffector.IsMovementSlowed;
			return !this._runningProhibitor.RunningProhibited && !flag;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000276B File Offset: 0x0000096B
		public static float MinimizeRotation(float rotation)
		{
			if (Mathf.Abs(rotation) <= 180f)
			{
				return rotation;
			}
			return rotation - Mathf.Sign(rotation) * 360f;
		}

		// Token: 0x04000017 RID: 23
		public static readonly float XRotationSpeed = 330f;

		// Token: 0x04000018 RID: 24
		public static readonly float YRotationSpeed = 360f;

		// Token: 0x04000019 RID: 25
		public static readonly float MovementSpeedInfluence = 2.7f;

		// Token: 0x0400001A RID: 26
		public static readonly float CornerSnapMaxAngle = 80f;

		// Token: 0x0400001B RID: 27
		public static readonly float CornerSnapMaxDistance = 0.9f;

		// Token: 0x0400001C RID: 28
		public readonly NavMeshGroupService _navMeshGroupService;

		// Token: 0x0400001D RID: 29
		public CharacterModel _characterModel;

		// Token: 0x0400001E RID: 30
		public RunningProhibitor _runningProhibitor;

		// Token: 0x0400001F RID: 31
		public IMovementSpeedAffector _movementSpeedAffector;

		// Token: 0x04000020 RID: 32
		public AnimatedPathFollower _animatedPathFollower;
	}
}
