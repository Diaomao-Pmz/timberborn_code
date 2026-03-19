using System;

namespace Timberborn.Goods
{
	// Token: 0x02000007 RID: 7
	public readonly struct GoodAmount
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public string GoodId { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public int Amount { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public GoodAmount(string goodId, int amount)
		{
			this.GoodId = goodId;
			this.Amount = amount;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002120 File Offset: 0x00000320
		public override string ToString()
		{
			return string.Format("{0}x {1}", this.Amount, this.GoodId);
		}
	}
}
