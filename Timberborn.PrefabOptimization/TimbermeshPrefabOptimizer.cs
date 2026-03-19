using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.AssetSystem;
using Timberborn.Timbermesh;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000023 RID: 35
	public class TimbermeshPrefabOptimizer : IPrefabOptimizer
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00005232 File Offset: 0x00003432
		public TimbermeshPrefabOptimizer(IAssetLoader assetLoader, TimbermeshImporter timbermeshImporter)
		{
			this._assetLoader = assetLoader;
			this._timbermeshImporter = timbermeshImporter;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005248 File Offset: 0x00003448
		public void Optimize(GameObject prefab)
		{
			TimbermeshDescription[] componentsInChildren = prefab.GetComponentsInChildren<TimbermeshDescription>(true);
			this.ImportTimbermeshModels(componentsInChildren);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005264 File Offset: 0x00003464
		public void ImportTimbermeshModels(IReadOnlyList<TimbermeshDescription> timbermeshDescriptions)
		{
			for (int i = 0; i < timbermeshDescriptions.Count; i++)
			{
				this.ImportTimbermeshModel(timbermeshDescriptions[i]);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005290 File Offset: 0x00003490
		public void ImportTimbermeshModel(TimbermeshDescription timbermeshDescription)
		{
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(this._assetLoader.Load<BinaryData>(timbermeshDescription.ModelName).Bytes))
				{
					this._timbermeshImporter.Import(memoryStream, timbermeshDescription.transform);
				}
			}
			catch (Exception)
			{
				Debug.LogError("Failed to import timbermesh model " + timbermeshDescription.ModelName + ".");
				throw;
			}
		}

		// Token: 0x0400008F RID: 143
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000090 RID: 144
		public readonly TimbermeshImporter _timbermeshImporter;
	}
}
