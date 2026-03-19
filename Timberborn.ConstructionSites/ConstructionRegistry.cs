using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Timberborn.PrioritySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200000D RID: 13
	public class ConstructionRegistry : ILoadableSingleton
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002997 File Offset: 0x00000B97
		public ReadOnlyCollection<ConstructionJob> GetJobs(Priority priority)
		{
			return this._jobsAsReadOnly[priority];
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029A8 File Offset: 0x00000BA8
		public void Load()
		{
			foreach (Priority key in Priorities.Ascending)
			{
				SortedList<int, ConstructionJob> sortedList = new SortedList<int, ConstructionJob>();
				this._jobs[key] = sortedList;
				this._jobsAsReadOnly[key] = new ReadOnlyCollection<ConstructionJob>(sortedList.Values);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029FD File Offset: 0x00000BFD
		public void AddJob(ConstructionJob job, Priority priority, int order)
		{
			this._jobs[priority].Add(order, job);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A12 File Offset: 0x00000C12
		public void RemoveJob(Priority priority, int order)
		{
			this._jobs[priority].Remove(order);
		}

		// Token: 0x04000027 RID: 39
		public readonly Dictionary<Priority, SortedList<int, ConstructionJob>> _jobs = new Dictionary<Priority, SortedList<int, ConstructionJob>>();

		// Token: 0x04000028 RID: 40
		public readonly Dictionary<Priority, ReadOnlyCollection<ConstructionJob>> _jobsAsReadOnly = new Dictionary<Priority, ReadOnlyCollection<ConstructionJob>>();
	}
}
