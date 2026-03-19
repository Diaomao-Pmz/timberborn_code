using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.MapIndexSystem;
using Timberborn.Persistence;
using Timberborn.SimulationSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TickSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000038 RID: 56
	[MapEditorTickable]
	public class WaterSimulator : ISaveableSingleton, ILoadableSingleton, IPostLoadableSingleton, ITickableSingleton, IParallelTickableSingleton
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005FE6 File Offset: 0x000041E6
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00005FEE File Offset: 0x000041EE
		public int MaxColumnCount { get; private set; } = 1;

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005FF7 File Offset: 0x000041F7
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00005FFF File Offset: 0x000041FF
		public bool AnyColumnChanged { get; private set; }

		// Token: 0x06000128 RID: 296 RVA: 0x00006008 File Offset: 0x00004208
		public WaterSimulator(MapIndexService mapIndexService, ITerrainService terrainService, ISingletonLoader singletonLoader, ColumnOutflowsPackedListSerializer columnOutflowsPackedListSerializer, WaterColumnPackedListSerializer waterColumnPackedListSerializer, MutableWaterColumnRetriever mutableWaterColumnRetriever, SimulationController simulationController, ITickableSingletonService tickableSingletonService, WaterMapLoader waterMapLoader, WaterSourceRegistry waterSourceRegistry, IThreadSafeWaterEvaporationMap threadSafeWaterEvaporationMap, FlowLimiterService flowLimiterService, WaterSimulationTaskStarter waterSimulationTaskStarter, WaterChangeService waterChangeService, TickOnlyArrayService tickOnlyArrayService, WaterSimulationMigrator waterSimulationMigrator)
		{
			this._mapIndexService = mapIndexService;
			this._terrainService = terrainService;
			this._singletonLoader = singletonLoader;
			this._columnOutflowsPackedListSerializer = columnOutflowsPackedListSerializer;
			this._waterColumnPackedListSerializer = waterColumnPackedListSerializer;
			this._mutableWaterColumnRetriever = mutableWaterColumnRetriever;
			this._simulationController = simulationController;
			this._tickableSingletonService = tickableSingletonService;
			this._waterMapLoader = waterMapLoader;
			this._waterSourceRegistry = waterSourceRegistry;
			this._threadSafeWaterEvaporationMap = threadSafeWaterEvaporationMap;
			this._flowLimiterService = flowLimiterService;
			this._waterSimulationTaskStarter = waterSimulationTaskStarter;
			this._waterChangeService = waterChangeService;
			this._tickOnlyArrayService = tickOnlyArrayService;
			this._waterSimulationMigrator = waterSimulationMigrator;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000060AA File Offset: 0x000042AA
		public ReadOnlySpan<WaterColumn> WaterColumns
		{
			get
			{
				return this._waterColumns.GetReadOnlySpan();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000060B7 File Offset: 0x000042B7
		public ReadOnlySpan<ColumnOutflows> Outflows
		{
			get
			{
				return this._outflows.GetReadOnlySpan();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000060C4 File Offset: 0x000042C4
		public ReadOnlySpan<byte> ColumnCounts
		{
			get
			{
				return this._columnCounts.GetReadOnlySpan();
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000060D4 File Offset: 0x000042D4
		public unsafe void Load()
		{
			this._mapSize = this._mapIndexService.TotalSize;
			this._maxColumnHeight = this._mapSize.z + 1;
			this._stride = this._mapIndexService.Stride;
			this._verticalStride = this._mapIndexService.VerticalStride;
			this._outflows = this._tickOnlyArrayService.Create<ColumnOutflows>(this._verticalStride);
			this._horizontalObstacles = this._tickOnlyArrayService.Create<byte>(this._verticalStride * this._maxColumnHeight);
			int maxIndex = this._mapIndexService.MaxIndex;
			this._baseLevelFlows = this._tickOnlyArrayService.Create<WaterFlow>(maxIndex);
			this._baseLevelDiffusions = this._tickOnlyArrayService.Create<Diffusions>(maxIndex);
			this._directedFlows = this._tickOnlyArrayService.Create<List<DirectedFlow>>(maxIndex);
			this._targetedDiffusions = this._tickOnlyArrayService.Create<List<TargetedDiffusion>>(maxIndex);
			Span<List<DirectedFlow>> span = this._directedFlows.GetSpan();
			Span<List<TargetedDiffusion>> span2 = this._targetedDiffusions.GetSpan();
			for (int i = 0; i < maxIndex; i++)
			{
				*span[i] = new List<DirectedFlow>(0);
				*span2[i] = new List<TargetedDiffusion>(0);
			}
			this._contaminationsBuffer = this._tickOnlyArrayService.Create<float>(this._verticalStride);
			this._targetedDiffusionCount = this._tickOnlyArrayService.Create<byte>(this._verticalStride);
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
			this._tickableSingletonService.ForcedParallelTickFinished += delegate(object _, EventArgs _)
			{
				this.ProcessModifications();
			};
			this.CreateColumns();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006254 File Offset: 0x00004454
		public unsafe void PostLoad()
		{
			this.Update();
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(WaterSimulator.WaterMapKey, out objectLoader))
			{
				int levels = objectLoader.Get(WaterSimulator.LevelsKey);
				PackedList<WaterColumn> packedList = objectLoader.Get<PackedList<WaterColumn>>(WaterSimulator.WaterColumnsKey, this._waterColumnPackedListSerializer);
				PackedList<ColumnOutflows> packedList2 = objectLoader.Get<PackedList<ColumnOutflows>>(WaterSimulator.ColumnOutflowsKey, this._columnOutflowsPackedListSerializer);
				WaterColumn[] array = this._mapIndexService.Unpack<WaterColumn>(packedList, levels);
				ColumnOutflows[] array2 = this._mapIndexService.Unpack<ColumnOutflows>(packedList2, levels);
				int num = array.Length;
				ReadOnlySpan<byte> readOnlySpan = this._columnCounts.GetReadOnlySpan();
				Span<WaterColumn> span = this._waterColumns.GetSpan();
				Span<ColumnOutflows> span2 = this._outflows.GetSpan();
				foreach (int num2 in this._mapIndexService.Indices2D)
				{
					for (int i = 0; i < (int)(*readOnlySpan[num2]); i++)
					{
						int num3 = i * this._verticalStride + num2;
						ref WaterColumn ptr = span[num3];
						if (num3 < num)
						{
							ref WaterColumn ptr2 = ref array[num3];
							ptr.WaterDepth = ptr2.WaterDepth;
							ptr.Contamination = ptr2.Contamination;
							ptr.Overflow = ptr2.Overflow;
							*span2[num3] = array2[num3];
						}
					}
				}
			}
			else
			{
				this._waterMapLoader.Load(this._waterColumns.GetSpan(), this._outflows.GetSpan());
			}
			this._waterSimulationMigrator.MigrateOutflows(this._outflows.GetSpan());
			this.AnyColumnChanged = true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000063ED File Offset: 0x000045ED
		public void Tick()
		{
			this.Update();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000063F8 File Offset: 0x000045F8
		public void StartParallelTick()
		{
			this._waterSimulationTaskStarter.Simulate(this._directedFlows.GetArray(), this._baseLevelFlows.GetArray(), this._waterColumns.GetArray(), this._outflows.GetArray(), this._contaminationsBuffer.GetArray(), this._baseLevelDiffusions.GetArray(), this._targetedDiffusionCount.GetArray(), this._targetedDiffusions.GetArray(), new ReadOnlyArray<byte>(this._columnCounts.GetArray()), this._flowLimiterService.LimitedDirections, this._flowLimiterService.LimitedValues, this._flowLimiterService.FlowControllers, this._flowLimiterService.OutflowLimits, this._threadSafeWaterEvaporationMap.EvaporationModifiers, this._waterSourceRegistry.ThreadSafeWaterSources, this._waterChangeService.ThreadSafeWaterChanges);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000064C8 File Offset: 0x000046C8
		public unsafe void Save(ISingletonSaver singletonSaver)
		{
			int num = 1;
			ReadOnlySpan<byte> readOnlySpan = this._columnCounts.GetReadOnlySpan();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				byte b = *readOnlySpan[i];
				if ((int)b > num)
				{
					num = (int)b;
				}
			}
			IObjectSaver singleton = singletonSaver.GetSingleton(WaterSimulator.WaterMapKey);
			singleton.Set(WaterSimulator.LevelsKey, num);
			singleton.Set<PackedList<WaterColumn>>(WaterSimulator.WaterColumnsKey, this._mapIndexService.Pack<WaterColumn>(this._waterColumns.GetReadOnlySpan(), num), this._waterColumnPackedListSerializer);
			singleton.Set<PackedList<ColumnOutflows>>(WaterSimulator.ColumnOutflowsKey, this._mapIndexService.Pack<ColumnOutflows>(this._outflows.GetReadOnlySpan(), num), this._columnOutflowsPackedListSerializer);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000656B File Offset: 0x0000476B
		public void AddFullObstacle(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new WaterSimulator.Modification(true, coordinates, WaterSimulator.ChangeType.FullObstacle));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006580 File Offset: 0x00004780
		public void RemoveFullObstacle(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new WaterSimulator.Modification(false, coordinates, WaterSimulator.ChangeType.FullObstacle));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006595 File Offset: 0x00004795
		public void AddHorizontalObstacle(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new WaterSimulator.Modification(true, coordinates, WaterSimulator.ChangeType.HorizontalObstacle));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000065AA File Offset: 0x000047AA
		public void RemoveHorizontalObstacle(Vector3Int coordinates)
		{
			this._modifications.Enqueue(new WaterSimulator.Modification(false, coordinates, WaterSimulator.ChangeType.HorizontalObstacle));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000065BF File Offset: 0x000047BF
		public void FullyBlockCell(Vector2Int coordinates)
		{
			this._modifications.Enqueue(new WaterSimulator.Modification(true, coordinates.XYZ(), WaterSimulator.ChangeType.FullCellBlock));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000065D9 File Offset: 0x000047D9
		public void FullyUnblockCell(Vector2Int coordinates)
		{
			this._modifications.Enqueue(new WaterSimulator.Modification(false, coordinates.XYZ(), WaterSimulator.ChangeType.FullCellBlock));
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000065F4 File Offset: 0x000047F4
		public unsafe void CreateColumns()
		{
			this._columnCounts = this._tickOnlyArrayService.Create<byte>(this._mapIndexService.MaxIndex);
			this._waterColumns = this._tickOnlyArrayService.Create<WaterColumn>(this._verticalStride);
			for (int i = 0; i < this._mapIndexService.MaxIndex; i++)
			{
				*this._waterColumns.GetSpan()[i] = new WaterColumn(0, this._maxColumnHeight);
				*this._columnCounts.GetSpan()[i] = 1;
				if (this._mapIndexService.IndexIsInActualMap(i))
				{
					for (int j = 0; j < this._mapIndexService.TerrainSize.z; j++)
					{
						int index = j * this._verticalStride + i;
						if (this._terrainService.UnsafeCellIsTerrain(index))
						{
							this.AddFullObstacleInternal(i, j);
						}
					}
				}
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000066DB File Offset: 0x000048DB
		public void Update()
		{
			this.AnyColumnChanged = false;
			this.ProcessModifications();
			if (this._simulationController.ShouldResetSimulation)
			{
				this.Reset();
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006700 File Offset: 0x00004900
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
			for (int i = change.From; i <= change.To; i++)
			{
				this._modifications.Enqueue(new WaterSimulator.Modification(change.SetTerrain, change.Coordinates.ToVector3Int(i), WaterSimulator.ChangeType.FullObstacle));
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006754 File Offset: 0x00004954
		public void ProcessModifications()
		{
			while (!this._modifications.IsEmpty<WaterSimulator.Modification>())
			{
				WaterSimulator.Modification modification = this._modifications.Dequeue();
				if (modification.Added)
				{
					this.Add(modification);
				}
				else
				{
					this.Remove(modification);
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006798 File Offset: 0x00004998
		public void Add(WaterSimulator.Modification modification)
		{
			Vector3Int coordinates = modification.Coordinates;
			switch (modification.ChangeType)
			{
			case WaterSimulator.ChangeType.FullObstacle:
				this.AddFullObstacleInternal(this._mapIndexService.CellToIndex(coordinates.XY()), coordinates.z);
				return;
			case WaterSimulator.ChangeType.HorizontalObstacle:
				this.AddHorizontalObstacleInternal(coordinates);
				return;
			case WaterSimulator.ChangeType.FullCellBlock:
				this.FullyBlockCellInternal(coordinates.XY());
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006804 File Offset: 0x00004A04
		public void Remove(WaterSimulator.Modification modification)
		{
			Vector3Int coordinates = modification.Coordinates;
			switch (modification.ChangeType)
			{
			case WaterSimulator.ChangeType.FullObstacle:
				this.RemoveFullObstacleInternal(coordinates);
				return;
			case WaterSimulator.ChangeType.HorizontalObstacle:
				this.RemoveHorizontalObstacleInternal(coordinates);
				return;
			case WaterSimulator.ChangeType.FullCellBlock:
				this.FullyUnblockCellInternal(coordinates.XY());
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006858 File Offset: 0x00004A58
		public void AddFullObstacleInternal(int index2D, int height)
		{
			ref WaterColumn column = ref this._mutableWaterColumnRetriever.GetColumn(this._columnCounts.GetReadOnlySpan(), this._waterColumns.GetSpan(), this._verticalStride, index2D, height);
			if ((int)column.Floor == height)
			{
				if ((int)(column.Ceiling - 1) == height)
				{
					this.RemoveColumn(index2D, this.GetColumnIndex(index2D, height));
				}
				else
				{
					column.Floor = Convert.ToByte((int)(column.Floor + 1));
					column.WaterDepth = ((column.WaterDepth > 1f) ? (column.WaterDepth - 1f) : 0f);
				}
			}
			else if ((int)(column.Ceiling - 1) == height)
			{
				column.Ceiling = Convert.ToByte((int)(column.Ceiling - 1));
			}
			else
			{
				this.SplitColumn(ref column, index2D, height, height + 1);
			}
			this.AnyColumnChanged = true;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006924 File Offset: 0x00004B24
		public unsafe void RemoveFullObstacleInternal(Vector3Int coordinates)
		{
			int num = this._mapIndexService.CellToIndex(coordinates.XY());
			int z = coordinates.z;
			int columnIndex = this.GetColumnIndex(num, z + 1);
			int columnIndex2 = this.GetColumnIndex(num, z - 1);
			bool flag = *this._horizontalObstacles.GetSpan()[(z + 1) * this._verticalStride + num] > 0;
			bool flag2 = *this._horizontalObstacles.GetSpan()[z * this._verticalStride + num] > 0;
			if ((columnIndex == -1 && columnIndex2 == -1) || (flag && flag2))
			{
				this.InsertNewColumn(z, num);
			}
			else if (columnIndex != -1 && columnIndex2 != -1 && !flag && !flag2)
			{
				this.MergeColumns(num, columnIndex, columnIndex2);
			}
			else if (columnIndex != -1 && !flag)
			{
				this._waterColumns.GetSpan()[columnIndex * this._verticalStride + num].Floor = Convert.ToByte(z);
			}
			else if (columnIndex2 != -1 && !flag2)
			{
				this._waterColumns.GetSpan()[columnIndex2 * this._verticalStride + num].Ceiling = Convert.ToByte(z + 1);
			}
			else
			{
				this.InsertNewColumn(z, num);
			}
			this.AnyColumnChanged = true;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006A5C File Offset: 0x00004C5C
		public void AddHorizontalObstacleInternal(Vector3Int coordinates)
		{
			int num = this._mapIndexService.CellToIndex(coordinates.XY());
			int z = coordinates.z;
			int num2 = z * this._verticalStride + num;
			ref byte ptr = this._horizontalObstacles.GetSpan()[num2];
			byte b = ptr + 1;
			ptr = b;
			if (b == 1)
			{
				int columnIndex = this.GetColumnIndex(num, z);
				if (columnIndex != -1)
				{
					ref WaterColumn ptr2 = this._waterColumns.GetSpan()[columnIndex * this._verticalStride + num];
					if ((int)ptr2.Floor != z)
					{
						this.SplitColumn(ref ptr2, num, z, z);
						this.AnyColumnChanged = true;
					}
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006AFC File Offset: 0x00004CFC
		public void RemoveHorizontalObstacleInternal(Vector3Int coordinates)
		{
			int num = this._mapIndexService.CellToIndex(coordinates.XY());
			int z = coordinates.z;
			int num2 = z * this._verticalStride + num;
			ref byte ptr = this._horizontalObstacles.GetSpan()[num2];
			byte b = ptr - 1;
			ptr = b;
			if (b == 0)
			{
				int columnIndex = this.GetColumnIndex(num, z);
				int columnIndex2 = this.GetColumnIndex(num, z - 1);
				if (columnIndex != -1 && columnIndex2 != -1)
				{
					this.MergeColumns(num, columnIndex, columnIndex2);
					this.AnyColumnChanged = true;
				}
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006B80 File Offset: 0x00004D80
		public unsafe void FullyBlockCellInternal(Vector2Int coordinates)
		{
			int num = this._mapIndexService.CellToIndex(coordinates);
			this.ClearColumns(num);
			*this._waterColumns.GetSpan()[num] = new WaterColumn(this._maxColumnHeight, this._maxColumnHeight);
			*this._columnCounts.GetSpan()[num] = 1;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public unsafe void FullyUnblockCellInternal(Vector2Int coordinates)
		{
			int num = this._mapIndexService.CellToIndex(coordinates);
			this.ClearColumns(num);
			*this._waterColumns.GetSpan()[num] = new WaterColumn(0, this._maxColumnHeight);
			*this._columnCounts.GetSpan()[num] = 1;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006C40 File Offset: 0x00004E40
		public void SplitColumn(ref WaterColumn column, int index, int splitHeight, int newFloorHeight)
		{
			WaterColumn waterColumn = new WaterColumn(newFloorHeight, (int)column.Ceiling);
			column.Ceiling = Convert.ToByte(splitHeight);
			float num = (float)column.Floor + column.WaterDepth;
			if (num > (float)newFloorHeight)
			{
				waterColumn.WaterDepth = num - (float)newFloorHeight;
				waterColumn.Contamination = column.Contamination;
				column.WaterDepth = (float)(splitHeight - (int)column.Floor);
			}
			this.InsertColumn(index, splitHeight, ref waterColumn);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public void MergeColumns(int index, int columnAboveIndex, int columnBelowIndex)
		{
			ref WaterColumn ptr = this._waterColumns.GetSpan()[columnAboveIndex * this._verticalStride + index];
			ref WaterColumn ptr2 = this._waterColumns.GetSpan()[columnBelowIndex * this._verticalStride + index];
			ptr2.Ceiling = ptr.Ceiling;
			float num = ptr2.WaterDepth + ptr.WaterDepth;
			if (num > 0f)
			{
				float num2 = ptr2.WaterDepth * ptr2.Contamination + ptr.WaterDepth * ptr.Contamination;
				ptr2.Contamination = num2 / num;
			}
			ptr2.WaterDepth = num;
			this.RemoveColumn(index, columnAboveIndex);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006D54 File Offset: 0x00004F54
		public void InsertNewColumn(int height, int index)
		{
			WaterColumn waterColumn = new WaterColumn(height, height + 1);
			this.InsertColumn(index, height, ref waterColumn);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006D78 File Offset: 0x00004F78
		public unsafe void InsertColumn(int index, int splitHeight, ref WaterColumn newColumn)
		{
			byte b = *this._columnCounts.GetSpan()[index];
			for (int i = (int)(*this._columnCounts.GetSpan()[index] - 1); i >= 0; i--)
			{
				int num = i * this._verticalStride + index;
				ref WaterColumn ptr = this._waterColumns.GetSpan()[num];
				if ((int)ptr.Floor <= splitHeight)
				{
					break;
				}
				int num2 = i + 1;
				if (num2 == this.MaxColumnCount)
				{
					this.IncreaseMaxColumnCount();
				}
				int num3 = num2 * this._verticalStride + index;
				*this._waterColumns.GetSpan()[num3] = ptr;
				*this._outflows.GetSpan()[num3] = *this._outflows.GetSpan()[num];
				this.UpdateNeighborsOutflows(index, num, num3);
				b = (byte)i;
			}
			int num4 = (int)b * this._verticalStride + index;
			ref byte ptr2 = this._columnCounts.GetSpan()[index];
			byte b2 = ptr2;
			ptr2 = b2 + 1;
			if ((int)b2 == this.MaxColumnCount)
			{
				this.IncreaseMaxColumnCount();
			}
			*this._waterColumns.GetSpan()[num4] = newColumn;
			WaterSimulator.ClearOutflows(this._outflows.GetSpan()[num4]);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006EEC File Offset: 0x000050EC
		public void IncreaseMaxColumnCount()
		{
			int maxColumnCount = this.MaxColumnCount;
			this.MaxColumnCount = maxColumnCount + 1;
			this.Resize();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006F10 File Offset: 0x00005110
		public void Resize()
		{
			int num = this.MaxColumnCount * this._mapIndexService.VerticalStride;
			this._waterColumns.Resize(num);
			this._outflows.Resize(num);
			this._contaminationsBuffer.Resize(num);
			this._targetedDiffusionCount.Resize(num);
			this.ClearOutflows((this.MaxColumnCount - 1) * this._mapIndexService.VerticalStride, num - 1);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006F80 File Offset: 0x00005180
		public unsafe void RemoveColumn(int index, int columnIndex)
		{
			byte b = *this._columnCounts.GetSpan()[index];
			for (int i = columnIndex + 1; i < (int)b; i++)
			{
				int num = i * this._verticalStride + index;
				int num2 = (i - 1) * this._verticalStride + index;
				*this._waterColumns.GetSpan()[num2] = *this._waterColumns.GetSpan()[num];
				*this._outflows.GetSpan()[num2] = *this._outflows.GetSpan()[num];
				this.UpdateNeighborsOutflows(index, num, num2);
			}
			ref byte ptr = this._columnCounts.GetSpan()[index];
			ptr -= 1;
			int num3 = (int)(b - 1) * this._verticalStride + index;
			*this._waterColumns.GetSpan()[num3] = default(WaterColumn);
			WaterSimulator.ClearOutflows(this._outflows.GetSpan()[num3]);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000070A4 File Offset: 0x000052A4
		public unsafe int GetColumnIndex(int index, int height)
		{
			for (int i = 0; i < (int)(*this._columnCounts.GetSpan()[index]); i++)
			{
				ref WaterColumn ptr = this._waterColumns.GetSpan()[i * this._verticalStride + index];
				if (height >= (int)ptr.Floor && height < (int)ptr.Ceiling)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007104 File Offset: 0x00005304
		public void UpdateNeighborsOutflows(int index, int previousIndex3D, int currentIndex3D)
		{
			this.UpdateOutflows(index - this._stride, previousIndex3D, currentIndex3D);
			this.UpdateOutflows(index - 1, previousIndex3D, currentIndex3D);
			this.UpdateOutflows(index + this._stride, previousIndex3D, currentIndex3D);
			this.UpdateOutflows(index + 1, previousIndex3D, currentIndex3D);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000713C File Offset: 0x0000533C
		public unsafe void UpdateOutflows(int index, int previousIndex3D, int currentIndex3D)
		{
			for (int i = 0; i < (int)(*this._columnCounts.GetSpan()[index]); i++)
			{
				ref ColumnOutflows ptr = this._outflows.GetSpan()[i * this._verticalStride + index];
				if (ptr.BottomFlow.Index3D == previousIndex3D)
				{
					ptr.BottomFlow.Index3D = currentIndex3D;
				}
				if (ptr.LeftFlow.Index3D == previousIndex3D)
				{
					ptr.LeftFlow.Index3D = currentIndex3D;
				}
				if (ptr.TopFlow.Index3D == previousIndex3D)
				{
					ptr.TopFlow.Index3D = currentIndex3D;
				}
				if (ptr.RightFlow.Index3D == previousIndex3D)
				{
					ptr.RightFlow.Index3D = currentIndex3D;
				}
				if (ptr.Outflows != null)
				{
					for (int j = 0; j < ptr.Outflows.Count; j++)
					{
						if (ptr.Outflows[j].Index3D == previousIndex3D)
						{
							ptr.Outflows[j] = new TargetedFlow(ptr.Outflows[j].Flow, currentIndex3D);
						}
					}
				}
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000724C File Offset: 0x0000544C
		public unsafe void ClearColumns(int index)
		{
			for (int i = 0; i < (int)(*this._columnCounts.GetSpan()[index]); i++)
			{
				int num = i * this._verticalStride + index;
				*this._waterColumns.GetSpan()[num] = default(WaterColumn);
				WaterSimulator.ClearOutflows(this._outflows.GetSpan()[num]);
				*this._columnCounts.GetSpan()[index] = 0;
			}
			this.AnyColumnChanged = true;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000072D8 File Offset: 0x000054D8
		public void ClearOutflows(int firstIndex, int lastIndex)
		{
			for (int i = firstIndex; i <= lastIndex; i++)
			{
				WaterSimulator.ClearOutflows(this._outflows.GetSpan()[i]);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000730C File Offset: 0x0000550C
		public unsafe void Reset()
		{
			foreach (int num in this._mapIndexService.Indices2D)
			{
				for (int i = 0; i < (int)(*this._columnCounts.GetSpan()[num]); i++)
				{
					int num2 = i * this._verticalStride + num;
					this._waterColumns.GetSpan()[num2].Reset();
					WaterSimulator.ClearOutflows(this._outflows.GetSpan()[num2]);
				}
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000073A4 File Offset: 0x000055A4
		public static void ClearOutflows(ref ColumnOutflows outflows)
		{
			outflows.BottomFlow.Flow = 0f;
			outflows.BottomFlow.Index3D = -1;
			outflows.LeftFlow.Flow = 0f;
			outflows.LeftFlow.Index3D = -1;
			outflows.TopFlow.Flow = 0f;
			outflows.TopFlow.Index3D = -1;
			outflows.RightFlow.Flow = 0f;
			outflows.RightFlow.Index3D = -1;
			List<TargetedFlow> outflows2 = outflows.Outflows;
			if (outflows2 == null)
			{
				return;
			}
			outflows2.Clear();
		}

		// Token: 0x04000109 RID: 265
		public static readonly SingletonKey WaterMapKey = new SingletonKey("WaterMapNew");

		// Token: 0x0400010A RID: 266
		public static readonly PropertyKey<int> LevelsKey = new PropertyKey<int>("Levels");

		// Token: 0x0400010B RID: 267
		public static readonly PropertyKey<PackedList<WaterColumn>> WaterColumnsKey = new PropertyKey<PackedList<WaterColumn>>("WaterColumns");

		// Token: 0x0400010C RID: 268
		public static readonly PropertyKey<PackedList<ColumnOutflows>> ColumnOutflowsKey = new PropertyKey<PackedList<ColumnOutflows>>("ColumnOutflows");

		// Token: 0x0400010F RID: 271
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000110 RID: 272
		public readonly ITerrainService _terrainService;

		// Token: 0x04000111 RID: 273
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000112 RID: 274
		public readonly ColumnOutflowsPackedListSerializer _columnOutflowsPackedListSerializer;

		// Token: 0x04000113 RID: 275
		public readonly WaterColumnPackedListSerializer _waterColumnPackedListSerializer;

		// Token: 0x04000114 RID: 276
		public readonly MutableWaterColumnRetriever _mutableWaterColumnRetriever;

		// Token: 0x04000115 RID: 277
		public readonly SimulationController _simulationController;

		// Token: 0x04000116 RID: 278
		public readonly WaterMapLoader _waterMapLoader;

		// Token: 0x04000117 RID: 279
		public readonly ITickableSingletonService _tickableSingletonService;

		// Token: 0x04000118 RID: 280
		public readonly WaterSourceRegistry _waterSourceRegistry;

		// Token: 0x04000119 RID: 281
		public readonly IThreadSafeWaterEvaporationMap _threadSafeWaterEvaporationMap;

		// Token: 0x0400011A RID: 282
		public readonly FlowLimiterService _flowLimiterService;

		// Token: 0x0400011B RID: 283
		public readonly WaterSimulationTaskStarter _waterSimulationTaskStarter;

		// Token: 0x0400011C RID: 284
		public readonly WaterChangeService _waterChangeService;

		// Token: 0x0400011D RID: 285
		public readonly TickOnlyArrayService _tickOnlyArrayService;

		// Token: 0x0400011E RID: 286
		public readonly WaterSimulationMigrator _waterSimulationMigrator;

		// Token: 0x0400011F RID: 287
		public TickOnlyArray<byte> _columnCounts;

		// Token: 0x04000120 RID: 288
		public TickOnlyArray<WaterColumn> _waterColumns;

		// Token: 0x04000121 RID: 289
		public TickOnlyArray<ColumnOutflows> _outflows;

		// Token: 0x04000122 RID: 290
		public TickOnlyArray<byte> _horizontalObstacles;

		// Token: 0x04000123 RID: 291
		public TickOnlyArray<WaterFlow> _baseLevelFlows;

		// Token: 0x04000124 RID: 292
		public TickOnlyArray<List<DirectedFlow>> _directedFlows;

		// Token: 0x04000125 RID: 293
		public TickOnlyArray<Diffusions> _baseLevelDiffusions;

		// Token: 0x04000126 RID: 294
		public TickOnlyArray<List<TargetedDiffusion>> _targetedDiffusions;

		// Token: 0x04000127 RID: 295
		public TickOnlyArray<float> _contaminationsBuffer;

		// Token: 0x04000128 RID: 296
		public TickOnlyArray<byte> _targetedDiffusionCount;

		// Token: 0x04000129 RID: 297
		public Vector3Int _mapSize;

		// Token: 0x0400012A RID: 298
		public int _stride;

		// Token: 0x0400012B RID: 299
		public int _verticalStride;

		// Token: 0x0400012C RID: 300
		public int _maxColumnHeight;

		// Token: 0x0400012D RID: 301
		public readonly Queue<WaterSimulator.Modification> _modifications = new Queue<WaterSimulator.Modification>();

		// Token: 0x02000039 RID: 57
		public readonly struct Modification
		{
			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000153 RID: 339 RVA: 0x00007477 File Offset: 0x00005677
			public bool Added { get; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000154 RID: 340 RVA: 0x0000747F File Offset: 0x0000567F
			public Vector3Int Coordinates { get; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000155 RID: 341 RVA: 0x00007487 File Offset: 0x00005687
			public WaterSimulator.ChangeType ChangeType { get; }

			// Token: 0x06000156 RID: 342 RVA: 0x0000748F File Offset: 0x0000568F
			public Modification(bool added, Vector3Int coordinates, WaterSimulator.ChangeType changeType)
			{
				this.Added = added;
				this.Coordinates = coordinates;
				this.ChangeType = changeType;
			}
		}

		// Token: 0x0200003A RID: 58
		public enum ChangeType
		{
			// Token: 0x04000132 RID: 306
			FullObstacle,
			// Token: 0x04000133 RID: 307
			HorizontalObstacle,
			// Token: 0x04000134 RID: 308
			FullCellBlock
		}
	}
}
