using System;
using Timberborn.Goods;

namespace Timberborn.Yielding
{
	// Token: 0x02000020 RID: 32
	public class YieldReservationCompletedEventArgs
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003FC4 File Offset: 0x000021C4
		public GoodAmount Yield { get; }

		// Token: 0x060000DD RID: 221 RVA: 0x00003FCC File Offset: 0x000021CC
		public YieldReservationCompletedEventArgs(GoodAmount yield)
		{
			this.Yield = yield;
		}
	}
}
