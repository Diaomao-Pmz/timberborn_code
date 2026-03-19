using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.GoodCollectionSystem;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x02000006 RID: 6
	public class FactionGoodCollectionIdsProvider : IGoodCollectionIdsProvider
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002243 File Offset: 0x00000443
		public FactionGoodCollectionIdsProvider(FactionService factionService)
		{
			this._factionService = factionService;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002252 File Offset: 0x00000452
		public IEnumerable<string> GetGoodCollectionIds()
		{
			foreach (string text in this._factionService.Current.GoodCollectionIds)
			{
				yield return text;
			}
			ImmutableArray<string>.Enumerator enumerator = default(ImmutableArray<string>.Enumerator);
			yield break;
		}

		// Token: 0x0400000E RID: 14
		public readonly FactionService _factionService;
	}
}
