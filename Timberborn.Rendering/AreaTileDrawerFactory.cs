using System;
using Timberborn.BlueprintSystem;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000008 RID: 8
	public class AreaTileDrawerFactory : ILoadableSingleton
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002468 File Offset: 0x00000668
		public AreaTileDrawerFactory(MapSize mapSize, ISpecService specService)
		{
			this._mapSize = mapSize;
			this._specService = specService;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000247E File Offset: 0x0000067E
		public void Load()
		{
			this._areaTileDrawerFactorySpec = this._specService.GetSingleSpec<AreaTileDrawerFactorySpec>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002494 File Offset: 0x00000694
		public AreaTileDrawer Create(Color color, GameObject parent)
		{
			GameObject gameObject = new GameObject(parent.name + "AreaTileDrawer");
			gameObject.transform.parent = parent.transform;
			Material material = new Material(this._areaTileDrawerFactorySpec.TileMaterial.Asset);
			material.SetColor(AreaTileDrawerFactory.ColorProperty, color);
			Vector2Int tileCount = WorldTiling.TileCount2D(this._mapSize.TerrainSize.x, this._mapSize.TerrainSize.y);
			return new AreaTileDrawer(this._areaTileDrawerFactorySpec.TileMesh.Asset, material, tileCount, gameObject);
		}

		// Token: 0x0400000F RID: 15
		public static readonly int ColorProperty = Shader.PropertyToID("_BaseColor");

		// Token: 0x04000010 RID: 16
		public readonly MapSize _mapSize;

		// Token: 0x04000011 RID: 17
		public readonly ISpecService _specService;

		// Token: 0x04000012 RID: 18
		public AreaTileDrawerFactorySpec _areaTileDrawerFactorySpec;
	}
}
