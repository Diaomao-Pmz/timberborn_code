using System;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000016 RID: 22
	public class WorkerChangedEventArgs
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000030F4 File Offset: 0x000012F4
		public Worker Worker { get; }

		// Token: 0x0600007B RID: 123 RVA: 0x000030FC File Offset: 0x000012FC
		public WorkerChangedEventArgs(Worker worker)
		{
			this.Worker = worker;
		}
	}
}
