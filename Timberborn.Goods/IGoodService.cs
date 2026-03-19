using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Goods
{
	// Token: 0x0200001B RID: 27
	public interface IGoodService
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B8 RID: 184
		ReadOnlyList<string> Goods { get; }

		// Token: 0x060000B9 RID: 185
		bool HasGood(string id);

		// Token: 0x060000BA RID: 186
		GoodSpec GetGoodOrNull(string id);

		// Token: 0x060000BB RID: 187
		GoodSpec GetGood(string id);

		// Token: 0x060000BC RID: 188
		IEnumerable<string> GetGoodsForGroup(string groupId);

		// Token: 0x060000BD RID: 189
		IEnumerable<string> GetGoodsForType(string goodType);
	}
}
