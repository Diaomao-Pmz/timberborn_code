using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Timberborn.AssetSystem;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using Timberborn.Timbermesh;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x0200000E RID: 14
	public class TerrainBlockRepository : ILoadableSingleton
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002957 File Offset: 0x00000B57
		public TerrainBlockRepository(SurfaceBlockCollectionFactory surfaceBlockCollectionFactory, IAssetLoader assetLoader, TimbermeshImporter timbermeshImporter)
		{
			this._surfaceBlockCollectionFactory = surfaceBlockCollectionFactory;
			this._assetLoader = assetLoader;
			this._timbermeshImporter = timbermeshImporter;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002974 File Offset: 0x00000B74
		public void Load()
		{
			IEnumerable<LoadedAsset<BinaryData>> binaryData = this._assetLoader.LoadAll<BinaryData>("Environment/Land");
			Transform transform = new GameObject("TerrainBlockRepository").transform;
			this.ImportModels(binaryData, transform);
			Object.Destroy(transform.gameObject);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029B5 File Offset: 0x00000BB5
		public ImmutableArray<IntermediateMesh> GetTerrainBlocks(SurfaceBlockShape shape)
		{
			return this._surfaceBlockCollection.GetVariations(shape);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029C4 File Offset: 0x00000BC4
		public void ImportModels(IEnumerable<LoadedAsset<BinaryData>> binaryData, Transform parent)
		{
			List<GameObject> list = new List<GameObject>();
			foreach (LoadedAsset<BinaryData> loadedAsset in binaryData)
			{
				using (MemoryStream memoryStream = new MemoryStream(loadedAsset.Asset.Bytes))
				{
					this._timbermeshImporter.Import(memoryStream, parent);
					GameObject gameObject = parent.GetChild(parent.childCount - 1).gameObject;
					gameObject.name = loadedAsset.Asset.name;
					list.Add(gameObject);
				}
			}
			this._surfaceBlockCollection = this._surfaceBlockCollectionFactory.CreateFromModels(list);
		}

		// Token: 0x0400001C RID: 28
		public readonly SurfaceBlockCollectionFactory _surfaceBlockCollectionFactory;

		// Token: 0x0400001D RID: 29
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400001E RID: 30
		public readonly TimbermeshImporter _timbermeshImporter;

		// Token: 0x0400001F RID: 31
		public SurfaceBlockCollection _surfaceBlockCollection;
	}
}
