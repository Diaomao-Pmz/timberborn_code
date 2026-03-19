using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.PrioritySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x0200001D RID: 29
	public class DemolishJobs : ILoadableSingleton
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000038C7 File Offset: 0x00001AC7
		public ReadOnlyList<DemolishJob> GetJobs(Priority priority)
		{
			return this._jobs[priority].AsReadOnlyList<DemolishJob>();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000038DC File Offset: 0x00001ADC
		public void Load()
		{
			foreach (Priority key in Priorities.Ascending)
			{
				this._jobs[key] = new List<DemolishJob>();
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003918 File Offset: 0x00001B18
		public void AddJob(DemolishJob job, Priority priority)
		{
			this._jobs[priority].Add(job);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000392C File Offset: 0x00001B2C
		public void RemoveJob(DemolishJob job, Priority priority)
		{
			this._jobs[priority].Remove(job);
		}

		// Token: 0x04000041 RID: 65
		public readonly Dictionary<Priority, List<DemolishJob>> _jobs = new Dictionary<Priority, List<DemolishJob>>();
	}
}
