using System;
using Timberborn.GameCycleSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000023 RID: 35
	public class Cycle50SurvivalAchievement : CycleSurvivalAchievement
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000035B6 File Offset: 0x000017B6
		public Cycle50SurvivalAchievement(EventBus eventBus, GameCycleService gameCycleService) : base(eventBus, gameCycleService, 50)
		{
		}
	}
}
