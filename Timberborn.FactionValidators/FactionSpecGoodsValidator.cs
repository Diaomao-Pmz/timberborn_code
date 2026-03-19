using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.FactionSystem;
using Timberborn.GoodCollectionSystem;

namespace Timberborn.FactionValidators
{
	// Token: 0x02000004 RID: 4
	public class FactionSpecGoodsValidator : IFactionSpecValidator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public FactionSpecGoodsValidator(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public bool IsValid(FactionSpec faction, out string errorMessage)
		{
			ImmutableArray<string> goodCollectionIds = faction.GoodCollectionIds;
			List<string> list = (from @group in this._specService.GetSpecs<GoodCollectionSpec>()
			select @group.CollectionId).ToList<string>();
			foreach (string text in goodCollectionIds)
			{
				if (!list.Contains(text))
				{
					errorMessage = "GoodCollectionSpec with id  " + text + " not found";
					return false;
				}
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x04000006 RID: 6
		public readonly ISpecService _specService;
	}
}
