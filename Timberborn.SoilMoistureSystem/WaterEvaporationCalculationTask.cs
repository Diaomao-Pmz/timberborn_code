using System;
using Timberborn.Common;
using Timberborn.Multithreading;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000018 RID: 24
	public readonly struct WaterEvaporationCalculationTask : IParallelizerLoopTask
	{
		// Token: 0x060000AB RID: 171 RVA: 0x000047C4 File Offset: 0x000029C4
		public WaterEvaporationCalculationTask(float[] evaporationModifiers, in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<byte> clusterSaturations, in ReadOnlyArray<float> saturationToEvaporationMap, int xMapSize, int stride, int verticalStride)
		{
			this._evaporationModifiers = evaporationModifiers;
			this._columnCounts = columnCounts;
			this._clusterSaturations = clusterSaturations;
			this._saturationToEvaporationMap = saturationToEvaporationMap;
			this._xMapSize = xMapSize;
			this._stride = stride;
			this._verticalStride = verticalStride;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004818 File Offset: 0x00002A18
		public unsafe void Run(int y)
		{
			int num = (y + 1) * this._stride;
			for (int i = 0; i < this._xMapSize; i++)
			{
				int num2 = i + 1 + num;
				byte b = *this._columnCounts[num2];
				for (int j = 0; j < (int)b; j++)
				{
					int num3 = num2 + j * this._verticalStride;
					byte b2 = *this._clusterSaturations[num3];
					float num4 = (b2 == 0) ? 1f : (*this._saturationToEvaporationMap[(int)b2]);
					this._evaporationModifiers[num3] = num4;
				}
			}
		}

		// Token: 0x04000080 RID: 128
		public readonly float[] _evaporationModifiers;

		// Token: 0x04000081 RID: 129
		public readonly ReadOnlyArray<byte> _columnCounts;

		// Token: 0x04000082 RID: 130
		public readonly ReadOnlyArray<byte> _clusterSaturations;

		// Token: 0x04000083 RID: 131
		public readonly ReadOnlyArray<float> _saturationToEvaporationMap;

		// Token: 0x04000084 RID: 132
		public readonly int _xMapSize;

		// Token: 0x04000085 RID: 133
		public readonly int _stride;

		// Token: 0x04000086 RID: 134
		public readonly int _verticalStride;
	}
}
