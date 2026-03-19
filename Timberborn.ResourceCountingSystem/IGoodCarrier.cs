using System;
using Timberborn.Goods;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x02000007 RID: 7
	public interface IGoodCarrier
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001E RID: 30
		bool CountGoodsAsAvailable { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001F RID: 31
		bool IsCarrying { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32
		GoodAmount CarriedGoods { get; }
	}
}
