using System;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.TickSystem
{
	// Token: 0x0200001C RID: 28
	public class TickService : ITickService, ILoadableSingleton
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002C3A File Offset: 0x00000E3A
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002C42 File Offset: 0x00000E42
		public float TickIntervalInSeconds { get; private set; }

		// Token: 0x0600006D RID: 109 RVA: 0x00002C4B File Offset: 0x00000E4B
		public TickService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002C5C File Offset: 0x00000E5C
		public void Load()
		{
			TickTimeSpec singleSpec = this._specService.GetSingleSpec<TickTimeSpec>();
			this.TickIntervalInSeconds = singleSpec.TickIntervalInSeconds;
		}

		// Token: 0x0400003F RID: 63
		public readonly ISpecService _specService;
	}
}
