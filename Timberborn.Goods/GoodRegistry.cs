using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;

namespace Timberborn.Goods
{
	// Token: 0x0200000D RID: 13
	public class GoodRegistry
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000282A File Offset: 0x00000A2A
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002832 File Offset: 0x00000A32
		public int TotalAmount { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000283B File Offset: 0x00000A3B
		public ReadOnlyList<GoodAmount> Goods
		{
			get
			{
				return this._registry.AsReadOnlyList<GoodAmount>();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002848 File Offset: 0x00000A48
		public void Clear()
		{
			this._registry.Clear();
			this.TotalAmount = 0;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000285C File Offset: 0x00000A5C
		public void Add(GoodAmount good)
		{
			GoodRegistry.AssertValueIsPositive(good);
			this.Modify(good.GoodId, good.Amount);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002878 File Offset: 0x00000A78
		public void Subtract(GoodAmount good)
		{
			GoodRegistry.AssertValueIsPositive(good);
			this.Modify(good.GoodId, -good.Amount);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002898 File Offset: 0x00000A98
		public int Amount(string goodId)
		{
			for (int i = 0; i < this._registry.Count; i++)
			{
				GoodAmount goodAmount = this._registry[i];
				if (goodAmount.GoodId == goodId)
				{
					return goodAmount.Amount;
				}
			}
			return 0;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028E0 File Offset: 0x00000AE0
		public override string ToString()
		{
			return this.Goods.CollectionToString("Goods");
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028F8 File Offset: 0x00000AF8
		public void Modify(string goodId, int amount)
		{
			int num = this.GetAmountAndRemove(goodId) + amount;
			if (num != 0)
			{
				this._registry.Add(new GoodAmount(goodId, num));
			}
			this.TotalAmount += amount;
			this.ValidateTotalAmount();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002938 File Offset: 0x00000B38
		public static void AssertValueIsPositive(GoodAmount goodAmount)
		{
			Asserts.ValueIsInRange<int>(goodAmount.Amount, 0, int.MaxValue, "GoodAmount");
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002954 File Offset: 0x00000B54
		public int GetAmountAndRemove(string goodId)
		{
			for (int i = 0; i < this._registry.Count; i++)
			{
				if (this._registry[i].GoodId == goodId)
				{
					int amount = this._registry[i].Amount;
					this._registry.RemoveAt(i);
					return amount;
				}
			}
			return 0;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029B8 File Offset: 0x00000BB8
		public void ValidateTotalAmount()
		{
			if (this.TotalAmount < 0)
			{
				int num = this._registry.Sum((GoodAmount good) => good.Amount);
				throw new InvalidOperationException(string.Format("{0} {1} is negative ", "TotalAmount", this.TotalAmount) + string.Format("and sum of goods is {0}", num));
			}
		}

		// Token: 0x04000019 RID: 25
		public readonly List<GoodAmount> _registry = new List<GoodAmount>();
	}
}
