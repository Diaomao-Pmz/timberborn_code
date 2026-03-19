using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000010 RID: 16
	public class UnemployedWorkers
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002861 File Offset: 0x00000A61
		public bool AnyUnemployed
		{
			get
			{
				return this._unemployed.Count > 0;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002871 File Offset: 0x00000A71
		public Worker GetUnemployedWorker()
		{
			return this._unemployed.First<Worker>();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002880 File Offset: 0x00000A80
		public void AddWorker(Worker worker)
		{
			WorkRefuser component = worker.GetComponent<WorkRefuser>();
			this.UpdateUnemployedState(worker, component);
			worker.GotUnemployed += this.OnGotUnemployed;
			worker.GotEmployed += this.OnGotEmployed;
			component.RefusesWorkChanged += this.OnRefusesWorkChanged;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028D4 File Offset: 0x00000AD4
		public void RemoveWorker(Worker worker)
		{
			this._unemployed.Remove(worker);
			worker.GotUnemployed -= this.OnGotUnemployed;
			worker.GotEmployed -= this.OnGotEmployed;
			worker.GetComponent<WorkRefuser>().RefusesWorkChanged -= this.OnRefusesWorkChanged;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000292C File Offset: 0x00000B2C
		public void OnGotUnemployed(object sender, EventArgs e)
		{
			Worker worker = (Worker)sender;
			this.UpdateUnemployedState(worker, worker.GetComponent<WorkRefuser>());
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000294D File Offset: 0x00000B4D
		public void OnGotEmployed(object sender, EventArgs e)
		{
			this._unemployed.Remove((Worker)sender);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002964 File Offset: 0x00000B64
		public void OnRefusesWorkChanged(object sender, EventArgs e)
		{
			WorkRefuser workRefuser = (WorkRefuser)sender;
			this.UpdateUnemployedState(workRefuser.GetComponent<Worker>(), workRefuser);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002985 File Offset: 0x00000B85
		public void UpdateUnemployedState(Worker worker, WorkRefuser workRefuser)
		{
			if (!worker.Employed && !workRefuser.RefusesWork)
			{
				this._unemployed.Add(worker);
				return;
			}
			this._unemployed.Remove(worker);
		}

		// Token: 0x04000018 RID: 24
		public readonly HashSet<Worker> _unemployed = new HashSet<Worker>();
	}
}
