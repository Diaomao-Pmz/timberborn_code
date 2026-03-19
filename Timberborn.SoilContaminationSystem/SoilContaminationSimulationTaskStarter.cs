using System;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.Multithreading;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x0200000E RID: 14
	public class SoilContaminationSimulationTaskStarter : ILoadableSingleton
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002FF7 File Offset: 0x000011F7
		public SoilContaminationSimulationTaskStarter(IParallelizer parallelizer, MapIndexService mapIndexService, ISpecService specService, WaterColumnRetriever waterColumnRetriever, CeilingRetriever ceilingRetriever, ITickService tickService)
		{
			this._parallelizer = parallelizer;
			this._mapIndexService = mapIndexService;
			this._specService = specService;
			this._waterColumnRetriever = waterColumnRetriever;
			this._ceilingRetriever = ceilingRetriever;
			this._tickService = tickService;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000302C File Offset: 0x0000122C
		public void Load()
		{
			this._mapSize = this._mapIndexService.TerrainSize;
			this._stride = this._mapIndexService.Stride;
			this._verticalStride = this._mapIndexService.VerticalStride;
			SoilContaminationSimulatorSpec singleSpec = this._specService.GetSingleSpec<SoilContaminationSimulatorSpec>();
			float tickIntervalInSeconds = this._tickService.TickIntervalInSeconds;
			this._maximumSoilContamination = 1f - singleSpec.ContaminationThreshold;
			this._regularSpreadCost = 1f / (float)singleSpec.MaxRangeFromSource;
			this._diagonalSpreadCost = Mathf.Sqrt(2f) / (float)singleSpec.MaxRangeFromSource;
			this._contaminationDecayRate = singleSpec.ContaminationDecayRate * tickIntervalInSeconds;
			this._contaminationSpreadingRate = singleSpec.ContaminationSpreadingRate * tickIntervalInSeconds;
			this._minimumWaterContamination = singleSpec.MinimumWaterContamination;
			this._contaminationScaler = 1f / (1f - singleSpec.MinimumWaterContamination);
			this._verticalCostModifier = singleSpec.VerticalSpreadCostMultiplier / (float)singleSpec.MaxRangeFromSource;
			this._contaminationThreshold = singleSpec.ContaminationThreshold;
			this._contaminationPositiveEqualizationRate = singleSpec.ContaminationPositiveEqualizationRate * tickIntervalInSeconds;
			this._contaminationNegativeEqualizationRate = singleSpec.ContaminationNegativeEqualizationRate * tickIntervalInSeconds;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003140 File Offset: 0x00001340
		public void StartTask(float[] contaminationLevels, bool[] contaminationsChangedLastTick, float[] contaminationCandidates, float[] lastTickContaminationCandidates, in ReadOnlyArray<byte> waterColumnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, in ReadOnlyArray<byte> terrainColumnCounts, in ReadOnlyArray<ReadOnlyTerrainColumn> terrainColumns, in ReadOnlyArray<bool> contaminationBarriers, in ReadOnlyArray<bool> aboveMoistureBarriers)
		{
			ContaminationDataPreparationTask contaminationDataPreparationTask = new ContaminationDataPreparationTask(contaminationCandidates, lastTickContaminationCandidates, contaminationsChangedLastTick);
			ContaminationCandidatesCountingTask contaminationCandidatesCountingTask = new ContaminationCandidatesCountingTask(this._waterColumnRetriever, this._ceilingRetriever, contaminationCandidates, waterColumnCounts, waterColumns, terrainColumnCounts, terrainColumns, contaminationBarriers, aboveMoistureBarriers, new ReadOnlyArray<float>(lastTickContaminationCandidates), this._mapSize.x, this._stride, this._verticalStride, this._maximumSoilContamination, this._regularSpreadCost, this._diagonalSpreadCost, this._contaminationDecayRate, this._contaminationSpreadingRate, this._minimumWaterContamination, this._contaminationScaler, this._verticalCostModifier);
			ContaminationsUpdateTask contaminationsUpdateTask = new ContaminationsUpdateTask(contaminationLevels, contaminationsChangedLastTick, terrainColumnCounts, new ReadOnlyArray<float>(contaminationCandidates), this._mapSize.x, this._stride, this._verticalStride, this._contaminationThreshold, this._contaminationPositiveEqualizationRate, this._contaminationNegativeEqualizationRate);
			this._parallelizer.Schedule<ContaminationDataPreparationTask>(contaminationDataPreparationTask).ContinueWith<ContaminationCandidatesCountingTask>(0, this._mapSize.y, 1, contaminationCandidatesCountingTask).ContinueWith<ContaminationsUpdateTask>(0, this._mapSize.y, 5, contaminationsUpdateTask);
		}

		// Token: 0x0400003D RID: 61
		public readonly IParallelizer _parallelizer;

		// Token: 0x0400003E RID: 62
		public readonly MapIndexService _mapIndexService;

		// Token: 0x0400003F RID: 63
		public readonly ISpecService _specService;

		// Token: 0x04000040 RID: 64
		public readonly WaterColumnRetriever _waterColumnRetriever;

		// Token: 0x04000041 RID: 65
		public readonly CeilingRetriever _ceilingRetriever;

		// Token: 0x04000042 RID: 66
		public readonly ITickService _tickService;

		// Token: 0x04000043 RID: 67
		public Vector3Int _mapSize;

		// Token: 0x04000044 RID: 68
		public int _stride;

		// Token: 0x04000045 RID: 69
		public int _verticalStride;

		// Token: 0x04000046 RID: 70
		public float _maximumSoilContamination;

		// Token: 0x04000047 RID: 71
		public float _regularSpreadCost;

		// Token: 0x04000048 RID: 72
		public float _diagonalSpreadCost;

		// Token: 0x04000049 RID: 73
		public float _contaminationDecayRate;

		// Token: 0x0400004A RID: 74
		public float _contaminationSpreadingRate;

		// Token: 0x0400004B RID: 75
		public float _minimumWaterContamination;

		// Token: 0x0400004C RID: 76
		public float _contaminationScaler;

		// Token: 0x0400004D RID: 77
		public float _verticalCostModifier;

		// Token: 0x0400004E RID: 78
		public float _contaminationThreshold;

		// Token: 0x0400004F RID: 79
		public float _contaminationPositiveEqualizationRate;

		// Token: 0x04000050 RID: 80
		public float _contaminationNegativeEqualizationRate;
	}
}
