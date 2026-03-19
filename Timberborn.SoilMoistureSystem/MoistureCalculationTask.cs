using System;
using Timberborn.Common;
using Timberborn.Multithreading;
using Timberborn.TerrainSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x0200000D RID: 13
	public readonly struct MoistureCalculationTask : IParallelizerLoopTask
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000280C File Offset: 0x00000A0C
		public MoistureCalculationTask(WaterColumnRetriever waterColumnRetriever, CeilingRetriever ceilingRetriever, float[] moistureLevels, bool[] moistureLevelsChangedLastTick, in ReadOnlyArray<byte> waterColumnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, in ReadOnlyArray<byte> terrainColumnCounts, in ReadOnlyArray<ReadOnlyTerrainColumn> terrainColumns, in ReadOnlyArray<float> lastTickMoistureLevels, in ReadOnlyArray<bool> fullMoistureBarriers, in ReadOnlyArray<bool> aboveMoistureBarriers, in ReadOnlyArray<byte> clusterSaturation, int xMapSize, int stride, int verticalStride, float moistureDecayRate, float moistureSpreadingRate, float minimumWaterContamination, int verticalSpreadCostMultiplier, float waterContaminationScaler)
		{
			this._waterColumnRetriever = waterColumnRetriever;
			this._ceilingRetriever = ceilingRetriever;
			this._moistureLevels = moistureLevels;
			this._moistureLevelsChangedLastTick = moistureLevelsChangedLastTick;
			this._waterColumnCounts = waterColumnCounts;
			this._waterColumns = waterColumns;
			this._terrainColumnCounts = terrainColumnCounts;
			this._terrainColumns = terrainColumns;
			this._lastTickMoistureLevels = lastTickMoistureLevels;
			this._fullMoistureBarriers = fullMoistureBarriers;
			this._aboveMoistureBarriers = aboveMoistureBarriers;
			this._clusterSaturation = clusterSaturation;
			this._xMapSize = xMapSize;
			this._stride = stride;
			this._verticalStride = verticalStride;
			this._moistureDecayRate = moistureDecayRate;
			this._moistureSpreadingRate = moistureSpreadingRate;
			this._minimumWaterContamination = minimumWaterContamination;
			this._verticalSpreadCostMultiplier = verticalSpreadCostMultiplier;
			this._waterContaminationScaler = waterContaminationScaler;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028E0 File Offset: 0x00000AE0
		public unsafe void Run(int y)
		{
			int num = (y + 1) * this._stride;
			for (int i = 0; i < this._xMapSize; i++)
			{
				int num2 = i + 1 + num;
				byte b = *this._terrainColumnCounts[num2];
				for (int j = 0; j < (int)b; j++)
				{
					int num3 = j * this._verticalStride + num2;
					float num4 = this.CalculateMoistureForCell(num2, num3);
					this._moistureLevels[num3] = num4;
					if (num4 != *this._lastTickMoistureLevels[num3])
					{
						this._moistureLevelsChangedLastTick[num3] = true;
					}
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000296C File Offset: 0x00000B6C
		public unsafe float CalculateMoistureForCell(int index2D, int index3D)
		{
			ref readonly ReadOnlyTerrainColumn ptr = ref this._terrainColumns[index3D];
			int ceiling = ptr.Ceiling;
			int floor = ptr.Floor;
			int index = ceiling * this._verticalStride + index2D;
			if (*this._fullMoistureBarriers[index] != 0)
			{
				return 0f;
			}
			int num;
			bool flag = this._waterColumnRetriever.TryGetColumnWithFloorAtHeight(this._waterColumnCounts, this._waterColumns, this._verticalStride, index2D, ceiling, out num);
			bool flag2 = *this._aboveMoistureBarriers[index] != 0;
			if (flag)
			{
				ref readonly ReadOnlyWaterColumn ptr2 = ref this._waterColumns[num];
				if (ptr2.WaterDepth > 0f && ptr2.Contamination <= this._minimumWaterContamination && !flag2)
				{
					return (float)((int)this.GetInitialMoisturizerRange(num));
				}
			}
			float num2 = 0f;
			int num3;
			if (this._waterColumnRetriever.TryGetColumnWithCeilingAtHeight(this._waterColumnCounts, this._waterColumns, this._verticalStride, index2D, floor, out num3))
			{
				ref readonly ReadOnlyWaterColumn ptr3 = ref this._waterColumns[num3];
				if (ptr3.WaterDepth + (float)ptr3.Floor >= (float)ptr3.Ceiling)
				{
					num2 = (float)this.GetMoisture(num3, ceiling - floor - 1);
				}
			}
			int num4 = index2D - this._stride;
			int num5 = index2D - 1;
			int num6 = index2D + 1;
			int num7 = index2D + this._stride;
			if (num2 < 16f)
			{
				int moistureFromWater = this.GetMoistureFromWater(num4, ceiling, floor, flag2);
				if ((float)moistureFromWater > num2)
				{
					num2 = (float)moistureFromWater;
				}
			}
			if (num2 < 16f)
			{
				int moistureFromWater2 = this.GetMoistureFromWater(num5, ceiling, floor, flag2);
				if ((float)moistureFromWater2 > num2)
				{
					num2 = (float)moistureFromWater2;
				}
			}
			if (num2 < 16f)
			{
				int moistureFromWater3 = this.GetMoistureFromWater(num7, ceiling, floor, flag2);
				if ((float)moistureFromWater3 > num2)
				{
					num2 = (float)moistureFromWater3;
				}
			}
			if (num2 < 16f)
			{
				int moistureFromWater4 = this.GetMoistureFromWater(num6, ceiling, floor, flag2);
				if ((float)moistureFromWater4 > num2)
				{
					num2 = (float)moistureFromWater4;
				}
			}
			float num8 = 0f;
			if (num2 < 16f)
			{
				int neighborIndex2D = index2D - this._stride - 1;
				int neighborIndex2D2 = index2D - this._stride + 1;
				int neighborIndex2D3 = index2D + this._stride - 1;
				int neighborIndex2D4 = index2D + this._stride + 1;
				num8 = this.GetMoistureFromNeighbor(num4, floor, ceiling, num8, 1f);
				num8 = this.GetMoistureFromNeighbor(num5, floor, ceiling, num8, 1f);
				num8 = this.GetMoistureFromNeighbor(num6, floor, ceiling, num8, 1f);
				num8 = this.GetMoistureFromNeighbor(num7, floor, ceiling, num8, 1f);
				num8 = this.GetMoistureFromNeighbor(neighborIndex2D, floor, ceiling, num8, 1.414f);
				num8 = this.GetMoistureFromNeighbor(neighborIndex2D2, floor, ceiling, num8, 1.414f);
				num8 = this.GetMoistureFromNeighbor(neighborIndex2D3, floor, ceiling, num8, 1.414f);
				num8 = this.GetMoistureFromNeighbor(neighborIndex2D4, floor, ceiling, num8, 1.414f);
			}
			float num9 = *this._lastTickMoistureLevels[index3D];
			float num10 = num9 - this._moistureDecayRate;
			if (num10 < 0f)
			{
				num10 = 0f;
			}
			float num11 = num10;
			if (num2 > num10 && num2 >= num8)
			{
				float num12 = num9 + this._moistureSpreadingRate;
				num11 = ((num2 > num12) ? num12 : num2);
			}
			else if (num8 > num10)
			{
				num11 = num8;
			}
			float num13 = flag ? this._waterColumns[num].Contamination : 0f;
			float num14 = num11 * (1f - num13);
			if (num14 < MoistureCalculationTask.MinimumMoisture)
			{
				return 0f;
			}
			return num14;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public unsafe int GetMoistureFromWater(int neighborIndex, int originTerrainHeight, int originTerrainFloor, bool aboveBarrier)
		{
			int num;
			if (!this._waterColumnRetriever.TryGetTopWateredColumn(this._waterColumnCounts, this._waterColumns, this._verticalStride, originTerrainHeight, neighborIndex, out num))
			{
				return 0;
			}
			ref readonly ReadOnlyWaterColumn ptr = ref this._waterColumns[num];
			float waterDepth = ptr.WaterDepth;
			byte floor = ptr.Floor;
			int num2 = (waterDepth > 0f) ? Mathf.CeilToInt((float)floor + waterDepth) : 0;
			if (num2 <= originTerrainFloor)
			{
				return 0;
			}
			if ((int)floor >= originTerrainHeight)
			{
				if (aboveBarrier)
				{
					return 0;
				}
				int ceilingAtOrBelowHeight = this._ceilingRetriever.GetCeilingAtOrBelowHeight(this._terrainColumnCounts, this._terrainColumns, this._verticalStride, neighborIndex, (int)floor);
				int index = neighborIndex + this._verticalStride * ceilingAtOrBelowHeight;
				if (*this._aboveMoistureBarriers[index] != 0)
				{
					return 0;
				}
			}
			int num3 = originTerrainHeight - num2;
			if (num3 < 0)
			{
				num3 = 0;
			}
			return this.GetMoisture(num, num3);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D6A File Offset: 0x00000F6A
		public int GetMoisture(int wateredNeighborIndex3D, int heightDifference)
		{
			return this.GetMoisture(wateredNeighborIndex3D) - heightDifference * this._verticalSpreadCostMultiplier;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D7C File Offset: 0x00000F7C
		public int GetMoisture(int index3D)
		{
			float contamination = this._waterColumns[index3D].Contamination;
			if (contamination < this._minimumWaterContamination)
			{
				return (int)this.GetInitialMoisturizerRange(index3D);
			}
			float num = contamination * this._waterContaminationScaler;
			if (num >= 1f)
			{
				return 0;
			}
			return (int)(this.GetInitialMoisturizerRange(index3D) * (1f - num));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public unsafe float GetInitialMoisturizerRange(int index3D)
		{
			return 2f * (float)(*this._clusterSaturation[index3D]);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public unsafe float GetMoistureFromNeighbor(int neighborIndex2D, int floor, int height, float currentMoisture, float cost)
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
					float moistureFromNeighbor = this.GetMoistureFromNeighbor(neighborIndex2D, num, ceiling, height, cost);
					if (moistureFromNeighbor > currentMoisture)
					{
						currentMoisture = moistureFromNeighbor;
					}
				}
			}
			return currentMoisture;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E58 File Offset: 0x00001058
		public unsafe float GetMoistureFromNeighbor(int neighborIndex2D, int neighborColumnIndex3D, int neighborTerrainHeight, int originTerrainHeight, float spreadCost)
		{
			float num = *this._lastTickMoistureLevels[neighborColumnIndex3D];
			if (num == 0f)
			{
				return 0f;
			}
			int num2 = originTerrainHeight - neighborTerrainHeight;
			if (num2 < 0)
			{
				return num - spreadCost;
			}
			int num3 = (int)Math.Ceiling((double)this._waterColumnRetriever.GetColumn(this._waterColumnCounts, this._waterColumns, this._verticalStride, neighborIndex2D, neighborTerrainHeight).WaterDepth);
			int num4 = num2 - num3;
			if (num4 < 0)
			{
				return num - spreadCost;
			}
			return num - (float)(num4 * this._verticalSpreadCostMultiplier) - spreadCost;
		}

		// Token: 0x0400001A RID: 26
		public static readonly float MinimumMoisture = 0.01f;

		// Token: 0x0400001B RID: 27
		public readonly WaterColumnRetriever _waterColumnRetriever;

		// Token: 0x0400001C RID: 28
		public readonly CeilingRetriever _ceilingRetriever;

		// Token: 0x0400001D RID: 29
		public readonly float[] _moistureLevels;

		// Token: 0x0400001E RID: 30
		public readonly bool[] _moistureLevelsChangedLastTick;

		// Token: 0x0400001F RID: 31
		public readonly ReadOnlyArray<byte> _waterColumnCounts;

		// Token: 0x04000020 RID: 32
		public readonly ReadOnlyArray<ReadOnlyWaterColumn> _waterColumns;

		// Token: 0x04000021 RID: 33
		public readonly ReadOnlyArray<byte> _terrainColumnCounts;

		// Token: 0x04000022 RID: 34
		public readonly ReadOnlyArray<ReadOnlyTerrainColumn> _terrainColumns;

		// Token: 0x04000023 RID: 35
		public readonly ReadOnlyArray<float> _lastTickMoistureLevels;

		// Token: 0x04000024 RID: 36
		public readonly ReadOnlyArray<bool> _fullMoistureBarriers;

		// Token: 0x04000025 RID: 37
		public readonly ReadOnlyArray<bool> _aboveMoistureBarriers;

		// Token: 0x04000026 RID: 38
		public readonly ReadOnlyArray<byte> _clusterSaturation;

		// Token: 0x04000027 RID: 39
		public readonly int _xMapSize;

		// Token: 0x04000028 RID: 40
		public readonly int _stride;

		// Token: 0x04000029 RID: 41
		public readonly int _verticalStride;

		// Token: 0x0400002A RID: 42
		public readonly float _moistureDecayRate;

		// Token: 0x0400002B RID: 43
		public readonly float _moistureSpreadingRate;

		// Token: 0x0400002C RID: 44
		public readonly float _minimumWaterContamination;

		// Token: 0x0400002D RID: 45
		public readonly int _verticalSpreadCostMultiplier;

		// Token: 0x0400002E RID: 46
		public readonly float _waterContaminationScaler;
	}
}
