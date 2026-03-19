using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.NeedCollectionSystem;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x0200000A RID: 10
	public class FactionNeedCollectionIdsProvider : INeedCollectionIdsProvider
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000246B File Offset: 0x0000066B
		public FactionNeedCollectionIdsProvider(FactionService factionService)
		{
			this._factionService = factionService;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000247A File Offset: 0x0000067A
		public IEnumerable<string> GetNeedCollectionIds()
		{
			foreach (string text in this._factionService.Current.NeedCollectionIds)
			{
				yield return text;
			}
			ImmutableArray<string>.Enumerator enumerator = default(ImmutableArray<string>.Enumerator);
			yield break;
		}

		// Token: 0x0400001A RID: 26
		public readonly FactionService _factionService;
	}
}
