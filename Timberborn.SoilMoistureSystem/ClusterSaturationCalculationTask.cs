using System;
using Timberborn.Common;
using Timberborn.Multithreading;
using Timberborn.WaterSystem;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000007 RID: 7
	public readonly struct ClusterSaturationCalculationTask : IParallelizerLoopTask
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public ClusterSaturationCalculationTask(byte[] clusterSaturations, in ReadOnlyArray<byte> wateredNeighbours, in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, int maxClusterSaturation, int xMapSize, int stride, int verticalStride)
		{
			this._clusterSaturations = clusterSaturations;
			this._wateredNeighbours = wateredNeighbours;
			this._columnCounts = columnCounts;
			this._waterColumns = waterColumns;
			this._maxClusterSaturation = maxClusterSaturation;
			this._xMapSize = xMapSize;
			this._stride = stride;
			this._verticalStride = verticalStride;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000215C File Offset: 0x0000035C
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
					ref readonly ReadOnlyWaterColumn ptr = ref this._waterColumns[num3];
					if (ptr.WaterDepth > 0f)
					{
						byte floor = ptr.Floor;
						byte ceiling = ptr.Ceiling;
						int index = num2 - this._stride;
						int index2 = num2 - 1;
						int index3 = num2 + this._stride;
						int index4 = num2 + 1;
						int num4 = (int)(*this._wateredNeighbours[num3]);
						int wateredNeighbors = this.GetWateredNeighbors(index, (int)floor, (int)ceiling);
						int wateredNeighbors2 = this.GetWateredNeighbors(index2, (int)floor, (int)ceiling);
						int wateredNeighbors3 = this.GetWateredNeighbors(index3, (int)floor, (int)ceiling);
						int wateredNeighbors4 = this.GetWateredNeighbors(index4, (int)floor, (int)ceiling);
						if (wateredNeighbors > num4)
						{
							num4 = wateredNeighbors - 1;
						}
						if (wateredNeighbors2 > num4)
						{
							num4 = wateredNeighbors2 - 1;
						}
						if (wateredNeighbors3 > num4)
						{
							num4 = wateredNeighbors3 - 1;
						}
						if (wateredNeighbors4 > num4)
						{
							num4 = wateredNeighbors4 - 1;
						}
						int num5 = (num4 > this._maxClusterSaturation) ? this._maxClusterSaturation : num4;
						this._clusterSaturations[num3] = (byte)num5;
					}
					else
					{
						this._clusterSaturations[num3] = 0;
					}
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022B8 File Offset: 0x000004B8
		public unsafe int GetWateredNeighbors(int index, int floor, int ceiling)
		{
			byte b = *this._columnCounts[index];
			int num = 0;
			for (int i = 0; i < (int)b; i++)
			{
				int index2 = index + i * this._verticalStride;
				byte b2 = *this._wateredNeighbours[index2];
				if ((int)b2 > num)
				{
					ref readonly ReadOnlyWaterColumn ptr = ref this._waterColumns[index2];
					if ((int)ptr.Ceiling > floor && (int)ptr.Floor < ceiling)
					{
						num = (int)b2;
					}
				}
			}
			return num;
		}

		// Token: 0x04000008 RID: 8
		public readonly byte[] _clusterSaturations;

		// Token: 0x04000009 RID: 9
		public readonly ReadOnlyArray<byte> _wateredNeighbours;

		// Token: 0x0400000A RID: 10
		public readonly ReadOnlyArray<byte> _columnCounts;

		// Token: 0x0400000B RID: 11
		public readonly ReadOnlyArray<ReadOnlyWaterColumn> _waterColumns;

		// Token: 0x0400000C RID: 12
		public readonly int _maxClusterSaturation;

		// Token: 0x0400000D RID: 13
		public readonly int _xMapSize;

		// Token: 0x0400000E RID: 14
		public readonly int _stride;

		// Token: 0x0400000F RID: 15
		public readonly int _verticalStride;
	}
}
