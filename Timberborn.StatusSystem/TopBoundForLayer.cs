using System;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200002B RID: 43
	public readonly struct TopBoundForLayer
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004BAC File Offset: 0x00002DAC
		public float ConstructionModeTopBound { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public float NormalModeTopBound { get; }

		// Token: 0x06000115 RID: 277 RVA: 0x00004BBC File Offset: 0x00002DBC
		public TopBoundForLayer(float constructionModeTopBound)
		{
			this = new TopBoundForLayer(constructionModeTopBound, constructionModeTopBound);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004BC6 File Offset: 0x00002DC6
		public TopBoundForLayer(float constructionModeTopBound, float normalModeTopBound)
		{
			this.ConstructionModeTopBound = constructionModeTopBound;
			this.NormalModeTopBound = normalModeTopBound;
		}
	}
}
