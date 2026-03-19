using System;
using Timberborn.Common;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000014 RID: 20
	public interface IThreadSafeWaterEvaporationMap
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70
		ReadOnlyArray<float> EvaporationModifiers { get; }
	}
}
