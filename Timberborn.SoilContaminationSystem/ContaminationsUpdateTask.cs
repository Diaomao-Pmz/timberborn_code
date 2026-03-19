using System;
using Timberborn.Common;
using Timberborn.Multithreading;

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x0200000A RID: 10
	public readonly struct ContaminationsUpdateTask : IParallelizerLoopTask
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000028AC File Offset: 0x00000AAC
		public ContaminationsUpdateTask(float[] contaminationLevels, bool[] contaminationsChangedLastTick, ReadOnlyArray<byte> terrainColumnCounts, ReadOnlyArray<float> contaminationCandidates, int xMapSize, int stride, int verticalStride, float contaminationThreshold, float contaminationPositiveEqualizationRate, float contaminationNegativeEqualizationRate)
		{
			this._contaminationLevels = contaminationLevels;
			this._contaminationsChangedLastTick = contaminationsChangedLastTick;
			this._terrainColumnCounts = terrainColumnCounts;
			this._contaminationCandidates = contaminationCandidates;
			this._xMapSize = xMapSize;
			this._stride = stride;
			this._verticalStride = verticalStride;
			this._contaminationThreshold = contaminationThreshold;
			this._contaminationPositiveEqualizationRate = contaminationPositiveEqualizationRate;
			this._contaminationNegativeEqualizationRate = contaminationNegativeEqualizationRate;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002908 File Offset: 0x00000B08
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
					float num4 = this._contaminationLevels[num3];
					float num5 = *this._contaminationCandidates[num3];
					float num6 = num5 - num4;
					float num7 = (num6 > 0f) ? this._contaminationPositiveEqualizationRate : this._contaminationNegativeEqualizationRate;
					float num8 = (num6 <= num7 && num6 >= -num7) ? num5 : (num4 + (float)Math.Sign(num6) * num7);
					if (num8 < this._contaminationThreshold)
					{
						num8 = 0f;
					}
					if (num8 != num4)
					{
						this._contaminationLevels[num3] = num8;
						this._contaminationsChangedLastTick[num3] = true;
					}
				}
			}
		}

		// Token: 0x04000025 RID: 37
		public readonly float[] _contaminationLevels;

		// Token: 0x04000026 RID: 38
		public readonly bool[] _contaminationsChangedLastTick;

		// Token: 0x04000027 RID: 39
		public readonly ReadOnlyArray<byte> _terrainColumnCounts;

		// Token: 0x04000028 RID: 40
		public readonly ReadOnlyArray<float> _contaminationCandidates;

		// Token: 0x04000029 RID: 41
		public readonly int _xMapSize;

		// Token: 0x0400002A RID: 42
		public readonly int _stride;

		// Token: 0x0400002B RID: 43
		public readonly int _verticalStride;

		// Token: 0x0400002C RID: 44
		public readonly float _contaminationThreshold;

		// Token: 0x0400002D RID: 45
		public readonly float _contaminationPositiveEqualizationRate;

		// Token: 0x0400002E RID: 46
		public readonly float _contaminationNegativeEqualizationRate;
	}
}
