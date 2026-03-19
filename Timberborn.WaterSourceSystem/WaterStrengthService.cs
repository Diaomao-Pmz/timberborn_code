using System;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000025 RID: 37
	public class WaterStrengthService : ILoadableSingleton
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004057 File Offset: 0x00002257
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000405F File Offset: 0x0000225F
		public float MaxWaterSourceStrength { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004068 File Offset: 0x00002268
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00004070 File Offset: 0x00002270
		public float MaxWaterSourceChangePerSecond { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004079 File Offset: 0x00002279
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00004081 File Offset: 0x00002281
		public float MinWaterSourceChangeScaler { get; private set; }

		// Token: 0x06000144 RID: 324 RVA: 0x0000408A File Offset: 0x0000228A
		public WaterStrengthService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000409C File Offset: 0x0000229C
		public void Load()
		{
			WaterStrengthSpec singleSpec = this._specService.GetSingleSpec<WaterStrengthSpec>();
			this.MaxWaterSourceStrength = singleSpec.MaxWaterSourceStrength;
			this.MaxWaterSourceChangePerSecond = singleSpec.MaxWaterSourceChangePerSecond;
			this.MinWaterSourceChangeScaler = singleSpec.MinWaterSourceChangeScaler;
		}

		// Token: 0x0400005F RID: 95
		public readonly ISpecService _specService;
	}
}
