using System;
using Timberborn.GameCycleSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000021 RID: 33
	public class Cycle10SurvivalAchievement : CycleSurvivalAchievement
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000359E File Offset: 0x0000179E
		public Cycle10SurvivalAchievement(EventBus eventBus, GameCycleService gameCycleService) : base(eventBus, gameCycleService, 10)
		{
		}
	}
}
