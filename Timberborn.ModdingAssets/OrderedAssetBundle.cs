using System;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x02000014 RID: 20
	public readonly struct OrderedAssetBundle
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000038A3 File Offset: 0x00001AA3
		public int Order { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000038AB File Offset: 0x00001AAB
		public AssetBundle AssetBundle { get; }

		// Token: 0x0600006A RID: 106 RVA: 0x000038B3 File Offset: 0x00001AB3
		public OrderedAssetBundle(int order, AssetBundle assetBundle)
		{
			this.Order = order;
			this.AssetBundle = assetBundle;
		}
	}
}
