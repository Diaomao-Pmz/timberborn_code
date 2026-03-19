using System;

namespace Timberborn.SaveMetadataSystem
{
	// Token: 0x02000006 RID: 6
	public class SaveMetadata
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000219A File Offset: 0x0000039A
		public DateTime Timestamp { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021A2 File Offset: 0x000003A2
		public int Cycle { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021AA File Offset: 0x000003AA
		public int Day { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021B2 File Offset: 0x000003B2
		public ModReference[] Mods { get; }

		// Token: 0x0600000F RID: 15 RVA: 0x000021BA File Offset: 0x000003BA
		public SaveMetadata(DateTime timestamp, int cycle, int day, ModReference[] mods)
		{
			this.Timestamp = timestamp;
			this.Cycle = cycle;
			this.Day = day;
			this.Mods = mods;
		}
	}
}
