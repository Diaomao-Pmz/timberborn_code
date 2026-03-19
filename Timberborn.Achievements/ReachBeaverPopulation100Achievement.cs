using System;
using Timberborn.Beavers;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000046 RID: 70
	public class ReachBeaverPopulation100Achievement : ReachBeaverPopulationAchievement
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00004761 File Offset: 0x00002961
		public ReachBeaverPopulation100Achievement(BeaverPopulation beaverPopulation, EventBus eventBus) : base(beaverPopulation, eventBus, 100)
		{
		}
	}
}
