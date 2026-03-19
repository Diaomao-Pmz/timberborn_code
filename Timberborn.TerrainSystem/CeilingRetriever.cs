using System;
using Timberborn.Common;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000004 RID: 4
	public class CeilingRetriever
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public unsafe int GetCeilingAtOrBelowHeight(in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<ReadOnlyTerrainColumn> columns, int verticalStride, int index2D, int height)
		{
			for (int i = (int)(*columnCounts[index2D] - 1); i >= 0; i--)
			{
				int index = i * verticalStride + index2D;
				int ceiling = columns[index].Ceiling;
				if (ceiling <= height)
				{
					return ceiling;
				}
			}
			throw new InvalidOperationException();
		}
	}
}
