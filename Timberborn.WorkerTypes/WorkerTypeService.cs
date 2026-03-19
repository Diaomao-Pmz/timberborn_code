using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.WorkerTypes
{
	// Token: 0x02000008 RID: 8
	public class WorkerTypeService : ILoadableSingleton
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002111 File Offset: 0x00000311
		public WorkerTypeService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212C File Offset: 0x0000032C
		public void Load()
		{
			foreach (WorkerTypeSpec workerTypeSpec in this._specService.GetSpecs<WorkerTypeSpec>())
			{
				this._workerTypes.Add(workerTypeSpec.Id, workerTypeSpec);
				foreach (string key in workerTypeSpec.BackwardCompatibleIds)
				{
					this._workerTypes.Add(key, workerTypeSpec);
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021BC File Offset: 0x000003BC
		public WorkerTypeSpec GetWorkerTypeSpec(string workerType)
		{
			return this._workerTypes[workerType];
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021CA File Offset: 0x000003CA
		public string GetWorkerType(string initialWorkerType)
		{
			return this.GetWorkerTypeSpec(initialWorkerType).Id;
		}

		// Token: 0x04000008 RID: 8
		public readonly ISpecService _specService;

		// Token: 0x04000009 RID: 9
		public readonly Dictionary<string, WorkerTypeSpec> _workerTypes = new Dictionary<string, WorkerTypeSpec>();
	}
}
