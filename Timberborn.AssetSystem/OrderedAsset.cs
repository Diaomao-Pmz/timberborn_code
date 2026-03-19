using System;
using UnityEngine;

namespace Timberborn.AssetSystem
{
	// Token: 0x0200000E RID: 14
	public readonly struct OrderedAsset
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002474 File Offset: 0x00000674
		public int Order { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000247C File Offset: 0x0000067C
		public Object Asset { get; }

		// Token: 0x0600002A RID: 42 RVA: 0x00002484 File Offset: 0x00000684
		public OrderedAsset(int order, Object asset)
		{
			this.Order = order;
			this.Asset = asset;
		}
	}
}
