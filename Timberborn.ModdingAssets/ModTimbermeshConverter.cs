using System;
using System.IO;
using Timberborn.AssetSystem;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x02000013 RID: 19
	public class ModTimbermeshConverter : IModFileConverter<BinaryData>
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000037D0 File Offset: 0x000019D0
		public bool CanConvert(FileInfo fileInfo)
		{
			return fileInfo.Extension == ModTimbermeshConverter.ValidExtension;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000037E4 File Offset: 0x000019E4
		public bool TryConvert(OrderedFile orderedFile, string path, SerializedObject metadata, out BinaryData asset)
		{
			FileInfo file = orderedFile.File;
			GameObject gameObject = new GameObject(Path.GetFileNameWithoutExtension(file.Name));
			gameObject.transform.SetParent(this._binaryDataCache.Value.transform);
			asset = gameObject.AddComponent<BinaryData>();
			asset.SetData(File.ReadAllBytes(file.FullName));
			return true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003842 File Offset: 0x00001A42
		public void Reset()
		{
			this._binaryDataCache = new Lazy<GameObject>(new Func<GameObject>(ModTimbermeshConverter.CreateBinaryDataCache));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000385B File Offset: 0x00001A5B
		public static GameObject CreateBinaryDataCache()
		{
			GameObject gameObject = new GameObject(ModTimbermeshConverter.BinaryDataCacheName);
			gameObject.SetActive(false);
			return gameObject;
		}

		// Token: 0x04000042 RID: 66
		public static readonly string BinaryDataCacheName = "BinaryDataCache";

		// Token: 0x04000043 RID: 67
		public static readonly string ValidExtension = ".timbermesh";

		// Token: 0x04000044 RID: 68
		public Lazy<GameObject> _binaryDataCache = new Lazy<GameObject>(new Func<GameObject>(ModTimbermeshConverter.CreateBinaryDataCache));
	}
}
