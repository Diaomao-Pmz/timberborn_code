using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Multithreading;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000023 RID: 35
	public readonly struct UpdateContaminationTask : IParallelizerLoopTask
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003E1C File Offset: 0x0000201C
		public UpdateContaminationTask(WaterColumn[] waterColumns, ReadOnlyArray<byte> waterColumnCounts, ReadOnlyArray<float> contaminationsBuffer, ReadOnlyArray<Diffusions> baseLevelDiffusions, ReadOnlyArray<byte> targetedDiffusionCount, ReadOnlyArray<List<TargetedDiffusion>> targetedDiffusions, int xMapSize, int stride, int verticalStride, float deltaTime, float maxContamination, float diffusionRate)
		{
			this._waterColumns = waterColumns;
			this._waterColumnCounts = waterColumnCounts;
			this._contaminationsBuffer = contaminationsBuffer;
			this._baseLevelDiffusions = baseLevelDiffusions;
			this._targetedDiffusionCount = targetedDiffusionCount;
			this._targetedDiffusions = targetedDiffusions;
			this._xMapSize = xMapSize;
			this._stride = stride;
			this._verticalStride = verticalStride;
			this._deltaTime = deltaTime;
			this._maxContamination = maxContamination;
			this._diffusionRate = diffusionRate;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003E88 File Offset: 0x00002088
		public unsafe void Run(int y)
		{
			int num = (y + 1) * this._stride;
			for (int i = 0; i < this._xMapSize; i++)
			{
				int num2 = i + 1 + num;
				for (int j = 0; j < (int)(*this._waterColumnCounts[num2]); j++)
				{
					int num3 = num2 + j * this._verticalStride;
					ref WaterColumn ptr = ref this._waterColumns[num3];
					if (ptr.WaterDepth > 0f)
					{
						float num4 = *this._contaminationsBuffer[num3] + this.GetContaminationDiffusionChange(ptr, num2, num3);
						ptr.Contamination = ((num4 > this._maxContamination) ? this._maxContamination : num4);
					}
					else
					{
						ptr.Contamination = 0f;
					}
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003F48 File Offset: 0x00002148
		public unsafe float GetContaminationDiffusionChange(in WaterColumn waterColumn, int index, int index3D)
		{
			byte b = *this._targetedDiffusionCount[index3D];
			if (b > 0)
			{
				float diffusionFraction = 1f / (float)b;
				float sourceContamination = *this._contaminationsBuffer[index3D];
				float waterDepth = waterColumn.WaterDepth;
				float num = 0f;
				if (index == index3D)
				{
					ref readonly Diffusions ptr = ref this._baseLevelDiffusions[index];
					if (ptr.Bottom)
					{
						num += this.CalculateDiffusion(sourceContamination, waterDepth, index - this._stride, diffusionFraction);
					}
					if (ptr.Left)
					{
						num += this.CalculateDiffusion(sourceContamination, waterDepth, index - 1, diffusionFraction);
					}
					if (ptr.Top)
					{
						num += this.CalculateDiffusion(sourceContamination, waterDepth, index + this._stride, diffusionFraction);
					}
					if (ptr.Right)
					{
						num += this.CalculateDiffusion(sourceContamination, waterDepth, index + 1, diffusionFraction);
					}
				}
				List<TargetedDiffusion> list = *this._targetedDiffusions[index];
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].OriginIndex3D == index3D)
					{
						int targetIndex3D = list[i].TargetIndex3D;
						num += this.CalculateDiffusion(sourceContamination, waterDepth, targetIndex3D, diffusionFraction);
					}
				}
				return num * this._deltaTime;
			}
			return 0f;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004074 File Offset: 0x00002274
		public unsafe float CalculateDiffusion(float sourceContamination, float sourceWaterDepth, int targetIndex3D, float diffusionFraction)
		{
			WaterColumn[] waterColumns = this._waterColumns;
			float num = 1f / (float)(*this._targetedDiffusionCount[targetIndex3D]);
			float num2 = *this._contaminationsBuffer[targetIndex3D];
			float waterDepth = waterColumns[targetIndex3D].WaterDepth;
			float num3 = num2 - sourceContamination;
			float num5;
			if (num3 > 0f)
			{
				float num4 = num * num2;
				num5 = ((num3 < num4) ? num3 : num4);
			}
			else
			{
				float num6 = -diffusionFraction * sourceContamination;
				num5 = ((num3 > num6) ? num3 : num6);
			}
			return waterDepth / (sourceWaterDepth + waterDepth) * num5 * this._diffusionRate;
		}

		// Token: 0x04000079 RID: 121
		public readonly WaterColumn[] _waterColumns;

		// Token: 0x0400007A RID: 122
		public readonly ReadOnlyArray<byte> _waterColumnCounts;

		// Token: 0x0400007B RID: 123
		public readonly ReadOnlyArray<float> _contaminationsBuffer;

		// Token: 0x0400007C RID: 124
		public readonly ReadOnlyArray<Diffusions> _baseLevelDiffusions;

		// Token: 0x0400007D RID: 125
		public readonly ReadOnlyArray<byte> _targetedDiffusionCount;

		// Token: 0x0400007E RID: 126
		public readonly ReadOnlyArray<List<TargetedDiffusion>> _targetedDiffusions;

		// Token: 0x0400007F RID: 127
		public readonly int _xMapSize;

		// Token: 0x04000080 RID: 128
		public readonly int _stride;

		// Token: 0x04000081 RID: 129
		public readonly int _verticalStride;

		// Token: 0x04000082 RID: 130
		public readonly float _deltaTime;

		// Token: 0x04000083 RID: 131
		public readonly float _maxContamination;

		// Token: 0x04000084 RID: 132
		public readonly float _diffusionRate;
	}
}
