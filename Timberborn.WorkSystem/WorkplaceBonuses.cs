using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000024 RID: 36
	public class WorkplaceBonuses : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004151 File Offset: 0x00002351
		public ImmutableArray<BonusSpec> WorkerBonuses
		{
			get
			{
				return this._workplaceBonusesSpec.WorkerBonuses;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000415E File Offset: 0x0000235E
		public void Awake()
		{
			this._workplaceBonusesSpec = base.GetComponent<WorkplaceBonusesSpec>();
			Workplace component = base.GetComponent<Workplace>();
			component.WorkerAssigned += this.OnWorkerAssigned;
			component.WorkerUnassigned += this.OnWorkerUnassigned;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004195 File Offset: 0x00002395
		public void OnWorkerAssigned(object sender, WorkerChangedEventArgs e)
		{
			this.AddBonuses(e.Worker);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000041A3 File Offset: 0x000023A3
		public void OnWorkerUnassigned(object sender, WorkerChangedEventArgs e)
		{
			this.RemoveBonuses(e.Worker);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000041B1 File Offset: 0x000023B1
		public void AddBonuses(Worker worker)
		{
			worker.GetComponent<BonusManager>().AddBonuses(this.WorkerBonuses);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000041C9 File Offset: 0x000023C9
		public void RemoveBonuses(Worker worker)
		{
			worker.GetComponent<BonusManager>().RemoveBonuses(this.WorkerBonuses);
		}

		// Token: 0x0400005D RID: 93
		public WorkplaceBonusesSpec _workplaceBonusesSpec;
	}
}
