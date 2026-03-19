using System;
using System.Collections.Generic;
using Timberborn.AssetSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x02000008 RID: 8
	public class ModAssetBundleProvider : ILoadableSingleton, IAssetProvider
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000026DB File Offset: 0x000008DB
		public ModAssetBundleProvider(ModAssetBundleLoader modAssetBundleLoader)
		{
			this._modAssetBundleLoader = modAssetBundleLoader;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026F5 File Offset: 0x000008F5
		public bool IsBuiltIn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026F8 File Offset: 0x000008F8
		public void Load()
		{
			this.CachePaths();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002700 File Offset: 0x00000900
		public bool TryLoad<T>(string path, out OrderedAsset orderedAsset) where T : Object
		{
			List<ModAssetBundleProvider.AssetBundleAssetPath> list;
			if (this._assetPaths.TryGetValue(path, out list))
			{
				List<ModAssetBundleProvider.AssetBundleAssetPath> list2 = list;
				if (list2[list2.Count - 1].TryLoad<T>(out orderedAsset))
				{
					return true;
				}
			}
			orderedAsset = default(OrderedAsset);
			return false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002740 File Offset: 0x00000940
		public IEnumerable<OrderedAsset> LoadAll<T>(string path) where T : Object
		{
			foreach (KeyValuePair<string, List<ModAssetBundleProvider.AssetBundleAssetPath>> keyValuePair in this._assetPaths)
			{
				string text;
				List<ModAssetBundleProvider.AssetBundleAssetPath> list;
				keyValuePair.Deconstruct(ref text, ref list);
				string text2 = text;
				List<ModAssetBundleProvider.AssetBundleAssetPath> list2 = list;
				if (text2.StartsWith(path))
				{
					foreach (ModAssetBundleProvider.AssetBundleAssetPath assetBundleAssetPath in list2)
					{
						OrderedAsset orderedAsset;
						if (assetBundleAssetPath.TryLoad<T>(out orderedAsset))
						{
							yield return orderedAsset;
						}
					}
					List<ModAssetBundleProvider.AssetBundleAssetPath>.Enumerator enumerator2 = default(List<ModAssetBundleProvider.AssetBundleAssetPath>.Enumerator);
				}
			}
			Dictionary<string, List<ModAssetBundleProvider.AssetBundleAssetPath>>.Enumerator enumerator = default(Dictionary<string, List<ModAssetBundleProvider.AssetBundleAssetPath>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002757 File Offset: 0x00000957
		public void Reset()
		{
			this._assetPaths.Clear();
			this._modAssetBundleLoader.Reload();
			this.CachePaths();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002778 File Offset: 0x00000978
		public void CachePaths()
		{
			foreach (OrderedAssetBundle orderedAssetBundle in this._modAssetBundleLoader.LoadedAssetBundles)
			{
				foreach (string assetPath in orderedAssetBundle.AssetBundle.GetAllAssetNames())
				{
					string key = AssetPathHelper.NormalizeAssetPath(assetPath);
					if (!this._assetPaths.ContainsKey(key))
					{
						this._assetPaths[key] = new List<ModAssetBundleProvider.AssetBundleAssetPath>();
					}
					this._assetPaths[key].Add(new ModAssetBundleProvider.AssetBundleAssetPath(orderedAssetBundle.AssetBundle, assetPath, orderedAssetBundle.Order));
				}
			}
		}

		// Token: 0x04000019 RID: 25
		public readonly ModAssetBundleLoader _modAssetBundleLoader;

		// Token: 0x0400001A RID: 26
		public readonly Dictionary<string, List<ModAssetBundleProvider.AssetBundleAssetPath>> _assetPaths = new Dictionary<string, List<ModAssetBundleProvider.AssetBundleAssetPath>>();

		// Token: 0x02000009 RID: 9
		public readonly struct AssetBundleAssetPath
		{
			// Token: 0x0600002B RID: 43 RVA: 0x00002848 File Offset: 0x00000A48
			public AssetBundleAssetPath(AssetBundle assetBundle, string assetPath, int order)
			{
				this._assetBundle = assetBundle;
				this._assetPath = assetPath;
				this._order = order;
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00002860 File Offset: 0x00000A60
			public bool TryLoad<T>(out OrderedAsset orderedAsset) where T : Object
			{
				T t = this._assetBundle.LoadAsset<T>(this._assetPath);
				if (!t && typeof(Component).IsAssignableFrom(typeof(T)))
				{
					GameObject gameObject = this._assetBundle.LoadAsset<GameObject>(this._assetPath);
					if (gameObject)
					{
						t = gameObject.GetComponent<T>();
					}
				}
				orderedAsset = new OrderedAsset(this._order, t);
				return t;
			}

			// Token: 0x0400001B RID: 27
			public readonly AssetBundle _assetBundle;

			// Token: 0x0400001C RID: 28
			public readonly string _assetPath;

			// Token: 0x0400001D RID: 29
			public readonly int _order;
		}
	}
}
