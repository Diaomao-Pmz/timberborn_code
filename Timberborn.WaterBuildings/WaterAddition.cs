using System;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200002D RID: 45
	public readonly struct WaterAddition
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006972 File Offset: 0x00004B72
		public float CleanWater { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000697A File Offset: 0x00004B7A
		public float ContaminatedWater { get; }

		// Token: 0x0600021F RID: 543 RVA: 0x00006982 File Offset: 0x00004B82
		public WaterAddition(float cleanWater, float contaminatedWater)
		{
			this.CleanWater = cleanWater;
			this.ContaminatedWater = contaminatedWater;
		}
	}
}
