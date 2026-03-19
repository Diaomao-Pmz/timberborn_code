using System;

namespace Timberborn.CoreUI
{
	// Token: 0x0200003B RID: 59
	public class PanelShownEvent
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000460C File Offset: 0x0000280C
		public bool IsDialog { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004614 File Offset: 0x00002814
		public bool LockSpeed { get; }

		// Token: 0x060000E2 RID: 226 RVA: 0x0000461C File Offset: 0x0000281C
		public PanelShownEvent(bool isDialog, bool lockSpeed)
		{
			this.IsDialog = isDialog;
			this.LockSpeed = lockSpeed;
		}
	}
}
