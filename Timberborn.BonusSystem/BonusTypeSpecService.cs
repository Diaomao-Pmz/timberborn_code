using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.BonusSystem
{
	// Token: 0x0200000E RID: 14
	public class BonusTypeSpecService : ILoadableSingleton
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002C14 File Offset: 0x00000E14
		public BonusTypeSpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002C23 File Offset: 0x00000E23
		public IEnumerable<string> BonusIds
		{
			get
			{
				return from spec in this._specs
				select spec.Id;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C4F File Offset: 0x00000E4F
		public void Load()
		{
			this._specs = this._specService.GetSpecs<BonusTypeSpec>().ToImmutableArray<BonusTypeSpec>();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C68 File Offset: 0x00000E68
		public BonusTypeSpec GetSpec(string bonusId)
		{
			BonusTypeSpec bonusTypeSpec = this._specs.SingleOrDefault((BonusTypeSpec spec) => spec.Id == bonusId);
			if (bonusTypeSpec != null)
			{
				return bonusTypeSpec;
			}
			throw new InvalidOperationException("Bonus type spec with id " + bonusId + " not found or multiple specs found");
		}

		// Token: 0x0400001C RID: 28
		public readonly ISpecService _specService;

		// Token: 0x0400001D RID: 29
		public ImmutableArray<BonusTypeSpec> _specs;
	}
}
