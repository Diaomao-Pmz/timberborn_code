using System;
using System.Collections.Generic;

namespace Timberborn.Goods
{
	// Token: 0x02000019 RID: 25
	public interface IAllowedGoodProvider
	{
		// Token: 0x060000B6 RID: 182
		IEnumerable<string> GetAllowedGoods();
	}
}
