using System;

namespace Timberborn.Goods
{
	// Token: 0x0200001F RID: 31
	public readonly struct StorableGoodAmount
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003ED4 File Offset: 0x000020D4
		public StorableGood StorableGood { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003EDC File Offset: 0x000020DC
		public int Amount { get; }

		// Token: 0x060000E1 RID: 225 RVA: 0x00003EE4 File Offset: 0x000020E4
		public StorableGoodAmount(StorableGood storableGood, int amount)
		{
			this.StorableGood = storableGood;
			this.Amount = amount;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003EF4 File Offset: 0x000020F4
		public override string ToString()
		{
			return string.Format("{0}x {1}", this.Amount, this.StorableGood.GoodId);
		}
	}
}
