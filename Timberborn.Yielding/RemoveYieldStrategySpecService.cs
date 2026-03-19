using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Yielding
{
	// Token: 0x0200000F RID: 15
	public class RemoveYieldStrategySpecService : ILoadableSingleton
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public RemoveYieldStrategySpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public void Load()
		{
			this._removeYieldStrategySpecs = this._specService.GetSpecs<RemoveYieldStrategySpec>().ToDictionary((RemoveYieldStrategySpec spec) => spec.Id, (RemoveYieldStrategySpec spec) => spec);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C15 File Offset: 0x00000E15
		public RemoveYieldStrategySpec GetRemoveYieldStrategySpec(string id)
		{
			return this._removeYieldStrategySpecs[id];
		}

		// Token: 0x0400001D RID: 29
		public readonly ISpecService _specService;

		// Token: 0x0400001E RID: 30
		public Dictionary<string, RemoveYieldStrategySpec> _removeYieldStrategySpecs;
	}
}
