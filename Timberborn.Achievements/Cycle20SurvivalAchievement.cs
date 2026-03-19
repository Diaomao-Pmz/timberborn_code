using System;
using Timberborn.GameCycleSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000022 RID: 34
	public class Cycle20SurvivalAchievement : CycleSurvivalAchievement
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000035AA File Offset: 0x000017AA
		public Cycle20SurvivalAchievement(EventBus eventBus, GameCycleService gameCycleService) : base(eventBus, gameCycleService, 20)
		{
		}
	}
}
