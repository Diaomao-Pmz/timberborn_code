using System;
using Timberborn.Goods;

namespace Timberborn.Carrying
{
	// Token: 0x02000008 RID: 8
	public struct CarriedGoodsChangedEventArgs
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021B7 File Offset: 0x000003B7
		public readonly GoodAmount CarriedGoods { get; }

		// Token: 0x06000010 RID: 16 RVA: 0x000021BF File Offset: 0x000003BF
		public CarriedGoodsChangedEventArgs(GoodAmount carriedGoods)
		{
			this.CarriedGoods = carriedGoods;
		}
	}
}
