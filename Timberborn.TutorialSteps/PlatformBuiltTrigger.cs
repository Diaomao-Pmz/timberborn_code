using System;
using System.Collections.Immutable;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200003D RID: 61
	public class PlatformBuiltTrigger : ILoadableSingleton
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000579B File Offset: 0x0000399B
		public PlatformBuiltTrigger(EventBus eventBus, ITutorialTriggers tutorialTriggers)
		{
			this._eventBus = eventBus;
			this._tutorialTriggers = tutorialTriggers;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000057B1 File Offset: 0x000039B1
		public void Load()
		{
			if (this._tutorialTriggers.TriggerPending(PlatformBuiltTrigger.TriggerId))
			{
				this._eventBus.Register(this);
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000057D4 File Offset: 0x000039D4
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			Building component = enteredFinishedStateEvent.BlockObject.GetComponent<Building>();
			if (component != null)
			{
				string templateName = component.GetComponent<TemplateSpec>().TemplateName;
				if (PlatformBuiltTrigger.PlatformTemplateNames.Contains(templateName))
				{
					this._eventBus.Unregister(this);
					this._tutorialTriggers.AddTrigger(PlatformBuiltTrigger.TriggerId);
				}
			}
		}

		// Token: 0x040000C3 RID: 195
		public static readonly string TriggerId = "PlatformBuiltTrigger";

		// Token: 0x040000C4 RID: 196
		public static readonly ImmutableArray<string> PlatformTemplateNames = ImmutableArray.Create<string>("Platform.Folktails", "DoublePlatform.Folktails", "TriplePlatform.Folktails");

		// Token: 0x040000C5 RID: 197
		public readonly EventBus _eventBus;

		// Token: 0x040000C6 RID: 198
		public readonly ITutorialTriggers _tutorialTriggers;
	}
}
