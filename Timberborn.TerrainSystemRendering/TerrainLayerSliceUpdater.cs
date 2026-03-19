using System;
using Timberborn.LevelVisibilitySystem;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TextureOperations;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000010 RID: 16
	public class TerrainLayerSliceUpdater : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002CE7 File Offset: 0x00000EE7
		public TerrainLayerSliceUpdater(ITerrainService terrainService, ILevelVisibilityService levelVisibilityService, TextureFactory textureFactory, MapSize mapSize)
		{
			this._terrainService = terrainService;
			this._levelVisibilityService = levelVisibilityService;
			this._textureFactory = textureFactory;
			this._mapSize = mapSize;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D0C File Offset: 0x00000F0C
		public void Load()
		{
			TextureSettings.Builder builder = new TextureSettings.Builder();
			builder.SetSize(this._mapSize.TerrainSize.x, this._mapSize.TerrainSize.y).SetTextureFormat(63).SetGenerateMipmap(false);
			this._terrainSliceMap = this._textureFactory.CreateTexture(builder.Build());
			Shader.SetGlobalTexture(TerrainLayerSliceUpdater.TerrainSliceMap, this._terrainSliceMap);
			this._terrainService.TerrainHeightChanged += delegate(object _, TerrainHeightChangeEventArgs args)
			{
				TerrainHeightChange change = args.Change;
				this.OnTerrainHeightChanged(change);
			};
			this._levelVisibilityService.MaxVisibleLevelChanged += delegate(object _, int maxVisibleLevel)
			{
				this.UpdateTerrainSliceTexture(maxVisibleLevel);
			};
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002DAE File Offset: 0x00000FAE
		public void Unload()
		{
			Object.Destroy(this._terrainSliceMap);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002DBC File Offset: 0x00000FBC
		public void OnTerrainHeightChanged(in TerrainHeightChange change)
		{
			int maxVisibleLevel = this._levelVisibilityService.MaxVisibleLevel;
			if (maxVisibleLevel >= change.From && maxVisibleLevel <= change.To)
			{
				this.UpdateTerrainSliceTexture(maxVisibleLevel);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public void UpdateTerrainSliceTexture(int maxVisibleLevel)
		{
			for (int i = 0; i < this._mapSize.TerrainSize.y; i++)
			{
				for (int j = 0; j < this._mapSize.TerrainSize.x; j++)
				{
					bool flag = this._terrainService.Underground(new Vector3Int(j, i, maxVisibleLevel));
					this._terrainSliceMap.SetPixel(j, i, flag ? Color.white : Color.black);
				}
			}
			this._terrainSliceMap.Apply();
		}

		// Token: 0x0400002A RID: 42
		public static readonly int TerrainSliceMap = Shader.PropertyToID("_TerrainSliceMap");

		// Token: 0x0400002B RID: 43
		public readonly ITerrainService _terrainService;

		// Token: 0x0400002C RID: 44
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400002D RID: 45
		public readonly TextureFactory _textureFactory;

		// Token: 0x0400002E RID: 46
		public readonly MapSize _mapSize;

		// Token: 0x0400002F RID: 47
		public Texture2D _terrainSliceMap;
	}
}
