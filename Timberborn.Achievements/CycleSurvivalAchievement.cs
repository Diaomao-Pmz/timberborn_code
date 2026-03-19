using System;
using Timberborn.AchievementSystem;
using Timberborn.GameCycleSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200001F RID: 31
	public abstract class CycleSurvivalAchievement : Achievement
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000350F File Offset: 0x0000170F
		public CycleSurvivalAchievement(EventBus eventBus, GameCycleService gameCycleService, int thresholdCycle)
		{
			this._eventBus = eventBus;
			this._gameCycleService = gameCycleService;
			this._thresholdCycle = thresholdCycle;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000352C File Offset: 0x0000172C
		public override string Id
		{
			get
			{
				return string.Format("SURVIVE_{0}_CYCLES", this._thresholdCycle);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003543 File Offset: 0x00001743
		[OnEvent]
		public void OnCycleStarted(CycleStartedEvent cycleStartedEvent)
		{
			if (this.CycleIsAboveThreshold)
			{
				base.Unlock();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003553 File Offset: 0x00001753
		public override void EnableInternal()
		{
			if (this.CycleIsAboveThreshold)
			{
				base.Unlock();
				return;
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003570 File Offset: 0x00001770
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000357E File Offset: 0x0000177E
		public bool CycleIsAboveThreshold
		{
			get
			{
				return this._gameCycleService.Cycle > this._thresholdCycle;
			}
		}

		// Token: 0x0400004A RID: 74
		public readonly EventBus _eventBus;

		// Token: 0x0400004B RID: 75
		public readonly GameCycleService _gameCycleService;

		// Token: 0x0400004C RID: 76
		public readonly int _thresholdCycle;
	}
}
