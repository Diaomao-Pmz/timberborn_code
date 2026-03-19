using System;
using Timberborn.Common;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000012 RID: 18
	public interface IFlowLimiterService
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003E RID: 62
		// (remove) Token: 0x0600003F RID: 63
		event EventHandler<int> LimitedValueChanged;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000040 RID: 64
		ReadOnlyArray<int> LimitedDirections { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65
		ReadOnlyArray<float> LimitedValues { get; }
	}
}
