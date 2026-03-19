using System;
using Timberborn.GameCycleSystem;
using Timberborn.Population;
using Timberborn.SingletonSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000057 RID: 87
	public class UnemployedBeaversTrigger : ILoadableSingleton
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00006FD6 File Offset: 0x000051D6
		public UnemployedBeaversTrigger(EventBus eventBus, ITutorialTriggers tutorialTriggers, PopulationService populationService, GameCycleService gameCycleService)
		{
			this._eventBus = eventBus;
			this._tutorialTriggers = tutorialTriggers;
			this._populationService = populationService;
			this._gameCycleService = gameCycleService;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00006FFB File Offset: 0x000051FB
		public void Load()
		{
			if (this._tutorialTriggers.TriggerPending(UnemployedBeaversTrigger.TriggerId))
			{
				this._eventBus.Register(this);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000701C File Offset: 0x0000521C
		[OnEvent]
		public void OnPopulationChangedEvent(PopulationChangedEvent populationChangedEvent)
		{
			PopulationData globalPopulationData = this._populationService.GlobalPopulationData;
			if (this._gameCycleService.Cycle >= UnemployedBeaversTrigger.CycleThreshold && (globalPopulationData.NumberOfAdults >= UnemployedBeaversTrigger.AdultBeaverThreshold || globalPopulationData.BeaverWorkplaceData.Unemployed >= UnemployedBeaversTrigger.UnemployedBeaverThreshold))
			{
				this._eventBus.Unregister(this);
				this._tutorialTriggers.AddTrigger(UnemployedBeaversTrigger.TriggerId);
			}
		}

		// Token: 0x0400011D RID: 285
		public static readonly int CycleThreshold = 3;

		// Token: 0x0400011E RID: 286
		public static readonly int UnemployedBeaverThreshold = 4;

		// Token: 0x0400011F RID: 287
		public static readonly int AdultBeaverThreshold = 40;

		// Token: 0x04000120 RID: 288
		public static readonly string TriggerId = "UnemployedBeaversTrigger";

		// Token: 0x04000121 RID: 289
		public readonly EventBus _eventBus;

		// Token: 0x04000122 RID: 290
		public readonly ITutorialTriggers _tutorialTriggers;

		// Token: 0x04000123 RID: 291
		public readonly PopulationService _populationService;

		// Token: 0x04000124 RID: 292
		public readonly GameCycleService _gameCycleService;
	}
}
