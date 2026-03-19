using System;

namespace Timberborn.NaturalResourcesMoisture
{
	// Token: 0x0200000F RID: 15
	public readonly struct WaterNeedsUnmetEventArgs
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002E8D File Offset: 0x0000108D
		public bool Flooded { get; }

		// Token: 0x06000063 RID: 99 RVA: 0x00002E95 File Offset: 0x00001095
		public WaterNeedsUnmetEventArgs(bool flooded)
		{
			this.Flooded = flooded;
		}
	}
}
