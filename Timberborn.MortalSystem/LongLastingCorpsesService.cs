using System;

namespace Timberborn.MortalSystem
{
	// Token: 0x0200000E RID: 14
	public class LongLastingCorpsesService
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002965 File Offset: 0x00000B65
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000296D File Offset: 0x00000B6D
		public bool Enabled { get; private set; }

		// Token: 0x06000047 RID: 71 RVA: 0x00002976 File Offset: 0x00000B76
		public void Toggle()
		{
			this.Enabled = !this.Enabled;
		}
	}
}
