using System;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000010 RID: 16
	public class ManualMigrationBlockingStateChangedEvent
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000264E File Offset: 0x0000084E
		public bool IsEnabled { get; }

		// Token: 0x0600002F RID: 47 RVA: 0x00002656 File Offset: 0x00000856
		public ManualMigrationBlockingStateChangedEvent(bool isEnabled)
		{
			this.IsEnabled = isEnabled;
		}
	}
}
