using System;
using System.Collections.Generic;
using Timberborn.Goods;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x0200000F RID: 15
	public static class WaterContaminationGoodToWaterContaminationAmountConverter
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000026C0 File Offset: 0x000008C0
		public static float GetWaterContaminationAmount(IReadOnlyList<GoodAmountSpec> goods)
		{
			for (int i = 0; i < goods.Count; i++)
			{
				GoodAmountSpec goodAmountSpec = goods[i];
				if (goodAmountSpec.Id == WaterContaminationGoodToWaterContaminationAmountConverter.WaterContaminationId)
				{
					return (float)goodAmountSpec.Amount * WaterContaminationGoodToWaterContaminationAmountConverter.WaterContaminationAmountConversion;
				}
			}
			return 0f;
		}

		// Token: 0x04000016 RID: 22
		public static readonly string WaterContaminationId = "Badwater";

		// Token: 0x04000017 RID: 23
		public static readonly float WaterContaminationAmountConversion = 0.2f;
	}
}
