using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;

namespace Timberborn.AssetSystem
{
	// Token: 0x02000004 RID: 4
	public class AssetLoader : IAssetLoader
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public AssetLoader(IEnumerable<IAssetProvider> assetProviders)
		{
			this._assetProviders = assetProviders.ToImmutableArray<IAssetProvider>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public T Load<T>(string path) where T : Object
		{
			T t = this.LoadSafe<T>(path);
			if (t != null)
			{
				return t;
			}
			throw new InvalidOperationException("Failed to load asset at " + path);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210C File Offset: 0x0000030C
		public T LoadSafe<T>(string path) where T : Object
		{
			string path2 = AssetPathHelper.NormalizePath(path);
			OrderedAsset? orderedAsset = null;
			ImmutableArray<IAssetProvider>.Enumerator enumerator = this._assetProviders.GetEnumerator();
			while (enumerator.MoveNext())
			{
				OrderedAsset value;
				if (enumerator.Current.TryLoad<T>(path2, out value) && (orderedAsset == null || value.Order > orderedAsset.Value.Order))
				{
					orderedAsset = new OrderedAsset?(value);
				}
			}
			return (T)((object)((orderedAsset != null) ? orderedAsset.GetValueOrDefault().Asset : null));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000219C File Offset: 0x0000039C
		public IEnumerable<LoadedAsset<T>> LoadAll<T>(string path) where T : Object
		{
			string normalizedPath = AssetPathHelper.NormalizePath(path);
			return from loadedAsset in this._assetProviders.SelectMany((IAssetProvider assetProvider) => AssetLoader.CreateLoadedAssets<T>(assetProvider, normalizedPath))
			orderby loadedAsset.Order
			select loadedAsset;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021FC File Offset: 0x000003FC
		public void Reset()
		{
			foreach (IAssetProvider assetProvider in this._assetProviders)
			{
				assetProvider.Reset();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000222C File Offset: 0x0000042C
		public static IEnumerable<LoadedAsset<T>> CreateLoadedAssets<T>(IAssetProvider assetProvider, string normalizedPath) where T : Object
		{
			return from asset in assetProvider.LoadAll<T>(normalizedPath)
			select new LoadedAsset<T>((T)((object)asset.Asset), assetProvider.IsBuiltIn, asset.Order);
		}

		// Token: 0x04000006 RID: 6
		public readonly ImmutableArray<IAssetProvider> _assetProviders;
	}
}
