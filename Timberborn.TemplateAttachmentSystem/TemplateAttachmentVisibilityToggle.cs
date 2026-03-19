using System;

namespace Timberborn.TemplateAttachmentSystem
{
	// Token: 0x0200000E RID: 14
	public class TemplateAttachmentVisibilityToggle
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000046 RID: 70 RVA: 0x00002A1C File Offset: 0x00000C1C
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x00002A54 File Offset: 0x00000C54
		public event EventHandler VisibilityChanged;

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002A89 File Offset: 0x00000C89
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002A91 File Offset: 0x00000C91
		public bool IsVisible { get; private set; }

		// Token: 0x0600004A RID: 74 RVA: 0x00002A9A File Offset: 0x00000C9A
		public void Show()
		{
			this.IsVisible = true;
			EventHandler visibilityChanged = this.VisibilityChanged;
			if (visibilityChanged == null)
			{
				return;
			}
			visibilityChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AB9 File Offset: 0x00000CB9
		public void Hide()
		{
			this.IsVisible = false;
			EventHandler visibilityChanged = this.VisibilityChanged;
			if (visibilityChanged == null)
			{
				return;
			}
			visibilityChanged(this, EventArgs.Empty);
		}
	}
}
