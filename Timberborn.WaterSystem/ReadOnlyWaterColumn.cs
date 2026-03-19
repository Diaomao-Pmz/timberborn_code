using System;
using JetBrains.Annotations;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200001D RID: 29
	public readonly struct ReadOnlyWaterColumn
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000033AE File Offset: 0x000015AE
		[UsedImplicitly]
		public byte Floor { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000033B6 File Offset: 0x000015B6
		[UsedImplicitly]
		public byte Ceiling { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000033BE File Offset: 0x000015BE
		[UsedImplicitly]
		public float WaterDepth { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000033C6 File Offset: 0x000015C6
		[UsedImplicitly]
		public float Contamination { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000033CE File Offset: 0x000015CE
		[UsedImplicitly]
		public float Overflow { get; }
	}
}
