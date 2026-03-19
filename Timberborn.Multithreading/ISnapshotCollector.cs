using System;

namespace Timberborn.Multithreading
{
	// Token: 0x02000008 RID: 8
	public interface ISnapshotCollector
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20
		bool IsCollecting { get; }

		// Token: 0x06000015 RID: 21
		void AddTaskSample(int run, int totalRuns, long startTimestamp, long endTimestamp, Type type);

		// Token: 0x06000016 RID: 22
		void AddMarker(string id);
	}
}
