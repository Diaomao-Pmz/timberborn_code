using System;
using System.Collections.Immutable;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000009 RID: 9
	public interface IStatusAggregator
	{
		// Token: 0x06000015 RID: 21
		ImmutableArray<StatusInstance> GetVisibleStatuses(string alertDescription);
	}
}
