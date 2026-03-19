using System;

namespace Timberborn.PopulationStatisticsSystem
{
	// Token: 0x02000009 RID: 9
	public interface IEmploymentStatisticsProvider
	{
		// Token: 0x06000014 RID: 20
		EmploymentStatistics GetEmploymentStatistics(string workerType);
	}
}
