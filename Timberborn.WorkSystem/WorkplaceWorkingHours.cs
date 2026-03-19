using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WorkerTypes;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200002C RID: 44
	public class WorkplaceWorkingHours : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0000504F File Offset: 0x0000324F
		public WorkplaceWorkingHours(WorkingHoursManager workingHoursManager, WorkerTypeService workerTypeService)
		{
			this._workingHoursManager = workingHoursManager;
			this._workerTypeService = workerTypeService;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005065 File Offset: 0x00003265
		public bool AreWorkingHours
		{
			get
			{
				return this._ignoreWorkingHours || this._workingHoursManager.AreWorkingHours;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000507C File Offset: 0x0000327C
		public void Awake()
		{
			this._workplaceWorkerType = base.GetComponent<WorkplaceWorkerType>();
			this._workplaceWorkerType.WorkerTypeChanged += this.OnWorkerTypeChanged;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000050A1 File Offset: 0x000032A1
		public void Start()
		{
			this.UpdateIgnoringWorkingHours(this._workplaceWorkerType.WorkerType);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000050B4 File Offset: 0x000032B4
		public void OnWorkerTypeChanged(object sender, WorkerTypeChangedEventArgs e)
		{
			this.UpdateIgnoringWorkingHours(e.CurrentWorkerType);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000050C4 File Offset: 0x000032C4
		public void UpdateIgnoringWorkingHours(string workerType)
		{
			WorkerTypeSpec workerTypeSpec = this._workerTypeService.GetWorkerTypeSpec(workerType);
			this._ignoreWorkingHours = workerTypeSpec.IgnoresWorkingHours;
		}

		// Token: 0x04000084 RID: 132
		public readonly WorkingHoursManager _workingHoursManager;

		// Token: 0x04000085 RID: 133
		public readonly WorkerTypeService _workerTypeService;

		// Token: 0x04000086 RID: 134
		public WorkplaceWorkerType _workplaceWorkerType;

		// Token: 0x04000087 RID: 135
		public bool _ignoreWorkingHours;
	}
}
