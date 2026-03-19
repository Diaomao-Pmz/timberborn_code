using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.PopulationStatisticsSystem;
using Timberborn.WorkSystem;

namespace Timberborn.PopulationWorkStatistics
{
	// Token: 0x0200000B RID: 11
	public class WorkRefuserRegistry
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000276C File Offset: 0x0000096C
		public WorkRefusingStatistics GetWorkRefusingStatistics(string workerType)
		{
			WorkRefuserRegistry.WorkRefuserAssigner workRefuserAssigner;
			if (this._workerTypeWorkRefusers.TryGetValue(workerType, out workRefuserAssigner))
			{
				return new WorkRefusingStatistics(workRefuserAssigner.NumberOfRefusingWorkers, workRefuserAssigner.NumberOfNotRefusingWorkers);
			}
			return new WorkRefusingStatistics(0, 0);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027A2 File Offset: 0x000009A2
		public void AddWorkRefuser(WorkRefuser workRefuser)
		{
			this.GetWorkRefuserAssigner(workRefuser).ReassignWorkRefuser(workRefuser);
			workRefuser.RefusesWorkChanged += this.OnRefusesWorkChanged;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027C3 File Offset: 0x000009C3
		public void RemoveWorkRefuser(WorkRefuser workRefuser)
		{
			this.GetWorkRefuserAssigner(workRefuser).RemoveWorkRefuser(workRefuser);
			workRefuser.RefusesWorkChanged -= this.OnRefusesWorkChanged;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027E4 File Offset: 0x000009E4
		public WorkRefuserRegistry.WorkRefuserAssigner GetWorkRefuserAssigner(WorkRefuser workRefuser)
		{
			string workerType = workRefuser.GetComponent<Worker>().WorkerType;
			return this._workerTypeWorkRefusers.GetOrAdd(workerType);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000280C File Offset: 0x00000A0C
		public void OnRefusesWorkChanged(object sender, EventArgs e)
		{
			WorkRefuser workRefuser = (WorkRefuser)sender;
			this.GetWorkRefuserAssigner(workRefuser).ReassignWorkRefuser(workRefuser);
		}

		// Token: 0x04000013 RID: 19
		public readonly Dictionary<string, WorkRefuserRegistry.WorkRefuserAssigner> _workerTypeWorkRefusers = new Dictionary<string, WorkRefuserRegistry.WorkRefuserAssigner>();

		// Token: 0x0200000C RID: 12
		public class WorkRefuserAssigner
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000034 RID: 52 RVA: 0x00002840 File Offset: 0x00000A40
			public int NumberOfRefusingWorkers
			{
				get
				{
					return this._refusingWorkRefusers.Count;
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000035 RID: 53 RVA: 0x0000284D File Offset: 0x00000A4D
			public int NumberOfNotRefusingWorkers
			{
				get
				{
					return this._notRefusingWorkRefusers.Count;
				}
			}

			// Token: 0x06000036 RID: 54 RVA: 0x0000285A File Offset: 0x00000A5A
			public void ReassignWorkRefuser(WorkRefuser workRefuser)
			{
				this._refusingWorkRefusers.Remove(workRefuser);
				this._notRefusingWorkRefusers.Remove(workRefuser);
				if (workRefuser.RefusesWork)
				{
					this._refusingWorkRefusers.Add(workRefuser);
					return;
				}
				this._notRefusingWorkRefusers.Add(workRefuser);
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002899 File Offset: 0x00000A99
			public void RemoveWorkRefuser(WorkRefuser workRefuser)
			{
				this._refusingWorkRefusers.Remove(workRefuser);
				this._notRefusingWorkRefusers.Remove(workRefuser);
			}

			// Token: 0x04000014 RID: 20
			public readonly HashSet<WorkRefuser> _refusingWorkRefusers = new HashSet<WorkRefuser>();

			// Token: 0x04000015 RID: 21
			public readonly HashSet<WorkRefuser> _notRefusingWorkRefusers = new HashSet<WorkRefuser>();
		}
	}
}
