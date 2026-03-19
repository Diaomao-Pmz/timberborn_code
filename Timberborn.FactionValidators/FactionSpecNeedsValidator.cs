using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.FactionSystem;
using Timberborn.NeedCollectionSystem;

namespace Timberborn.FactionValidators
{
	// Token: 0x02000008 RID: 8
	public class FactionSpecNeedsValidator : IFactionSpecValidator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002211 File Offset: 0x00000411
		public FactionSpecNeedsValidator(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002220 File Offset: 0x00000420
		public bool IsValid(FactionSpec faction, out string errorMessage)
		{
			ImmutableArray<string> needCollectionIds = faction.NeedCollectionIds;
			List<string> list = (from @group in this._specService.GetSpecs<NeedCollectionSpec>()
			select @group.CollectionId).ToList<string>();
			foreach (string text in needCollectionIds)
			{
				if (!list.Contains(text))
				{
					errorMessage = "NeedCollectionSpec with id " + text + " not found";
					return false;
				}
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x0400000C RID: 12
		public readonly ISpecService _specService;
	}
}
