using System;
using System.Threading;

namespace Timberborn.MultithreadingAnalysis
{
	// Token: 0x02000004 RID: 4
	public readonly struct Marker
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public string Id { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public long Timestamp { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public Thread Thread { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020D6 File Offset: 0x000002D6
		public Marker(string id, long timestamp, Thread thread)
		{
			this.Id = id;
			this.Timestamp = timestamp;
			this.Thread = thread;
		}
	}
}
