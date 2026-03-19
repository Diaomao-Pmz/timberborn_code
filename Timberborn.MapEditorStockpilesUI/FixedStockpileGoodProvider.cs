using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.FactionSystem;
using Timberborn.GoodCollectionSystem;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.MapEditorStockpilesUI
{
	// Token: 0x02000005 RID: 5
	internal class FixedStockpileGoodProvider : ILoadableSingleton
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000023C8 File Offset: 0x000005C8
		public FixedStockpileGoodProvider(IGoodService goodService, ISpecService specService, GoodDescriber goodDescriber, FactionSpecService factionSpecService, CommonGoodCollectionIdsProvider commonGoodCollectionIdsProvider)
		{
			this._goodService = goodService;
			this._specService = specService;
			this._goodDescriber = goodDescriber;
			this._factionSpecService = factionSpecService;
			this._commonGoodCollectionIdsProvider = commonGoodCollectionIdsProvider;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002400 File Offset: 0x00000600
		public void Load()
		{
			foreach (GoodCollectionSpec goodCollectionSpec in this._specService.GetSpecs<GoodCollectionSpec>())
			{
				this._collectionIdToGoods.GetOrAdd(goodCollectionSpec.CollectionId).AddRange(goodCollectionSpec.Goods);
			}
			this._commonId = this._commonGoodCollectionIdsProvider.GetGoodCollectionIds().Single<string>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002484 File Offset: 0x00000684
		public ImmutableArray<string> GetGoods(string goodType)
		{
			return (from good in this._goodService.GetGoodsForType(goodType)
			select this._goodService.GetGood(good) into good
			orderby this.GetGoodOrder(good.Id), good.PluralDisplayName.Value
			select good.Id).ToImmutableArray<string>();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000250C File Offset: 0x0000070C
		public string GetDisplayText(string goodId)
		{
			string value = this._goodService.GetGood(goodId).PluralDisplayName.Value;
			if (this.IsCommonGood(goodId))
			{
				return value;
			}
			string id;
			if (!this.IsSingleFactionGood(goodId, out id))
			{
				return "✱ - " + value;
			}
			return this._factionSpecService.GetFaction(id).DisplayName.Value + " - " + value;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002573 File Offset: 0x00000773
		public Sprite GetIcon(string goodId)
		{
			return this._goodDescriber.GetIcon(goodId);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002584 File Offset: 0x00000784
		public string GetTooltip(string goodId)
		{
			IEnumerable<string> values = from faction in this._factionSpecService.Factions
			where this.IsFactionGood(faction, goodId)
			select faction.DisplayName.Value;
			return string.Join(", ", values);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025F4 File Offset: 0x000007F4
		private int GetGoodOrder(string goodId)
		{
			if (this.IsCommonGood(goodId))
			{
				return int.MinValue;
			}
			string id;
			if (!this.IsSingleFactionGood(goodId, out id))
			{
				return -2147483647;
			}
			return this._factionSpecService.GetFaction(id).Order;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002632 File Offset: 0x00000832
		private bool IsCommonGood(string goodId)
		{
			return this._collectionIdToGoods[this._commonId].Contains(goodId);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000264C File Offset: 0x0000084C
		private bool IsSingleFactionGood(string goodId, out string singleFactionId)
		{
			int num = 0;
			singleFactionId = string.Empty;
			foreach (FactionSpec factionSpec in this._factionSpecService.Factions)
			{
				if (this.IsFactionGood(factionSpec, goodId))
				{
					singleFactionId = factionSpec.Id;
					num++;
				}
			}
			return num == 1;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026A4 File Offset: 0x000008A4
		private bool IsFactionGood(FactionSpec factionSpec, string goodId)
		{
			foreach (string key in factionSpec.GoodCollectionIds)
			{
				if (this._collectionIdToGoods[key].Contains(goodId))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000012 RID: 18
		private readonly IGoodService _goodService;

		// Token: 0x04000013 RID: 19
		private readonly ISpecService _specService;

		// Token: 0x04000014 RID: 20
		private readonly GoodDescriber _goodDescriber;

		// Token: 0x04000015 RID: 21
		private readonly FactionSpecService _factionSpecService;

		// Token: 0x04000016 RID: 22
		private readonly CommonGoodCollectionIdsProvider _commonGoodCollectionIdsProvider;

		// Token: 0x04000017 RID: 23
		private readonly Dictionary<string, HashSet<string>> _collectionIdToGoods = new Dictionary<string, HashSet<string>>();

		// Token: 0x04000018 RID: 24
		private string _commonId;
	}
}
