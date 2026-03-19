using System;
using System.Linq;
using Timberborn.LevelVisibilitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200005C RID: 92
	public class VisibleLevelChangeService : ILoadableSingleton
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00007388 File Offset: 0x00005588
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00007390 File Offset: 0x00005590
		public bool WasAtZero { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00007399 File Offset: 0x00005599
		// (set) Token: 0x06000273 RID: 627 RVA: 0x000073A1 File Offset: 0x000055A1
		public int LevelsIncreasedSinceZero { get; private set; }

		// Token: 0x06000274 RID: 628 RVA: 0x000073AA File Offset: 0x000055AA
		public VisibleLevelChangeService(ILevelVisibilityService levelVisibilityService, EventBus eventBus)
		{
			this._levelVisibilityService = levelVisibilityService;
			this._eventBus = eventBus;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000073C0 File Offset: 0x000055C0
		public bool IsAtMax
		{
			get
			{
				return this._levelVisibilityService.LevelIsAtMax;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000073CD File Offset: 0x000055CD
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000073DC File Offset: 0x000055DC
		[OnEvent]
		public void OnTutorialStageStarted(TutorialStageStartedEvent tutorialStageStartedEvent)
		{
			if (tutorialStageStartedEvent.TutorialStage.TutorialSteps.Any((TutorialStep step) => step.Step is VisibleLevelChangeStep))
			{
				this.LevelsIncreasedSinceZero = 0;
				this.WasAtZero = false;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00007428 File Offset: 0x00005628
		[OnEvent]
		public void OnMaxVisibleLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			int num = this._levelVisibilityService.MaxVisibleLevel - maxVisibleLevelChangedEvent.OldMaxVisibleLevel;
			this.WasAtZero = (this.WasAtZero || this._levelVisibilityService.MaxVisibleLevel == 0);
			if (this.WasAtZero && num > 0)
			{
				this.LevelsIncreasedSinceZero += num;
			}
		}

		// Token: 0x04000135 RID: 309
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000136 RID: 310
		public readonly EventBus _eventBus;
	}
}
