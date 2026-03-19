using System;

namespace Timberborn.Buildings
{
	// Token: 0x0200001F RID: 31
	public interface IBuildingEfficiencyProvider
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010A RID: 266
		bool CanUse { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010B RID: 267
		float Efficiency { get; }
	}
}
