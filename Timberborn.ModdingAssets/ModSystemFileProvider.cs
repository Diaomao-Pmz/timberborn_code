using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.AssetSystem;
using Timberborn.Modding;
using Timberborn.SerializationSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x0200000E RID: 14
	public class ModSystemFileProvider<T> : ILoadableSingleton, IAssetProvider where T : Object
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002E8E File Offset: 0x0000108E
		public ModSystemFileProvider(ModRepository modRepository, SerializedObjectReaderWriter serializedObjectReaderWriter, IModFileConverter<T> modFileConverter)
		{
			this._modRepository = modRepository;
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
			this._modFileConverter = modFileConverter;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000026F5 File Offset: 0x000008F5
		public bool IsBuiltIn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002EC4 File Offset: 0x000010C4
		public void Load()
		{
			foreach (Mod mod in this._modRepository.EnabledMods)
			{
				this.CacheFilesFromMod(mod.ModDirectory.Directory, mod);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002F24 File Offset: 0x00001124
		public bool TryLoad<TU>(string path, out OrderedAsset orderedAsset) where TU : Object
		{
			List<OrderedFile> list;
			if (typeof(T) == typeof(TU) && this._filePaths.TryGetValue(path, out list))
			{
				List<OrderedFile> list2 = list;
				OrderedAsset orderedAsset2;
				if (this.TryGetFromCacheOrConvert(list2[list2.Count - 1], path, out orderedAsset2))
				{
					orderedAsset = orderedAsset2;
					return true;
				}
			}
			orderedAsset = default(OrderedAsset);
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F86 File Offset: 0x00001186
		public IEnumerable<OrderedAsset> LoadAll<TU>(string path) where TU : Object
		{
			if (typeof(T) == typeof(TU))
			{
				foreach (KeyValuePair<string, List<OrderedFile>> keyValuePair in this._filePaths)
				{
					string text;
					List<OrderedFile> list;
					keyValuePair.Deconstruct(ref text, ref list);
					string key = text;
					List<OrderedFile> list2 = list;
					if (key.StartsWith(path))
					{
						foreach (OrderedFile orderedFile in list2)
						{
							OrderedAsset orderedAsset;
							if (this.TryGetFromCacheOrConvert(orderedFile, key, out orderedAsset))
							{
								yield return orderedAsset;
							}
						}
						List<OrderedFile>.Enumerator enumerator2 = default(List<OrderedFile>.Enumerator);
					}
					key = null;
				}
				Dictionary<string, List<OrderedFile>>.Enumerator enumerator = default(Dictionary<string, List<OrderedFile>>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F9D File Offset: 0x0000119D
		public void Reset()
		{
			this._cache.Clear();
			this._modFileConverter.Reset();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002FB8 File Offset: 0x000011B8
		public void CacheFilesFromMod(DirectoryInfo modDirectory, Mod mod)
		{
			foreach (FileInfo fileInfo in modDirectory.GetFiles("*", SearchOption.AllDirectories))
			{
				if (this._modFileConverter.CanConvert(fileInfo))
				{
					string key = AssetPathHelper.NormalizeAssetPath(fileInfo.FullName, modDirectory.FullName);
					if (!this._filePaths.ContainsKey(key))
					{
						this._filePaths[key] = new List<OrderedFile>();
					}
					this._filePaths[key].Add(new OrderedFile(this._modRepository.Mods.IndexOf(mod), fileInfo, mod.DisplayName));
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003058 File Offset: 0x00001258
		public bool TryGetFromCacheOrConvert(OrderedFile orderedFile, string path, out OrderedAsset asset)
		{
			FileInfo file = orderedFile.File;
			OrderedAsset orderedAsset;
			if (this._cache.TryGetValue(file.FullName, out orderedAsset))
			{
				asset = orderedAsset;
				return true;
			}
			T t;
			if (this._modFileConverter.TryConvert(orderedFile, path, this.GetMetadata(file), out t))
			{
				asset = new OrderedAsset(orderedFile.Order, t);
				this._cache.Add(file.FullName, asset);
				return true;
			}
			Debug.LogWarning("Failed to convert " + file.FullName + " to " + typeof(T).Name);
			asset = default(OrderedAsset);
			return false;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003108 File Offset: 0x00001308
		public SerializedObject GetMetadata(FileInfo fileInfo)
		{
			string path = fileInfo.FullName + ModSystemFileProvider<T>.MetadataExtension;
			if (File.Exists(path))
			{
				using (FileStream fileStream = File.OpenRead(path))
				{
					return this._serializedObjectReaderWriter.ReadJson(fileStream);
				}
			}
			return ModSystemFileProvider<T>.EmptyMetadata;
		}

		// Token: 0x0400002C RID: 44
		public static readonly string MetadataExtension = ".meta.json";

		// Token: 0x0400002D RID: 45
		public static readonly SerializedObject EmptyMetadata = new SerializedObject();

		// Token: 0x0400002E RID: 46
		public readonly ModRepository _modRepository;

		// Token: 0x0400002F RID: 47
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;

		// Token: 0x04000030 RID: 48
		public readonly IModFileConverter<T> _modFileConverter;

		// Token: 0x04000031 RID: 49
		public readonly Dictionary<string, List<OrderedFile>> _filePaths = new Dictionary<string, List<OrderedFile>>();

		// Token: 0x04000032 RID: 50
		public readonly Dictionary<string, OrderedAsset> _cache = new Dictionary<string, OrderedAsset>();
	}
}
