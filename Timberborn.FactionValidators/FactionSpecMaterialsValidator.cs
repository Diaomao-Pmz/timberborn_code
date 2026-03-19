using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.FactionSystem;
using Timberborn.TimbermeshMaterials;

namespace Timberborn.FactionValidators
{
	// Token: 0x02000006 RID: 6
	public class FactionSpecMaterialsValidator : IFactionSpecValidator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002169 File Offset: 0x00000369
		public FactionSpecMaterialsValidator(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002178 File Offset: 0x00000378
		public bool IsValid(FactionSpec faction, out string errorMessage)
		{
			ImmutableArray<string> materialCollectionIds = faction.MaterialCollectionIds;
			List<string> list = (from @group in this._specService.GetSpecs<MaterialCollectionSpec>()
			select @group.CollectionId).ToList<string>();
			foreach (string text in materialCollectionIds)
			{
				if (!list.Contains(text))
				{
					errorMessage = "MaterialCollectionSpec with id " + text + " not found";
					return false;
				}
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x04000009 RID: 9
		public readonly ISpecService _specService;
	}
}
