using System;
using Timberborn.Common;
using Timberborn.Multithreading;
using Timberborn.WaterSystem;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000017 RID: 23
	public readonly struct WateredNeighborsCountingTask : IParallelizerLoopTask
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000045AF File Offset: 0x000027AF
		public WateredNeighborsCountingTask(byte[] wateredNeighbours, in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, int stride, int verticalStride, int xMapSize)
		{
			this._wateredNeighbours = wateredNeighbours;
			this._columnCounts = columnCounts;
			this._waterColumns = waterColumns;
			this._stride = stride;
			this._verticalStride = verticalStride;
			this._xMapSize = xMapSize;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000045E8 File Offset: 0x000027E8
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
					if (this._waterColumns[num3].WaterDepth > 0f)
					{
						int num4 = this.CountWateredNeighbors(num2, num3);
						this._wateredNeighbours[num3] = (byte)(num4 + 1);
					}
					else
					{
						this._wateredNeighbours[num3] = 0;
					}
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004680 File Offset: 0x00002880
		public int CountWateredNeighbors(int index, int index3D)
		{
			int num = 0;
			ref readonly ReadOnlyWaterColumn originColumn = ref this._waterColumns[index3D];
			return num + this.IsNeighborWatered(originColumn, index - this._stride - 1) + this.IsNeighborWatered(originColumn, index - this._stride) + this.IsNeighborWatered(originColumn, index - this._stride + 1) + this.IsNeighborWatered(originColumn, index - 1) + this.IsNeighborWatered(originColumn, index + 1) + this.IsNeighborWatered(originColumn, index + this._stride - 1) + this.IsNeighborWatered(originColumn, index + this._stride) + this.IsNeighborWatered(originColumn, index + this._stride + 1);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000471C File Offset: 0x0000291C
		public unsafe int IsNeighborWatered(in ReadOnlyWaterColumn originColumn, int targetIndex2D)
		{
			byte floor = originColumn.Floor;
			byte ceiling = originColumn.Ceiling;
			ref readonly ReadOnlyWaterColumn ptr = ref this._waterColumns[targetIndex2D];
			if (ptr.Floor < floor)
			{
				for (int i = (int)(*this._columnCounts[targetIndex2D] - 1); i >= 0; i--)
				{
					int index = targetIndex2D + i * this._verticalStride;
					ref readonly ReadOnlyWaterColumn ptr2 = ref this._waterColumns[index];
					if (ptr2.Floor < ceiling && ptr2.Ceiling > floor && ptr2.WaterDepth > 0f)
					{
						return 1;
					}
				}
				return 0;
			}
			if (ceiling > ptr.Floor && ptr.WaterDepth > 0f)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0400007A RID: 122
		public readonly byte[] _wateredNeighbours;

		// Token: 0x0400007B RID: 123
		public readonly ReadOnlyArray<byte> _columnCounts;

		// Token: 0x0400007C RID: 124
		public readonly ReadOnlyArray<ReadOnlyWaterColumn> _waterColumns;

		// Token: 0x0400007D RID: 125
		public readonly int _stride;

		// Token: 0x0400007E RID: 126
		public readonly int _verticalStride;

		// Token: 0x0400007F RID: 127
		public readonly int _xMapSize;
	}
}
