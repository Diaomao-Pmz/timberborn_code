using System;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000011 RID: 17
	public class TerrainHeightChangeEventArgs
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002A37 File Offset: 0x00000C37
		public TerrainHeightChange Change { get; }

		// Token: 0x0600006B RID: 107 RVA: 0x00002A3F File Offset: 0x00000C3F
		public TerrainHeightChangeEventArgs(TerrainHeightChange change)
		{
			this.Change = change;
		}
	}
}
