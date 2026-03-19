using System;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000015 RID: 21
	public class SpeedManager : IPostLoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002BBE File Offset: 0x00000DBE
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002BC6 File Offset: 0x00000DC6
		public float CurrentSpeed { get; private set; }

		// Token: 0x06000086 RID: 134 RVA: 0x00002BCF File Offset: 0x00000DCF
		public SpeedManager(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002BE9 File Offset: 0x00000DE9
		public void PostLoad()
		{
			this._eventBus.Post(new CurrentSpeedChangedEvent(this.CurrentSpeed));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002C01 File Offset: 0x00000E01
		public void LateUpdateSingleton()
		{
			this.ChangeSpeed();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002C0C File Offset: 0x00000E0C
		public void ChangeSpeedScale(float speedScale)
		{
			Asserts.ValueIsInRange<float>(speedScale, 0f, 1f, "speedScale");
			this._currentSpeedScale = speedScale;
			float value = this._nextSpeed.GetValueOrDefault();
			if (this._nextSpeed == null)
			{
				value = this.CurrentSpeed;
				this._nextSpeed = new float?(value);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002C61 File Offset: 0x00000E61
		public void ChangeSpeed(float speed)
		{
			if (!this._isLocked)
			{
				this._nextSpeed = new float?(speed);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002C77 File Offset: 0x00000E77
		public void ChangeAndLockSpeed(float value)
		{
			if (!this._isLocked)
			{
				this._speedBefore = this.CurrentSpeed;
				this.ChangeSpeed(value);
				this._isLocked = true;
				this._eventBus.Post(new SpeedLockChangedEvent(this._isLocked));
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002CB1 File Offset: 0x00000EB1
		public void UnlockSpeed()
		{
			if (this._isLocked)
			{
				this._isLocked = false;
				this.ChangeSpeed(this._speedBefore);
				this._eventBus.Post(new SpeedLockChangedEvent(this._isLocked));
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public void ChangeSpeed()
		{
			if (this._nextSpeed != null)
			{
				float value = this._nextSpeed.Value;
				Time.timeScale = this.ScaleSpeed(value);
				this.CurrentSpeed = value;
				this._nextSpeed = null;
				this._eventBus.Post(new CurrentSpeedChangedEvent(this.CurrentSpeed));
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002D3F File Offset: 0x00000F3F
		public float ScaleSpeed(float speed)
		{
			if (speed <= 1f)
			{
				return speed;
			}
			return Mathf.Lerp(1f, speed, this._currentSpeedScale);
		}

		// Token: 0x0400002B RID: 43
		public readonly EventBus _eventBus;

		// Token: 0x0400002C RID: 44
		public float _currentSpeedScale = 1f;

		// Token: 0x0400002D RID: 45
		public float? _nextSpeed;

		// Token: 0x0400002E RID: 46
		public float _speedBefore;

		// Token: 0x0400002F RID: 47
		public bool _isLocked;
	}
}
