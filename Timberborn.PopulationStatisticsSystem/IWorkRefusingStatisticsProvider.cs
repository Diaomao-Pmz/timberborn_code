using System;

namespace Timberborn.PopulationStatisticsSystem
{
	// Token: 0x0200000A RID: 10
	public interface IWorkRefusingStatisticsProvider
	{
		// Token: 0x06000015 RID: 21
		WorkRefusingStatistics GetWorkRefusingStatistics(string workerType);
	}
}
