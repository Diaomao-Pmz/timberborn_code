using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.BlueprintSystem;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.ModdingAssets
{
	// Token: 0x0200000B RID: 11
	public class ModBlueprintConverter : IModFileConverter<BlueprintAsset>
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002B23 File Offset: 0x00000D23
		public bool CanConvert(FileInfo fileInfo)
		{
			return fileInfo.Name.EndsWith(BlueprintAsset.FullExtension);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B38 File Offset: 0x00000D38
		public bool TryConvert(OrderedFile orderedFile, string path, SerializedObject metadata, out BlueprintAsset asset)
		{
			FileInfo file = orderedFile.File;
			asset = BlueprintAsset.Create(path, File.ReadAllText(file.FullName), orderedFile.Source);
			asset.name = Path.GetFileNameWithoutExtension(file.Name);
			this._blueprints.Add(asset);
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B8C File Offset: 0x00000D8C
		public void Reset()
		{
			foreach (BlueprintAsset blueprintAsset in this._blueprints)
			{
				if (blueprintAsset != null)
				{
					Object.Destroy(blueprintAsset);
				}
			}
			this._blueprints.Clear();
		}

		// Token: 0x04000026 RID: 38
		public readonly List<BlueprintAsset> _blueprints = new List<BlueprintAsset>();
	}
}
