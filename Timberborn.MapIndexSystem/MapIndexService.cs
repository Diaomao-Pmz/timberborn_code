using System;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.MapIndexSystem
{
	// Token: 0x02000008 RID: 8
	public class MapIndexService : ILoadableSingleton
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021EF File Offset: 0x000003EF
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021F7 File Offset: 0x000003F7
		public int Stride { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002200 File Offset: 0x00000400
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002208 File Offset: 0x00000408
		public int VerticalStride { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002211 File Offset: 0x00000411
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002219 File Offset: 0x00000419
		public int MaxIndex { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002222 File Offset: 0x00000422
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000222A File Offset: 0x0000042A
		public int MaxSize3D { get; private set; }

		// Token: 0x06000018 RID: 24 RVA: 0x00002233 File Offset: 0x00000433
		public MapIndexService(MapSize mapSize)
		{
			this._mapSize = mapSize;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002242 File Offset: 0x00000442
		public Vector3Int TerrainSize
		{
			get
			{
				return this._mapSize.TerrainSize;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000224F File Offset: 0x0000044F
		public Vector3Int TotalSize
		{
			get
			{
				return this._mapSize.TotalSize;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000225C File Offset: 0x0000045C
		public Index2DEnumerator Indices2D
		{
			get
			{
				return new Index2DEnumerator(this.TerrainSize.x, this.TerrainSize.y, MapIndexService.Margin, this._startingIndex);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002298 File Offset: 0x00000498
		public void Load()
		{
			int num = MapIndexService.Margin * 2;
			int num2 = this.TerrainSize.x + num;
			int num3 = this.TerrainSize.y + num;
			this.Stride = num2;
			this.VerticalStride = num2 * num3;
			this._startingIndex = this.Stride * MapIndexService.Margin + MapIndexService.Margin;
			this.MaxIndex = num2 * num3;
			this.MaxSize3D = this.VerticalStride * this.TerrainSize.z;
			this.InitializeActualMap();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002321 File Offset: 0x00000521
		public int CellToIndex(Vector2Int coordinates)
		{
			return (coordinates.y + 1) * this.Stride + coordinates.x + 1;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000233D File Offset: 0x0000053D
		public int CoordinatesToIndex3D(Vector3Int coordinates)
		{
			return (coordinates.y + 1) * this.Stride + coordinates.x + 1 + coordinates.z * this.VerticalStride;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002368 File Offset: 0x00000568
		public Vector3Int Index3DToCoordinates(int index3D)
		{
			int num = index3D / this.VerticalStride;
			int num2 = index3D - num * this.VerticalStride;
			int num3 = num2 % this.Stride;
			int num4 = num2 / this.Stride;
			return new Vector3Int(num3 - 1, num4 - 1, num);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023A8 File Offset: 0x000005A8
		public Vector3Int IndexToCoordinates(int index, int height)
		{
			int num = index % this.Stride;
			int num2 = index / this.Stride;
			return new Vector3Int(num - 1, num2 - 1, height);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023D4 File Offset: 0x000005D4
		public int Index2DTo3D(int index, int z)
		{
			int num = index % this.Stride;
			return index / this.Stride * this.Stride + num + z * this.VerticalStride;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002404 File Offset: 0x00000604
		public int CoordinatesToActualMapIndex(Vector2Int coordinates)
		{
			return coordinates.x + coordinates.y * this.TerrainSize.x;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000242F File Offset: 0x0000062F
		public bool IndexIsInActualMap(int index)
		{
			return this._actualMap[index];
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002439 File Offset: 0x00000639
		public PackedList<T> Pack<T>(T[] inputArray, int levels = 1)
		{
			return this.Pack<T>(MemoryExtensions.AsSpan<T>(inputArray), levels);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002450 File Offset: 0x00000650
		public unsafe PackedList<T> Pack<T>(ReadOnlySpan<T> inputArray, int levels = 1)
		{
			Vector3Int terrainSize = this._mapSize.TerrainSize;
			int num = this._startingIndex;
			int num2 = 0;
			int num3 = terrainSize.x * terrainSize.y;
			T[] array = new T[num3 * levels];
			for (int i = 0; i < terrainSize.y; i++)
			{
				for (int j = 0; j < terrainSize.x; j++)
				{
					for (int k = 0; k < levels; k++)
					{
						int num4 = num2 + k * num3;
						int num5 = num + k * this.VerticalStride;
						array[num4] = *inputArray[num5];
					}
					num++;
					num2++;
				}
				num += 2;
			}
			return new PackedList<T>(array);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002508 File Offset: 0x00000708
		public PackedList<T> Pack3D<T>(T[] inputArray)
		{
			return this.Pack<T>(inputArray, this._mapSize.TerrainSize.z);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002530 File Offset: 0x00000730
		public T[] Unpack2DHeightData<T>(PackedList<T> packedList, int levels = 1)
		{
			T[] array = packedList.Array;
			Vector3Int terrainSize = this._mapSize.TerrainSize;
			int num = 0;
			int num2 = terrainSize.x * terrainSize.y;
			int num3 = 0;
			T[] array2 = new T[array.Length];
			for (int i = 0; i < terrainSize.y; i++)
			{
				for (int j = 0; j < terrainSize.x; j++)
				{
					for (int k = 0; k < levels; k++)
					{
						int num4 = num3 + k * this.VerticalStride;
						int num5 = num + k * num2;
						array2[num4] = array[num5];
					}
					num++;
					num3++;
				}
			}
			return array2;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025E0 File Offset: 0x000007E0
		public T[] Unpack3D<T>(T[] inputArray)
		{
			Vector3Int terrainSize = this._mapSize.TerrainSize;
			int z = this._mapSize.TerrainSize.z;
			int num = 0;
			int num2 = terrainSize.x * terrainSize.y;
			int num3 = this._startingIndex;
			T[] array = new T[this.MaxSize3D];
			for (int i = 0; i < terrainSize.y; i++)
			{
				for (int j = 0; j < terrainSize.x; j++)
				{
					for (int k = 0; k < z; k++)
					{
						int num4 = num3 + k * this.VerticalStride;
						int num5 = num + k * num2;
						array[num4] = inputArray[num5];
					}
					num++;
					num3++;
				}
				num3 += 2;
			}
			return array;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026AC File Offset: 0x000008AC
		public T[] Unpack<T>(PackedList<T> packedList, int levels = 1)
		{
			T[] array = packedList.Array;
			Vector3Int terrainSize = this._mapSize.TerrainSize;
			int num = 0;
			int num2 = terrainSize.x * terrainSize.y;
			int num3 = this._startingIndex;
			T[] array2 = new T[this.MaxIndex * levels];
			for (int i = 0; i < terrainSize.y; i++)
			{
				for (int j = 0; j < terrainSize.x; j++)
				{
					for (int k = 0; k < levels; k++)
					{
						int num4 = num3 + k * this.VerticalStride;
						int num5 = num + k * num2;
						array2[num4] = array[num5];
					}
					num++;
					num3++;
				}
				num3 += 2;
			}
			return array2;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000276C File Offset: 0x0000096C
		public unsafe void Unpack<T>(PackedList<T> packedList, Span<T> unpacked, int levels = 1)
		{
			T[] array = packedList.Array;
			Vector3Int terrainSize = this._mapSize.TerrainSize;
			int num = 0;
			int num2 = terrainSize.x * terrainSize.y;
			int num3 = this._startingIndex;
			for (int i = 0; i < terrainSize.y; i++)
			{
				for (int j = 0; j < terrainSize.x; j++)
				{
					for (int k = 0; k < levels; k++)
					{
						int num4 = num3 + k * this.VerticalStride;
						int num5 = num + k * num2;
						if (num4 < unpacked.Length)
						{
							*unpacked[num4] = array[num5];
						}
					}
					num++;
					num3++;
				}
				num3 += 2;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000282C File Offset: 0x00000A2C
		public void InitializeActualMap()
		{
			this._actualMap = new bool[this.MaxIndex];
			int num = this._startingIndex;
			for (int i = 0; i < this.TerrainSize.y; i++)
			{
				for (int j = 0; j < this.TerrainSize.x; j++)
				{
					this._actualMap[num] = true;
					num++;
				}
				num += 2;
			}
		}

		// Token: 0x0400000B RID: 11
		public static readonly int Margin = 1;

		// Token: 0x04000010 RID: 16
		public readonly MapSize _mapSize;

		// Token: 0x04000011 RID: 17
		public bool[] _actualMap;

		// Token: 0x04000012 RID: 18
		public int _startingIndex;
	}
}
