using System;

namespace Timberborn.GameStartup
{
	// Token: 0x0200000C RID: 12
	public class SettlementNameChangedEvent
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002427 File Offset: 0x00000627
		public string SettlementName { get; }

		// Token: 0x0600001F RID: 31 RVA: 0x0000242F File Offset: 0x0000062F
		public SettlementNameChangedEvent(string settlementName)
		{
			this.SettlementName = settlementName;
		}
	}
}
