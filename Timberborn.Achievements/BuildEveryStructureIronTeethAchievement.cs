using System;
using Timberborn.BuildingAvailability;
using Timberborn.EntitySystem;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000018 RID: 24
	public class BuildEveryStructureIronTeethAchievement : BuildEveryStructureAchievement
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002F50 File Offset: 0x00001150
		public BuildEveryStructureIronTeethAchievement(EventBus eventBus, FactionService factionService, TemplateService templateService, EntityComponentRegistry entityComponentRegistry, BuildingAvailabilityValidator buildingAvailabilityValidator) : base(eventBus, factionService, templateService, entityComponentRegistry, buildingAvailabilityValidator, AchievementHelper.IronTeeth)
		{
		}
	}
}
