using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.TimbermeshMaterials;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x02000008 RID: 8
	public class FactionMaterialCollectionIdsProvider : IMaterialCollectionIdsProvider
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002357 File Offset: 0x00000557
		public FactionMaterialCollectionIdsProvider(FactionService factionService)
		{
			this._factionService = factionService;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002366 File Offset: 0x00000566
		public IEnumerable<string> GetMaterialCollectionIds()
		{
			foreach (string text in this._factionService.Current.MaterialCollectionIds)
			{
				yield return text;
			}
			ImmutableArray<string>.Enumerator enumerator = default(ImmutableArray<string>.Enumerator);
			yield break;
		}

		// Token: 0x04000014 RID: 20
		public readonly FactionService _factionService;
	}
}
