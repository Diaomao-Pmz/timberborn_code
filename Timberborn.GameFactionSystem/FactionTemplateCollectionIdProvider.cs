using System;
using System.Collections.Generic;
using Timberborn.TemplateCollectionSystem;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x02000011 RID: 17
	public class FactionTemplateCollectionIdProvider : ITemplateCollectionIdProvider
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000029AA File Offset: 0x00000BAA
		public FactionTemplateCollectionIdProvider(FactionService factionService)
		{
			this._factionService = factionService;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029B9 File Offset: 0x00000BB9
		public IEnumerable<string> GetTemplateCollectionIds()
		{
			return this._factionService.Current.TemplateCollectionIds;
		}

		// Token: 0x04000034 RID: 52
		public readonly FactionService _factionService;
	}
}
