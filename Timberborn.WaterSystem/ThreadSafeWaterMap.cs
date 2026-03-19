using System;
using System.Runtime.InteropServices;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000021 RID: 33
	[MapEditorTickable]
	public class ThreadSafeWaterMap : ILoadableSingleton, IPostLoadableSingleton, IThreadSafeWaterMap, ITickableSingleton
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000091 RID: 145 RVA: 0x00003858 File Offset: 0x00001A58
		// (remove) Token: 0x06000092 RID: 146 RVA: 0x00003890 File Offset: 0x00001A90
		public event EventHandler<int> MaxWaterColumnCountChanged;

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000038C5 File Offset: 0x00001AC5
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000038CD File Offset: 0x00001ACD
		public int MaxColumnCount { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000038D6 File Offset: 0x00001AD6
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000038DE File Offset: 0x00001ADE
		public bool AnyColumnChanged { get; private set; }

		// Token: 0x06000097 RID: 151 RVA: 0x000038E7 File Offset: 0x00001AE7
		public ThreadSafeWaterMap(MapIndexService mapIndexService, ITerrainService terrainService, WaterColumnRetriever waterColumnRetriever, FlowVectorCalculator flowVectorCalculator, WaterSimulator waterSimulator)
		{
			this._mapIndexService = mapIndexService;
			this._terrainService = terrainService;
			this._waterColumnRetriever = waterColumnRetriever;
			this._flowVectorCalculator = flowVectorCalculator;
			this._waterSimulator = waterSimulator;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003914 File Offset: 0x00001B14
		public ReadOnlyArray<byte> ColumnCounts
		{
			get
			{
				return new ReadOnlyArray<byte>(this._threadSafeColumnCounts);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003921 File Offset: 0x00001B21
		public ReadOnlyArray<ReadOnlyWaterColumn> WaterColumns
		{
			get
			{
				return new ReadOnlyArray<ReadOnlyWaterColumn>(this._threadSafeWaterColumns);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000392E File Offset: 0x00001B2E
		public ReadOnlyArray<Vector2> FlowDirections
		{
			get
			{
				return new ReadOnlyArray<Vector2>(this._waterFlowDirections);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000393C File Offset: 0x00001B3C
		public void Load()
		{
			this._verticalStride = this._mapIndexService.VerticalStride;
			this._threadSafeColumnCounts = new byte[this._mapIndexService.MaxIndex];
			this._threadSafeWaterColumns = new ReadOnlyWaterColumn[this._verticalStride];
			this._waterFlowDirections = new Vector2[this._verticalStride];
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003992 File Offset: 0x00001B92
		public void PostLoad()
		{
			this.Update();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003992 File Offset: 0x00001B92
		public void Tick()
		{
			this.Update();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000399A File Offset: 0x00001B9A
		public int ColumnCount(int index2D)
		{
			return (int)this._threadSafeColumnCounts[index2D];
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000039A4 File Offset: 0x00001BA4
		public byte ColumnFloor(int index3D)
		{
			return this._threadSafeWaterColumns[index3D].Floor;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000039B7 File Offset: 0x00001BB7
		public byte ColumnCeiling(int index3D)
		{
			return this._threadSafeWaterColumns[index3D].Ceiling;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000039CA File Offset: 0x00001BCA
		public float WaterDepth(int index3D)
		{
			return this._threadSafeWaterColumns[index3D].WaterDepth;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000039E0 File Offset: 0x00001BE0
		public bool IsWaterOnAnyHeight(Vector2Int coordinates)
		{
			if (this._terrainService.Contains(coordinates))
			{
				int num = this._mapIndexService.CellToIndex(coordinates);
				for (int i = 0; i < (int)this._threadSafeColumnCounts[num]; i++)
				{
					int num2 = i * this._verticalStride + num;
					if (this._threadSafeWaterColumns[num2].WaterDepth > 0f)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003A44 File Offset: 0x00001C44
		public bool TryGetColumnFloor(Vector3Int coordinates, out int floor)
		{
			if (this._terrainService.Contains(coordinates.XY()))
			{
				int num = this._mapIndexService.CellToIndex(coordinates.XY());
				for (int i = 0; i < (int)this._threadSafeColumnCounts[num]; i++)
				{
					int num2 = i * this._verticalStride + num;
					ref ReadOnlyWaterColumn ptr = ref this._threadSafeWaterColumns[num2];
					floor = (int)ptr.Floor;
					if (floor > coordinates.z)
					{
						break;
					}
					if ((int)ptr.Ceiling > coordinates.z)
					{
						return true;
					}
				}
			}
			floor = 0;
			return false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003ACC File Offset: 0x00001CCC
		public Vector2 WaterFlowDirection(Vector3Int coordinates)
		{
			int num = this._mapIndexService.CellToIndex(coordinates.XY());
			int z = coordinates.z;
			for (int i = 0; i < (int)this._threadSafeColumnCounts[num]; i++)
			{
				int num2 = i * this._verticalStride + num;
				ref ReadOnlyWaterColumn ptr = ref this._threadSafeWaterColumns[num2];
				if (z < (int)ptr.Floor)
				{
					break;
				}
				if (z < (int)ptr.Ceiling)
				{
					return this._waterFlowDirections[num2];
				}
			}
			return Vector2.zero;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003B45 File Offset: 0x00001D45
		public float WaterDepth(Vector3Int coordinates)
		{
			return this.GetColumn(coordinates).WaterDepth;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B54 File Offset: 0x00001D54
		public float ColumnContamination(Vector3Int coordinates)
		{
			ref readonly ReadOnlyWaterColumn column = ref this.GetColumn(coordinates);
			if ((float)column.Floor + column.WaterDepth <= (float)coordinates.z)
			{
				return 0f;
			}
			return column.Contamination;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B90 File Offset: 0x00001D90
		public int CeiledWaterHeight(Vector3Int coordinates)
		{
			if (!this._terrainService.Contains(coordinates.XY()))
			{
				return 0;
			}
			ref readonly ReadOnlyWaterColumn column = ref this.GetColumn(coordinates);
			float waterDepth = column.WaterDepth;
			if (waterDepth <= 0f)
			{
				return 0;
			}
			return Mathf.CeilToInt((float)column.Floor + waterDepth);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BDC File Offset: 0x00001DDC
		public float WaterHeightOrFloor(Vector3Int coordinates)
		{
			ref readonly ReadOnlyWaterColumn column = ref this.GetColumn(coordinates);
			return column.WaterDepth + (float)column.Floor;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003C00 File Offset: 0x00001E00
		public bool CellIsUnderwater(Vector3Int coordinates)
		{
			int num = this.CeiledWaterHeight(coordinates);
			return num > coordinates.z && num > 0;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003C28 File Offset: 0x00001E28
		public void Update()
		{
			int maxColumnCount = this.MaxColumnCount;
			this.MaxColumnCount = this._waterSimulator.MaxColumnCount;
			this.AnyColumnChanged = this._waterSimulator.AnyColumnChanged;
			if (this.MaxColumnCount > maxColumnCount)
			{
				int newSize = this.MaxColumnCount * this._mapIndexService.VerticalStride;
				Array.Resize<ReadOnlyWaterColumn>(ref this._threadSafeWaterColumns, newSize);
				Array.Resize<Vector2>(ref this._waterFlowDirections, newSize);
			}
			MemoryMarshal.Cast<WaterColumn, ReadOnlyWaterColumn>(this._waterSimulator.WaterColumns).CopyTo(this._threadSafeWaterColumns);
			if (this.AnyColumnChanged)
			{
				this._waterSimulator.ColumnCounts.CopyTo(this._threadSafeColumnCounts);
			}
			if (this.MaxColumnCount > maxColumnCount)
			{
				EventHandler<int> maxWaterColumnCountChanged = this.MaxWaterColumnCountChanged;
				if (maxWaterColumnCountChanged != null)
				{
					maxWaterColumnCountChanged(this, this.MaxColumnCount);
				}
			}
			this.UpdateWaterFlowDirections(this._waterSimulator.ColumnCounts, this._waterSimulator.Outflows);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003D18 File Offset: 0x00001F18
		public unsafe void UpdateWaterFlowDirections(ReadOnlySpan<byte> columnCounts, ReadOnlySpan<ColumnOutflows> outflows)
		{
			foreach (int num in this._mapIndexService.Indices2D)
			{
				byte b = *columnCounts[num];
				for (int i = 0; i < (int)b; i++)
				{
					int num2 = i * this._verticalStride + num;
					ref ColumnOutflows outflows2 = outflows[num2];
					this._waterFlowDirections[num2] = this._flowVectorCalculator.GetFlowVectorAtTop(outflows2);
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003D98 File Offset: 0x00001F98
		public ref readonly ReadOnlyWaterColumn GetColumn(Vector3Int coordinates)
		{
			int index = this._mapIndexService.CellToIndex(coordinates.XY());
			return this._waterColumnRetriever.GetColumn(this.ColumnCounts, this.WaterColumns, this._verticalStride, index, coordinates.z);
		}

		// Token: 0x0400006D RID: 109
		public readonly MapIndexService _mapIndexService;

		// Token: 0x0400006E RID: 110
		public readonly ITerrainService _terrainService;

		// Token: 0x0400006F RID: 111
		public readonly WaterColumnRetriever _waterColumnRetriever;

		// Token: 0x04000070 RID: 112
		public readonly FlowVectorCalculator _flowVectorCalculator;

		// Token: 0x04000071 RID: 113
		public readonly WaterSimulator _waterSimulator;

		// Token: 0x04000072 RID: 114
		public byte[] _threadSafeColumnCounts;

		// Token: 0x04000073 RID: 115
		public ReadOnlyWaterColumn[] _threadSafeWaterColumns;

		// Token: 0x04000074 RID: 116
		public Vector2[] _waterFlowDirections;

		// Token: 0x04000075 RID: 117
		public int _verticalStride;
	}
}
