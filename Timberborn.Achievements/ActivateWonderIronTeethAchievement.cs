using System;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000009 RID: 9
	public class ActivateWonderIronTeethAchievement : ActivateWonderAchievement
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000266C File Offset: 0x0000086C
		public ActivateWonderIronTeethAchievement(EventBus eventBus, FactionService factionService) : base(eventBus, factionService, AchievementHelper.IronTeeth)
		{
		}
	}
}
