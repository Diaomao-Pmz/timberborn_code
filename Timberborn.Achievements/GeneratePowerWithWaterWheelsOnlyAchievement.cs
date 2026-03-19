using System;
using Timberborn.MechanicalSystem;
using Timberborn.PowerGeneration;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000029 RID: 41
	public class GeneratePowerWithWaterWheelsOnlyAchievement : GeneratePowerWithAchievement<WaterPoweredGenerator>
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00003B1C File Offset: 0x00001D1C
		public GeneratePowerWithWaterWheelsOnlyAchievement(MechanicalGraphRegistry mechanicalGraphRegistry, EventBus eventBus) : base(mechanicalGraphRegistry, eventBus, "GENERATE_POWER_WITH_WATER_WHEELS_ONLY", 10000)
		{
		}
	}
}
