using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Persistence;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x02000007 RID: 7
	public class AnimateExecutor : BaseComponent, IAwakableComponent, IExecutor
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AnimateExecutor(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000210F File Offset: 0x0000030F
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._animator = base.GetComponentInChildren<IAnimator>(false);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000032C
		public void Launch(string animationName, float hours)
		{
			bool continuation = this._animationName == animationName;
			this._animationName = animationName;
			this._finishTimestamp = this._dayNightCycle.DayNumberHoursFromNow(hours);
			this.TurnOnAnimation(continuation);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002166 File Offset: 0x00000366
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (this._dayNightCycle.PartialDayNumber > this._finishTimestamp)
			{
				this.TurnOffAnimation();
				return ExecutorStatus.Success;
			}
			return ExecutorStatus.Running;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002184 File Offset: 0x00000384
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(AnimateExecutor.AnimateExecutorKey);
			component.Set(AnimateExecutor.AnimationNameKey, this._animationName);
			component.Set(AnimateExecutor.FinishTimestampKey, this._finishTimestamp);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B4 File Offset: 0x000003B4
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(AnimateExecutor.AnimateExecutorKey);
			this._animationName = component.Get(AnimateExecutor.AnimationNameKey);
			this._finishTimestamp = component.Get(AnimateExecutor.FinishTimestampKey);
			this.TurnOnAnimation(false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021F6 File Offset: 0x000003F6
		public void TurnOnAnimation(bool continuation = false)
		{
			this._characterAnimator.SetBool(this._animationName, true);
			if (continuation)
			{
				this._animator.SetTime(this._turnedOffTime);
			}
			this._turnedOffTime = 0f;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002229 File Offset: 0x00000429
		public void TurnOffAnimation()
		{
			this._turnedOffTime = this._animator.Time;
			this._characterAnimator.SetBool(this._animationName, false);
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey AnimateExecutorKey = new ComponentKey("AnimateExecutor");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<string> AnimationNameKey = new PropertyKey<string>("AnimationName");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<float> FinishTimestampKey = new PropertyKey<float>("FinishTimestamp");

		// Token: 0x0400000B RID: 11
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000C RID: 12
		public CharacterAnimator _characterAnimator;

		// Token: 0x0400000D RID: 13
		public IAnimator _animator;

		// Token: 0x0400000E RID: 14
		public string _animationName;

		// Token: 0x0400000F RID: 15
		public float _finishTimestamp;

		// Token: 0x04000010 RID: 16
		public float _turnedOffTime;
	}
}
