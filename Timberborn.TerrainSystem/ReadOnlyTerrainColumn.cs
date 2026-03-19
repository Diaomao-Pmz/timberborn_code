using System;
using JetBrains.Annotations;

namespace Timberborn.TerrainSystem
{
	// Token: 0x0200000D RID: 13
	public readonly struct ReadOnlyTerrainColumn
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002919 File Offset: 0x00000B19
		public int Floor { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002921 File Offset: 0x00000B21
		public int Ceiling { get; }

		// Token: 0x06000060 RID: 96 RVA: 0x00002929 File Offset: 0x00000B29
		[UsedImplicitly]
		public ReadOnlyTerrainColumn(int floor, int ceiling)
		{
			this.Floor = floor;
			this.Ceiling = ceiling;
		}
	}
}
