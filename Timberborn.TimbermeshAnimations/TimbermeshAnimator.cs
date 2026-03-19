using System;
using System.Collections.Generic;
using System.Linq;
using Bindito.Core;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000015 RID: 21
	public class TimbermeshAnimator : MonoBehaviour, IAnimator
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000078 RID: 120 RVA: 0x00002F80 File Offset: 0x00001180
		// (remove) Token: 0x06000079 RID: 121 RVA: 0x00002FB8 File Offset: 0x000011B8
		public event EventHandler AnimationChanged;

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002FED File Offset: 0x000011ED
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002FF5 File Offset: 0x000011F5
		public bool Enabled { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002FFE File Offset: 0x000011FE
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003006 File Offset: 0x00001206
		public float Time { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000300F File Offset: 0x0000120F
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003017 File Offset: 0x00001217
		public float RepeatedTime { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003020 File Offset: 0x00001220
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003028 File Offset: 0x00001228
		public bool PlayingFinished { get; private set; }

		// Token: 0x06000082 RID: 130 RVA: 0x00003031 File Offset: 0x00001231
		[Inject]
		public void InjectDependencies(AnimatorRegistry animatorRegistry)
		{
			this._animatorRegistry = animatorRegistry;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000303A File Offset: 0x0000123A
		public float AnimationLength
		{
			get
			{
				AnimationMetadata currentAnimation = this._currentAnimation;
				if (currentAnimation == null)
				{
					return 0f;
				}
				return currentAnimation.Length;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003051 File Offset: 0x00001251
		public string AnimationName
		{
			get
			{
				AnimationMetadata currentAnimation = this._currentAnimation;
				if (currentAnimation == null)
				{
					return null;
				}
				return currentAnimation.Name;
			}
		}

		// Token: 0x17000021 RID: 33
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003064 File Offset: 0x00001264
		public bool PlayBackwards
		{
			set
			{
				this._playBackwards = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000306D File Offset: 0x0000126D
		public float Speed
		{
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value))
				{
					this._speed = 0f;
					return;
				}
				this._speed = value;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003094 File Offset: 0x00001294
		public void Awake()
		{
			this._animationUpdaters = base.GetComponentsInChildren<IAnimationUpdater>(true);
			this._animationMap = this._animations.ToDictionary((AnimationMetadata anim) => anim.Name, (AnimationMetadata anim) => anim);
			this.InitializeAnimationUpdaters();
			this.Play(this._animations.First<AnimationMetadata>().Name, true);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000311A File Offset: 0x0000131A
		public void Start()
		{
			this._animatorRegistry.Add(this);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003128 File Offset: 0x00001328
		public void OnDestroy()
		{
			this._animatorRegistry.Remove(this);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003136 File Offset: 0x00001336
		public void SetTime(float time)
		{
			if (this._currentAnimation != null)
			{
				this.Time = time;
				this.UpdateAnimationProgress();
				this.UpdateAnimationUpdaters();
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003154 File Offset: 0x00001354
		public bool HasAnimation(string animationName)
		{
			for (int i = 0; i < this._animations.Count; i++)
			{
				if (this._animations[i].Name == animationName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003193 File Offset: 0x00001393
		public void SetAnimations(IEnumerable<AnimationMetadata> animations)
		{
			this._animations = new List<AnimationMetadata>(animations);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000031A1 File Offset: 0x000013A1
		public void Play(string animationName, bool looped = true)
		{
			this.UpdateInterruptionState(animationName);
			this.SetAnimation(animationName, looped);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000031B2 File Offset: 0x000013B2
		public void Stop()
		{
			this.Time = 0f;
			this.RepeatedTime = 0f;
			this._looped = false;
			this._currentAnimation = null;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000031D8 File Offset: 0x000013D8
		public void UpdateAnimation(float deltaTime)
		{
			if (this.Enabled && this._speed != 0f && this._currentAnimation != null && !this.PlayingFinished)
			{
				this.UpdateTime(deltaTime);
				this.UpdateAnimationUpdaters();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000320C File Offset: 0x0000140C
		public void InitializeAnimationUpdaters()
		{
			for (int i = 0; i < this._animationUpdaters.Length; i++)
			{
				this._animationUpdaters[i].Initialize();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000323C File Offset: 0x0000143C
		public void UpdateInterruptionState(string animationName)
		{
			int frameCount = UnityEngine.Time.frameCount;
			bool flag = this._currentAnimation != null && !this.PlayingFinished;
			if (frameCount > this._interruptionFrame && flag)
			{
				this._interruptionTime = this.Time;
				this._interruptionRepeatedTime = this.RepeatedTime;
				this._interruptionFrame = frameCount;
				this._interruptionAnimation = this.AnimationName;
				this.Stop();
				return;
			}
			this.Stop();
			if (frameCount == this._interruptionFrame && animationName == this._interruptionAnimation)
			{
				this.Time = this._interruptionTime;
				this.RepeatedTime = this._interruptionRepeatedTime;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000032D8 File Offset: 0x000014D8
		public void SetAnimation(string animationName, bool looped)
		{
			AnimationMetadata currentAnimation;
			if (!this._animationMap.TryGetValue(animationName, out currentAnimation))
			{
				throw new Exception(string.Concat(new string[]
				{
					"Animation ",
					animationName,
					" not found in ",
					base.gameObject.name,
					" animator."
				}));
			}
			this._currentAnimation = currentAnimation;
			this._looped = looped;
			this.PlayingFinished = false;
			for (int i = 0; i < this._animationUpdaters.Length; i++)
			{
				this._animationUpdaters[i].SetAnimation(animationName, this._looped);
			}
			this.UpdateAnimationUpdaters();
			EventHandler animationChanged = this.AnimationChanged;
			if (animationChanged == null)
			{
				return;
			}
			animationChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003388 File Offset: 0x00001588
		public void UpdateTime(float deltaTime)
		{
			this.Time += deltaTime * this._speed;
			this.UpdateAnimationProgress();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000033A8 File Offset: 0x000015A8
		public void UpdateAnimationProgress()
		{
			float length = this._currentAnimation.Length;
			if (!this._looped && this.Time >= length)
			{
				this.Time = (this.RepeatedTime = length);
				this.PlayingFinished = true;
				return;
			}
			this.RepeatedTime = Mathf.Repeat(this.Time, length);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000033FC File Offset: 0x000015FC
		public void UpdateAnimationUpdaters()
		{
			float num = Mathf.Clamp01(this.RepeatedTime / this._currentAnimation.Length);
			float normalizedTime = this._playBackwards ? (1f - num) : num;
			for (int i = 0; i < this._animationUpdaters.Length; i++)
			{
				this._animationUpdaters[i].UpdateAnimation(normalizedTime);
			}
		}

		// Token: 0x0400002E RID: 46
		[HideInInspector]
		[SerializeField]
		public List<AnimationMetadata> _animations;

		// Token: 0x0400002F RID: 47
		[HideInInspector]
		[SerializeField]
		public bool _playBackwards;

		// Token: 0x04000035 RID: 53
		[HideInInspector]
		public AnimatorRegistry _animatorRegistry;

		// Token: 0x04000036 RID: 54
		[HideInInspector]
		public IAnimationUpdater[] _animationUpdaters;

		// Token: 0x04000037 RID: 55
		[HideInInspector]
		public Dictionary<string, AnimationMetadata> _animationMap;

		// Token: 0x04000038 RID: 56
		[HideInInspector]
		public AnimationMetadata _currentAnimation;

		// Token: 0x04000039 RID: 57
		[HideInInspector]
		public float _speed = 1f;

		// Token: 0x0400003A RID: 58
		[HideInInspector]
		public bool _looped;

		// Token: 0x0400003B RID: 59
		[HideInInspector]
		public float _interruptionTime;

		// Token: 0x0400003C RID: 60
		[HideInInspector]
		public float _interruptionRepeatedTime;

		// Token: 0x0400003D RID: 61
		[HideInInspector]
		public int _interruptionFrame;

		// Token: 0x0400003E RID: 62
		[HideInInspector]
		public string _interruptionAnimation;
	}
}
