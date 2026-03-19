using System;
using System.Collections.Generic;

namespace Timberborn.GoodCollectionSystem
{
	// Token: 0x02000007 RID: 7
	public class CommonGoodCollectionIdsProvider : IGoodCollectionIdsProvider
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public IEnumerable<string> GetGoodCollectionIds()
		{
			yield return CommonGoodCollectionIdsProvider.CommonCollectionId;
			yield break;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string CommonCollectionId = "Common";
	}
}
