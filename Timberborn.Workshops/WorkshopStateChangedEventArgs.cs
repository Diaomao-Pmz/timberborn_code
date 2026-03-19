using System;

namespace Timberborn.Workshops
{
	// Token: 0x02000030 RID: 48
	public class WorkshopStateChangedEventArgs
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005A82 File Offset: 0x00003C82
		public bool CurrentlyProducing { get; }

		// Token: 0x0600017F RID: 383 RVA: 0x00005A8A File Offset: 0x00003C8A
		public WorkshopStateChangedEventArgs(bool currentlyProducing)
		{
			this.CurrentlyProducing = currentlyProducing;
		}
	}
}
