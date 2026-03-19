using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Goods
{
	// Token: 0x02000011 RID: 17
	public class GoodService : IGoodService, ILoadableSingleton
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002B6E File Offset: 0x00000D6E
		public GoodService(ISpecService specService, IGoodFilter goodFilter)
		{
			this._specService = specService;
			this._goodFilter = goodFilter;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002B9A File Offset: 0x00000D9A
		public ReadOnlyList<string> Goods
		{
			get
			{
				return this._goods.AsReadOnlyList<string>();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public void Load()
		{
			this.LoadGoods();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BAF File Offset: 0x00000DAF
		public bool HasGood(string id)
		{
			return this._goodSpecsById.ContainsKey(id);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BBD File Offset: 0x00000DBD
		public GoodSpec GetGoodOrNull(string id)
		{
			return CollectionExtensions.GetValueOrDefault<string, GoodSpec>(this._goodSpecsById, id);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002BCC File Offset: 0x00000DCC
		public GoodSpec GetGood(string id)
		{
			GoodSpec result;
			if (this._goodSpecsById.TryGetValue(id, out result))
			{
				return result;
			}
			throw new ArgumentException("GoodSpec with id " + id + " not found!");
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C00 File Offset: 0x00000E00
		public IEnumerable<string> GetGoodsForGroup(string groupId)
		{
			foreach (string text in this.Goods)
			{
				if (this.GetGood(text).GoodGroupId == groupId)
				{
					yield return text;
				}
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C17 File Offset: 0x00000E17
		public IEnumerable<string> GetGoodsForType(string goodType)
		{
			foreach (string text in this.Goods)
			{
				if (this.GetGood(text).GoodType == goodType)
				{
					yield return text;
				}
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C30 File Offset: 0x00000E30
		public void LoadGoods()
		{
			foreach (GoodSpec goodSpec3 in from goodSpec in this._specService.GetSpecs<GoodSpec>()
			orderby goodSpec.GoodOrder
			select goodSpec)
			{
				if (goodSpec3.Blueprint.IsAllowedByFeatureToggles() && this._goodFilter.IsUsable(goodSpec3))
				{
					this.AddGood(goodSpec3);
				}
			}
			foreach (GoodSpec goodSpec2 in this._goodSpecsById.Values.ToList<GoodSpec>())
			{
				this.BindGoodToItsBackwardCompatibleIds(goodSpec2);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D10 File Offset: 0x00000F10
		public void AddGood(GoodSpec goodSpec)
		{
			this._goods.Add(goodSpec.Id);
			this._goodSpecsById[goodSpec.Id] = goodSpec;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D38 File Offset: 0x00000F38
		public void BindGoodToItsBackwardCompatibleIds(GoodSpec goodSpec)
		{
			foreach (string text in goodSpec.BackwardCompatibleIds)
			{
				this._goodSpecsById.TryAdd(text, goodSpec);
			}
		}

		// Token: 0x0400001E RID: 30
		public readonly ISpecService _specService;

		// Token: 0x0400001F RID: 31
		public readonly IGoodFilter _goodFilter;

		// Token: 0x04000020 RID: 32
		public readonly List<string> _goods = new List<string>();

		// Token: 0x04000021 RID: 33
		public readonly Dictionary<string, GoodSpec> _goodSpecsById = new Dictionary<string, GoodSpec>();
	}
}
