using System;
using Timberborn.Common;
using Timberborn.Multithreading;
using Timberborn.TerrainSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x02000008 RID: 8
	public readonly struct ContaminationCandidatesCountingTask : IParallelizerLoopTask
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000022A8 File Offset: 0x000004A8
		public ContaminationCandidatesCountingTask(WaterColumnRetriever waterColumnRetriever, CeilingRetriever ceilingRetriever, float[] contaminationCandidates, ReadOnlyArray<byte> waterColumnCounts, ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, ReadOnlyArray<byte> terrainColumnCounts, ReadOnlyArray<ReadOnlyTerrainColumn> terrainColumns, ReadOnlyArray<bool> contaminationBarriers, ReadOnlyArray<bool> aboveMoistureBarriers, ReadOnlyArray<float> lastTickContaminationCandidates, int xMapSize, int stride, int verticalStride, float maximumSoilContamination, float regularSpreadCost, float diagonalSpreadCost, float contaminationDecayRate, float contaminationSpreadingRate, float minimumWaterContamination, float contaminationScaler, float verticalCostModifier)
		{
			this = default(ContaminationCandidatesCountingTask);
			this._waterColumnRetriever = waterColumnRetriever;
			this._ceilingRetriever = ceilingRetriever;
			this._contaminationCandidates = contaminationCandidates;
			this._waterColumnCounts = waterColumnCounts;
			this._waterColumns = waterColumns;
			this._terrainColumnCounts = terrainColumnCounts;
			this._terrainColumns = terrainColumns;
			this._contaminationBarriers = contaminationBarriers;
			this._aboveMoistureBarriers = aboveMoistureBarriers;
			this._lastTickContaminationCandidates = lastTickContaminationCandidates;
			this._xMapSize = xMapSize;
			this._stride = stride;
			this._verticalStride = verticalStride;
			this._maximumSoilContamination = maximumSoilContamination;
			this._regularSpreadCost = regularSpreadCost;
			this._diagonalSpreadCost = diagonalSpreadCost;
			this._contaminationDecayRate = contaminationDecayRate;
			this._contaminationSpreadingRate = contaminationSpreadingRate;
			this._minimumWaterContamination = minimumWaterContamination;
			this._contaminationScaler = contaminationScaler;
			this._verticalCostModifier = verticalCostModifier;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002364 File Offset: 0x00000564
		public unsafe void Run(int y)
		{
			int num = (y + 1) * this._stride;
			for (int i = 0; i < this._xMapSize; i++)
			{
				int num2 = i + 1 + num;
				byte b = *this._terrainColumnCounts[num2];
				for (int j = 0; j < (int)b; j++)
				{
					int num3 = num2 + j * this._verticalStride;
					this._contaminationCandidates[num3] = this.GetContaminationCandidate(num2, num3);
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D0 File Offset: 0x000005D0
		public unsafe float GetContaminationCandidate(int index2D, int index3D)
		{
			ref readonly ReadOnlyTerrainColumn ptr = ref this._terrainColumns[index3D];
			int ceiling = ptr.Ceiling;
			int floor = ptr.Floor;
			int index = ceiling * this._verticalStride + index2D;
			if (*this._contaminationBarriers[index] != 0)
			{
				return 0f;
			}
			float num = 0f;
			int num2;
			if (this._waterColumnRetriever.TryGetColumnWithCeilingAtHeight(this._waterColumnCounts, this._waterColumns, this._verticalStride, index2D, floor, out num2))
			{
				ref readonly ReadOnlyWaterColumn ptr2 = ref this._waterColumns[num2];
				if (ptr2.WaterDepth + (float)ptr2.Floor >= (float)ptr2.Ceiling)
				{
					num = this.GetContaminationFromWaterBelow(num2, ceiling - floor - 1);
				}
			}
			int num3 = index2D - this._stride;
			int num4 = index2D - 1;
			int num5 = index2D + 1;
			int num6 = index2D + this._stride;
			bool aboveBarrier = *this._aboveMoistureBarriers[index] != 0;
			if (num < this._maximumSoilContamination)
			{
				float contaminationFromWater = this.GetContaminationFromWater(num3, ceiling, floor, aboveBarrier);
				if (contaminationFromWater > num)
				{
					num = contaminationFromWater;
				}
			}
			if (num < this._maximumSoilContamination)
			{
				float contaminationFromWater2 = this.GetContaminationFromWater(num4, ceiling, floor, aboveBarrier);
				if (contaminationFromWater2 > num)
				{
					num = contaminationFromWater2;
				}
			}
			if (num < this._maximumSoilContamination)
			{
				float contaminationFromWater3 = this.GetContaminationFromWater(num6, ceiling, floor, aboveBarrier);
				if (contaminationFromWater3 > num)
				{
					num = contaminationFromWater3;
				}
			}
			if (num < this._maximumSoilContamination)
			{
				float contaminationFromWater4 = this.GetContaminationFromWater(num5, ceiling, floor, aboveBarrier);
				if (contaminationFromWater4 > num)
				{
					num = contaminationFromWater4;
				}
			}
			float num7 = 0f;
			if (num < this._maximumSoilContamination)
			{
				int neighborIndex2D = index2D - this._stride - 1;
				int neighborIndex2D2 = index2D - this._stride + 1;
				int neighborIndex2D3 = index2D + this._stride - 1;
				int neighborIndex2D4 = index2D + this._stride + 1;
				num7 = this.GetContaminationFromNeighbor(num3, floor, ceiling, num7, this._regularSpreadCost);
				num7 = this.GetContaminationFromNeighbor(num4, floor, ceiling, num7, this._regularSpreadCost);
				num7 = this.GetContaminationFromNeighbor(num5, floor, ceiling, num7, this._regularSpreadCost);
				num7 = this.GetContaminationFromNeighbor(num6, floor, ceiling, num7, this._regularSpreadCost);
				num7 = this.GetContaminationFromNeighbor(neighborIndex2D, floor, ceiling, num7, this._diagonalSpreadCost);
				num7 = this.GetContaminationFromNeighbor(neighborIndex2D2, floor, ceiling, num7, this._diagonalSpreadCost);
				num7 = this.GetContaminationFromNeighbor(neighborIndex2D3, floor, ceiling, num7, this._diagonalSpreadCost);
				num7 = this.GetContaminationFromNeighbor(neighborIndex2D4, floor, ceiling, num7, this._diagonalSpreadCost);
			}
			float num8 = *this._lastTickContaminationCandidates[index3D];
			float num9 = num8 - this._contaminationDecayRate;
			if (num9 < 0f)
			{
				num9 = 0f;
			}
			if (num > num9 && num >= num7)
			{
				float num10 = num8 + this._contaminationSpreadingRate;
				if (num <= num10)
				{
					return num;
				}
				return num10;
			}
			else
			{
				if (num7 > num9)
				{
					return num7;
				}
				return num9;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002658 File Offset: 0x00000858
		public float GetContaminationFromWaterBelow(int waterIndex3D, int heightDifference)
		{
			float num = this._waterColumns[waterIndex3D].Contamination - this._minimumWaterContamination;
			if (num < 0f)
			{
				return 0f;
			}
			float num2 = num * this._contaminationScaler;
			float num3 = (float)heightDifference * this._verticalCostModifier;
			return num2 - num3;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026A0 File Offset: 0x000008A0
		public unsafe float GetContaminationFromWater(int neighborIndex, int originTerrainHeight, int originTerrainFloor, bool aboveBarrier)
		{
			int index;
			if (!this._waterColumnRetriever.TryGetTopContaminatedColumn(this._waterColumnCounts, this._waterColumns, this._verticalStride, originTerrainHeight, neighborIndex, out index))
			{
				return 0f;
			}
			ref readonly ReadOnlyWaterColumn ptr = ref this._waterColumns[index];
			float num = ptr.Contamination - this._minimumWaterContamination;
			if (num < 0f)
			{
				return 0f;
			}
			float waterDepth = ptr.WaterDepth;
			byte floor = ptr.Floor;
			int num2 = (waterDepth > 0f) ? Mathf.CeilToInt((float)floor + waterDepth) : 0;
			if (num2 <= originTerrainFloor)
			{
				return 0f;
			}
			if ((int)floor >= originTerrainHeight)
			{
				if (aboveBarrier)
				{
					return 0f;
				}
				int ceilingAtOrBelowHeight = this._ceilingRetriever.GetCeilingAtOrBelowHeight(this._terrainColumnCounts, this._terrainColumns, this._verticalStride, neighborIndex, (int)floor);
				int index2 = neighborIndex + this._verticalStride * ceilingAtOrBelowHeight;
				if (*this._aboveMoistureBarriers[index2] != 0)
				{
					return 0f;
				}
			}
			float num3 = num * this._contaminationScaler;
			int num4 = originTerrainHeight - num2;
			if (num4 < 0)
			{
				return num3;
			}
			float num5 = (float)num4 * this._verticalCostModifier;
			return num3 - num5;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000027B0 File Offset: 0x000009B0
		public unsafe float GetContaminationFromNeighbor(int neighborIndex2D, int floor, int height, float currentContamination, float cost)
		{
			byte b = *this._terrainColumnCounts[neighborIndex2D];
			for (int i = 0; i < (int)b; i++)
			{
				int num = i * this._verticalStride + neighborIndex2D;
				ref readonly ReadOnlyTerrainColumn ptr = ref this._terrainColumns[num];
				int ceiling = ptr.Ceiling;
				if (ceiling >= floor)
				{
					if (ptr.Floor > height)
					{
						break;
					}
					float contaminationFromNeighbor = this.GetContaminationFromNeighbor(num, ceiling, height, cost);
					if (contaminationFromNeighbor > currentContamination)
					{
						currentContamination = contaminationFromNeighbor;
					}
				}
			}
			return currentContamination;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002820 File Offset: 0x00000A20
		public unsafe float GetContaminationFromNeighbor(int neighborIndex3D, int neighborTerrainHeight, int originTerrainHeight, float spreadCost)
		{
			int num = originTerrainHeight - neighborTerrainHeight;
			if (num < 0)
			{
				num = 0;
			}
			float num2 = (float)num * this._verticalCostModifier;
			return *this._lastTickContaminationCandidates[neighborIndex3D] - num2 - spreadCost;
		}

		// Token: 0x0400000D RID: 13
		public readonly WaterColumnRetriever _waterColumnRetriever;

		// Token: 0x0400000E RID: 14
		public readonly CeilingRetriever _ceilingRetriever;

		// Token: 0x0400000F RID: 15
		public readonly float[] _contaminationCandidates;

		// Token: 0x04000010 RID: 16
		public readonly ReadOnlyArray<byte> _waterColumnCounts;

		// Token: 0x04000011 RID: 17
		public readonly ReadOnlyArray<ReadOnlyWaterColumn> _waterColumns;

		// Token: 0x04000012 RID: 18
		public readonly ReadOnlyArray<byte> _terrainColumnCounts;

		// Token: 0x04000013 RID: 19
		public readonly ReadOnlyArray<ReadOnlyTerrainColumn> _terrainColumns;

		// Token: 0x04000014 RID: 20
		public readonly ReadOnlyArray<bool> _contaminationBarriers;

		// Token: 0x04000015 RID: 21
		public readonly ReadOnlyArray<bool> _aboveMoistureBarriers;

		// Token: 0x04000016 RID: 22
		public readonly ReadOnlyArray<float> _lastTickContaminationCandidates;

		// Token: 0x04000017 RID: 23
		public readonly int _xMapSize;

		// Token: 0x04000018 RID: 24
		public readonly int _stride;

		// Token: 0x04000019 RID: 25
		public readonly int _verticalStride;

		// Token: 0x0400001A RID: 26
		public readonly float _maximumSoilContamination;

		// Token: 0x0400001B RID: 27
		public readonly float _regularSpreadCost;

		// Token: 0x0400001C RID: 28
		public readonly float _diagonalSpreadCost;

		// Token: 0x0400001D RID: 29
		public readonly float _contaminationDecayRate;

		// Token: 0x0400001E RID: 30
		public readonly float _contaminationSpreadingRate;

		// Token: 0x0400001F RID: 31
		public readonly float _minimumWaterContamination;

		// Token: 0x04000020 RID: 32
		public readonly float _contaminationScaler;

		// Token: 0x04000021 RID: 33
		public readonly float _verticalCostModifier;
	}
}
