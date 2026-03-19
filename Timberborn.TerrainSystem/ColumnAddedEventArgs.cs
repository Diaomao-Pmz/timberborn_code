using System;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000005 RID: 5
	public readonly struct ColumnAddedEventArgs
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002103 File Offset: 0x00000303
		public int Index { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000210B File Offset: 0x0000030B
		public int ColumnCount { get; }

		// Token: 0x06000007 RID: 7 RVA: 0x00002113 File Offset: 0x00000313
		public ColumnAddedEventArgs(int index, int columnCount)
		{
			this.Index = index;
			this.ColumnCount = columnCount;
		}
	}
}
