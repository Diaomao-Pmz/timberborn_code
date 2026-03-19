using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.CharacterMovementSystem;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000016 RID: 22
	public class SwimmingAnimator : BaseComponent, IAwakableComponent, IPostLoadableEntity
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600005B RID: 91 RVA: 0x00002E14 File Offset: 0x00001014
		// (remove) Token: 0x0600005C RID: 92 RVA: 0x00002E4C File Offset: 0x0000104C
		public event EventHandler SwimmingStateChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600005D RID: 93 RVA: 0x00002E84 File Offset: 0x00001084
		// (remove) Token: 0x0600005E RID: 94 RVA: 0x00002EBC File Offset: 0x000010BC
		public event EventHandler UnderwaterStateChanged;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002EF1 File Offset: 0x000010F1
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002EF9 File Offset: 0x000010F9
		public bool IsSwimming { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002F02 File Offset: 0x00001102
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002F0A File Offset: 0x0000110A
		public bool IsUnderwater { get; private set; }

		// Token: 0x06000063 RID: 99 RVA: 0x00002F13 File Offset: 0x00001113
		public SwimmingAnimator(IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F24 File Offset: 0x00001124
		public void Awake()
		{
			this._characterModel = base.GetComponent<CharacterModel>();
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._swimmingAnimatorSpec = base.GetComponent<SwimmingAnimatorSpec>();
			base.GetComponent<NavMeshObserver>().PlacedOnNavMesh += delegate(object _, EventArgs _)
			{
				this.InstantlyUpdateSwimming();
			};
			base.GetComponent<MovementAnimator>().AnimationUpdated += delegate(object _, AnimationUpdatedEventArgs e)
			{
				this.UpdateSwimming(e.AnimationSpeed, false);
			};
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002F83 File Offset: 0x00001183
		public void PostLoadEntity()
		{
			if (base.Enabled)
			{
				this.InstantlyUpdateSwimming();
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F93 File Offset: 0x00001193
		public void BlockSwimmingMovement()
		{
			this._blockModel = true;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002F9C File Offset: 0x0000119C
		public void BlockSwimmingMovementAndResetPosition()
		{
			this.BlockSwimmingMovement();
			Vector3 position = this._characterModel.Position;
			Vector3 position2 = base.Transform.position;
			this._characterModel.Position = new Vector3(position.x, position2.y, position.z);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002FE9 File Offset: 0x000011E9
		public void UnblockSwimmingMovement()
		{
			this._blockModel = false;
			this.InstantlyUpdateSwimming();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002FF8 File Offset: 0x000011F8
		public Vector3Int Coordinates
		{
			get
			{
				return NavigationCoordinateSystem.WorldToGridInt(this._characterModel.Position);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000300A File Offset: 0x0000120A
		public void InstantlyUpdateSwimming()
		{
			this.UpdateSwimming(0f, true);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003018 File Offset: 0x00001218
		public void UpdateSwimming(float movementSpeed, bool fastForward = false)
		{
			float modelDepth = this.ModelDepth(movementSpeed);
			this.UpdateUnderwaterState(modelDepth);
			this.UpdateSwimmingState(modelDepth);
			this._characterAnimator.SetBool("Swimming", this.IsSwimming);
			this.UpdateModel(movementSpeed, modelDepth, fastForward);
			this._yModelPositionLastUpdate = this._characterModel.Position.y;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003070 File Offset: 0x00001270
		public float ModelDepth(float movementSpeed)
		{
			return this._threadSafeWaterMap.WaterHeightOrFloor(this.Coordinates) - this.YPosition(movementSpeed);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000308C File Offset: 0x0000128C
		public void UpdateUnderwaterState(float modelDepth)
		{
			if (this.IsUnderwater || modelDepth <= 0f)
			{
				if (this.IsUnderwater && modelDepth <= 0f)
				{
					this.IsUnderwater = false;
					EventHandler underwaterStateChanged = this.UnderwaterStateChanged;
					if (underwaterStateChanged == null)
					{
						return;
					}
					underwaterStateChanged(this, EventArgs.Empty);
				}
				return;
			}
			this.IsUnderwater = true;
			EventHandler underwaterStateChanged2 = this.UnderwaterStateChanged;
			if (underwaterStateChanged2 == null)
			{
				return;
			}
			underwaterStateChanged2(this, EventArgs.Empty);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030F4 File Offset: 0x000012F4
		public void UpdateSwimmingState(float modelDepth)
		{
			if (this.IsSwimming || modelDepth <= this._swimmingAnimatorSpec.UpperSwimmingDepthThreshold)
			{
				if (this.IsSwimming && modelDepth < this._swimmingAnimatorSpec.LowerSwimmingDepthThreshold)
				{
					this.IsSwimming = false;
					EventHandler swimmingStateChanged = this.SwimmingStateChanged;
					if (swimmingStateChanged == null)
					{
						return;
					}
					swimmingStateChanged(this, EventArgs.Empty);
				}
				return;
			}
			this.IsSwimming = true;
			EventHandler swimmingStateChanged2 = this.SwimmingStateChanged;
			if (swimmingStateChanged2 == null)
			{
				return;
			}
			swimmingStateChanged2(this, EventArgs.Empty);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003168 File Offset: 0x00001368
		public void UpdateModel(float movementSpeed, float modelDepth, bool fastForward)
		{
			if (this.IsSwimming)
			{
				this._updateModel = true;
			}
			if (this._updateModel && !this._blockModel)
			{
				float num = this.YPosition(movementSpeed) + this.SmoothOffset(modelDepth);
				float num2 = fastForward ? num : this.SmoothlyOffsetNewYPosition(num, movementSpeed);
				Vector3 position = this._characterModel.Position;
				this._characterModel.Position = new Vector3(position.x, num2, position.z);
				if ((double)Math.Abs(num - num2) < 0.0001 && !this.IsSwimming)
				{
					this._updateModel = false;
				}
				if (modelDepth < SwimmingAnimator.DivingDepthThreshold)
				{
					this._characterModel.Rotation = Quaternion.Euler(0f, this._characterModel.Rotation.eulerAngles.y, 0f);
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000323F File Offset: 0x0000143F
		public float YPosition(float movementSpeed)
		{
			if (movementSpeed <= 0f)
			{
				return base.Transform.position.y;
			}
			return this._characterModel.Position.y;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000326A File Offset: 0x0000146A
		public float SmoothOffset(float modelDepth)
		{
			if (!this.IsSwimming)
			{
				return 0f;
			}
			if (modelDepth >= SwimmingAnimator.DivingDepthThreshold)
			{
				return SwimmingAnimator.SmoothDivingOffset(modelDepth);
			}
			return modelDepth;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000328A File Offset: 0x0000148A
		public static float SmoothDivingOffset(float modelDepth)
		{
			return Math.Max(SwimmingAnimator.DivingDepthThreshold - (modelDepth - SwimmingAnimator.DivingDepthThreshold), SwimmingAnimator.MinDivingDepth);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032A4 File Offset: 0x000014A4
		public float SmoothlyOffsetNewYPosition(float targetYPosition, float movementSpeed)
		{
			float num = targetYPosition - this._yModelPositionLastUpdate;
			float num2 = Time.deltaTime * Math.Max(movementSpeed, SwimmingAnimator.MinOffsettingSpeed);
			float num3 = Mathf.Clamp(num, -num2, num2);
			return this._yModelPositionLastUpdate + num3;
		}

		// Token: 0x0400002F RID: 47
		public static readonly float DivingDepthThreshold = 0.9f;

		// Token: 0x04000030 RID: 48
		public static readonly float MinDivingDepth = 0.5f;

		// Token: 0x04000031 RID: 49
		public static readonly float MinOffsettingSpeed = 6f;

		// Token: 0x04000036 RID: 54
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000037 RID: 55
		public CharacterModel _characterModel;

		// Token: 0x04000038 RID: 56
		public CharacterAnimator _characterAnimator;

		// Token: 0x04000039 RID: 57
		public SwimmingAnimatorSpec _swimmingAnimatorSpec;

		// Token: 0x0400003A RID: 58
		public float _yModelPositionLastUpdate;

		// Token: 0x0400003B RID: 59
		public bool _updateModel;

		// Token: 0x0400003C RID: 60
		public bool _blockModel;
	}
}
