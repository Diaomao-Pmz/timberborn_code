using System;
using System.Collections.Generic;

namespace Timberborn.Goods
{
	// Token: 0x02000008 RID: 8
	public class GoodAmountComparer : IComparer<GoodAmount>
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002140 File Offset: 0x00000340
		public int Compare(GoodAmount x, GoodAmount y)
		{
			int num = x.Amount.CompareTo(y.Amount);
			if (num == 0)
			{
				return string.Compare(x.GoodId, y.GoodId, StringComparison.Ordinal);
			}
			return num;
		}
	}
}
