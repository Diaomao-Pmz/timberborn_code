using System;

namespace Timberborn.NotificationSystem
{
	// Token: 0x02000004 RID: 4
	public class Notification
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public string Description { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public Guid Subject { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public int Cycle { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D6 File Offset: 0x000002D6
		public int CycleDay { get; }

		// Token: 0x06000007 RID: 7 RVA: 0x000020DE File Offset: 0x000002DE
		public Notification(string description, Guid subject, int cycle, int cycleDay)
		{
			this.Description = description;
			this.Subject = subject;
			this.Cycle = cycle;
			this.CycleDay = cycleDay;
		}
	}
}
