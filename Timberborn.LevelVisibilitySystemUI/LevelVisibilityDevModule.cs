using System;
using Timberborn.Debugging;
using Timberborn.LevelVisibilitySystem;
using Timberborn.QuickNotificationSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.LevelVisibilitySystemUI
{
	// Token: 0x02000005 RID: 5
	public class LevelVisibilityDevModule : IDevModule, IUpdatableSingleton
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020BE File Offset: 0x000002BE
		public LevelVisibilityDevModule(ILevelVisibilityService levelVisibilityService, QuickNotificationService quickNotificationService)
		{
			this._levelVisibilityService = levelVisibilityService;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Auto move levels up", delegate
			{
				this.ToggleState(LevelVisibilityDevModule.AutoMoveState.Up);
			})).AddMethod(DevMethod.Create("Auto move levels down", delegate
			{
				this.ToggleState(LevelVisibilityDevModule.AutoMoveState.Down);
			})).Build();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002121 File Offset: 0x00000321
		public void UpdateSingleton()
		{
			if (this._state != LevelVisibilityDevModule.AutoMoveState.Disabled)
			{
				this.Update();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002134 File Offset: 0x00000334
		public void ToggleState(LevelVisibilityDevModule.AutoMoveState state)
		{
			this.Reset();
			this._state = state;
			string text = (this._state == LevelVisibilityDevModule.AutoMoveState.Disabled) ? "Auto move levels disabled" : string.Format("Auto move levels enabled\nWarmup time: {0} seconds", LevelVisibilityDevModule.WarmupTimeInSeconds);
			this._quickNotificationService.SendNotification(text);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217E File Offset: 0x0000037E
		public void Update()
		{
			if ((this._state == LevelVisibilityDevModule.AutoMoveState.Up && !this._levelVisibilityService.LevelIsAtMax) || (this._state == LevelVisibilityDevModule.AutoMoveState.Down && !this._levelVisibilityService.LevelIsAtMin))
			{
				this.UpdateLevels();
				return;
			}
			this.Reset();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021BC File Offset: 0x000003BC
		public void UpdateLevels()
		{
			if (this._timeToNextChange < 0f)
			{
				int num = (this._state == LevelVisibilityDevModule.AutoMoveState.Up) ? 1 : -1;
				this._levelVisibilityService.SetMaxVisibleLevel(this._levelVisibilityService.MaxVisibleLevel + num);
				this._timeToNextChange = LevelVisibilityDevModule.ChangeIntervalInSeconds;
				return;
			}
			this._timeToNextChange -= Time.unscaledDeltaTime;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000221A File Offset: 0x0000041A
		public void Reset()
		{
			this._state = LevelVisibilityDevModule.AutoMoveState.Disabled;
			this._timeToNextChange = LevelVisibilityDevModule.WarmupTimeInSeconds;
		}

		// Token: 0x04000006 RID: 6
		public static readonly float WarmupTimeInSeconds = 5f;

		// Token: 0x04000007 RID: 7
		public static readonly float ChangeIntervalInSeconds = 0.5f;

		// Token: 0x04000008 RID: 8
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000009 RID: 9
		public readonly QuickNotificationService _quickNotificationService;

		// Token: 0x0400000A RID: 10
		public LevelVisibilityDevModule.AutoMoveState _state;

		// Token: 0x0400000B RID: 11
		public float _timeToNextChange;

		// Token: 0x02000006 RID: 6
		public enum AutoMoveState
		{
			// Token: 0x0400000D RID: 13
			Disabled,
			// Token: 0x0400000E RID: 14
			Up,
			// Token: 0x0400000F RID: 15
			Down
		}
	}
}
