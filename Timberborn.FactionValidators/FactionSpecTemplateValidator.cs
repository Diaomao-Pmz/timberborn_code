using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.FactionSystem;
using Timberborn.TemplateCollectionSystem;

namespace Timberborn.FactionValidators
{
	// Token: 0x0200000A RID: 10
	public class FactionSpecTemplateValidator : IFactionSpecValidator
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000022B9 File Offset: 0x000004B9
		public FactionSpecTemplateValidator(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C8 File Offset: 0x000004C8
		public bool IsValid(FactionSpec faction, out string errorMessage)
		{
			ImmutableArray<string> templateCollectionIds = faction.TemplateCollectionIds;
			List<string> list = (from @group in this._specService.GetSpecs<TemplateCollectionSpec>()
			select @group.CollectionId).ToList<string>();
			foreach (string text in templateCollectionIds)
			{
				if (!list.Contains(text))
				{
					errorMessage = "TemplateCollectionSpec with id " + text + " not found";
					return false;
				}
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x0400000F RID: 15
		public readonly ISpecService _specService;
	}
}
