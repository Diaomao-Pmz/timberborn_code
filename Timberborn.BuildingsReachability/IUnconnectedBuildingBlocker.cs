using System;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x0200000D RID: 13
	public interface IUnconnectedBuildingBlocker
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000029 RID: 41
		// (remove) Token: 0x0600002A RID: 42
		event EventHandler IsUnconnectedBlockedChanged;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002B RID: 43
		bool IsUnconnectedBlocked { get; }
	}
}
