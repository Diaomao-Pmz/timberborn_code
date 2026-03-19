using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Multithreading;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200001E RID: 30
	public readonly struct SimulateContaminationTask : IParallelizerLoopTask
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000033D8 File Offset: 0x000015D8
		public SimulateContaminationTask(FlowLimitCalculator flowLimitCalculator, WaterFlowRetriever waterFlowRetriever, float[] contaminationsBuffer, Diffusions[] baseLevelDiffusions, byte[] targetedDiffusionCount, List<TargetedDiffusion>[] targetedDiffusions, ReadOnlyArray<byte> waterColumnCounts, ReadOnlyArray<WaterColumn> waterColumns, ReadOnlyArray<ColumnOutflows> outflows, ReadOnlyArray<float> limitedValues, int xMapSize, int stride, int verticalStride, float deltaTime, float overflowPressureFactor, float maxWaterContamination, double diffusionOutflowLimit, double diffusionDepthLimit)
		{
			this._flowLimitCalculator = flowLimitCalculator;
			this._waterFlowRetriever = waterFlowRetriever;
			this._contaminationsBuffer = contaminationsBuffer;
			this._baseLevelDiffusions = baseLevelDiffusions;
			this._targetedDiffusionCount = targetedDiffusionCount;
			this._targetedDiffusions = targetedDiffusions;
			this._waterColumnCounts = waterColumnCounts;
			this._waterColumns = waterColumns;
			this._outflows = outflows;
			this._limitedValues = limitedValues;
			this._xMapSize = xMapSize;
			this._stride = stride;
			this._verticalStride = verticalStride;
			this._deltaTime = deltaTime;
			this._overflowPressureFactor = overflowPressureFactor;
			this._maxWaterContamination = maxWaterContamination;
			this._diffusionOutflowLimit = diffusionOutflowLimit;
			this._diffusionDepthLimit = diffusionDepthLimit;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003474 File Offset: 0x00001674
		public unsafe void Run(int y)
		{
			int num = (y + 1) * this._stride;
			for (int i = 0; i < this._xMapSize; i++)
			{
				int num2 = i + 1 + num;
				this._targetedDiffusions[num2].Clear();
				for (int j = 0; j < (int)(*this._waterColumnCounts[num2]); j++)
				{
					int num3 = num2 + j * this._verticalStride;
					ref readonly WaterColumn ptr = ref this._waterColumns[num3];
					if (ptr.WaterDepth > 0f)
					{
						this.SimulateContamination(ptr, num2, num3);
					}
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000034FC File Offset: 0x000016FC
		public void SimulateContamination(in WaterColumn waterColumn, int index, int index3D)
		{
			ref readonly ColumnOutflows outflows = ref this._outflows[index3D];
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			ref Diffusions baseLevelDiffusion = ref this._baseLevelDiffusions[index];
			this.CalculateContaminationChange(waterColumn, index, index3D, -this._stride, outflows, ref baseLevelDiffusion, ref num, ref num2, ref num3);
			this.CalculateContaminationChange(waterColumn, index, index3D, -1, outflows, ref baseLevelDiffusion, ref num, ref num2, ref num3);
			this.CalculateContaminationChange(waterColumn, index, index3D, this._stride, outflows, ref baseLevelDiffusion, ref num, ref num2, ref num3);
			this.CalculateContaminationChange(waterColumn, index, index3D, 1, outflows, ref baseLevelDiffusion, ref num, ref num2, ref num3);
			if (num <= 0f)
			{
				this._contaminationsBuffer[index3D] = waterColumn.Contamination;
				return;
			}
			float num4 = waterColumn.WaterDepth + waterColumn.Overflow * this._overflowPressureFactor;
			float num5 = num4 - (num2 + num);
			float num6 = (waterColumn.Contamination * (num5 + num2) + num3) / num4;
			if (num6 < 0f)
			{
				this._contaminationsBuffer[index3D] = 0f;
				return;
			}
			this._contaminationsBuffer[index3D] = ((num6 > this._maxWaterContamination) ? this._maxWaterContamination : num6);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003608 File Offset: 0x00001808
		public unsafe void CalculateContaminationChange(in WaterColumn waterColumn, int index, int index3D, int targetOffset, in ColumnOutflows outflows, ref Diffusions baseLevelDiffusion, ref float waterReceived, ref float waterDisposed, ref float contaminationChange)
		{
			int num = index + targetOffset;
			byte b = *this._waterColumnCounts[num];
			for (int i = 0; i < (int)b; i++)
			{
				int num2 = num + i * this._verticalStride;
				float flow = this._waterFlowRetriever.GetFlow(num2, outflows);
				ref readonly ColumnOutflows outflows2 = ref this._outflows[num2];
				float num3 = this._waterFlowRetriever.GetFlow(index3D, outflows2) - flow;
				if (num3 != 0f)
				{
					float num4 = num3 * this._deltaTime;
					if (num4 > 0f)
					{
						waterReceived += num4;
						ref readonly WaterColumn ptr = ref this._waterColumns[num2];
						contaminationChange += num4 * ptr.Contamination;
					}
					else
					{
						waterDisposed += num4;
					}
					if (this.CanDiffuse(waterColumn, index, num, num2, num3))
					{
						if (index == index3D && num == num2)
						{
							if (targetOffset == -this._stride)
							{
								baseLevelDiffusion.Bottom = true;
							}
							else if (targetOffset == -1)
							{
								baseLevelDiffusion.Left = true;
							}
							else if (targetOffset == this._stride)
							{
								baseLevelDiffusion.Top = true;
							}
							else if (targetOffset == 1)
							{
								baseLevelDiffusion.Right = true;
							}
						}
						else
						{
							this._targetedDiffusions[index].Add(new TargetedDiffusion(num2, index3D));
						}
						byte[] targetedDiffusionCount = this._targetedDiffusionCount;
						targetedDiffusionCount[index3D] += 1;
					}
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003754 File Offset: 0x00001954
		public bool CanDiffuse(in WaterColumn waterColumn, int index, int target, int target3D, float netFlowToTarget)
		{
			if ((double)netFlowToTarget >= this._diffusionOutflowLimit || (double)netFlowToTarget <= -this._diffusionOutflowLimit)
			{
				return false;
			}
			float waterDepth = waterColumn.WaterDepth;
			byte floor = waterColumn.Floor;
			if (waterDepth <= 1f && this._flowLimitCalculator.HasInflowLimit(this._limitedValues, index, (int)floor))
			{
				return false;
			}
			ref readonly WaterColumn ptr = ref this._waterColumns[target3D];
			float waterDepth2 = ptr.WaterDepth;
			if (waterDepth2 <= 0f)
			{
				return false;
			}
			byte floor2 = ptr.Floor;
			if (waterDepth2 <= 1f && this._flowLimitCalculator.HasInflowLimit(this._limitedValues, target, (int)floor2))
			{
				return false;
			}
			float num = waterDepth2 + (float)floor2 + ptr.Overflow * this._overflowPressureFactor;
			float num2 = waterDepth + (float)floor + waterColumn.Overflow * this._overflowPressureFactor;
			return (double)((num > num2) ? (num - num2) : (num2 - num)) <= this._diffusionDepthLimit;
		}

		// Token: 0x04000054 RID: 84
		public readonly FlowLimitCalculator _flowLimitCalculator;

		// Token: 0x04000055 RID: 85
		public readonly WaterFlowRetriever _waterFlowRetriever;

		// Token: 0x04000056 RID: 86
		public readonly float[] _contaminationsBuffer;

		// Token: 0x04000057 RID: 87
		public readonly Diffusions[] _baseLevelDiffusions;

		// Token: 0x04000058 RID: 88
		public readonly byte[] _targetedDiffusionCount;

		// Token: 0x04000059 RID: 89
		public readonly List<TargetedDiffusion>[] _targetedDiffusions;

		// Token: 0x0400005A RID: 90
		public readonly ReadOnlyArray<byte> _waterColumnCounts;

		// Token: 0x0400005B RID: 91
		public readonly ReadOnlyArray<WaterColumn> _waterColumns;

		// Token: 0x0400005C RID: 92
		public readonly ReadOnlyArray<ColumnOutflows> _outflows;

		// Token: 0x0400005D RID: 93
		public readonly ReadOnlyArray<float> _limitedValues;

		// Token: 0x0400005E RID: 94
		public readonly int _xMapSize;

		// Token: 0x0400005F RID: 95
		public readonly int _stride;

		// Token: 0x04000060 RID: 96
		public readonly int _verticalStride;

		// Token: 0x04000061 RID: 97
		public readonly float _deltaTime;

		// Token: 0x04000062 RID: 98
		public readonly float _overflowPressureFactor;

		// Token: 0x04000063 RID: 99
		public readonly float _maxWaterContamination;

		// Token: 0x04000064 RID: 100
		public readonly double _diffusionOutflowLimit;

		// Token: 0x04000065 RID: 101
		public readonly double _diffusionDepthLimit;
	}
}
