using System;
using Timberborn.GameCycleSystem;
using Timberborn.SingletonSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000038 RID: 56
	public class MissingDamTrigger : ILoadableSingleton
	{
		// Token: 0x0600018B RID: 395 RVA: 0x0000544A File Offset: 0x0000364A
		public MissingDamTrigger(EventBus eventBus, ITutorialTriggers tutorialTriggers, BuiltBuildingService builtBuildingService)
		{
			this._eventBus = eventBus;
			this._tutorialTriggers = tutorialTriggers;
			this._builtBuildingService = builtBuildingService;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005467 File Offset: 0x00003667
		public void Load()
		{
			if (this._tutorialTriggers.TriggerPending(MissingDamTrigger.TriggerId))
			{
				this._eventBus.Register(this);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005488 File Offset: 0x00003688
		[OnEvent]
		public void OnCycleEnded(CycleEndedEvent cycleEndedEvent)
		{
			if (cycleEndedEvent.Cycle == 3 && this._builtBuildingService.NumberOfAllBuildings(new string[]
			{
				"Dam.Folktails",
				"Floodgate.Folktails",
				"DoubleFloodgate.Folktails",
				"TripleFloodgate.Folktails",
				"Valve.Folktails",
				"Sluice.Folktails"
			}) == 0)
			{
				this._eventBus.Unregister(this);
				this._tutorialTriggers.AddTrigger(MissingDamTrigger.TriggerId);
			}
		}

		// Token: 0x040000B2 RID: 178
		public static readonly string TriggerId = "MissingDamTrigger";

		// Token: 0x040000B3 RID: 179
		public readonly EventBus _eventBus;

		// Token: 0x040000B4 RID: 180
		public readonly ITutorialTriggers _tutorialTriggers;

		// Token: 0x040000B5 RID: 181
		public readonly BuiltBuildingService _builtBuildingService;
	}
}
