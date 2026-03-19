using System;

namespace Timberborn.TerrainSystem
{
	// Token: 0x0200000E RID: 14
	public struct TerrainColumn
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002939 File Offset: 0x00000B39
		public TerrainColumn(int floor, int ceiling)
		{
			this.Floor = floor;
			this.Ceiling = ceiling;
		}

		// Token: 0x04000015 RID: 21
		public int Floor;

		// Token: 0x04000016 RID: 22
		public int Ceiling;
	}
}
