using System;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x0200000C RID: 12
	public class WorkerOutfitChangedEventArgs
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000029C7 File Offset: 0x00000BC7
		public WorkerOutfitSpec WorkerOutfitSpec { get; }

		// Token: 0x0600003B RID: 59 RVA: 0x000029CF File Offset: 0x00000BCF
		public WorkerOutfitChangedEventArgs(WorkerOutfitSpec workerOutfitSpec)
		{
			this.WorkerOutfitSpec = workerOutfitSpec;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000029DE File Offset: 0x00000BDE
		public static WorkerOutfitChangedEventArgs None
		{
			get
			{
				return new WorkerOutfitChangedEventArgs(null);
			}
		}
	}
}
