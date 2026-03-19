using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.TemplateSystem;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200001F RID: 31
	public class PlaceableBlockObjectSpecService
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000035C8 File Offset: 0x000017C8
		public PlaceableBlockObjectSpecService(BlockObjectToolGroupSpecService blockObjectToolGroupSpecService, TemplateService templateService)
		{
			this._blockObjectToolGroupSpecService = blockObjectToolGroupSpecService;
			this._templateService = templateService;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000035E0 File Offset: 0x000017E0
		public IEnumerable<PlaceableBlockObjectSpec> GetBlockObjects(BlockObjectToolGroupSpec blockObjectToolGroupSpec)
		{
			return from spec in this._templateService.GetAll<PlaceableBlockObjectSpec>()
			where blockObjectToolGroupSpec.Id == spec.ToolGroupId
			orderby spec.ToolOrder, spec.GetSpec<LabeledEntitySpec>().DisplayNameLocKey
			select spec;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003660 File Offset: 0x00001860
		public IEnumerable<PlaceableBlockObjectSpec> GetBlockObjectsWithoutValidGroup()
		{
			HashSet<string> toolGroupIds = (from toolGroupSpec in this._blockObjectToolGroupSpecService.AllSpecs
			select toolGroupSpec.Id).ToHashSet<string>();
			return from spec in this._templateService.GetAll<PlaceableBlockObjectSpec>()
			where !toolGroupIds.Contains(spec.ToolGroupId)
			orderby spec.ToolOrder, spec.GetSpec<LabeledEntitySpec>().DisplayNameLocKey
			select spec;
		}

		// Token: 0x0400005F RID: 95
		public readonly BlockObjectToolGroupSpecService _blockObjectToolGroupSpecService;

		// Token: 0x04000060 RID: 96
		public readonly TemplateService _templateService;
	}
}
