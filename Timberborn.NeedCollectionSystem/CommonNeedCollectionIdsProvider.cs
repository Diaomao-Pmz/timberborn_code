using System;
using System.Collections.Generic;

namespace Timberborn.NeedCollectionSystem
{
	// Token: 0x02000007 RID: 7
	public class CommonNeedCollectionIdsProvider : INeedCollectionIdsProvider
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public IEnumerable<string> GetNeedCollectionIds()
		{
			yield return CommonNeedCollectionIdsProvider.CommonCollectionId;
			yield break;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string CommonCollectionId = "Common";
	}
}
