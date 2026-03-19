using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.MapIndexSystem;
using Timberborn.Persistence;
using Timberborn.SimulationSystem;
using Timberborn.SingletonSystem;
using Timberborn.SoilBarrierSystem;
using Timberborn.TerrainSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000012 RID: 18
	[MapEditorTickable]
	public class SoilMoistureSimulator : ISaveableSingleton, ILoadableSingleton, ITickableSingleton, IParallelTickableSingleton
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000037D4 File Offset: 0x000019D4
		public SoilMoistureSimulator(ISingletonLoader singletonLoader, MapIndexService mapIndexService, ISpecService specService, FloatPackedListSerializer floatPackedListSerializer, SoilMoistureSimulationTaskStarter soilMoistureSimulationTaskStarter, SoilBarrierMap soilBarrierMap, IThreadSafeWaterMap threadSafeWaterMap, IThreadSafeColumnTerrainMap threadSafeColumnTerrainMap, SimulationController simulationController, ITerrainService terrainService, ITickableSingletonService tickableSingletonService, WaterEvaporationMap waterEvaporationMap, TickOnlyArrayService tickOnlyArrayService)
		{
			this._singletonLoader = singletonLoader;
			this._mapIndexService = mapIndexService;
			this._specService = specService;
			this._floatPackedListSerializer = floatPackedListSerializer;
			this._soilMoistureSimulationTaskStarter = soilMoistureSimulationTaskStarter;
			this._soilBarrierMap = soilBarrierMap;
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._threadSafeColumnTerrainMap = threadSafeColumnTerrainMap;
			this._simulationController = simulationController;
			this._terrainService = terrainService;
			this._tickableSingletonService = tickableSingletonService;
			this._waterEvaporationMap = waterEvaporationMap;
			this._tickOnlyArrayService = tickOnlyArrayService;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003862 File Offset: 0x00001A62
		public ReadOnlySpan<float> MoistureLevels
		{
			get
			{
				return this._moistureLevels.GetReadOnlySpan();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000386F File Offset: 0x00001A6F
		public ReadOnlySpan<bool> MoistureLevelsChangedLastTick
		{
			get
			{
				return this._moistureLevelsChangedLastTick.GetReadOnlySpan();
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000387C File Offset: 0x00001A7C
		public void Load()
		{
			this._verticalStride = this._mapIndexService.VerticalStride;
			int maxColumnCount = this._threadSafeColumnTerrainMap.MaxColumnCount;
			int size = this._verticalStride * maxColumnCount;
			this._moistureLevels = this._tickOnlyArrayService.Create<float>(size);
			this._lastTickMoistureLevels = this._tickOnlyArrayService.Create<float>(size);
			this._moistureLevelsChangedLastTick = this._tickOnlyArrayService.Create<bool>(size);
			this._wateredNeighbours = this._tickOnlyArrayService.Create<byte>(this._verticalStride);
			this._clusterSaturations = this._tickOnlyArrayService.Create<byte>(this._verticalStride);
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(SoilMoistureSimulator.SoilMoistureSimulatorKey, out objectLoader))
			{
				int val = objectLoader.Has<int>(SoilMoistureSimulator.SizeKey) ? objectLoader.Get(SoilMoistureSimulator.SizeKey) : 1;
				PackedList<float> packedList = objectLoader.Get<PackedList<float>>(SoilMoistureSimulator.MoistureLevelsKey, this._floatPackedListSerializer);
				Span<float> span = this._moistureLevels.GetSpan();
				this._mapIndexService.Unpack<float>(packedList, span, Math.Min(val, maxColumnCount));
			}
			SoilMoistureSimulatorSpec singleSpec = this._specService.GetSingleSpec<SoilMoistureSimulatorSpec>();
			this._verticalSpreadCostMultiplier = singleSpec.VerticalSpreadCostMultiplier;
			this._threadSafeWaterMap.MaxWaterColumnCountChanged += delegate(object _, int i)
			{
				this.AddAction(SoilMoistureSimulator.ActionType.MaxWaterColumnCountChange, i);
			};
			this._threadSafeColumnTerrainMap.MaxTerrainColumnCountChanged += delegate(object _, int i)
			{
				this.AddAction(SoilMoistureSimulator.ActionType.MaxTerrainThreadSafeColumnCountChanged, i);
			};
			this._threadSafeColumnTerrainMap.ColumnMovedUp += delegate(object _, int i)
			{
				this.AddAction(SoilMoistureSimulator.ActionType.ColumnMovedUp, i);
			};
			this._threadSafeColumnTerrainMap.ColumnMovedDown += delegate(object _, int i)
			{
				this.AddAction(SoilMoistureSimulator.ActionType.ColumnMovedDown, i);
			};
			this._threadSafeColumnTerrainMap.ColumnReset += delegate(object _, int i)
			{
				this.AddAction(SoilMoistureSimulator.ActionType.ColumnReset, i);
			};
			this._terrainService.TerrainHeightChanged += delegate(object _, TerrainHeightChangeEventArgs eventArgs)
			{
				this._terrainHeightChanges.Add(eventArgs.Change);
			};
			this._tickableSingletonService.ForcedParallelTickFinished += delegate(object _, EventArgs _)
			{
				this.Tick();
			};
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A34 File Offset: 0x00001C34
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(SoilMoistureSimulator.SoilMoistureSimulatorKey);
			int maxColumnCount = this._threadSafeColumnTerrainMap.MaxColumnCount;
			singleton.Set(SoilMoistureSimulator.SizeKey, maxColumnCount);
			singleton.Set<PackedList<float>>(SoilMoistureSimulator.MoistureLevelsKey, this._mapIndexService.Pack<float>(this._moistureLevels.GetReadOnlySpan(), maxColumnCount), this._floatPackedListSerializer);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A8B File Offset: 0x00001C8B
		public void Tick()
		{
			this.ProcessActions();
			this.ProcessTerrainHeightChanges();
			this.Reset();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public void StartParallelTick()
		{
			SoilMoistureSimulationTaskStarter soilMoistureSimulationTaskStarter = this._soilMoistureSimulationTaskStarter;
			byte[] array = this._wateredNeighbours.GetArray();
			byte[] array2 = this._clusterSaturations.GetArray();
			float[] array3 = this._moistureLevels.GetArray();
			bool[] array4 = this._moistureLevelsChangedLastTick.GetArray();
			float[] array5 = this._lastTickMoistureLevels.GetArray();
			float[] unsafeEvaporationModifiers = this._waterEvaporationMap.UnsafeEvaporationModifiers;
			ReadOnlyArray<bool> fullMoistureBarriers = this._soilBarrierMap.FullMoistureBarriers;
			ReadOnlyArray<bool> aboveMoistureBarriers = this._soilBarrierMap.AboveMoistureBarriers;
			ReadOnlyArray<byte> columnCounts = this._threadSafeWaterMap.ColumnCounts;
			ReadOnlyArray<ReadOnlyWaterColumn> waterColumns = this._threadSafeWaterMap.WaterColumns;
			ReadOnlyArray<byte> columnCounts2 = this._threadSafeColumnTerrainMap.ColumnCounts;
			ReadOnlyArray<ReadOnlyTerrainColumn> terrainColumns = this._threadSafeColumnTerrainMap.TerrainColumns;
			soilMoistureSimulationTaskStarter.StartTask(array, array2, array3, array4, array5, unsafeEvaporationModifiers, fullMoistureBarriers, aboveMoistureBarriers, columnCounts, waterColumns, columnCounts2, terrainColumns);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003B50 File Offset: 0x00001D50
		public void AddAction(SoilMoistureSimulator.ActionType actionType, int i)
		{
			this._actions.Add(new SoilMoistureSimulator.Action(actionType, i));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003B64 File Offset: 0x00001D64
		public void ProcessActions()
		{
			foreach (SoilMoistureSimulator.Action action in this._actions)
			{
				this.ProcessAction(action);
			}
			this._actions.Clear();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public void ProcessTerrainHeightChanges()
		{
			for (int i = 0; i < this._terrainHeightChanges.Count; i++)
			{
				this.UpdateMoistureFromHeightChange(this._terrainHeightChanges[i]);
			}
			this._terrainHeightChanges.Clear();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003C04 File Offset: 0x00001E04
		public void Reset()
		{
			if (this._simulationController.ShouldResetSimulation)
			{
				this._wateredNeighbours.GetSpan().Clear();
				this._clusterSaturations.GetSpan().Clear();
				this._moistureLevels.GetSpan().Clear();
				this._moistureLevelsChangedLastTick.GetSpan().Clear();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003C6C File Offset: 0x00001E6C
		public void ProcessAction(SoilMoistureSimulator.Action action)
		{
			switch (action.Type)
			{
			case SoilMoistureSimulator.ActionType.MaxWaterColumnCountChange:
				this.ResizeWaterBasedArrays(action.Value);
				return;
			case SoilMoistureSimulator.ActionType.MaxTerrainThreadSafeColumnCountChanged:
				this.ResizeTerrainBasedArrays(action.Value);
				return;
			case SoilMoistureSimulator.ActionType.ColumnMovedUp:
				this.MoveColumn(action.Value, action.Value - this._verticalStride);
				return;
			case SoilMoistureSimulator.ActionType.ColumnMovedDown:
				this.MoveColumn(action.Value, action.Value + this._verticalStride);
				return;
			case SoilMoistureSimulator.ActionType.ColumnReset:
				this.ResetColumn(action.Value);
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003D04 File Offset: 0x00001F04
		public unsafe void UpdateMoistureFromHeightChange(TerrainHeightChange terrainHeightChange)
		{
			Span<float> span = this._moistureLevels.GetSpan();
			Span<bool> span2 = this._moistureLevelsChangedLastTick.GetSpan();
			bool setTerrain = terrainHeightChange.SetTerrain;
			int num = setTerrain ? (terrainHeightChange.To + 1) : terrainHeightChange.From;
			int index2D = this._mapIndexService.CellToIndex(terrainHeightChange.Coordinates);
			int num2;
			if (this._threadSafeColumnTerrainMap.TryGetIndexAtOrAboveCeiling(index2D, num, out num2))
			{
				*span2[num2] = true;
				if (setTerrain)
				{
					int num3 = num - terrainHeightChange.From;
					float val = *span[num2] - (float)(this._verticalSpreadCostMultiplier * num3);
					*span[num2] = Math.Max(0f, val);
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public void ResizeWaterBasedArrays(int maxColumnCount)
		{
			int newSize = maxColumnCount * this._verticalStride;
			this._wateredNeighbours.Resize(newSize);
			this._clusterSaturations.Resize(newSize);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public void ResizeTerrainBasedArrays(int maxColumnCount)
		{
			int newSize = maxColumnCount * this._verticalStride;
			this._moistureLevels.Resize(newSize);
			this._lastTickMoistureLevels.Resize(newSize);
			this._moistureLevelsChangedLastTick.Resize(newSize);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E20 File Offset: 0x00002020
		public unsafe void MoveColumn(int target, int source)
		{
			Span<float> span = this._moistureLevels.GetSpan();
			*span[target] = *span[source];
			Span<float> span2 = this._lastTickMoistureLevels.GetSpan();
			*span2[target] = *span2[source];
			*this._moistureLevelsChangedLastTick.GetSpan()[target] = true;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003E80 File Offset: 0x00002080
		public unsafe void ResetColumn(int index3D)
		{
			*this._moistureLevels.GetSpan()[index3D] = 0f;
			*this._lastTickMoistureLevels.GetSpan()[index3D] = 0f;
			*this._moistureLevelsChangedLastTick.GetSpan()[index3D] = true;
		}

		// Token: 0x0400004F RID: 79
		public static readonly SingletonKey SoilMoistureSimulatorKey = new SingletonKey("SoilMoistureSimulator");

		// Token: 0x04000050 RID: 80
		public static readonly PropertyKey<int> SizeKey = new PropertyKey<int>("Size");

		// Token: 0x04000051 RID: 81
		public static readonly PropertyKey<PackedList<float>> MoistureLevelsKey = new PropertyKey<PackedList<float>>("MoistureLevels");

		// Token: 0x04000052 RID: 82
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000053 RID: 83
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000054 RID: 84
		public readonly ISpecService _specService;

		// Token: 0x04000055 RID: 85
		public readonly FloatPackedListSerializer _floatPackedListSerializer;

		// Token: 0x04000056 RID: 86
		public readonly SoilMoistureSimulationTaskStarter _soilMoistureSimulationTaskStarter;

		// Token: 0x04000057 RID: 87
		public readonly SoilBarrierMap _soilBarrierMap;

		// Token: 0x04000058 RID: 88
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000059 RID: 89
		public readonly IThreadSafeColumnTerrainMap _threadSafeColumnTerrainMap;

		// Token: 0x0400005A RID: 90
		public readonly SimulationController _simulationController;

		// Token: 0x0400005B RID: 91
		public readonly ITerrainService _terrainService;

		// Token: 0x0400005C RID: 92
		public readonly ITickableSingletonService _tickableSingletonService;

		// Token: 0x0400005D RID: 93
		public readonly WaterEvaporationMap _waterEvaporationMap;

		// Token: 0x0400005E RID: 94
		public readonly TickOnlyArrayService _tickOnlyArrayService;

		// Token: 0x0400005F RID: 95
		public TickOnlyArray<float> _moistureLevels;

		// Token: 0x04000060 RID: 96
		public TickOnlyArray<bool> _moistureLevelsChangedLastTick;

		// Token: 0x04000061 RID: 97
		public TickOnlyArray<float> _lastTickMoistureLevels;

		// Token: 0x04000062 RID: 98
		public TickOnlyArray<byte> _wateredNeighbours;

		// Token: 0x04000063 RID: 99
		public TickOnlyArray<byte> _clusterSaturations;

		// Token: 0x04000064 RID: 100
		public int _verticalStride;

		// Token: 0x04000065 RID: 101
		public int _verticalSpreadCostMultiplier;

		// Token: 0x04000066 RID: 102
		public readonly List<SoilMoistureSimulator.Action> _actions = new List<SoilMoistureSimulator.Action>();

		// Token: 0x04000067 RID: 103
		public readonly List<TerrainHeightChange> _terrainHeightChanges = new List<TerrainHeightChange>();

		// Token: 0x02000013 RID: 19
		public readonly struct Action
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000081 RID: 129 RVA: 0x00003F53 File Offset: 0x00002153
			public SoilMoistureSimulator.ActionType Type { get; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000082 RID: 130 RVA: 0x00003F5B File Offset: 0x0000215B
			public int Value { get; }

			// Token: 0x06000083 RID: 131 RVA: 0x00003F63 File Offset: 0x00002163
			public Action(SoilMoistureSimulator.ActionType type, int value)
			{
				this.Type = type;
				this.Value = value;
			}
		}

		// Token: 0x02000014 RID: 20
		public enum ActionType
		{
			// Token: 0x0400006B RID: 107
			MaxWaterColumnCountChange,
			// Token: 0x0400006C RID: 108
			MaxTerrainThreadSafeColumnCountChanged,
			// Token: 0x0400006D RID: 109
			ColumnMovedUp,
			// Token: 0x0400006E RID: 110
			ColumnMovedDown,
			// Token: 0x0400006F RID: 111
			ColumnReset
		}
	}
}
