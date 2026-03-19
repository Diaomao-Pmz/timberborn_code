using System;
using Timberborn.AssetSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.FileBrowsing
{
	// Token: 0x0200000B RID: 11
	public class FileFilterProvider : ILoadableSingleton
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002B1A File Offset: 0x00000D1A
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002B22 File Offset: 0x00000D22
		public FileFilter Images { get; private set; }

		// Token: 0x06000046 RID: 70 RVA: 0x00002B2B File Offset: 0x00000D2B
		public FileFilterProvider(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B3A File Offset: 0x00000D3A
		public void Load()
		{
			this.Images = new FileFilter(this._assetLoader.Load<Sprite>("UI/Images/Core/image-icon"), new string[]
			{
				".png",
				".jpg"
			});
		}

		// Token: 0x0400002A RID: 42
		public readonly IAssetLoader _assetLoader;
	}
}
