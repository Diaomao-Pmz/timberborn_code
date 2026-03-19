using System;

namespace Timberborn.AssetSystem
{
	// Token: 0x0200000D RID: 13
	public readonly struct LoadedAsset<T>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002445 File Offset: 0x00000645
		public T Asset { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000244D File Offset: 0x0000064D
		public bool IsBuiltIn { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002455 File Offset: 0x00000655
		public int Order { get; }

		// Token: 0x06000027 RID: 39 RVA: 0x0000245D File Offset: 0x0000065D
		public LoadedAsset(T asset, bool isBuiltIn, int order)
		{
			this.Asset = asset;
			this.IsBuiltIn = isBuiltIn;
			this.Order = order;
		}
	}
}
