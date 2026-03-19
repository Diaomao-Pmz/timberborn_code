using System;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000005 RID: 5
	public readonly struct DebugFragmentButton
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212B File Offset: 0x0000032B
		public Action Action { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002133 File Offset: 0x00000333
		public string Text { get; }

		// Token: 0x0600000E RID: 14 RVA: 0x0000213B File Offset: 0x0000033B
		public DebugFragmentButton(Action action, string text)
		{
			this.Action = action;
			this.Text = text;
		}
	}
}
