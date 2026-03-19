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

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x0200000F RID: 15
	[MapEditorTickable]
	public class SoilContaminationSimulator : ISaveableSingleton, ILoadableSingleton, ITickableSingleton, IParallelTickableSingleton
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003260 File Offset: 0x00001460
		public SoilContaminationSimulator(ISingletonLoader singletonLoader, MapIndexService mapIndexService, ISpecService specService, FloatPackedListSerializer floatPackedListSerializer, IThreadSafeWaterMap threadSafeWaterMap, SoilBarrierMap soilBarrierMap, IThreadSafeColumnTerrainMap threadSafeColumnTerrainMap, SoilContaminationSimulationTaskStarter soilContaminationSimulationTaskStarter, SimulationController simulationController, ITerrainService terrainService, ITickableSingletonService tickableSingletonService, TickOnlyArrayService tickOnlyArrayService)
		{
			this._singletonLoader = singletonLoader;
			this._mapIndexService = mapIndexService;
			this._specService = specService;
			this._floatPackedListSerializer = floatPackedListSerializer;
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._soilBarrierMap = soilBarrierMap;
			this._threadSafeColumnTerrainMap = threadSafeColumnTerrainMap;
			this._soilContaminationSimulationTaskStarter = soilContaminationSimulationTaskStarter;
			this._simulationController = simulationController;
			this._terrainService = terrainService;
			this._tickableSingletonService = tickableSingletonService;
			this._tickOnlyArrayService = tickOnlyArrayService;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000032E6 File Offset: 0x000014E6
		public ReadOnlySpan<float> ContaminationLevels
		{
			get
			{
				return this._contaminationLevels.GetReadOnlySpan();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000032F3 File Offset: 0x000014F3
		public ReadOnlySpan<bool> ContaminationsChangedLastTick
		{
			get
			{
				return this._contaminationsChangedLastTick.GetReadOnlySpan();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003300 File Offset: 0x00001500
		[BackwardCompatible(2023, 9, 22, Compatibility.Map)]
		public void Load()
		{
			this._verticalStride = this._mapIndexService.VerticalStride;
			int maxColumnCount = this._threadSafeColumnTerrainMap.MaxColumnCount;
			int size = this._verticalStride * maxColumnCount;
			this._lastTickContaminationCandidates = this._tickOnlyArrayService.Create<float>(size);
			this._contaminationsChangedLastTick = this._tickOnlyArrayService.Create<bool>(size);
			this._contaminationCandidates = this._tickOnlyArrayService.Create<float>(size);
			this._contaminationLevels = this._tickOnlyArrayService.Create<float>(size);
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(SoilContaminationSimulator.SoilContaminationSimulatorKey, out objectLoader))
			{
				int levels = objectLoader.Has<int>(SoilContaminationSimulator.SizeKey) ? Math.Min(objectLoader.Get(SoilContaminationSimulator.SizeKey), maxColumnCount) : 1;
				PackedList<float> packedList = objectLoader.Get<PackedList<float>>(SoilContaminationSimulator.ContaminationCandidatesKey, this._floatPackedListSerializer);
				this._mapIndexService.Unpack<float>(packedList, this._contaminationCandidates.GetSpan(), levels);
				PackedList<float> packedList2 = objectLoader.Get<PackedList<float>>(SoilContaminationSimulator.ContaminationLevelsKey, this._floatPackedListSerializer);
				Span<float> span = this._contaminationLevels.GetSpan();
				this._mapIndexService.Unpack<float>(packedList2, span, levels);
			}
			else
			{
				this.BackwardCompatibleLoad();
			}
			SoilContaminationSimulatorSpec singleSpec = this._specService.GetSingleSpec<SoilContaminationSimulatorSpec>();
			this._verticalCostModifier = singleSpec.VerticalSpreadCostMultiplier / (float)singleSpec.MaxRangeFromSource;
			this._verticalStride = this._mapIndexService.VerticalStride;
			this._terrainService.TerrainHeightChanged += delegate(object _, TerrainHeightChangeEventArgs eventArgs)
			{
				this._terrainHeightChanges.Add(eventArgs.Change);
			};
			this._threadSafeColumnTerrainMap.MaxTerrainColumnCountChanged += delegate(object _, int i)
			{
				this.AddAction(SoilContaminationSimulator.ActionType.MaxTerrainThreadSafeColumnCountChanged, i);
			};
			this._threadSafeColumnTerrainMap.ColumnMovedUp += delegate(object _, int i)
			{
				this.AddAction(SoilContaminationSimulator.ActionType.ColumnMovedUp, i);
			};
			this._threadSafeColumnTerrainMap.ColumnMovedDown += delegate(object _, int i)
			{
				this.AddAction(SoilContaminationSimulator.ActionType.ColumnMovedDown, i);
			};
			this._threadSafeColumnTerrainMap.ColumnReset += delegate(object _, int i)
			{
				this.AddAction(SoilContaminationSimulator.ActionType.ColumnReset, i);
			};
			this._tickableSingletonService.ForcedParallelTickFinished += delegate(object _, EventArgs _)
			{
				this.Tick();
			};
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000034D8 File Offset: 0x000016D8
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(SoilContaminationSimulator.SoilContaminationSimulatorKey);
			int maxColumnCount = this._threadSafeColumnTerrainMap.MaxColumnCount;
			singleton.Set(SoilContaminationSimulator.SizeKey, maxColumnCount);
			ReadOnlySpan<float> readOnlySpan = this._contaminationCandidates.GetReadOnlySpan();
			singleton.Set<PackedList<float>>(SoilContaminationSimulator.ContaminationCandidatesKey, this._mapIndexService.Pack<float>(readOnlySpan, maxColumnCount), this._floatPackedListSerializer);
			ReadOnlySpan<float> readOnlySpan2 = this._contaminationLevels.GetReadOnlySpan();
			singleton.Set<PackedList<float>>(SoilContaminationSimulator.ContaminationLevelsKey, this._mapIndexService.Pack<float>(readOnlySpan2, maxColumnCount), this._floatPackedListSerializer);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000355B File Offset: 0x0000175B
		public void Tick()
		{
			this.ProcessActions();
			this.ProcessTerrainHeightChanges();
			this.Reset();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003570 File Offset: 0x00001770
		public void StartParallelTick()
		{
			SoilContaminationSimulationTaskStarter soilContaminationSimulationTaskStarter = this._soilContaminationSimulationTaskStarter;
			float[] array = this._contaminationLevels.GetArray();
			bool[] array2 = this._contaminationsChangedLastTick.GetArray();
			float[] array3 = this._contaminationCandidates.GetArray();
			float[] array4 = this._lastTickContaminationCandidates.GetArray();
			ReadOnlyArray<byte> columnCounts = this._threadSafeWaterMap.ColumnCounts;
			ReadOnlyArray<ReadOnlyWaterColumn> waterColumns = this._threadSafeWaterMap.WaterColumns;
			ReadOnlyArray<byte> columnCounts2 = this._threadSafeColumnTerrainMap.ColumnCounts;
			ReadOnlyArray<ReadOnlyTerrainColumn> terrainColumns = this._threadSafeColumnTerrainMap.TerrainColumns;
			ReadOnlyArray<bool> contaminationBarriers = this._soilBarrierMap.ContaminationBarriers;
			ReadOnlyArray<bool> aboveMoistureBarriers = this._soilBarrierMap.AboveMoistureBarriers;
			soilContaminationSimulationTaskStarter.StartTask(array, array2, array3, array4, columnCounts, waterColumns, columnCounts2, terrainColumns, contaminationBarriers, aboveMoistureBarriers);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000360C File Offset: 0x0000180C
		public void BackwardCompatibleLoad()
		{
			int maxIndex = this._mapIndexService.MaxIndex;
			SingletonKey key = new SingletonKey("SoilPollutionSimulator");
			PropertyKey<PackedList<float>> key2 = new PropertyKey<PackedList<float>>("PollutionCandidates");
			PropertyKey<PackedList<float>> key3 = new PropertyKey<PackedList<float>>("PollutionLevels");
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(key, out objectLoader))
			{
				this._contaminationCandidates = this._tickOnlyArrayService.Create<float>(maxIndex);
				if (objectLoader.Has<PackedList<float>>(key2))
				{
					PackedList<float> packedList = objectLoader.Get<PackedList<float>>(key2, this._floatPackedListSerializer);
					this._mapIndexService.Unpack<float>(packedList, this._contaminationCandidates.GetSpan(), 1);
				}
				PackedList<float> packedList2 = objectLoader.Get<PackedList<float>>(key3, this._floatPackedListSerializer);
				this._mapIndexService.Unpack<float>(packedList2, this._contaminationLevels.GetSpan(), 1);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000036C7 File Offset: 0x000018C7
		public void AddAction(SoilContaminationSimulator.ActionType actionType, int value)
		{
			this._actions.Add(new SoilContaminationSimulator.Action(actionType, value));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000036DC File Offset: 0x000018DC
		public void ProcessActions()
		{
			foreach (SoilContaminationSimulator.Action action in this._actions)
			{
				this.ProcessAction(action);
			}
			this._actions.Clear();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000373C File Offset: 0x0000193C
		public void ProcessTerrainHeightChanges()
		{
			for (int i = 0; i < this._terrainHeightChanges.Count; i++)
			{
				this.UpdateContaminationFromHeightChange(this._terrainHeightChanges[i]);
			}
			this._terrainHeightChanges.Clear();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000377C File Offset: 0x0000197C
		public void Reset()
		{
			if (this._simulationController.ShouldResetSimulation)
			{
				this._contaminationCandidates.GetSpan().Clear();
				this._contaminationLevels.GetSpan().Clear();
				this._contaminationsChangedLastTick.GetSpan().Clear();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000037D0 File Offset: 0x000019D0
		public void ProcessAction(SoilContaminationSimulator.Action action)
		{
			switch (action.Type)
			{
			case SoilContaminationSimulator.ActionType.MaxTerrainThreadSafeColumnCountChanged:
				this.ResizeTerrainBasedArrays(action.Value);
				return;
			case SoilContaminationSimulator.ActionType.ColumnMovedUp:
				this.MoveColumn(action.Value, action.Value - this._verticalStride);
				return;
			case SoilContaminationSimulator.ActionType.ColumnMovedDown:
				this.MoveColumn(action.Value, action.Value + this._verticalStride);
				return;
			case SoilContaminationSimulator.ActionType.ColumnReset:
				this.ResetColumn(action.Value);
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003858 File Offset: 0x00001A58
		public unsafe void UpdateContaminationFromHeightChange(TerrainHeightChange terrainHeightChange)
		{
			Span<bool> span = this._contaminationsChangedLastTick.GetSpan();
			Span<float> span2 = this._contaminationLevels.GetSpan();
			Span<float> span3 = this._contaminationCandidates.GetSpan();
			bool setTerrain = terrainHeightChange.SetTerrain;
			int num = setTerrain ? (terrainHeightChange.To + 1) : terrainHeightChange.From;
			int index2D = this._mapIndexService.CellToIndex(terrainHeightChange.Coordinates);
			int num2;
			if (this._threadSafeColumnTerrainMap.TryGetIndexAtOrAboveCeiling(index2D, num, out num2))
			{
				*span[num2] = true;
				if (setTerrain)
				{
					int num3 = num - terrainHeightChange.From;
					float val = *span2[num2] - this._verticalCostModifier * (float)num3;
					*span2[num2] = (*span3[num2] = Math.Max(0f, val));
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003928 File Offset: 0x00001B28
		public void ResizeTerrainBasedArrays(int maxColumnCount)
		{
			int newSize = maxColumnCount * this._verticalStride;
			this._lastTickContaminationCandidates.Resize(newSize);
			this._contaminationsChangedLastTick.Resize(newSize);
			this._contaminationCandidates.Resize(newSize);
			this._contaminationLevels.Resize(newSize);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003970 File Offset: 0x00001B70
		public unsafe void MoveColumn(int target, int source)
		{
			Span<float> span = this._contaminationLevels.GetSpan();
			*span[target] = *span[source];
			Span<float> span2 = this._contaminationCandidates.GetSpan();
			*span2[target] = *span2[source];
			Span<float> span3 = this._lastTickContaminationCandidates.GetSpan();
			*span3[target] = *span3[source];
			*this._contaminationsChangedLastTick.GetSpan()[target] = true;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000039F0 File Offset: 0x00001BF0
		public unsafe void ResetColumn(int index3D)
		{
			*this._contaminationLevels.GetSpan()[index3D] = 0f;
			*this._contaminationCandidates.GetSpan()[index3D] = 0f;
			*this._lastTickContaminationCandidates.GetSpan()[index3D] = 0f;
			*this._contaminationsChangedLastTick.GetSpan()[index3D] = true;
		}

		// Token: 0x04000051 RID: 81
		public static readonly SingletonKey SoilContaminationSimulatorKey = new SingletonKey("SoilContaminationSimulator");

		// Token: 0x04000052 RID: 82
		public static readonly PropertyKey<int> SizeKey = new PropertyKey<int>("Size");

		// Token: 0x04000053 RID: 83
		public static readonly PropertyKey<PackedList<float>> ContaminationCandidatesKey = new PropertyKey<PackedList<float>>("ContaminationCandidates");

		// Token: 0x04000054 RID: 84
		public static readonly PropertyKey<PackedList<float>> ContaminationLevelsKey = new PropertyKey<PackedList<float>>("ContaminationLevels");

		// Token: 0x04000055 RID: 85
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000056 RID: 86
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000057 RID: 87
		public readonly ISpecService _specService;

		// Token: 0x04000058 RID: 88
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000059 RID: 89
		public readonly SoilBarrierMap _soilBarrierMap;

		// Token: 0x0400005A RID: 90
		public readonly FloatPackedListSerializer _floatPackedListSerializer;

		// Token: 0x0400005B RID: 91
		public readonly IThreadSafeColumnTerrainMap _threadSafeColumnTerrainMap;

		// Token: 0x0400005C RID: 92
		public readonly SoilContaminationSimulationTaskStarter _soilContaminationSimulationTaskStarter;

		// Token: 0x0400005D RID: 93
		public readonly SimulationController _simulationController;

		// Token: 0x0400005E RID: 94
		public readonly ITerrainService _terrainService;

		// Token: 0x0400005F RID: 95
		public readonly ITickableSingletonService _tickableSingletonService;

		// Token: 0x04000060 RID: 96
		public readonly TickOnlyArrayService _tickOnlyArrayService;

		// Token: 0x04000061 RID: 97
		public TickOnlyArray<float> _contaminationCandidates;

		// Token: 0x04000062 RID: 98
		public TickOnlyArray<float> _lastTickContaminationCandidates;

		// Token: 0x04000063 RID: 99
		public TickOnlyArray<float> _contaminationLevels;

		// Token: 0x04000064 RID: 100
		public TickOnlyArray<bool> _contaminationsChangedLastTick;

		// Token: 0x04000065 RID: 101
		public int _verticalStride;

		// Token: 0x04000066 RID: 102
		public float _verticalCostModifier;

		// Token: 0x04000067 RID: 103
		public readonly List<SoilContaminationSimulator.Action> _actions = new List<SoilContaminationSimulator.Action>();

		// Token: 0x04000068 RID: 104
		public readonly List<TerrainHeightChange> _terrainHeightChanges = new List<TerrainHeightChange>();

		// Token: 0x02000010 RID: 16
		public readonly struct Action
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000059 RID: 89 RVA: 0x00003AE2 File Offset: 0x00001CE2
			public SoilContaminationSimulator.ActionType Type { get; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600005A RID: 90 RVA: 0x00003AEA File Offset: 0x00001CEA
			public int Value { get; }

			// Token: 0x0600005B RID: 91 RVA: 0x00003AF2 File Offset: 0x00001CF2
			public Action(SoilContaminationSimulator.ActionType type, int value)
			{
				this.Type = type;
				this.Value = value;
			}
		}

		// Token: 0x02000011 RID: 17
		public enum ActionType
		{
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
