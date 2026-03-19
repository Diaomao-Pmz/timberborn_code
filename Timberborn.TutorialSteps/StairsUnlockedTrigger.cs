using System;
using Timberborn.Buildings;
using Timberborn.ScienceSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000052 RID: 82
	public class StairsUnlockedTrigger : ILoadableSingleton
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00006B11 File Offset: 0x00004D11
		public StairsUnlockedTrigger(EventBus eventBus, ITutorialTriggers tutorialTriggers, TemplateNameMapper templateNameMapper)
		{
			this._eventBus = eventBus;
			this._tutorialTriggers = tutorialTriggers;
			this._templateNameMapper = templateNameMapper;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006B30 File Offset: 0x00004D30
		public void Load()
		{
			if (this._tutorialTriggers.TriggerPending(StairsUnlockedTrigger.TriggerId))
			{
				TemplateSpec template = this._templateNameMapper.GetTemplate("Stairs.Folktails");
				this._buildingSpec = template.GetSpec<BuildingSpec>();
				this._eventBus.Register(this);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006B78 File Offset: 0x00004D78
		[OnEvent]
		public void OnBuildingUnlocked(BuildingUnlockedEvent buildingUnlockedEvent)
		{
			if (buildingUnlockedEvent.BuildingSpec == this._buildingSpec)
			{
				this._eventBus.Unregister(this);
				this._tutorialTriggers.AddTrigger(StairsUnlockedTrigger.TriggerId);
			}
		}

		// Token: 0x04000111 RID: 273
		public static readonly string TriggerId = "StairsUnlockedTrigger";

		// Token: 0x04000112 RID: 274
		public readonly EventBus _eventBus;

		// Token: 0x04000113 RID: 275
		public readonly ITutorialTriggers _tutorialTriggers;

		// Token: 0x04000114 RID: 276
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x04000115 RID: 277
		public BuildingSpec _buildingSpec;
	}
}
