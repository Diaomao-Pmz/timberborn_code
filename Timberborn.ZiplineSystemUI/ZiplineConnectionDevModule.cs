using System;
using Timberborn.AssetSystem;
using Timberborn.Debugging;
using Timberborn.PrefabOptimization;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using Timberborn.ZiplineSystem;
using UnityEngine;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x0200000D RID: 13
	public class ZiplineConnectionDevModule : IDevModule, ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000029B9 File Offset: 0x00000BB9
		public ZiplineConnectionDevModule(ZiplineConnectionService ziplineConnectionService, MeshDrawerFactory markerDrawerFactory, IPrefabOptimizationChain prefabOptimizationChain, IAssetLoader assetLoader)
		{
			this._ziplineConnectionService = ziplineConnectionService;
			this._markerDrawerFactory = markerDrawerFactory;
			this._prefabOptimizationChain = prefabOptimizationChain;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029E0 File Offset: 0x00000BE0
		public void Load()
		{
			GameObject inputPrefab = this._assetLoader.Load<GameObject>(ZiplineConnectionDevModule.PrefabPath);
			GameObject gameObject = this._prefabOptimizationChain.Process(inputPrefab);
			this._meshDrawer = this._markerDrawerFactory.Create(gameObject.GetComponent<MeshFilter>().sharedMesh, gameObject.GetComponent<MeshRenderer>().sharedMaterial);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A32 File Offset: 0x00000C32
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle zipline cable blocks", delegate
			{
				this._enabled = !this._enabled;
			})).Build();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A5C File Offset: 0x00000C5C
		public void UpdateSingleton()
		{
			if (this._enabled)
			{
				foreach (Vector3Int coordinates in this._ziplineConnectionService.GetConnectionCoordinates())
				{
					this._meshDrawer.DrawAtCoordinates(coordinates, 0f);
				}
			}
		}

		// Token: 0x04000036 RID: 54
		public static readonly string PrefabPath = "UI/Markers/Debug/Tile";

		// Token: 0x04000037 RID: 55
		public readonly ZiplineConnectionService _ziplineConnectionService;

		// Token: 0x04000038 RID: 56
		public readonly MeshDrawerFactory _markerDrawerFactory;

		// Token: 0x04000039 RID: 57
		public readonly IPrefabOptimizationChain _prefabOptimizationChain;

		// Token: 0x0400003A RID: 58
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400003B RID: 59
		public MeshDrawer _meshDrawer;

		// Token: 0x0400003C RID: 60
		public bool _enabled;
	}
}
