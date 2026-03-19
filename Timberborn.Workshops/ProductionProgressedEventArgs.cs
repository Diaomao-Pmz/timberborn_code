using System;

namespace Timberborn.Workshops
{
	// Token: 0x02000020 RID: 32
	public class ProductionProgressedEventArgs
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004053 File Offset: 0x00002253
		public float ProductionProgressChange { get; }

		// Token: 0x060000D0 RID: 208 RVA: 0x0000405B File Offset: 0x0000225B
		public ProductionProgressedEventArgs(float productionProgressChange)
		{
			this.ProductionProgressChange = productionProgressChange;
		}
	}
}
