using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.Common;
using Timberborn.Modding;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x02000005 RID: 5
	public class ModAssetBundleLoader : ILoadableSingleton
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020C0 File Offset: 0x000002C0
		public ModAssetBundleLoader(ModRepository modRepository)
		{
			this._modRepository = modRepository;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020DA File Offset: 0x000002DA
		public ReadOnlyList<OrderedAssetBundle> LoadedAssetBundles
		{
			get
			{
				return this._loadedAssetBundles.AsReadOnlyList<OrderedAssetBundle>();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E7 File Offset: 0x000002E7
		public void Load()
		{
			this.FindPreloadedAssetBundles();
			this.Reload();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F8 File Offset: 0x000002F8
		public void Reload()
		{
			foreach (OrderedAssetBundle orderedAssetBundle in this._loadedAssetBundles)
			{
				orderedAssetBundle.AssetBundle.Unload(true);
			}
			this._loadedAssetBundles.Clear();
			this.LoadModAssetBundles();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002164 File Offset: 0x00000364
		public void FindPreloadedAssetBundles()
		{
			foreach (AssetBundle assetBundle in AssetBundle.GetAllLoadedAssetBundles())
			{
				foreach (OrderedFile orderedFile in this.GetAllModAssetBundleFiles())
				{
					if (orderedFile.File.Name == assetBundle.name)
					{
						this._loadedAssetBundles.Add(new OrderedAssetBundle(orderedFile.Order, assetBundle));
						break;
					}
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002214 File Offset: 0x00000414
		public void LoadModAssetBundles()
		{
			foreach (OrderedFile orderedFile in this.GetAllModAssetBundleFiles())
			{
				AssetBundle assetBundle = AssetBundle.LoadFromFile(orderedFile.File.FullName);
				if (!assetBundle)
				{
					throw new InvalidOperationException("Failed to load asset bundle " + orderedFile.File.FullName + ", check logs for more information");
				}
				this._loadedAssetBundles.Add(new OrderedAssetBundle(orderedFile.Order, assetBundle));
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022B0 File Offset: 0x000004B0
		public IEnumerable<OrderedFile> GetAllModAssetBundleFiles()
		{
			foreach (Mod mod in this._modRepository.EnabledMods)
			{
				foreach (FileInfo file in ModAssetBundleLoader.GetAssetBundleFiles(mod.ModDirectory.Directory))
				{
					yield return new OrderedFile(this._modRepository.Mods.IndexOf(mod), file, mod.DisplayName);
				}
				IEnumerator<FileInfo> enumerator2 = null;
				mod = null;
			}
			IEnumerator<Mod> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C0 File Offset: 0x000004C0
		public static IEnumerable<FileInfo> GetAssetBundleFiles(DirectoryInfo modDirectory)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(modDirectory.FullName, ModAssetBundleLoader.AssetBundleDirectory));
			if (directoryInfo.Exists)
			{
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					if (ModAssetBundleLoader.IsNotManifestFile(fileInfo) && ModAssetBundleLoader.CanBeLoadedOnCurrentPlatform(fileInfo))
					{
						yield return fileInfo;
					}
				}
				FileInfo[] array = null;
			}
			yield break;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022D0 File Offset: 0x000004D0
		public static bool IsNotManifestFile(FileInfo fileInfo)
		{
			return fileInfo.Extension != ".manifest";
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022E2 File Offset: 0x000004E2
		public static bool CanBeLoadedOnCurrentPlatform(FileInfo fileInfo)
		{
			return (!ModAssetBundleLoader.HasSuffix(fileInfo, ModAssetBundleLoader.WinAssetBundleSuffix) || !ApplicationPlatform.IsMacOS()) && (!ModAssetBundleLoader.HasSuffix(fileInfo, ModAssetBundleLoader.MacAssetBundleSuffix) || ApplicationPlatform.IsMacOS());
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000230E File Offset: 0x0000050E
		public static bool HasSuffix(FileInfo fileInfo, string suffix)
		{
			return fileInfo.Name.EndsWith(suffix) || Path.GetFileNameWithoutExtension(fileInfo.Name).EndsWith(suffix);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string AssetBundleDirectory = "AssetBundles";

		// Token: 0x04000007 RID: 7
		public static readonly string WinAssetBundleSuffix = "_win";

		// Token: 0x04000008 RID: 8
		public static readonly string MacAssetBundleSuffix = "_mac";

		// Token: 0x04000009 RID: 9
		public readonly ModRepository _modRepository;

		// Token: 0x0400000A RID: 10
		public readonly List<OrderedAssetBundle> _loadedAssetBundles = new List<OrderedAssetBundle>();
	}
}
