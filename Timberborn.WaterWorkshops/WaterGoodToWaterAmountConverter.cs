using System;
using System.Collections.Generic;
using Timberborn.Goods;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x02000010 RID: 16
	public static class WaterGoodToWaterAmountConverter
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002724 File Offset: 0x00000924
		public static float GetWaterAmount(IReadOnlyList<GoodAmountSpec> goods)
		{
			for (int i = 0; i < goods.Count; i++)
			{
				GoodAmountSpec goodAmountSpec = goods[i];
				if (goodAmountSpec.Id == WaterGoodToWaterAmountConverter.WaterId)
				{
					return (float)goodAmountSpec.Amount * WaterGoodToWaterAmountConverter.WaterAmountConversion;
				}
			}
			return 0f;
		}

		// Token: 0x04000018 RID: 24
		public static readonly string WaterId = "Water";

		// Token: 0x04000019 RID: 25
		public static readonly float WaterAmountConversion = 0.2f;
	}
}
