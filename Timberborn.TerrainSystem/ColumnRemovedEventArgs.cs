using System;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000006 RID: 6
	public readonly struct ColumnRemovedEventArgs
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002123 File Offset: 0x00000323
		public int Index { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212B File Offset: 0x0000032B
		public int ColumnCount { get; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002133 File Offset: 0x00000333
		public ColumnRemovedEventArgs(int index, int columnCount)
		{
			this.Index = index;
			this.ColumnCount = columnCount;
		}
	}
}
