using System;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000008 RID: 8
	public class WorkerTypeChangedEventArgs
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public string PreviousWorkerType { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public string CurrentWorkerType { get; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002118 File Offset: 0x00000318
		public WorkerTypeChangedEventArgs(string previousWorkerType, string currentWorkerType)
		{
			this.PreviousWorkerType = previousWorkerType;
			this.CurrentWorkerType = currentWorkerType;
		}
	}
}
