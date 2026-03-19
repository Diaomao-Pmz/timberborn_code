using System;

namespace Timberborn.CoreUI
{
	// Token: 0x0200003A RID: 58
	public class PanelHiddenEvent
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000045D7 File Offset: 0x000027D7
		public bool AnyPanelShown { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000045DF File Offset: 0x000027DF
		public bool UnlockSpeed { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000045E7 File Offset: 0x000027E7
		public bool WasDialog { get; }

		// Token: 0x060000DF RID: 223 RVA: 0x000045EF File Offset: 0x000027EF
		public PanelHiddenEvent(bool anyPanelShown, bool unlockSpeed, bool wasDialog)
		{
			this.AnyPanelShown = anyPanelShown;
			this.UnlockSpeed = unlockSpeed;
			this.WasDialog = wasDialog;
		}
	}
}
