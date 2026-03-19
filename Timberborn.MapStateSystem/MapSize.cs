using System;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.MapStateSystem
{
	// Token: 0x02000008 RID: 8
	public class MapSize : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002125 File Offset: 0x00000325
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000212D File Offset: 0x0000032D
		public Vector3Int TerrainSize { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002136 File Offset: 0x00000336
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000213E File Offset: 0x0000033E
		public Vector3Int TotalSize { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002147 File Offset: 0x00000347
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000214F File Offset: 0x0000034F
		public Vector2Int TerrainSize2D { get; private set; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002158 File Offset: 0x00000358
		public MapSize(ISingletonLoader singletonLoader, ISpecService specService)
		{
			this._singletonLoader = singletonLoader;
			this._specService = specService;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000216E File Offset: 0x0000036E
		public static MapSize NewMap(Vector2Int size)
		{
			return new MapSize(null, null)
			{
				TerrainSize2D = size
			};
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000217E File Offset: 0x0000037E
		public int MaxGameTerrainHeight
		{
			get
			{
				return this._mapSizeSpec.MaxGameTerrainHeight;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000218B File Offset: 0x0000038B
		public int MaxMapEditorTerrainHeight
		{
			get
			{
				return this._mapSizeSpec.MaxMapEditorTerrainHeight;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002198 File Offset: 0x00000398
		public int MaxHeightAboveTerrain
		{
			get
			{
				return this._mapSizeSpec.MaxHeightAboveTerrain;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021A8 File Offset: 0x000003A8
		public void Load()
		{
			IObjectLoader singleton = this._singletonLoader.GetSingleton(MapSize.MapSizeKey);
			this._mapSizeSpec = this._specService.GetSingleSpec<MapSizeSpec>();
			this.Initialize(singleton.Get(MapSize.SizeKey));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021E8 File Offset: 0x000003E8
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(MapSize.MapSizeKey).Set(MapSize.SizeKey, this.TerrainSize2D);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002205 File Offset: 0x00000405
		public bool ContainsInTerrain(Vector2Int coordinates)
		{
			return Sizing.SizeContains(this.TerrainSize, coordinates);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002213 File Offset: 0x00000413
		public bool ContainsInTotal(Vector3Int coordinates)
		{
			return Sizing.SizeContains(this.TotalSize, coordinates);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002224 File Offset: 0x00000424
		public void Initialize(Vector2Int size)
		{
			this.TerrainSize2D = size;
			this.TerrainSize = size.ToVector3Int(this._mapSizeSpec.MaxGameTerrainHeight + 1);
			this.TotalSize = this.TerrainSize + new Vector3Int(0, 0, this._mapSizeSpec.MaxHeightAboveTerrain);
		}

		// Token: 0x04000009 RID: 9
		public static readonly SingletonKey MapSizeKey = new SingletonKey("MapSize");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<Vector2Int> SizeKey = new PropertyKey<Vector2Int>("Size");

		// Token: 0x0400000E RID: 14
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400000F RID: 15
		public readonly ISpecService _specService;

		// Token: 0x04000010 RID: 16
		public MapSizeSpec _mapSizeSpec;
	}
}
