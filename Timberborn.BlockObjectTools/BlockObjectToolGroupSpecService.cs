using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000016 RID: 22
	public class BlockObjectToolGroupSpecService : ILoadableSingleton
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003290 File Offset: 0x00001490
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003298 File Offset: 0x00001498
		public ImmutableArray<BlockObjectToolGroupSpec> AllSpecs { get; private set; }

		// Token: 0x06000070 RID: 112 RVA: 0x000032A1 File Offset: 0x000014A1
		public BlockObjectToolGroupSpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000032B0 File Offset: 0x000014B0
		public void Load()
		{
			this.AllSpecs = (from spec in this._specService.GetSpecs<BlockObjectToolGroupSpec>()
			orderby spec.Order
			select spec).ToImmutableArray<BlockObjectToolGroupSpec>();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000032EC File Offset: 0x000014EC
		public BlockObjectToolGroupSpec GetFallbackSpec()
		{
			return this.AllSpecs.Single((BlockObjectToolGroupSpec spec) => spec.FallbackGroup);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003318 File Offset: 0x00001518
		public BlockObjectToolGroupSpec GetSpec(string groupId)
		{
			return this.AllSpecs.Single((BlockObjectToolGroupSpec spec) => spec.Id == groupId);
		}

		// Token: 0x04000052 RID: 82
		public readonly ISpecService _specService;
	}
}
