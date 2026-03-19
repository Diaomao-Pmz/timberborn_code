using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Goods;

namespace Timberborn.RecoverableGoodSystem
{
	// Token: 0x02000007 RID: 7
	public class RecoverableGoodRegistry
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022F5 File Offset: 0x000004F5
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022FD File Offset: 0x000004FD
		public int TotalAmount { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002306 File Offset: 0x00000506
		public ReadOnlyList<GoodAmount> GoodAmounts
		{
			get
			{
				return this._goodAmounts.AsReadOnlyList<GoodAmount>();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002314 File Offset: 0x00000514
		public void Add(GoodAmount goodAmount)
		{
			RecoverableGoodRegistry.AssertValueIsPositive(goodAmount);
			GoodAmount goodAmount2 = this.ClampGoodAmount(goodAmount);
			if (goodAmount2.Amount != 0)
			{
				this.AddInternal(goodAmount2);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002340 File Offset: 0x00000540
		public void TakePercent(float percentage, ICollection<GoodAmount> takenGoods)
		{
			for (int i = this._goodAmounts.Count - 1; i >= 0; i--)
			{
				GoodAmount goodAmount = this._goodAmounts[i];
				int num = (int)Math.Round((double)(percentage * (float)goodAmount.Amount));
				if (num > 0)
				{
					int num2 = goodAmount.Amount - num;
					if (num2 > 0)
					{
						this._goodAmounts[i] = new GoodAmount(goodAmount.GoodId, num2);
					}
					else
					{
						this._goodAmounts.RemoveAt(i);
					}
					this.TotalAmount -= num;
					takenGoods.Add(new GoodAmount(goodAmount.GoodId, num));
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023E1 File Offset: 0x000005E1
		public void Clear()
		{
			this._goodAmounts.Clear();
			this.TotalAmount = 0;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023F8 File Offset: 0x000005F8
		public GoodAmount ClampGoodAmount(GoodAmount goodAmount)
		{
			int num = int.MaxValue - this.TotalAmount;
			if (num < goodAmount.Amount)
			{
				return new GoodAmount(goodAmount.GoodId, num);
			}
			return goodAmount;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000242B File Offset: 0x0000062B
		public void AddInternal(GoodAmount goodAmount)
		{
			this.TotalAmount += goodAmount.Amount;
			if (!this.TryAddToExistingGood(goodAmount))
			{
				this._goodAmounts.Add(new GoodAmount(goodAmount.GoodId, goodAmount.Amount));
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002468 File Offset: 0x00000668
		public bool TryAddToExistingGood(GoodAmount goodAmount)
		{
			for (int i = 0; i < this._goodAmounts.Count; i++)
			{
				GoodAmount goodAmount2 = this._goodAmounts[i];
				if (goodAmount2.GoodId == goodAmount.GoodId)
				{
					this._goodAmounts[i] = new GoodAmount(goodAmount.GoodId, goodAmount.Amount + goodAmount2.Amount);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024D7 File Offset: 0x000006D7
		public static void AssertValueIsPositive(GoodAmount goodAmount)
		{
			Asserts.ValueIsInRange<int>(goodAmount.Amount, 0, int.MaxValue, "GoodAmount");
		}

		// Token: 0x04000010 RID: 16
		public readonly List<GoodAmount> _goodAmounts = new List<GoodAmount>();
	}
}
