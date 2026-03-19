using System;
using Timberborn.MechanicalSystem;
using Timberborn.PowerGeneration;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200002B RID: 43
	public class GeneratePowerWithWindTurbinesOnlyAchievement : GeneratePowerWithAchievement<WindPoweredGenerator>
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003B44 File Offset: 0x00001D44
		public GeneratePowerWithWindTurbinesOnlyAchievement(MechanicalGraphRegistry mechanicalGraphRegistry, EventBus eventBus) : base(mechanicalGraphRegistry, eventBus, "GENERATE_POWER_WITH_WIND_TURBINES_ONLY", 10000)
		{
		}
	}
}
