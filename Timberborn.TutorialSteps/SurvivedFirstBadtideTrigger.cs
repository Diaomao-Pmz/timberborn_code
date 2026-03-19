using System;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000053 RID: 83
	public class SurvivedFirstBadtideTrigger : ILoadableSingleton
	{
		// Token: 0x0600023B RID: 571 RVA: 0x00006BB5 File Offset: 0x00004DB5
		public SurvivedFirstBadtideTrigger(EventBus eventBus, ITutorialTriggers tutorialTriggers)
		{
			this._eventBus = eventBus;
			this._tutorialTriggers = tutorialTriggers;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006BCB File Offset: 0x00004DCB
		public void Load()
		{
			if (this._tutorialTriggers.TriggerPending(SurvivedFirstBadtideTrigger.TriggerId))
			{
				this._eventBus.Register(this);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006BEB File Offset: 0x00004DEB
		[OnEvent]
		public void OnHazardousWeatherEnded(HazardousWeatherEndedEvent hazardousWeatherEndedEvent)
		{
			if (hazardousWeatherEndedEvent.HazardousWeather is BadtideWeather)
			{
				this._eventBus.Unregister(this);
				this._tutorialTriggers.AddTrigger(SurvivedFirstBadtideTrigger.TriggerId);
			}
		}

		// Token: 0x04000116 RID: 278
		public static readonly string TriggerId = "SurvivedFirstBadtideTrigger";

		// Token: 0x04000117 RID: 279
		public readonly EventBus _eventBus;

		// Token: 0x04000118 RID: 280
		public readonly ITutorialTriggers _tutorialTriggers;
	}
}
