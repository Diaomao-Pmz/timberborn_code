using System;
using System.Linq;
using Timberborn.Common;
using Timberborn.ErrorReporting;
using Timberborn.MapIndexSystem;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000012 RID: 18
	public class TerrainMap : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600006C RID: 108 RVA: 0x00002A50 File Offset: 0x00000C50
		// (remove) Token: 0x0600006D RID: 109 RVA: 0x00002A88 File Offset: 0x00000C88
		public event EventHandler<Vector3Int> TerrainAdded;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600006E RID: 110 RVA: 0x00002AC0 File Offset: 0x00000CC0
		// (remove) Token: 0x0600006F RID: 111 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public event EventHandler<Vector3Int> TerrainRemoved;

		// Token: 0x06000070 RID: 112 RVA: 0x00002B2D File Offset: 0x00000D2D
		public TerrainMap(ISingletonLoader singletonLoader, MapIndexService mapSerializer, IntPackedListSerializer intPackedListSerializer, BoolPackedListSerializer boolPackedListSerializer, MapSize mapSize, ILoadingIssueService loadingIssueService)
		{
			this._singletonLoader = singletonLoader;
			this._mapIndexService = mapSerializer;
			this._intPackedListSerializer = intPackedListSerializer;
			this._boolPackedListSerializer = boolPackedListSerializer;
			this._mapSize = mapSize;
			this._loadingIssueService = loadingIssueService;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002B64 File Offset: 0x00000D64
		public bool TryGetRelativeHeight(Vector3Int coordinates, out int relativeHeight)
		{
			Vector2Int coordinates2 = coordinates.XY();
			if (!this.Contains(coordinates2))
			{
				relativeHeight = TerrainMap.DefaultHeight;
				return false;
			}
			Vector3Int coordinates3 = this.ClampToTerrain(coordinates);
			int num = this._mapIndexService.CoordinatesToIndex3D(coordinates3);
			if (this._terrainVoxels[num])
			{
				relativeHeight = this.GetDistanceToTerrainTop(num, coordinates3);
				return true;
			}
			int num2 = Math.Max(0, coordinates.z - this._mapSize.TerrainSize.z);
			relativeHeight = -this.GetDistanceToTerrainBelow(num, coordinates3) - num2;
			return true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public int GetTerrainHeightBelow(Vector3Int coordinates)
		{
			Vector3Int coordinates2 = this.ClampToTerrain(coordinates);
			int num = this._mapIndexService.CoordinatesToIndex3D(coordinates2);
			int z = coordinates2.z;
			for (int i = coordinates2.z; i >= 0; i--)
			{
				int num2 = z - i;
				int num3 = num - num2 * this._mapIndexService.VerticalStride;
				if (this._terrainVoxels[num3])
				{
					return i + 1;
				}
			}
			return 0;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002C4C File Offset: 0x00000E4C
		public void SetTerrain(Vector3Int coordinates)
		{
			int num = this._mapIndexService.CoordinatesToIndex3D(coordinates);
			this._terrainVoxels[num] = true;
			EventHandler<Vector3Int> terrainAdded = this.TerrainAdded;
			if (terrainAdded == null)
			{
				return;
			}
			terrainAdded(this, coordinates);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002C84 File Offset: 0x00000E84
		public void UnsetTerrain(Vector3Int coordinates)
		{
			int num = this._mapIndexService.CoordinatesToIndex3D(coordinates);
			this._terrainVoxels[num] = false;
			EventHandler<Vector3Int> terrainRemoved = this.TerrainRemoved;
			if (terrainRemoved == null)
			{
				return;
			}
			terrainRemoved(this, coordinates);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002CB9 File Offset: 0x00000EB9
		public bool UnsafeIsTerrainVoxel(int index)
		{
			return this._terrainVoxels[index];
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public bool IsTerrainVoxel(Vector3Int coordinates)
		{
			return this.Contains(coordinates.XY()) && (coordinates.z < 0 || (coordinates.z < this._mapSize.TerrainSize.z && this._terrainVoxels[this._mapIndexService.CoordinatesToIndex3D(coordinates)]));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002D1E File Offset: 0x00000F1E
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(TerrainMap.TerrainMapKey).Set<PackedList<bool>>(TerrainMap.VoxelsKey, this._mapIndexService.Pack3D<bool>(this._terrainVoxels), this._boolPackedListSerializer);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002D4C File Offset: 0x00000F4C
		[BackwardCompatible(2024, 8, 29, Compatibility.Map)]
		public void Load()
		{
			IObjectLoader objectLoader;
			if (!this._singletonLoader.TryGetSingleton(TerrainMap.TerrainMapKey, out objectLoader))
			{
				this.InitializeHeights();
				return;
			}
			PropertyKey<PackedList<int>> key = new PropertyKey<PackedList<int>>("Heights");
			if (objectLoader.Has<PackedList<int>>(key))
			{
				this.BackwardCompatibleLoad(objectLoader.Get<PackedList<int>>(key, this._intPackedListSerializer));
				return;
			}
			PackedList<bool> packedList = objectLoader.Get<PackedList<bool>>(TerrainMap.VoxelsKey, this._boolPackedListSerializer);
			this._terrainVoxels = this._mapIndexService.Unpack3D<bool>(this.GetTerrainData(packedList.Array));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002DCD File Offset: 0x00000FCD
		public bool Contains(Vector2Int coordinates)
		{
			return Sizing.SizeContains(this._mapSize.TerrainSize2D, coordinates);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public int GetDistanceToTerrainTop(int startIndex, Vector3Int coordinates)
		{
			int num = 0;
			for (int i = 0; i < this._mapSize.TerrainSize.z - coordinates.z; i++)
			{
				if (!this._terrainVoxels[startIndex + this._mapIndexService.VerticalStride * i])
				{
					return num;
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002E38 File Offset: 0x00001038
		public int GetDistanceToTerrainBelow(int startIndex, Vector3Int coordinates)
		{
			int num = 0;
			for (int i = 1; i <= coordinates.z; i++)
			{
				if (this._terrainVoxels[startIndex - this._mapIndexService.VerticalStride * i])
				{
					return num;
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002E7C File Offset: 0x0000107C
		public Vector3Int ClampToTerrain(Vector3Int position)
		{
			return new Vector3Int(position.x, position.y, Math.Clamp(position.z, 0, this._mapSize.TerrainSize.z - 1));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002EC0 File Offset: 0x000010C0
		public void BackwardCompatibleLoad(PackedList<int> packedHeightsList)
		{
			int[] array = this._mapIndexService.Unpack2DHeightData<int>(packedHeightsList, 1);
			if (array.Any((int height) => height > this._mapIndexService.TerrainSize.z))
			{
				throw new InvalidOperationException("Loaded map has heights exceeding map size");
			}
			int x = this._mapIndexService.TerrainSize.x;
			this._terrainVoxels = new bool[this._mapIndexService.MaxSize3D];
			for (int i = 0; i < array.Length; i++)
			{
				int num = array[i];
				int z = this._mapIndexService.TerrainSize.z;
				for (int j = 0; j < z; j++)
				{
					if (j < num)
					{
						this.SetTerrain(new Vector3Int(i % x, i / x, j));
					}
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002F78 File Offset: 0x00001178
		public bool[] GetTerrainData(bool[] currentTerrainData)
		{
			int num = this._mapSize.TerrainSize.x * this._mapSize.TerrainSize.y;
			if (currentTerrainData.Length / num <= this._mapSize.TerrainSize.z)
			{
				return currentTerrainData;
			}
			this._loadingIssueService.AddIssue("Terrain data height exceeds map size, truncating to fit.", TerrainMap.TerrainCutOffIssueLocKey, null, false);
			bool[] array = new bool[this._mapSize.TerrainSize.z * num];
			for (int i = 0; i < num * this._mapSize.MaxGameTerrainHeight; i++)
			{
				array[i] = currentTerrainData[i];
			}
			return array;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000301C File Offset: 0x0000121C
		public void InitializeHeights()
		{
			this._terrainVoxels = new bool[this._mapIndexService.MaxSize3D];
			foreach (int num in this._mapIndexService.Indices2D)
			{
				for (int i = 0; i < TerrainMap.NewMapHeight; i++)
				{
					this._terrainVoxels[num + i * this._mapIndexService.VerticalStride] = true;
				}
			}
		}

		// Token: 0x0400001D RID: 29
		public static readonly string TerrainCutOffIssueLocKey = "LoadingIssue.TerrainCutOffIssue";

		// Token: 0x0400001E RID: 30
		public static readonly SingletonKey TerrainMapKey = new SingletonKey("TerrainMap");

		// Token: 0x0400001F RID: 31
		public static readonly PropertyKey<PackedList<bool>> VoxelsKey = new PropertyKey<PackedList<bool>>("Voxels");

		// Token: 0x04000020 RID: 32
		public static readonly int NewMapHeight = 4;

		// Token: 0x04000021 RID: 33
		public static readonly int DefaultHeight = -1;

		// Token: 0x04000024 RID: 36
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000025 RID: 37
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000026 RID: 38
		public readonly IntPackedListSerializer _intPackedListSerializer;

		// Token: 0x04000027 RID: 39
		public readonly BoolPackedListSerializer _boolPackedListSerializer;

		// Token: 0x04000028 RID: 40
		public readonly MapSize _mapSize;

		// Token: 0x04000029 RID: 41
		public readonly ILoadingIssueService _loadingIssueService;

		// Token: 0x0400002A RID: 42
		public bool[] _terrainVoxels;
	}
}
