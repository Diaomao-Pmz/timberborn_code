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

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000011 RID: 17
	public class SoilMoistureSimulationTaskStarter : ILoadableSingleton
	{
		// Token: 0x06000064 RID: 100 RVA: 0x0000350D File Offset: 0x0000170D
		public SoilMoistureSimulationTaskStarter(IParallelizer parallelizer, MapIndexService mapIndexService, ISpecService specService, WaterColumnRetriever waterColumnRetriever, CeilingRetriever ceilingRetriever, ITickService tickService)
		{
			this._parallelizer = parallelizer;
			this._mapIndexService = mapIndexService;
			this._specService = specService;
			this._waterColumnRetriever = waterColumnRetriever;
			this._ceilingRetriever = ceilingRetriever;
			this._tickService = tickService;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003544 File Offset: 0x00001744
		public void Load()
		{
			this._mapSize = this._mapIndexService.TerrainSize;
			this._stride = this._mapIndexService.Stride;
			this._verticalStride = this._mapIndexService.VerticalStride;
			SoilMoistureSimulatorSpec singleSpec = this._specService.GetSingleSpec<SoilMoistureSimulatorSpec>();
			float tickIntervalInSeconds = this._tickService.TickIntervalInSeconds;
			this._moistureDecayRate = singleSpec.MoistureDecayRate * tickIntervalInSeconds;
			this._moistureSpreadingRate = singleSpec.MoistureSpreadingRate * tickIntervalInSeconds;
			this._minimumWaterContamination = singleSpec.MinimumWaterContamination;
			this._verticalSpreadCostMultiplier = singleSpec.VerticalSpreadCostMultiplier;
			this._waterContaminationScaler = 1f / singleSpec.MaximumWaterContamination;
			this._maxClusterSaturation = singleSpec.MaxClusterSaturation;
			this.InitializeEvaporationModifiers(singleSpec);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000035F8 File Offset: 0x000017F8
		public void StartTask(byte[] wateredNeighbours, byte[] clusterSaturations, float[] moistureLevels, bool[] moistureLevelsChangedLastTick, float[] lastTickMoistureLevels, float[] evaporationModifiers, in ReadOnlyArray<bool> fullMoistureBarriers, in ReadOnlyArray<bool> aboveMoistureBarriers, in ReadOnlyArray<byte> waterColumnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, in ReadOnlyArray<byte> terrainColumnCounts, in ReadOnlyArray<ReadOnlyTerrainColumn> terrainColumns)
		{
			MoistureDataPreparationTask moistureDataPreparationTask = new MoistureDataPreparationTask(moistureLevels, lastTickMoistureLevels, moistureLevelsChangedLastTick);
			WateredNeighborsCountingTask wateredNeighborsCountingTask = new WateredNeighborsCountingTask(wateredNeighbours, ref waterColumnCounts, ref waterColumns, this._stride, this._verticalStride, this._mapSize.x);
			ReadOnlyArray<byte> readOnlyArray = new ReadOnlyArray<byte>(wateredNeighbours);
			ClusterSaturationCalculationTask clusterSaturationCalculationTask = new ClusterSaturationCalculationTask(clusterSaturations, ref readOnlyArray, ref waterColumnCounts, ref waterColumns, this._maxClusterSaturation, this._mapSize.x, this._stride, this._verticalStride);
			readOnlyArray = new ReadOnlyArray<byte>(clusterSaturations);
			ReadOnlyArray<float> readOnlyArray2 = new ReadOnlyArray<float>(this._saturationToEvaporationMap);
			WaterEvaporationCalculationTask waterEvaporationCalculationTask = new WaterEvaporationCalculationTask(evaporationModifiers, ref waterColumnCounts, ref readOnlyArray, ref readOnlyArray2, this._mapSize.x, this._stride, this._verticalStride);
			WaterColumnRetriever waterColumnRetriever = this._waterColumnRetriever;
			CeilingRetriever ceilingRetriever = this._ceilingRetriever;
			readOnlyArray2 = new ReadOnlyArray<float>(lastTickMoistureLevels);
			readOnlyArray = new ReadOnlyArray<byte>(clusterSaturations);
			MoistureCalculationTask moistureCalculationTask = new MoistureCalculationTask(waterColumnRetriever, ceilingRetriever, moistureLevels, moistureLevelsChangedLastTick, ref waterColumnCounts, ref waterColumns, ref terrainColumnCounts, ref terrainColumns, ref readOnlyArray2, ref fullMoistureBarriers, ref aboveMoistureBarriers, ref readOnlyArray, this._mapSize.x, this._stride, this._verticalStride, this._moistureDecayRate, this._moistureSpreadingRate, this._minimumWaterContamination, this._verticalSpreadCostMultiplier, this._waterContaminationScaler);
			this._parallelizer.Schedule<MoistureDataPreparationTask>(moistureDataPreparationTask).ContinueWith<WateredNeighborsCountingTask>(0, this._mapSize.y, 5, wateredNeighborsCountingTask).ContinueWith<ClusterSaturationCalculationTask>(0, this._mapSize.y, 5, clusterSaturationCalculationTask).ContinueWith<WaterEvaporationCalculationTask>(0, this._mapSize.y, 10, waterEvaporationCalculationTask).ContinueWith<MoistureCalculationTask>(0, this._mapSize.y, 1, moistureCalculationTask);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000377C File Offset: 0x0000197C
		public void InitializeEvaporationModifiers(SoilMoistureSimulatorSpec spec)
		{
			int maxEvaporationSaturation = spec.MaxEvaporationSaturation;
			this._saturationToEvaporationMap = new float[maxEvaporationSaturation];
			for (int i = 0; i < maxEvaporationSaturation; i++)
			{
				int num = maxEvaporationSaturation - i;
				float num2 = spec.QuadraticEvaporationCoefficient * (float)num * (float)num + spec.LinearQuadraticCoefficient * (float)num + spec.ConstantQuadraticCoefficient;
				this._saturationToEvaporationMap[i] = num2;
			}
		}

		// Token: 0x0400003F RID: 63
		public readonly IParallelizer _parallelizer;

		// Token: 0x04000040 RID: 64
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000041 RID: 65
		public readonly ISpecService _specService;

		// Token: 0x04000042 RID: 66
		public readonly WaterColumnRetriever _waterColumnRetriever;

		// Token: 0x04000043 RID: 67
		public readonly CeilingRetriever _ceilingRetriever;

		// Token: 0x04000044 RID: 68
		public readonly ITickService _tickService;

		// Token: 0x04000045 RID: 69
		public Vector3Int _mapSize;

		// Token: 0x04000046 RID: 70
		public int _stride;

		// Token: 0x04000047 RID: 71
		public int _verticalStride;

		// Token: 0x04000048 RID: 72
		public float _moistureDecayRate;

		// Token: 0x04000049 RID: 73
		public float _moistureSpreadingRate;

		// Token: 0x0400004A RID: 74
		public float _minimumWaterContamination;

		// Token: 0x0400004B RID: 75
		public int _verticalSpreadCostMultiplier;

		// Token: 0x0400004C RID: 76
		public float _waterContaminationScaler;

		// Token: 0x0400004D RID: 77
		public int _maxClusterSaturation;

		// Token: 0x0400004E RID: 78
		public float[] _saturationToEvaporationMap;
	}
}
