using System;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using UnityEngine;

namespace Timberborn.Carrying
{
	// Token: 0x0200000C RID: 12
	public class CarryAmountCalculator
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002657 File Offset: 0x00000857
		public CarryAmountCalculator(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002668 File Offset: 0x00000868
		public GoodAmount AmountToCarry(int liftingCapacity, string goodId, IAmountProvider input, IAmountProvider output)
		{
			GoodAmount good = new GoodAmount(goodId, output.UnreservedAmountInStock(goodId));
			return this.AmountToCarry(liftingCapacity, good, input);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002690 File Offset: 0x00000890
		public GoodAmount AmountToCarry(int liftingCapacity, GoodAmount good, IAmountProvider input)
		{
			GoodSpec good2 = this._goodService.GetGood(good.GoodId);
			int num = Math.Max(liftingCapacity / good2.Weight, 1);
			int num2 = input.UnreservedCapacity(good2.Id);
			int amount = Mathf.Min(new int[]
			{
				num,
				good.Amount,
				num2
			});
			return new GoodAmount(good.GoodId, amount);
		}

		// Token: 0x04000019 RID: 25
		public readonly IGoodService _goodService;
	}
}
