using System;

namespace Timberborn.Workshops
{
	// Token: 0x0200000E RID: 14
	public interface IManufactoryLimiter
	{
		// Token: 0x0600002D RID: 45
		float ProductionEfficiency();

		// Token: 0x0600002E RID: 46
		float MaxProductionProgressChange();
	}
}
