using System;

namespace Timberborn.CoreUI
{
	// Token: 0x02000057 RID: 87
	public class UIVisibilityChangedEvent
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005DC3 File Offset: 0x00003FC3
		public bool UIVisible { get; }

		// Token: 0x0600016C RID: 364 RVA: 0x00005DCB File Offset: 0x00003FCB
		public UIVisibilityChangedEvent(bool uiVisible)
		{
			this.UIVisible = uiVisible;
		}
	}
}
