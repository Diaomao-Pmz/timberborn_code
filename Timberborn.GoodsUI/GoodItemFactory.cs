using System;
using Timberborn.Goods;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.GoodsUI
{
	// Token: 0x02000006 RID: 6
	public class GoodItemFactory
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002188 File Offset: 0x00000388
		public GoodItemFactory(DescribedAmountFactory describedAmountFactory, GoodDescriber goodDescriber)
		{
			this._describedAmountFactory = describedAmountFactory;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A0 File Offset: 0x000003A0
		public VisualElement Create(GoodAmountSpec goodAmount, bool bordered)
		{
			DescribedGood describedGood = this._goodDescriber.GetDescribedGood(goodAmount.Id);
			VisualElement result;
			if (bordered)
			{
				result = this._describedAmountFactory.CreateBordered(goodAmount.Amount.ToString(), describedGood.Icon, describedGood.DisplayName);
			}
			else
			{
				result = this._describedAmountFactory.CreatePlain("", goodAmount.Amount.ToString(), describedGood.Icon, describedGood.DisplayName);
			}
			return result;
		}

		// Token: 0x0400000A RID: 10
		public readonly DescribedAmountFactory _describedAmountFactory;

		// Token: 0x0400000B RID: 11
		public readonly GoodDescriber _goodDescriber;
	}
}
