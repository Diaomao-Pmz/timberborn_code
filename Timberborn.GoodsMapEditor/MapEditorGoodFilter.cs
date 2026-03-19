using System;
using Timberborn.Goods;

namespace Timberborn.GoodsMapEditor
{
	// Token: 0x02000005 RID: 5
	public class MapEditorGoodFilter : IGoodFilter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D6 File Offset: 0x000002D6
		public bool IsUsable(GoodSpec goodSpec)
		{
			return true;
		}
	}
}
