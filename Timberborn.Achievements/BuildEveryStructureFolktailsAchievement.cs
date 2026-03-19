using System;
using Timberborn.BuildingAvailability;
using Timberborn.EntitySystem;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000017 RID: 23
	public class BuildEveryStructureFolktailsAchievement : BuildEveryStructureAchievement
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002F3C File Offset: 0x0000113C
		public BuildEveryStructureFolktailsAchievement(EventBus eventBus, FactionService factionService, TemplateService templateService, EntityComponentRegistry entityComponentRegistry, BuildingAvailabilityValidator buildingAvailabilityValidator) : base(eventBus, factionService, templateService, entityComponentRegistry, buildingAvailabilityValidator, AchievementHelper.Folktails)
		{
		}
	}
}
