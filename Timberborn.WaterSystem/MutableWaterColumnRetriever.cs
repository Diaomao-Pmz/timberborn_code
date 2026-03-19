using System;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000019 RID: 25
	public class MutableWaterColumnRetriever
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002C20 File Offset: 0x00000E20
		public unsafe ref WaterColumn GetColumn(ReadOnlySpan<byte> columnCounts, Span<WaterColumn> waterColumns, int verticalStride, int index, int height)
		{
			for (int i = 0; i < (int)(*columnCounts[index]); i++)
			{
				ref WaterColumn ptr = waterColumns[i * verticalStride + index];
				if (height < (int)ptr.Floor)
				{
					break;
				}
				if (height < (int)ptr.Ceiling)
				{
					return ref ptr;
				}
			}
			throw new InvalidOperationException(string.Format("Column for index {0} and height {1} not found", index, height));
		}
	}
}
