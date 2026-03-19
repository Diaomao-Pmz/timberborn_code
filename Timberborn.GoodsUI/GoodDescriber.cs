using System;
using Timberborn.Goods;
using Timberborn.UIFormatters;
using UnityEngine;

namespace Timberborn.GoodsUI
{
	// Token: 0x02000005 RID: 5
	public class GoodDescriber
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020DE File Offset: 0x000002DE
		public GoodDescriber(ResourceAmountFormatter resourceAmountFormatter, IGoodService goodService)
		{
			this._resourceAmountFormatter = resourceAmountFormatter;
			this._goodService = goodService;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
		public DescribedGood GetDescribedGood(string goodId)
		{
			GoodSpec good = this._goodService.GetGood(goodId);
			return new DescribedGood(good.PluralDisplayName.Value, good.IconSmall.Value);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212C File Offset: 0x0000032C
		public string Describe(string goodId)
		{
			return this.GetDescribedGood(goodId).DisplayName;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		public string Describe(GoodAmount goodAmount)
		{
			return this._resourceAmountFormatter.Format(this.Describe(goodAmount.GoodId), goodAmount.Amount);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216C File Offset: 0x0000036C
		public Sprite GetIcon(string goodId)
		{
			return this.GetDescribedGood(goodId).Icon;
		}

		// Token: 0x04000008 RID: 8
		public readonly ResourceAmountFormatter _resourceAmountFormatter;

		// Token: 0x04000009 RID: 9
		public readonly IGoodService _goodService;
	}
}
