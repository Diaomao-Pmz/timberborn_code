using System;
using System.Collections.Generic;
using System.Linq;
using Bindito.Core;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000012 RID: 18
	public class NodeAnimationUpdater : MonoBehaviour, IAnimationUpdater
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002BD9 File Offset: 0x00000DD9
		[Inject]
		public void InjectDependencies(NodeAnimationCache nodeAnimationCache)
		{
			this._nodeAnimationCache = nodeAnimationCache;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public void Initialize()
		{
			this._animationsMap = this._nodeAnimationCache.GetAnimations(this._animationsId).ToDictionary((NodeAnimation anim) => anim.Name, (NodeAnimation anim) => anim);
			this._selfTransform = base.transform;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C57 File Offset: 0x00000E57
		public void AssignAnimationsId(int animationSetId)
		{
			this._animationsId = animationSetId;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002C60 File Offset: 0x00000E60
		public void SetAnimation(string animationName, bool looped)
		{
			this._currentAnimation = null;
			NodeAnimation currentAnimation;
			if (this._animationsMap.TryGetValue(animationName, out currentAnimation))
			{
				this._currentAnimation = currentAnimation;
				this._looped = looped;
				this.ResetAnimationToInitialState();
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C98 File Offset: 0x00000E98
		public void UpdateAnimation(float normalizedTime)
		{
			if (this._currentAnimation != null)
			{
				int frameCount = this._currentAnimation.FrameCount;
				if (this._looped)
				{
					int num = Mathf.FloorToInt(normalizedTime * (float)frameCount) % frameCount;
					int toFrame = (num + 1) % frameCount;
					float weight = normalizedTime * (float)frameCount % 1f;
					this.UpdateTransform(num, toFrame, weight);
					return;
				}
				int num2 = frameCount - 1;
				int num3 = Mathf.FloorToInt(normalizedTime * (float)num2);
				int toFrame2 = Math.Clamp(num3 + 1, 0, num2);
				float weight2 = normalizedTime * (float)num2 % 1f;
				this.UpdateTransform(num3, toFrame2, weight2);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002D20 File Offset: 0x00000F20
		public void ResetAnimationToInitialState()
		{
			this._selfTransform.SetLocalPositionAndRotation(this._currentAnimation.GetPositionUnsafe(0), this._currentAnimation.GetRotationUnsafe(0));
			this._selfTransform.localScale = this._currentAnimation.GetScaleUnsafe(0);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002D5C File Offset: 0x00000F5C
		public void UpdateTransform(int fromFrame, int toFrame, float weight)
		{
			int fromFrame2 = Math.Clamp(fromFrame, 0, this._currentAnimation.FrameCount - 1);
			int toFrame2 = Math.Clamp(toFrame, 0, this._currentAnimation.FrameCount - 1);
			if (this._currentAnimation.HasDifferentPositions && this._currentAnimation.HasDifferentRotations)
			{
				this._selfTransform.SetLocalPositionAndRotation(this.GetPosition(fromFrame2, toFrame2, weight), this.GetRotation(fromFrame2, toFrame2, weight));
			}
			else if (this._currentAnimation.HasDifferentPositions)
			{
				this._selfTransform.localPosition = this.GetPosition(fromFrame2, toFrame2, weight);
			}
			else if (this._currentAnimation.HasDifferentRotations)
			{
				this._selfTransform.localRotation = this.GetRotation(fromFrame2, toFrame2, weight);
			}
			if (this._currentAnimation.HasDifferentScales)
			{
				this._selfTransform.localScale = this.GetScale(fromFrame2, toFrame2, weight);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E34 File Offset: 0x00001034
		public Vector3 GetPosition(int fromFrame, int toFrame, float weight)
		{
			Vector3 positionUnsafe = this._currentAnimation.GetPositionUnsafe(fromFrame);
			Vector3 positionUnsafe2 = this._currentAnimation.GetPositionUnsafe(toFrame);
			return Vector3.Lerp(positionUnsafe, positionUnsafe2, weight);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E64 File Offset: 0x00001064
		public Quaternion GetRotation(int fromFrame, int toFrame, float weight)
		{
			Quaternion rotationUnsafe = this._currentAnimation.GetRotationUnsafe(fromFrame);
			Quaternion rotationUnsafe2 = this._currentAnimation.GetRotationUnsafe(toFrame);
			return Quaternion.Lerp(rotationUnsafe, rotationUnsafe2, weight);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002E94 File Offset: 0x00001094
		public Vector3 GetScale(int fromFrame, int toFrame, float weight)
		{
			Vector3 scaleUnsafe = this._currentAnimation.GetScaleUnsafe(fromFrame);
			Vector3 scaleUnsafe2 = this._currentAnimation.GetScaleUnsafe(toFrame);
			return Vector3.Lerp(scaleUnsafe, scaleUnsafe2, weight);
		}

		// Token: 0x04000025 RID: 37
		[HideInInspector]
		[SerializeField]
		public int _animationsId;

		// Token: 0x04000026 RID: 38
		[HideInInspector]
		public NodeAnimationCache _nodeAnimationCache;

		// Token: 0x04000027 RID: 39
		[HideInInspector]
		public Dictionary<string, NodeAnimation> _animationsMap;

		// Token: 0x04000028 RID: 40
		[HideInInspector]
		public NodeAnimation _currentAnimation;

		// Token: 0x04000029 RID: 41
		[HideInInspector]
		public bool _looped;

		// Token: 0x0400002A RID: 42
		[HideInInspector]
		public Transform _selfTransform;
	}
}
