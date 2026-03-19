using System;

namespace Timberborn.QuickNotificationSystem
{
	// Token: 0x02000009 RID: 9
	public class QuickNotificationService
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000F RID: 15 RVA: 0x000022E0 File Offset: 0x000004E0
		// (remove) Token: 0x06000010 RID: 16 RVA: 0x00002318 File Offset: 0x00000518
		public event EventHandler<QuickNotificationEventArgs> AlertSent;

		// Token: 0x06000011 RID: 17 RVA: 0x0000234D File Offset: 0x0000054D
		public void SendNotification(string text)
		{
			this.SendNotification(text, false);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002357 File Offset: 0x00000557
		public void SendWarningNotification(string text)
		{
			this.SendNotification(text, true);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002361 File Offset: 0x00000561
		public void SendNotification(string text, bool isWaring)
		{
			EventHandler<QuickNotificationEventArgs> alertSent = this.AlertSent;
			if (alertSent == null)
			{
				return;
			}
			alertSent(this, new QuickNotificationEventArgs(text, isWaring));
		}
	}
}
