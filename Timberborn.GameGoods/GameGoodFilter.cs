using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.GoodCollectionSystem;
using Timberborn.Goods;
using Timberborn.SingletonSystem;

namespace Timberborn.GameGoods
{
	// Token: 0x02000004 RID: 4
	public class GameGoodFilter : IGoodFilter, ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public GameGoodFilter(ISpecService specService, IEnumerable<IGoodCollectionIdsProvider> goodCollectionIdsProviders)
		{
			this._specService = specService;
			this._goodCollectionIdsProviders = goodCollectionIdsProviders.ToImmutableArray<IGoodCollectionIdsProvider>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public void Load()
		{
			HashSet<string> hashSet = this._goodCollectionIdsProviders.SelectMany((IGoodCollectionIdsProvider provider) => provider.GetGoodCollectionIds()).ToHashSet<string>();
			foreach (GoodCollectionSpec goodCollectionSpec in this._specService.GetSpecs<GoodCollectionSpec>())
			{
				if (hashSet.Contains(goodCollectionSpec.CollectionId))
				{
					this._allowedGoods.AddRange(goodCollectionSpec.Goods);
				}
			}
			List<GoodSpec> source = this._specService.GetSpecs<GoodSpec>().ToList<GoodSpec>();
			using (HashSet<string>.Enumerator enumerator2 = this._allowedGoods.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string goodId = enumerator2.Current;
					if (source.All((GoodSpec good) => good.Id != goodId))
					{
						throw new ArgumentException("There is no GoodSpec with id: " + goodId);
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002210 File Offset: 0x00000410
		public bool IsUsable(GoodSpec goodSpec)
		{
			return this._allowedGoods.Contains(goodSpec.Id);
		}

		// Token: 0x04000006 RID: 6
		public readonly ISpecService _specService;

		// Token: 0x04000007 RID: 7
		public readonly ImmutableArray<IGoodCollectionIdsProvider> _goodCollectionIdsProviders;

		// Token: 0x04000008 RID: 8
		public readonly HashSet<string> _allowedGoods = new HashSet<string>();
	}
}
