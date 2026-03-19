using System;

namespace Timberborn.FactionSystem
{
	// Token: 0x0200000D RID: 13
	public class FactionUnlockedEvent
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002F2C File Offset: 0x0000112C
		public FactionUnlockedEvent(FactionSpec faction)
		{
			this.Faction = faction;
		}

		// Token: 0x0400002D RID: 45
		public readonly FactionSpec Faction;
	}
}
