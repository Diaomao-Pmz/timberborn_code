using System;
using System.Runtime.InteropServices;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000007 RID: 7
	public class ColumnTerrainMap : ILoadableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000B RID: 11 RVA: 0x00002144 File Offset: 0x00000344
		// (remove) Token: 0x0600000C RID: 12 RVA: 0x0000217C File Offset: 0x0000037C
		public event EventHandler<ColumnAddedEventArgs> ColumnAdded;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000D RID: 13 RVA: 0x000021B4 File Offset: 0x000003B4
		// (remove) Token: 0x0600000E RID: 14 RVA: 0x000021EC File Offset: 0x000003EC
		public event EventHandler<ColumnRemovedEventArgs> ColumnRemoved;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002221 File Offset: 0x00000421
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002229 File Offset: 0x00000429
		public byte[] ColumnCount { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002232 File Offset: 0x00000432
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000223A File Offset: 0x0000043A
		public bool AnyColumnChanged { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002243 File Offset: 0x00000443
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000224B File Offset: 0x0000044B
		public int MaxColumnCount { get; private set; } = 1;

		// Token: 0x06000015 RID: 21 RVA: 0x00002254 File Offset: 0x00000454
		public ColumnTerrainMap(MapIndexService mapIndexService, TerrainMap terrainMap)
		{
			this._mapIndexService = mapIndexService;
			this._terrainMap = terrainMap;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002274 File Offset: 0x00000474
		public void Load()
		{
			this._verticalStride = this._mapIndexService.VerticalStride;
			this._terrainColumns = new TerrainColumn[this._verticalStride];
			this.ColumnCount = new byte[this._verticalStride];
			this.LoadColumns();
			this._terrainMap.TerrainAdded += this.OnTerrainAdded;
			this._terrainMap.TerrainRemoved += this.OnTerrainRemoved;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022E8 File Offset: 0x000004E8
		public ref TerrainColumn GetColumn(int index3D)
		{
			return ref this._terrainColumns[index3D];
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022F8 File Offset: 0x000004F8
		public void CopyTerrainColumnsData(ReadOnlyTerrainColumn[] terrainColumns, byte[] columnCount, int levels)
		{
			int num = levels * this._verticalStride;
			MemoryMarshal.Cast<TerrainColumn, ReadOnlyTerrainColumn>(MemoryExtensions.AsSpan<TerrainColumn>(this._terrainColumns, 0, num)).CopyTo(terrainColumns);
			this.ColumnCount.CopyTo(columnCount, 0);
			this.AnyColumnChanged = false;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002342 File Offset: 0x00000542
		public void OnTerrainAdded(object sender, Vector3Int coords)
		{
			this.OnTerrainChanged(coords, true);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000234C File Offset: 0x0000054C
		public void OnTerrainRemoved(object sender, Vector3Int coords)
		{
			this.OnTerrainChanged(coords, false);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002356 File Offset: 0x00000556
		public void OnTerrainChanged(Vector3Int coords, bool added)
		{
			if (added)
			{
				this.AddTerrain(coords);
				return;
			}
			this.RemoveTerrain(coords);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000236C File Offset: 0x0000056C
		public void AddTerrain(Vector3Int coordinates)
		{
			int num = this._mapIndexService.CellToIndex(coordinates.XY());
			int z = coordinates.z;
			int columnIndex = this.GetColumnIndex(num, z + 1);
			int num2 = (z == 0) ? 0 : this.GetColumnIndex(num, z - 1);
			if (columnIndex == -1 && num2 == -1)
			{
				this.InsertNewColumn(z, num);
			}
			else if (columnIndex != -1 && num2 != -1)
			{
				this.MergeColumns(num, columnIndex, num2);
			}
			else if (columnIndex != -1)
			{
				this._terrainColumns[columnIndex * this._verticalStride + num].Floor = z;
			}
			else if (num2 != -1)
			{
				this._terrainColumns[num2 * this._verticalStride + num].Ceiling = z + 1;
			}
			else
			{
				this.InsertNewColumn(z, num);
			}
			this.AnyColumnChanged = true;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002428 File Offset: 0x00000628
		public void RemoveTerrain(Vector3Int coordinates)
		{
			int index = this._mapIndexService.CellToIndex(coordinates.XY());
			int z = coordinates.z;
			ref TerrainColumn column = ref this.GetColumn(index, z);
			if (column.Floor == z)
			{
				if (column.Ceiling - 1 == z)
				{
					if (z != 0)
					{
						this.RemoveColumn(index, this.GetColumnIndex(index, z));
					}
					else
					{
						column.Ceiling = 0;
					}
				}
				else if (z == 0)
				{
					this.SplitColumn(ref column, index, 0, 1);
				}
				else
				{
					column.Floor++;
				}
			}
			else if (column.Ceiling - 1 == z)
			{
				column.Ceiling--;
			}
			else
			{
				this.SplitColumn(ref column, index, z, z + 1);
			}
			this.AnyColumnChanged = true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024D0 File Offset: 0x000006D0
		public int GetColumnIndex(int index, int height)
		{
			byte b = this.ColumnCount[index];
			for (int i = 0; i < (int)b; i++)
			{
				ref TerrainColumn ptr = ref this._terrainColumns[i * this._verticalStride + index];
				if (height >= ptr.Floor && height < ptr.Ceiling)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002520 File Offset: 0x00000720
		public ref TerrainColumn GetColumn(int index, int height)
		{
			byte b = this.ColumnCount[index];
			for (int i = 0; i < (int)b; i++)
			{
				ref TerrainColumn ptr = ref this._terrainColumns[i * this._verticalStride + index];
				if (height < ptr.Floor)
				{
					break;
				}
				if (height < ptr.Ceiling)
				{
					return ref ptr;
				}
			}
			throw new InvalidOperationException(string.Format("Column for index {0} and height {1} not found", index, height));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002588 File Offset: 0x00000788
		public void InsertNewColumn(int height, int index)
		{
			TerrainColumn terrainColumn = new TerrainColumn(height, height + 1);
			this.InsertColumn(index, height, ref terrainColumn);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025AC File Offset: 0x000007AC
		public void MergeColumns(int index, int columnAboveIndex, int columnBelowIndex)
		{
			ref TerrainColumn ptr = ref this._terrainColumns[columnAboveIndex * this._verticalStride + index];
			this._terrainColumns[columnBelowIndex * this._verticalStride + index].Ceiling = ptr.Ceiling;
			this.RemoveColumn(index, columnAboveIndex);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025F8 File Offset: 0x000007F8
		public void RemoveColumn(int index, int columnIndex)
		{
			byte b = this.ColumnCount[index];
			for (int i = columnIndex + 1; i < (int)b; i++)
			{
				int num = i * this._verticalStride + index;
				int num2 = (i - 1) * this._verticalStride + index;
				this._terrainColumns[num2] = this._terrainColumns[num];
			}
			byte[] columnCount = this.ColumnCount;
			columnCount[index] -= 1;
			int num3 = (int)(b - 1) * this._verticalStride + index;
			this._terrainColumns[num3] = default(TerrainColumn);
			EventHandler<ColumnRemovedEventArgs> columnRemoved = this.ColumnRemoved;
			if (columnRemoved == null)
			{
				return;
			}
			columnRemoved(this, new ColumnRemovedEventArgs(columnIndex * this._verticalStride + index, (int)this.ColumnCount[index]));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026A8 File Offset: 0x000008A8
		public void SplitColumn(ref TerrainColumn column, int index, int splitHeight, int newFloorHeight)
		{
			TerrainColumn terrainColumn = new TerrainColumn(newFloorHeight, column.Ceiling);
			column.Ceiling = splitHeight;
			this.InsertColumn(index, splitHeight, ref terrainColumn);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026D8 File Offset: 0x000008D8
		public void InsertColumn(int index, int splitHeight, ref TerrainColumn newColumn)
		{
			byte b = this.ColumnCount[index];
			for (int i = (int)(this.ColumnCount[index] - 1); i >= 0; i--)
			{
				int num = i * this._verticalStride + index;
				ref TerrainColumn ptr = ref this._terrainColumns[num];
				if (ptr.Floor <= splitHeight)
				{
					break;
				}
				int num2 = i + 1;
				if (num2 == this.MaxColumnCount)
				{
					this.IncreaseMaxColumnCount();
				}
				int num3 = num2 * this._verticalStride + index;
				this._terrainColumns[num3] = ptr;
				b = (byte)i;
			}
			int num4 = (int)b * this._verticalStride + index;
			byte[] columnCount = this.ColumnCount;
			byte b2 = columnCount[index];
			columnCount[index] = b2 + 1;
			if ((int)b2 == this.MaxColumnCount)
			{
				this.IncreaseMaxColumnCount();
			}
			this._terrainColumns[num4] = newColumn;
			EventHandler<ColumnAddedEventArgs> columnAdded = this.ColumnAdded;
			if (columnAdded == null)
			{
				return;
			}
			columnAdded(this, new ColumnAddedEventArgs(num4, (int)this.ColumnCount[index]));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027BC File Offset: 0x000009BC
		public void IncreaseMaxColumnCount()
		{
			int maxColumnCount = this.MaxColumnCount;
			this.MaxColumnCount = maxColumnCount + 1;
			this.Resize(this.MaxColumnCount);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027E8 File Offset: 0x000009E8
		public void Resize(int currentMaxIndex)
		{
			int newSize = currentMaxIndex * this._mapIndexService.VerticalStride;
			Array.Resize<TerrainColumn>(ref this._terrainColumns, newSize);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002810 File Offset: 0x00000A10
		public void LoadColumns()
		{
			Vector3Int terrainSize = this._mapIndexService.TerrainSize;
			foreach (int num in this._mapIndexService.Indices2D)
			{
				int num2 = -1;
				bool flag = true;
				int floor = 0;
				for (int i = 0; i < terrainSize.z; i++)
				{
					int index = num + i * this._verticalStride;
					if (this._terrainMap.UnsafeIsTerrainVoxel(index))
					{
						if (!flag)
						{
							floor = i;
						}
						flag = true;
					}
					else
					{
						if (flag)
						{
							this.CreateColumn(++num2, num, new TerrainColumn(floor, i));
						}
						flag = false;
					}
				}
				if (flag)
				{
					this.CreateColumn(++num2, num, new TerrainColumn(floor, terrainSize.z));
				}
				this.ColumnCount[num] = (byte)(num2 + 1);
			}
			this.AnyColumnChanged = true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028F0 File Offset: 0x00000AF0
		public void CreateColumn(int columnIndex, int index2D, TerrainColumn column)
		{
			if (columnIndex + 1 > this.MaxColumnCount)
			{
				this.IncreaseMaxColumnCount();
			}
			this._terrainColumns[index2D + columnIndex * this._verticalStride] = column;
		}

		// Token: 0x0400000F RID: 15
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000010 RID: 16
		public readonly TerrainMap _terrainMap;

		// Token: 0x04000011 RID: 17
		public TerrainColumn[] _terrainColumns;

		// Token: 0x04000012 RID: 18
		public int _verticalStride;
	}
}
