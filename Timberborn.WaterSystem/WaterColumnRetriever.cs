using System;
using Timberborn.Common;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200002A RID: 42
	public class WaterColumnRetriever
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000046C4 File Offset: 0x000028C4
		public unsafe ref readonly ReadOnlyWaterColumn GetColumn(ReadOnlyArray<byte> columnCounts, ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, int verticalStride, int index, int height)
		{
			for (int i = 0; i < (int)(*columnCounts[index]); i++)
			{
				ref readonly ReadOnlyWaterColumn ptr = ref waterColumns[i * verticalStride + index];
				if (height < (int)ptr.Floor)
				{
					break;
				}
				if (height < (int)ptr.Ceiling)
				{
					return ref ptr;
				}
			}
			return ref this._emptyWaterColumn;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004710 File Offset: 0x00002910
		public unsafe bool TryGetColumnWithFloorAtHeight(in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, int verticalStride, int index2D, int height, out int index3D)
		{
			for (int i = 0; i < (int)(*columnCounts[index2D]); i++)
			{
				index3D = i * verticalStride + index2D;
				byte floor = waterColumns[index3D].Floor;
				if (height == (int)floor)
				{
					return true;
				}
				if ((int)floor > height)
				{
					return false;
				}
			}
			index3D = 0;
			return false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000475C File Offset: 0x0000295C
		public unsafe bool TryGetColumnWithCeilingAtHeight(in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, int verticalStride, int index2D, int height, out int index3D)
		{
			for (int i = 0; i < (int)(*columnCounts[index2D]); i++)
			{
				index3D = i * verticalStride + index2D;
				byte ceiling = waterColumns[index3D].Ceiling;
				if (height == (int)ceiling)
				{
					return true;
				}
				if ((int)ceiling > height)
				{
					return false;
				}
			}
			index3D = 0;
			return false;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000047A8 File Offset: 0x000029A8
		public unsafe bool TryGetTopWateredColumn(in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, int verticalStride, int terrainHeight, int targetIndex2D, out int index3D)
		{
			for (int i = (int)(*columnCounts[targetIndex2D] - 1); i >= 0; i--)
			{
				index3D = targetIndex2D + i * verticalStride;
				ref readonly ReadOnlyWaterColumn ptr = ref waterColumns[index3D];
				if ((int)ptr.Floor <= terrainHeight && ptr.WaterDepth > 0f)
				{
					return true;
				}
			}
			index3D = 0;
			return false;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000047FC File Offset: 0x000029FC
		public unsafe bool TryGetTopContaminatedColumn(in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, int verticalStride, int terrainHeight, int targetIndex2D, out int index3D)
		{
			for (int i = (int)(*columnCounts[targetIndex2D] - 1); i >= 0; i--)
			{
				index3D = targetIndex2D + i * verticalStride;
				ref readonly ReadOnlyWaterColumn ptr = ref waterColumns[index3D];
				if ((int)ptr.Floor <= terrainHeight && ptr.Contamination > 0f)
				{
					return true;
				}
			}
			index3D = 0;
			return false;
		}

		// Token: 0x040000A3 RID: 163
		public readonly ReadOnlyWaterColumn _emptyWaterColumn;
	}
}
