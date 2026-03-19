using System;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000029 RID: 41
	public class StatusVisibilityToggle
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600010C RID: 268 RVA: 0x00004AF0 File Offset: 0x00002CF0
		// (remove) Token: 0x0600010D RID: 269 RVA: 0x00004B28 File Offset: 0x00002D28
		public event EventHandler StateChanged;

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004B5D File Offset: 0x00002D5D
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00004B65 File Offset: 0x00002D65
		public bool Hidden { get; private set; }

		// Token: 0x06000110 RID: 272 RVA: 0x00004B6E File Offset: 0x00002D6E
		public void Hide()
		{
			this.Hidden = true;
			EventHandler stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004B8D File Offset: 0x00002D8D
		public void Show()
		{
			this.Hidden = false;
			EventHandler stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this, EventArgs.Empty);
		}
	}
}
