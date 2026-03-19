using System;
using Timberborn.GameCycleSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000020 RID: 32
	public class Cycle5SurvivalAchievement : CycleSurvivalAchievement
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003593 File Offset: 0x00001793
		public Cycle5SurvivalAchievement(EventBus eventBus, GameCycleService gameCycleService) : base(eventBus, gameCycleService, 5)
		{
		}
	}
}
