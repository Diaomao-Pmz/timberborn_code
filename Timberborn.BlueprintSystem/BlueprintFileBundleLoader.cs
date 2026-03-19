using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AssetSystem;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000016 RID: 22
	public class BlueprintFileBundleLoader
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000035F0 File Offset: 0x000017F0
		public BlueprintFileBundleLoader(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003600 File Offset: 0x00001800
		public IEnumerable<BlueprintFileBundle> GetBundles(string rootPath)
		{
			return (from asset in this._assetLoader.LoadAll<BlueprintAsset>(rootPath)
			group asset.Asset by asset.Asset.Path.Replace(BlueprintAsset.OptionalExtension, BlueprintAsset.Extension)).Where(new Func<IGrouping<string, BlueprintAsset>, bool>(this.IsValidGroup)).Select(new Func<IGrouping<string, BlueprintAsset>, BlueprintFileBundle>(BlueprintFileBundle.CreateBundled));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003680 File Offset: 0x00001880
		public BlueprintFileBundle GetBundle(string path)
		{
			IGrouping<string, BlueprintAsset> grouping = (from asset in this._assetLoader.LoadAll<BlueprintAsset>(path).Concat(this._assetLoader.LoadAll<BlueprintAsset>(path.Replace(BlueprintAsset.Extension, BlueprintAsset.OptionalExtension)))
			group asset.Asset by asset.Asset.Path.Replace(BlueprintAsset.OptionalExtension, BlueprintAsset.Extension)).SingleOrDefault<IGrouping<string, BlueprintAsset>>();
			if (grouping != null)
			{
				if (!grouping.All((BlueprintAsset asset) => asset.Path.EndsWith(BlueprintAsset.OptionalExtension)))
				{
					return BlueprintFileBundle.CreateBundled(grouping);
				}
			}
			return null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003734 File Offset: 0x00001934
		public bool IsValidGroup(IGrouping<string, BlueprintAsset> assetGroup)
		{
			return assetGroup.Any((BlueprintAsset asset) => !asset.Path.EndsWith(BlueprintAsset.OptionalExtension));
		}

		// Token: 0x0400003E RID: 62
		public readonly IAssetLoader _assetLoader;
	}
}
