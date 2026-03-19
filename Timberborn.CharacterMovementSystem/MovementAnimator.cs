using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x0200000E RID: 14
	public class MovementAnimator : BaseComponent, IAwakableComponent, IStartableComponent, IUpdatableComponent, IDeletableEntity, IPersistentEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000038 RID: 56 RVA: 0x000027D0 File Offset: 0x000009D0
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002808 File Offset: 0x00000A08
		public event EventHandler<AnimationUpdatedEventArgs> AnimationUpdated;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003A RID: 58 RVA: 0x00002840 File Offset: 0x00000A40
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00002878 File Offset: 0x00000A78
		public event EventHandler<GroupIdUpdatedEventArgs> GroupIdUpdated;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600003C RID: 60 RVA: 0x000028B0 File Offset: 0x00000AB0
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x000028E8 File Offset: 0x00000AE8
		public event EventHandler<float> XRotationUpdated;

		// Token: 0x0600003E RID: 62 RVA: 0x0000291D File Offset: 0x00000B1D
		public MovementAnimator(AnimatedPathFollower animatedPathFollower)
		{
			this._animatedPathFollower = animatedPathFollower;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000292C File Offset: 0x00000B2C
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._characterModel = base.GetComponent<CharacterModel>();
			this._characterRotator = base.GetComponent<CharacterRotator>();
			this._movementAnimatorSpec = base.GetComponent<MovementAnimatorSpec>();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000295E File Offset: 0x00000B5E
		public void Start()
		{
			this._characterRotator.Initialize(this._animatedPathFollower);
			this.AnimateMovementFromLoadedPositionToObjectPosition();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002977 File Offset: 0x00000B77
		public void Update()
		{
			if (Time.deltaTime > 0f)
			{
				this.Update(Time.deltaTime);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002990 File Offset: 0x00000B90
		public void DeleteEntity()
		{
			this.StopAnimatingMovement();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002998 File Offset: 0x00000B98
		public void SetPostLoadGroupId(int groupId)
		{
			this._groupId = groupId;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void AnimateMovementAlongPath(IEnumerable<AnimatedPathCorner> pathCorners, string animationName)
		{
			this._animatedPathFollower.SetNewPath(pathCorners);
			this._characterModel.Position = this._animatedPathFollower.CurrentPosition;
			this.StartAnimation(animationName, this._animatedPathFollower.CurrentSpeed);
			this.NotifyGroupIdUpdated();
			this.NotifyRotationUpdated();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029F1 File Offset: 0x00000BF1
		public void StopAnimatingMovement()
		{
			this._characterModel.ResetModelPosition();
			this._animatedPathFollower.Stop();
			this._characterRotator.ResetXRotation();
			this.StopAnimation();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A1C File Offset: 0x00000C1C
		public void Save(IEntitySaver entitySaver)
		{
			if (!this._animatedPathFollower.ReachedDestination())
			{
				IObjectSaver component = entitySaver.GetComponent(MovementAnimator.MovementAnimatorKey);
				component.Set(MovementAnimator.PositionKey, this._animatedPathFollower.CurrentPosition);
				float value = this._animatedPathFollower.LastCornerTime() - Time.time;
				component.Set(MovementAnimator.LeftTimeKey, value);
				component.Set(MovementAnimator.AnimationNameKey, this._animationName);
				component.Set(MovementAnimator.AnimationSpeedKey, this._animationSpeed);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A98 File Offset: 0x00000C98
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(MovementAnimator.MovementAnimatorKey, out objectLoader))
			{
				this._loadedPosition = new Vector3?(objectLoader.Get(MovementAnimator.PositionKey));
				this._loadedLeftTime = objectLoader.Get(MovementAnimator.LeftTimeKey);
				this._animationName = objectLoader.Get(MovementAnimator.AnimationNameKey);
				this._animationSpeed = objectLoader.Get(MovementAnimator.AnimationSpeedKey);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B00 File Offset: 0x00000D00
		public void AnimateMovementFromLoadedPositionToObjectPosition()
		{
			Vector3? loadedPosition = this._loadedPosition;
			if (loadedPosition != null)
			{
				Vector3 valueOrDefault = loadedPosition.GetValueOrDefault();
				this._characterModel.Position = valueOrDefault;
				float distanceToPathCorner = Vector3.Distance(base.Transform.position, valueOrDefault);
				List<AnimatedPathCorner> pathCorners = new List<AnimatedPathCorner>
				{
					new AnimatedPathCorner(valueOrDefault, Time.time, this._animationSpeed, distanceToPathCorner, this._groupId),
					new AnimatedPathCorner(base.Transform.position, Time.time + this._loadedLeftTime, this._animationSpeed, 0f, this._groupId)
				};
				this.AnimateMovementAlongPath(pathCorners, this._animationName);
				this.UpdateCharacterAnimator();
				this.NotifyAnimationUpdated();
				this.NotifyRotationUpdated();
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BBC File Offset: 0x00000DBC
		public void Update(float deltaTime)
		{
			this._animatedPathFollower.Update(Time.time);
			if (!this._animatedPathFollower.Stopped)
			{
				this.UpdateTransform(deltaTime);
				this.UpdateAnimationSpeed();
				this.UpdateGroupId();
			}
			this.NotifyAnimationUpdated();
			this.UpdateRotation();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BFC File Offset: 0x00000DFC
		public void NotifyAnimationUpdated()
		{
			float animationSpeed = this._animationSpeed * this._movementAnimatorSpec.AnimationSpeedScale;
			EventHandler<AnimationUpdatedEventArgs> animationUpdated = this.AnimationUpdated;
			if (animationUpdated == null)
			{
				return;
			}
			animationUpdated(this, new AnimationUpdatedEventArgs(animationSpeed));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C33 File Offset: 0x00000E33
		public void UpdateTransform(float deltaTime)
		{
			this._characterModel.Position = this._animatedPathFollower.CurrentPosition;
			this._characterModel.Rotation = this._characterRotator.GetCharacterRotation(deltaTime);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C64 File Offset: 0x00000E64
		public void UpdateAnimationSpeed()
		{
			float currentSpeed = this._animatedPathFollower.CurrentSpeed;
			if (this._animationSpeed != currentSpeed)
			{
				this._animationSpeed = currentSpeed;
				this.UpdateSpeedInCharacterAnimator();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C94 File Offset: 0x00000E94
		public void UpdateGroupId()
		{
			int currentGroupId = this._animatedPathFollower.CurrentGroupId;
			if (this._groupId != currentGroupId)
			{
				this._groupId = currentGroupId;
				this.NotifyGroupIdUpdated();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CC3 File Offset: 0x00000EC3
		public void NotifyGroupIdUpdated()
		{
			EventHandler<GroupIdUpdatedEventArgs> groupIdUpdated = this.GroupIdUpdated;
			if (groupIdUpdated == null)
			{
				return;
			}
			groupIdUpdated(this, new GroupIdUpdatedEventArgs(this._groupId));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public void UpdateRotation()
		{
			float currentXRotation = this._animatedPathFollower.CurrentXRotation;
			if (Mathf.Abs(this._xRotation - currentXRotation) > MovementAnimator.RotationNotificationThreshold)
			{
				this._xRotation = currentXRotation;
				this.NotifyRotationUpdated();
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D1E File Offset: 0x00000F1E
		public void NotifyRotationUpdated()
		{
			EventHandler<float> xrotationUpdated = this.XRotationUpdated;
			if (xrotationUpdated == null)
			{
				return;
			}
			xrotationUpdated(this, this._xRotation);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D37 File Offset: 0x00000F37
		public void StartAnimation(string animationName, float animationSpeed)
		{
			if (this._animationName != animationName || this._animationSpeed != animationSpeed)
			{
				this._animationName = animationName;
				this._animationSpeed = animationSpeed;
				this.UpdateCharacterAnimator();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D64 File Offset: 0x00000F64
		public void StopAnimation()
		{
			if (this._animationName != null)
			{
				this._characterAnimator.SetBool(this._animationName, false);
			}
			this._animationName = null;
			this._animationSpeed = 0f;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D92 File Offset: 0x00000F92
		public void UpdateCharacterAnimator()
		{
			this._characterAnimator.SetBool(this._animationName, true);
			this.UpdateSpeedInCharacterAnimator();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002DAC File Offset: 0x00000FAC
		public void UpdateSpeedInCharacterAnimator()
		{
			this._characterAnimator.SetFloat(MovementAnimator.AnimatorSpeedPropertyName, this._animationSpeed * this._movementAnimatorSpec.AnimationSpeedScale);
		}

		// Token: 0x04000022 RID: 34
		public static readonly float RotationNotificationThreshold = 0.01f;

		// Token: 0x04000023 RID: 35
		public static readonly string AnimatorSpeedPropertyName = "WalkingSpeed";

		// Token: 0x04000024 RID: 36
		public static readonly ComponentKey MovementAnimatorKey = new ComponentKey("MovementAnimator");

		// Token: 0x04000025 RID: 37
		public static readonly PropertyKey<Vector3> PositionKey = new PropertyKey<Vector3>("Position");

		// Token: 0x04000026 RID: 38
		public static readonly PropertyKey<float> LeftTimeKey = new PropertyKey<float>("LeftTimeInSeconds");

		// Token: 0x04000027 RID: 39
		public static readonly PropertyKey<string> AnimationNameKey = new PropertyKey<string>("AnimationName");

		// Token: 0x04000028 RID: 40
		public static readonly PropertyKey<float> AnimationSpeedKey = new PropertyKey<float>("AnimationSpeed");

		// Token: 0x0400002C RID: 44
		public readonly AnimatedPathFollower _animatedPathFollower;

		// Token: 0x0400002D RID: 45
		public CharacterAnimator _characterAnimator;

		// Token: 0x0400002E RID: 46
		public CharacterModel _characterModel;

		// Token: 0x0400002F RID: 47
		public CharacterRotator _characterRotator;

		// Token: 0x04000030 RID: 48
		public MovementAnimatorSpec _movementAnimatorSpec;

		// Token: 0x04000031 RID: 49
		public string _animationName;

		// Token: 0x04000032 RID: 50
		public float _animationSpeed;

		// Token: 0x04000033 RID: 51
		public int _groupId;

		// Token: 0x04000034 RID: 52
		public float _xRotation;

		// Token: 0x04000035 RID: 53
		public Vector3? _loadedPosition;

		// Token: 0x04000036 RID: 54
		public float _loadedLeftTime;
	}
}
