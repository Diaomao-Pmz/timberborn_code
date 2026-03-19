using System;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000008 RID: 8
	public class ActivateWonderFolktailsAchievement : ActivateWonderAchievement
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000265D File Offset: 0x0000085D
		public ActivateWonderFolktailsAchievement(EventBus eventBus, FactionService factionService) : base(eventBus, factionService, AchievementHelper.Folktails)
		{
		}
	}
}
