using System;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000019 RID: 25
	public readonly struct TileBounds2D
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004592 File Offset: 0x00002792
		public int MinX { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000459A File Offset: 0x0000279A
		public int MinY { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000045A2 File Offset: 0x000027A2
		public int MaxX { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000045AA File Offset: 0x000027AA
		public int MaxY { get; }

		// Token: 0x060000E0 RID: 224 RVA: 0x000045B2 File Offset: 0x000027B2
		public TileBounds2D(int minX, int minY, int maxX, int maxY)
		{
			this.MinX = minX;
			this.MinY = minY;
			this.MaxX = maxX;
			this.MaxY = maxY;
		}
	}
}
