using System;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000054 RID: 84
	public class SurvivedFirstDroughtTrigger : ILoadableSingleton
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00006C22 File Offset: 0x00004E22
		public SurvivedFirstDroughtTrigger(EventBus eventBus, ITutorialTriggers tutorialTriggers)
		{
			this._eventBus = eventBus;
			this._tutorialTriggers = tutorialTriggers;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006C38 File Offset: 0x00004E38
		public void Load()
		{
			if (this._tutorialTriggers.TriggerPending(SurvivedFirstDroughtTrigger.TriggerId))
			{
				this._eventBus.Register(this);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006C58 File Offset: 0x00004E58
		[OnEvent]
		public void OnHazardousWeatherEnded(HazardousWeatherEndedEvent hazardousWeatherEndedEvent)
		{
			if (hazardousWeatherEndedEvent.HazardousWeather is DroughtWeather)
			{
				this._eventBus.Unregister(this);
				this._tutorialTriggers.AddTrigger(SurvivedFirstDroughtTrigger.TriggerId);
			}
		}

		// Token: 0x04000119 RID: 281
		public static readonly string TriggerId = "SurvivedFirstDroughtTrigger";

		// Token: 0x0400011A RID: 282
		public readonly EventBus _eventBus;

		// Token: 0x0400011B RID: 283
		public readonly ITutorialTriggers _tutorialTriggers;
	}
}
