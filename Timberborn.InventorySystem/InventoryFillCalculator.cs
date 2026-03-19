using System;
using System.Runtime.CompilerServices;
using Timberborn.Common;
using Timberborn.Goods;
using UnityEngine;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000017 RID: 23
	public class InventoryFillCalculator
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00004069 File Offset: 0x00002269
		public float GetInputFillPercentage(Inventory inventory)
		{
			return InventoryFillCalculator.GetInventoryFillPercentage(inventory, inventory.InputGoods, false).Item2;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000407D File Offset: 0x0000227D
		public float GetOutputFillPercentage(Inventory inventory)
		{
			return InventoryFillCalculator.GetInventoryFillPercentage(inventory, inventory.OutputGoods, false).Item1;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004091 File Offset: 0x00002291
		public float GetInStockOutputFillPercentage(Inventory inventory)
		{
			return InventoryFillCalculator.GetInventoryFillPercentage(inventory, inventory.OutputGoods, true).Item1;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000040A8 File Offset: 0x000022A8
		[return: TupleElementNames(new string[]
		{
			"average",
			"lowest"
		})]
		public static ValueTuple<float, float> GetInventoryFillPercentage(Inventory inventory, ReadOnlyHashSet<string> goods, bool onlyInStock)
		{
			float num = 1f;
			int num2 = 0;
			int num3 = 0;
			foreach (StorableGoodAmount storableGoodAmount in inventory.AllowedGoods)
			{
				string goodId = storableGoodAmount.StorableGood.GoodId;
				if (goods.Contains(goodId))
				{
					int num4 = inventory.LimitedAmount(goodId);
					if (num4 > 0)
					{
						int num5 = inventory.AmountInStock(goodId);
						if (!onlyInStock || num5 > 0)
						{
							num2 += num4;
							num3 += num5;
							float num6 = (float)num5 / (float)num4;
							if (num6 < num)
							{
								num = num6;
							}
						}
					}
				}
			}
			num2 = Math.Min(num2, inventory.Capacity);
			return new ValueTuple<float, float>((num2 == 0) ? 0f : Mathf.Clamp01((float)num3 / (float)num2), num);
		}
	}
}
