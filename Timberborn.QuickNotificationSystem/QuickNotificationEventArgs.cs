using System;

namespace Timberborn.QuickNotificationSystem
{
	// Token: 0x02000007 RID: 7
	public class QuickNotificationEventArgs
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public string Text { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public bool IsWarning { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x0000210E File Offset: 0x0000030E
		public QuickNotificationEventArgs(string text, bool isWarning)
		{
			this.Text = text;
			this.IsWarning = isWarning;
		}
	}
}
