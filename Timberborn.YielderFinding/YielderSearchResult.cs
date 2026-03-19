using System;
using Timberborn.Goods;
using Timberborn.Yielding;

namespace Timberborn.YielderFinding
{
	// Token: 0x0200000B RID: 11
	public readonly struct YielderSearchResult
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002462 File Offset: 0x00000662
		public Yielder Yielder { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000246A File Offset: 0x0000066A
		public GoodAmount Yield { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002472 File Offset: 0x00000672
		public bool NoYielderInRange { get; }

		// Token: 0x0600001E RID: 30 RVA: 0x0000247A File Offset: 0x0000067A
		public YielderSearchResult(Yielder yielder, GoodAmount yield, bool noYielderInRange)
		{
			this.Yielder = yielder;
			this.Yield = yield;
			this.NoYielderInRange = noYielderInRange;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002491 File Offset: 0x00000691
		public static YielderSearchResult CreateSearchResult(Yielder yielder, GoodAmount yield)
		{
			return new YielderSearchResult(yielder, yield, false);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000249C File Offset: 0x0000069C
		public static YielderSearchResult CreateNoYielderInRange()
		{
			return new YielderSearchResult(null, default(GoodAmount), true);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024BC File Offset: 0x000006BC
		public static YielderSearchResult CreateEmpty()
		{
			return new YielderSearchResult(null, default(GoodAmount), false);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000024D9 File Offset: 0x000006D9
		public bool HasYielder
		{
			get
			{
				return this.Yielder;
			}
		}
	}
}
