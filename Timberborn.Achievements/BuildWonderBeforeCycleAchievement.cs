using System;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.GameCycleSystem;
using Timberborn.SingletonSystem;
using Timberborn.Wonders;

namespace Timberborn.Achievements
{
	// Token: 0x0200001D RID: 29
	public class BuildWonderBeforeCycleAchievement : Achievement
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003424 File Offset: 0x00001624
		public BuildWonderBeforeCycleAchievement(EventBus eventBus, GameCycleService gameCycleService)
		{
			this._eventBus = eventBus;
			this._gameCycleService = gameCycleService;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000343A File Offset: 0x0000163A
		public override string Id
		{
			get
			{
				return "BUILD_WONDER_BEFORE_CYCLE";
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003441 File Offset: 0x00001641
		[OnEvent]
		public void OnCycleStarted(CycleStartedEvent cycleStartedEvent)
		{
			if (this._gameCycleService.Cycle > BuildWonderBeforeCycleAchievement.ThresholdCycle)
			{
				base.Disable();
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000345B File Offset: 0x0000165B
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			if (this._gameCycleService.Cycle <= BuildWonderBeforeCycleAchievement.ThresholdCycle && enteredFinishedStateEvent.BlockObject.GetComponent<Wonder>())
			{
				base.Unlock();
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003487 File Offset: 0x00001687
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003495 File Offset: 0x00001695
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x04000046 RID: 70
		public static readonly int ThresholdCycle = 15;

		// Token: 0x04000047 RID: 71
		public readonly EventBus _eventBus;

		// Token: 0x04000048 RID: 72
		public readonly GameCycleService _gameCycleService;
	}
}
