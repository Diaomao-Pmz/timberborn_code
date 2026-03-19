using System;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ConstructionGuidelines
{
	// Token: 0x02000014 RID: 20
	public class TileDrawerFactory : ILoadableSingleton
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003519 File Offset: 0x00001719
		public TileDrawerFactory(IAssetLoader assetLoader, MeshDrawerFactory meshDrawerFactory, ISpecService specService)
		{
			this._assetLoader = assetLoader;
			this._meshDrawerFactory = meshDrawerFactory;
			this._specService = specService;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003538 File Offset: 0x00001738
		public void Load()
		{
			TileDrawerFactorySpec singleSpec = this._specService.GetSingleSpec<TileDrawerFactorySpec>();
			this._mesh = this._assetLoader.Load<Mesh>(singleSpec.MeshResourcePath);
			this._tilesOnSameLevelMaterial = this._assetLoader.Load<Material>(singleSpec.TilesOnSameLevelMaterialResourcePath);
			this._tilesBelowMaterial = this._assetLoader.Load<Material>(singleSpec.TilesBelowMaterialResourcePath);
			this._tilesAboveMaterial = this._assetLoader.Load<Material>(singleSpec.TilesAboveMaterialResourcePath);
			this._footprintTilesMaterial = this._assetLoader.Load<Material>(singleSpec.FootprintTilesMaterialResourcePath);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035C4 File Offset: 0x000017C4
		public MeshDrawer CrateSameLevelTileDrawer()
		{
			return this._meshDrawerFactory.Create(this._mesh, this._tilesOnSameLevelMaterial);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000035DD File Offset: 0x000017DD
		public MeshDrawer CreateBelowTileDrawer()
		{
			return this._meshDrawerFactory.Create(this._mesh, this._tilesBelowMaterial);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000035F6 File Offset: 0x000017F6
		public MeshDrawer CreateAboveTileDrawer()
		{
			return this._meshDrawerFactory.Create(this._mesh, this._tilesAboveMaterial);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000360F File Offset: 0x0000180F
		public MeshDrawer CreateFootprintTileDrawer()
		{
			return this._meshDrawerFactory.Create(this._mesh, this._footprintTilesMaterial);
		}

		// Token: 0x04000052 RID: 82
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000053 RID: 83
		public readonly MeshDrawerFactory _meshDrawerFactory;

		// Token: 0x04000054 RID: 84
		public readonly ISpecService _specService;

		// Token: 0x04000055 RID: 85
		public Mesh _mesh;

		// Token: 0x04000056 RID: 86
		public Material _tilesOnSameLevelMaterial;

		// Token: 0x04000057 RID: 87
		public Material _tilesBelowMaterial;

		// Token: 0x04000058 RID: 88
		public Material _tilesAboveMaterial;

		// Token: 0x04000059 RID: 89
		public Material _footprintTilesMaterial;
	}
}
