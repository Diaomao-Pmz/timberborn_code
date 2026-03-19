using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;
using Timberborn.WorkSystem;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x0200000E RID: 14
	public class WorkerOutfitService : ILoadableSingleton
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public WorkerOutfitService(FactionService factionService, ISpecService specService)
		{
			this._factionService = factionService;
			this._specService = specService;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B08 File Offset: 0x00000D08
		public void Load()
		{
			this._workerOutfitSpecs = (from s in this._specService.GetSpecs<WorkerOutfitSpec>()
			where s.FactionId == this._factionService.Current.Id
			select s).ToDictionary((WorkerOutfitSpec s) => WorkerOutfitService.GetSpecKey(s.Id, s.WorkerType), (WorkerOutfitSpec s) => s);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B7C File Offset: 0x00000D7C
		public bool TryGetOutfitSpec(Worker worker, out WorkerOutfitSpec workerOutfitSpec)
		{
			workerOutfitSpec = null;
			WorkplaceWorkerOutfitSpec component = worker.Workplace.GetComponent<WorkplaceWorkerOutfitSpec>();
			if (component != null)
			{
				string workerOutfit = component.WorkerOutfit;
				if (!string.IsNullOrWhiteSpace(workerOutfit))
				{
					int specKey = WorkerOutfitService.GetSpecKey(workerOutfit, worker.WorkerType);
					return this._workerOutfitSpecs.TryGetValue(specKey, out workerOutfitSpec);
				}
			}
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BCC File Offset: 0x00000DCC
		public static int GetSpecKey(string id, string workerType)
		{
			return id.GetHashCode() * 397 ^ workerType.GetHashCode();
		}

		// Token: 0x0400001D RID: 29
		public readonly FactionService _factionService;

		// Token: 0x0400001E RID: 30
		public readonly ISpecService _specService;

		// Token: 0x0400001F RID: 31
		public Dictionary<int, WorkerOutfitSpec> _workerOutfitSpecs;
	}
}
