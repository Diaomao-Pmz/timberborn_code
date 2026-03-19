using System;

namespace Timberborn.NotificationSystem
{
	// Token: 0x02000006 RID: 6
	public class NotificationEventArgs
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021E5 File Offset: 0x000003E5
		public Notification Notification { get; }

		// Token: 0x0600000E RID: 14 RVA: 0x000021ED File Offset: 0x000003ED
		public NotificationEventArgs(Notification notification)
		{
			this.Notification = notification;
		}
	}
}
