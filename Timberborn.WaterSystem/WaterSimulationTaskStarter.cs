using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.Multithreading;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000036 RID: 54
	public class WaterSimulationTaskStarter : ILoadableSingleton
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00005960 File Offset: 0x00003B60
		public WaterSimulationTaskStarter(IParallelizer parallelizer, MapIndexService mapIndexService, ISpecService specService, FlowLimitCalculator flowLimitCalculator, WaterFlowRetriever waterFlowRetriever, WaterDepthSetter waterDepthSetter, MutableWaterColumnRetriever mutableWaterColumnRetriever, ITickService tickService)
		{
			this._parallelizer = parallelizer;
			this._mapIndexService = mapIndexService;
			this._specService = specService;
			this._flowLimitCalculator = flowLimitCalculator;
			this._waterFlowRetriever = waterFlowRetriever;
			this._waterDepthSetter = waterDepthSetter;
			this._mutableWaterColumnRetriever = mutableWaterColumnRetriever;
			this._tickService = tickService;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000059B0 File Offset: 0x00003BB0
		public void Load()
		{
			this._mapSize = this._mapIndexService.TotalSize;
			this._stride = this._mapIndexService.Stride;
			this._verticalStride = this._mapIndexService.VerticalStride;
			this._neighborOffsets = new int[]
			{
				-this._stride,
				-1,
				this._stride,
				1
			}.ToImmutableArray<int>();
			WaterSimulatorSpec singleSpec = this._specService.GetSingleSpec<WaterSimulatorSpec>();
			this._deltaTime = this._tickService.TickIntervalInSeconds / (float)WaterSimulationTaskStarter.SubstepCount;
			this._overflowPressureFactor = singleSpec.OverflowPressureFactor;
			this._maxHardDamDecrease = singleSpec.MaxHardDamDecrease;
			this._hardDamOffset = singleSpec.HardDamOffset;
			this._softDamOffset = singleSpec.SoftDamOffset;
			this._waterSpillThreshold = singleSpec.WaterSpillThreshold;
			this._waterFlowFactor = singleSpec.WaterFlowFactor * this._deltaTime;
			this._flowChangeLimit = singleSpec.FlowChangeLimit;
			this._outflowBalancingScaler = singleSpec.OutflowBalancingScaler;
			this._fastEvaporationDepthThreshold = singleSpec.FastEvaporationDepthThreshold;
			this._fastEvaporationSpeed = singleSpec.FastEvaporationSpeed;
			this._normalEvaporationSpeed = singleSpec.NormalEvaporationSpeed;
			this._maxWaterContamination = singleSpec.MaxWaterContamination;
			this._diffusionOutflowLimit = (double)singleSpec.DiffusionOutflowLimit;
			this._diffusionDepthLimit = (double)singleSpec.DiffusionDepthLimit;
			this._diffusionRate = singleSpec.DiffusionRate;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005B00 File Offset: 0x00003D00
		public void Simulate(List<DirectedFlow>[] directedFlows, WaterFlow[] baseLevelFlows, WaterColumn[] waterColumns, ColumnOutflows[] outflows, float[] contaminationsBuffer, Diffusions[] baseLevelDiffusions, byte[] targetedDiffusionCount, List<TargetedDiffusion>[] targetedDiffusions, ReadOnlyArray<byte> waterColumnCounts, ReadOnlyArray<int> limitedDirections, ReadOnlyArray<float> limitedValues, ReadOnlyArray<sbyte> flowControllers, ReadOnlyArray<float> outflowLimits, ReadOnlyArray<float> evaporationModifiers, ReadOnlyList<ThreadSafeWaterSource> waterSources, ReadOnlyList<WaterChange> waterChanges)
		{
			WaterSimulationTaskStarter.SimulationParameters simulationParameters = new WaterSimulationTaskStarter.SimulationParameters(directedFlows, baseLevelFlows, waterColumns, outflows, contaminationsBuffer, baseLevelDiffusions, targetedDiffusionCount, targetedDiffusions, waterColumnCounts, limitedDirections, limitedValues, flowControllers, outflowLimits, evaporationModifiers, waterSources, waterChanges);
			ParallelizerHandle dependency = this.RunSimulationSubstep(simulationParameters);
			ParallelizerHandle dependency2 = this.RunSimulationSubstep(dependency, simulationParameters);
			this.ScheduleUpdateWaterChanges(dependency2, simulationParameters);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005B50 File Offset: 0x00003D50
		public ParallelizerHandle RunSimulationSubstep(in WaterSimulationTaskStarter.SimulationParameters parameters)
		{
			IParallelizer parallelizer = this._parallelizer;
			ClearBuffersTask clearBuffersTask = WaterSimulationTaskStarter.CreateClearBuffersTask(parameters);
			ParallelizerHandle dependency = parallelizer.Schedule<ClearBuffersTask>(clearBuffersTask);
			return this.ScheduleSimulationSteps(dependency, parameters);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005B7C File Offset: 0x00003D7C
		public ParallelizerHandle RunSimulationSubstep(ParallelizerHandle dependency, in WaterSimulationTaskStarter.SimulationParameters parameters)
		{
			IParallelizer parallelizer = this._parallelizer;
			ClearBuffersTask clearBuffersTask = WaterSimulationTaskStarter.CreateClearBuffersTask(parameters);
			ParallelizerHandle dependency2 = parallelizer.Schedule<ClearBuffersTask>(clearBuffersTask, dependency);
			return this.ScheduleSimulationSteps(dependency2, parameters);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005BA7 File Offset: 0x00003DA7
		public static ClearBuffersTask CreateClearBuffersTask(in WaterSimulationTaskStarter.SimulationParameters parameters)
		{
			return new ClearBuffersTask(parameters.ContaminationsBuffer, parameters.TargetedDiffusionCount, parameters.BaseLevelFlows, parameters.BaseLevelDiffusions);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public ParallelizerHandle ScheduleSimulationSteps(ParallelizerHandle dependency, in WaterSimulationTaskStarter.SimulationParameters parameters)
		{
			OutflowsUpdateTask outflowsUpdateTask = new OutflowsUpdateTask(this._flowLimitCalculator, this._waterFlowRetriever, parameters.DirectedFlows, parameters.BaseLevelFlows, parameters.WaterColumnCounts, new ReadOnlyArray<WaterColumn>(parameters.WaterColumns), parameters.LimitedDirections, parameters.LimitedValues, parameters.FlowControllers, parameters.OutflowLimits, new ReadOnlyArray<ColumnOutflows>(parameters.Outflows), this._mapSize.x, this._stride, this._verticalStride, this._deltaTime, this._overflowPressureFactor, this._maxHardDamDecrease, this._hardDamOffset, this._softDamOffset, this._waterSpillThreshold, this._waterFlowFactor, this._flowChangeLimit);
			WaterParametersUpdateTask waterParametersUpdateTask = new WaterParametersUpdateTask(this._waterDepthSetter, parameters.WaterColumns, parameters.Outflows, parameters.WaterColumnCounts, this._neighborOffsets, new ReadOnlyArray<WaterFlow>(parameters.BaseLevelFlows), parameters.EvaporationModifiers, new ReadOnlyArray<List<DirectedFlow>>(parameters.DirectedFlows), this._mapSize.x, this._stride, this._verticalStride, this._deltaTime, this._outflowBalancingScaler, this._fastEvaporationDepthThreshold, this._fastEvaporationSpeed, this._normalEvaporationSpeed);
			SimulateContaminationTask simulateContaminationTask = new SimulateContaminationTask(this._flowLimitCalculator, this._waterFlowRetriever, parameters.ContaminationsBuffer, parameters.BaseLevelDiffusions, parameters.TargetedDiffusionCount, parameters.TargetedDiffusions, parameters.WaterColumnCounts, new ReadOnlyArray<WaterColumn>(parameters.WaterColumns), new ReadOnlyArray<ColumnOutflows>(parameters.Outflows), parameters.LimitedValues, this._mapSize.x, this._stride, this._verticalStride, this._deltaTime, this._overflowPressureFactor, this._maxWaterContamination, this._diffusionOutflowLimit, this._diffusionDepthLimit);
			UpdateContaminationTask updateContaminationTask = new UpdateContaminationTask(parameters.WaterColumns, parameters.WaterColumnCounts, new ReadOnlyArray<float>(parameters.ContaminationsBuffer), new ReadOnlyArray<Diffusions>(parameters.BaseLevelDiffusions), new ReadOnlyArray<byte>(parameters.TargetedDiffusionCount), new ReadOnlyArray<List<TargetedDiffusion>>(parameters.TargetedDiffusions), this._mapSize.x, this._stride, this._verticalStride, this._deltaTime, this._maxWaterContamination, this._diffusionRate);
			UpdateWaterSourcesTask updateWaterSourcesTask = new UpdateWaterSourcesTask(this._mapIndexService, this._waterDepthSetter, this._mutableWaterColumnRetriever, parameters.WaterColumns, parameters.WaterColumnCounts, parameters.WaterSources, this._verticalStride, this._deltaTime, this._overflowPressureFactor, this._maxWaterContamination);
			return this._parallelizer.Schedule<OutflowsUpdateTask>(0, this._mapSize.y, 1, outflowsUpdateTask, dependency).ContinueWith<WaterParametersUpdateTask>(0, this._mapSize.y, 1, waterParametersUpdateTask).ContinueWith<SimulateContaminationTask>(0, this._mapSize.y, 1, simulateContaminationTask).ContinueWith<UpdateContaminationTask>(0, this._mapSize.y, 3, updateContaminationTask).ContinueWith<UpdateWaterSourcesTask>(updateWaterSourcesTask);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005E80 File Offset: 0x00004080
		public void ScheduleUpdateWaterChanges(ParallelizerHandle dependency, in WaterSimulationTaskStarter.SimulationParameters parameters)
		{
			UpdateWaterChangesTask updateWaterChangesTask = new UpdateWaterChangesTask(this._mapIndexService, this._mutableWaterColumnRetriever, parameters.WaterColumns, parameters.WaterColumnCounts, parameters.WaterChanges, this._verticalStride, this._overflowPressureFactor, this._maxWaterContamination);
			this._parallelizer.Schedule<UpdateWaterChangesTask>(updateWaterChangesTask, dependency);
		}

		// Token: 0x040000DC RID: 220
		public static readonly int SubstepCount = 2;

		// Token: 0x040000DD RID: 221
		public readonly IParallelizer _parallelizer;

		// Token: 0x040000DE RID: 222
		public readonly MapIndexService _mapIndexService;

		// Token: 0x040000DF RID: 223
		public readonly ISpecService _specService;

		// Token: 0x040000E0 RID: 224
		public readonly FlowLimitCalculator _flowLimitCalculator;

		// Token: 0x040000E1 RID: 225
		public readonly WaterFlowRetriever _waterFlowRetriever;

		// Token: 0x040000E2 RID: 226
		public readonly WaterDepthSetter _waterDepthSetter;

		// Token: 0x040000E3 RID: 227
		public readonly MutableWaterColumnRetriever _mutableWaterColumnRetriever;

		// Token: 0x040000E4 RID: 228
		public readonly ITickService _tickService;

		// Token: 0x040000E5 RID: 229
		public ImmutableArray<int> _neighborOffsets;

		// Token: 0x040000E6 RID: 230
		public Vector3Int _mapSize;

		// Token: 0x040000E7 RID: 231
		public int _stride;

		// Token: 0x040000E8 RID: 232
		public int _verticalStride;

		// Token: 0x040000E9 RID: 233
		public float _deltaTime;

		// Token: 0x040000EA RID: 234
		public float _overflowPressureFactor;

		// Token: 0x040000EB RID: 235
		public float _maxHardDamDecrease;

		// Token: 0x040000EC RID: 236
		public float _hardDamOffset;

		// Token: 0x040000ED RID: 237
		public float _softDamOffset;

		// Token: 0x040000EE RID: 238
		public float _waterSpillThreshold;

		// Token: 0x040000EF RID: 239
		public float _waterFlowFactor;

		// Token: 0x040000F0 RID: 240
		public float _flowChangeLimit;

		// Token: 0x040000F1 RID: 241
		public float _outflowBalancingScaler;

		// Token: 0x040000F2 RID: 242
		public float _fastEvaporationDepthThreshold;

		// Token: 0x040000F3 RID: 243
		public float _fastEvaporationSpeed;

		// Token: 0x040000F4 RID: 244
		public float _normalEvaporationSpeed;

		// Token: 0x040000F5 RID: 245
		public float _maxWaterContamination;

		// Token: 0x040000F6 RID: 246
		public double _diffusionOutflowLimit;

		// Token: 0x040000F7 RID: 247
		public double _diffusionDepthLimit;

		// Token: 0x040000F8 RID: 248
		public float _diffusionRate;

		// Token: 0x02000037 RID: 55
		public readonly struct SimulationParameters
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000113 RID: 275 RVA: 0x00005EDB File Offset: 0x000040DB
			public List<DirectedFlow>[] DirectedFlows { get; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000114 RID: 276 RVA: 0x00005EE3 File Offset: 0x000040E3
			public WaterFlow[] BaseLevelFlows { get; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000115 RID: 277 RVA: 0x00005EEB File Offset: 0x000040EB
			public WaterColumn[] WaterColumns { get; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000116 RID: 278 RVA: 0x00005EF3 File Offset: 0x000040F3
			public ColumnOutflows[] Outflows { get; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000117 RID: 279 RVA: 0x00005EFB File Offset: 0x000040FB
			public float[] ContaminationsBuffer { get; }

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000118 RID: 280 RVA: 0x00005F03 File Offset: 0x00004103
			public Diffusions[] BaseLevelDiffusions { get; }

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000119 RID: 281 RVA: 0x00005F0B File Offset: 0x0000410B
			public byte[] TargetedDiffusionCount { get; }

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x0600011A RID: 282 RVA: 0x00005F13 File Offset: 0x00004113
			public List<TargetedDiffusion>[] TargetedDiffusions { get; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x0600011B RID: 283 RVA: 0x00005F1B File Offset: 0x0000411B
			public ReadOnlyArray<byte> WaterColumnCounts { get; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x0600011C RID: 284 RVA: 0x00005F23 File Offset: 0x00004123
			public ReadOnlyArray<int> LimitedDirections { get; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x0600011D RID: 285 RVA: 0x00005F2B File Offset: 0x0000412B
			public ReadOnlyArray<float> LimitedValues { get; }

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x0600011E RID: 286 RVA: 0x00005F33 File Offset: 0x00004133
			public ReadOnlyArray<sbyte> FlowControllers { get; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x0600011F RID: 287 RVA: 0x00005F3B File Offset: 0x0000413B
			public ReadOnlyArray<float> OutflowLimits { get; }

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000120 RID: 288 RVA: 0x00005F43 File Offset: 0x00004143
			public ReadOnlyArray<float> EvaporationModifiers { get; }

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000121 RID: 289 RVA: 0x00005F4B File Offset: 0x0000414B
			public ReadOnlyList<ThreadSafeWaterSource> WaterSources { get; }

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000122 RID: 290 RVA: 0x00005F53 File Offset: 0x00004153
			public ReadOnlyList<WaterChange> WaterChanges { get; }

			// Token: 0x06000123 RID: 291 RVA: 0x00005F5C File Offset: 0x0000415C
			public SimulationParameters(List<DirectedFlow>[] directedFlows, WaterFlow[] baseLevelFlows, WaterColumn[] waterColumns, ColumnOutflows[] outflows, float[] contaminationsBuffer, Diffusions[] baseLevelDiffusions, byte[] targetedDiffusionCount, List<TargetedDiffusion>[] targetedDiffusions, ReadOnlyArray<byte> waterColumnCounts, ReadOnlyArray<int> limitedDirections, ReadOnlyArray<float> limitedValues, ReadOnlyArray<sbyte> flowControllers, ReadOnlyArray<float> outflowLimits, ReadOnlyArray<float> evaporationModifiers, ReadOnlyList<ThreadSafeWaterSource> waterSources, ReadOnlyList<WaterChange> waterChanges)
			{
				this.DirectedFlows = directedFlows;
				this.BaseLevelFlows = baseLevelFlows;
				this.WaterColumns = waterColumns;
				this.Outflows = outflows;
				this.ContaminationsBuffer = contaminationsBuffer;
				this.BaseLevelDiffusions = baseLevelDiffusions;
				this.TargetedDiffusionCount = targetedDiffusionCount;
				this.TargetedDiffusions = targetedDiffusions;
				this.WaterColumnCounts = waterColumnCounts;
				this.LimitedDirections = limitedDirections;
				this.LimitedValues = limitedValues;
				this.FlowControllers = flowControllers;
				this.OutflowLimits = outflowLimits;
				this.EvaporationModifiers = evaporationModifiers;
				this.WaterSources = waterSources;
				this.WaterChanges = waterChanges;
			}
		}
	}
}
