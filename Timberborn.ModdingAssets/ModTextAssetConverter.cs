using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.BlueprintSystem;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x02000010 RID: 16
	public class ModTextAssetConverter : IModFileConverter<TextAsset>
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000033FB File Offset: 0x000015FB
		public bool CanConvert(FileInfo fileInfo)
		{
			return !fileInfo.Name.EndsWith(BlueprintAsset.FullExtension) && ModTextAssetConverter.ValidExtensions.Contains(fileInfo.Extension);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003424 File Offset: 0x00001624
		public bool TryConvert(OrderedFile orderedFile, string path, SerializedObject metadata, out TextAsset asset)
		{
			FileInfo file = orderedFile.File;
			asset = new TextAsset(File.ReadAllText(file.FullName))
			{
				name = Path.GetFileNameWithoutExtension(file.FullName)
			};
			this._assets.Add(asset);
			return true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000346C File Offset: 0x0000166C
		public void Reset()
		{
			foreach (TextAsset textAsset in this._assets)
			{
				Object.Destroy(textAsset);
			}
			this._assets.Clear();
		}

		// Token: 0x0400003C RID: 60
		public static readonly List<string> ValidExtensions = new List<string>
		{
			".txt",
			".json",
			".xml",
			".csv"
		};

		// Token: 0x0400003D RID: 61
		public readonly List<TextAsset> _assets = new List<TextAsset>();
	}
}
