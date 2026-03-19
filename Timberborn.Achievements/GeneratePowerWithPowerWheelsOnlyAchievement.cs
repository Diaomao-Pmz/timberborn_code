using System;
using Timberborn.MechanicalSystem;
using Timberborn.PowerGeneration;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200002A RID: 42
	public class GeneratePowerWithPowerWheelsOnlyAchievement : GeneratePowerWithAchievement<WalkerPoweredGenerator>
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00003B30 File Offset: 0x00001D30
		public GeneratePowerWithPowerWheelsOnlyAchievement(MechanicalGraphRegistry mechanicalGraphRegistry, EventBus eventBus) : base(mechanicalGraphRegistry, eventBus, "GENERATE_POWER_WITH_POWER_WHEELS_ONLY", 2000)
		{
		}
	}
}
