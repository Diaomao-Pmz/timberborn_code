using System;

namespace Timberborn.TerrainSystem
{
	// Token: 0x0200001A RID: 26
	public readonly struct TileBounds3D
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000045D1 File Offset: 0x000027D1
		public int MinX { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000045D9 File Offset: 0x000027D9
		public int MinY { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000045E1 File Offset: 0x000027E1
		public int MinZ { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000045E9 File Offset: 0x000027E9
		public int MaxX { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000045F1 File Offset: 0x000027F1
		public int MaxY { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000045F9 File Offset: 0x000027F9
		public int MaxZ { get; }

		// Token: 0x060000E7 RID: 231 RVA: 0x00004601 File Offset: 0x00002801
		public TileBounds3D(int minX, int minY, int minZ, int maxX, int maxY, int maxZ)
		{
			this.MinX = minX;
			this.MinY = minY;
			this.MinZ = minZ;
			this.MaxX = maxX;
			this.MaxY = maxY;
			this.MaxZ = maxZ;
		}
	}
}
