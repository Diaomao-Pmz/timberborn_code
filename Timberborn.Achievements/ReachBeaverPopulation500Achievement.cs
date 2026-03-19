using System;
using Timberborn.Beavers;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000048 RID: 72
	public class ReachBeaverPopulation500Achievement : ReachBeaverPopulationAchievement
	{
		// Token: 0x06000117 RID: 279 RVA: 0x0000477C File Offset: 0x0000297C
		public ReachBeaverPopulation500Achievement(BeaverPopulation beaverPopulation, EventBus eventBus) : base(beaverPopulation, eventBus, 500)
		{
		}
	}
}
