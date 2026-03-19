using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WorkerTypes;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200001A RID: 26
	public class WorkerWorkingHours : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00003589 File Offset: 0x00001789
		public WorkerWorkingHours(WorkerTypeService workerTypeService, WorkingHoursManager workingHoursManager)
		{
			this._workerTypeService = workerTypeService;
			this._workingHoursManager = workingHoursManager;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000359F File Offset: 0x0000179F
		public bool AreWorkingHours
		{
			get
			{
				return this._ignoreWorkingHours || this._workingHoursManager.AreWorkingHours;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000035B8 File Offset: 0x000017B8
		public void Awake()
		{
			string workerType = base.GetComponent<Worker>().WorkerType;
			WorkerTypeSpec workerTypeSpec = this._workerTypeService.GetWorkerTypeSpec(workerType);
			this._ignoreWorkingHours = workerTypeSpec.IgnoresWorkingHours;
		}

		// Token: 0x0400003C RID: 60
		public readonly WorkerTypeService _workerTypeService;

		// Token: 0x0400003D RID: 61
		public readonly WorkingHoursManager _workingHoursManager;

		// Token: 0x0400003E RID: 62
		public bool _ignoreWorkingHours;
	}
}
