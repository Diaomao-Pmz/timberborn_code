using System;
using Timberborn.GameDistricts;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x0200000E RID: 14
	public interface IDistributorTemplate
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33
		string ComponentName { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34
		int Current { get; }

		// Token: 0x06000023 RID: 35
		void MigrateTo(DistrictCenter target, int amount);
	}
}
