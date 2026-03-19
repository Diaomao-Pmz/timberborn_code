using System;
using Timberborn.Common;

namespace Timberborn.MultithreadingAnalysis
{
	// Token: 0x02000006 RID: 6
	public class Snapshot
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
		public int Ticks { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002117 File Offset: 0x00000317
		public ReadOnlyList<TaskSample> TaskSamples { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000211F File Offset: 0x0000031F
		public ReadOnlyList<Marker> Markers { get; }

		// Token: 0x0600000C RID: 12 RVA: 0x00002127 File Offset: 0x00000327
		public Snapshot(int ticks, ReadOnlyList<TaskSample> taskSamples, ReadOnlyList<Marker> markers)
		{
			this.Ticks = ticks;
			this.TaskSamples = taskSamples;
			this.Markers = markers;
		}
	}
}
