using System;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000028 RID: 40
	public struct WaterColumn
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000451D File Offset: 0x0000271D
		public WaterColumn(int floor, int ceiling)
		{
			this.Floor = Convert.ToByte(floor);
			this.Ceiling = Convert.ToByte(ceiling);
			this.WaterDepth = 0f;
			this.Contamination = 0f;
			this.Overflow = 0f;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004558 File Offset: 0x00002758
		public void Reset()
		{
			this.WaterDepth = 0f;
			this.Contamination = 0f;
			this.Overflow = 0f;
		}

		// Token: 0x0400009C RID: 156
		public byte Floor;

		// Token: 0x0400009D RID: 157
		public byte Ceiling;

		// Token: 0x0400009E RID: 158
		public float WaterDepth;

		// Token: 0x0400009F RID: 159
		public float Contamination;

		// Token: 0x040000A0 RID: 160
		public float Overflow;
	}
}
