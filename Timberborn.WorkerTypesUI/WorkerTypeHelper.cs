using System;
using Timberborn.WorkerTypes;

namespace Timberborn.WorkerTypesUI
{
	// Token: 0x02000004 RID: 4
	public class WorkerTypeHelper
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public WorkerTypeHelper(WorkerTypeService workerTypeService)
		{
			this._workerTypeService = workerTypeService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CA File Offset: 0x000002CA
		public string GetDisallowedWorkerText(string workerType)
		{
			return this._workerTypeService.GetWorkerTypeSpec(workerType).WorkerOnlyText.Value;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E2 File Offset: 0x000002E2
		public bool IsBeaverWorkerType(string workerType)
		{
			return workerType == WorkerTypeHelper.BeaverWorkerType;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EF File Offset: 0x000002EF
		public bool IsBotWorkerType(string workerType)
		{
			return workerType == WorkerTypeHelper.BotWorkerType;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FC File Offset: 0x000002FC
		public string GetBeaverWorkerTypeDisplayText()
		{
			return this.GetDisplayText(WorkerTypeHelper.BeaverWorkerType);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002109 File Offset: 0x00000309
		public string GetBotWorkerTypeDisplayText()
		{
			return this.GetDisplayText(WorkerTypeHelper.BotWorkerType);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002116 File Offset: 0x00000316
		public string GetDisplayText(string workerType)
		{
			return this._workerTypeService.GetWorkerTypeSpec(workerType).DisplayName.Value;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string BeaverWorkerType = "Beaver";

		// Token: 0x04000007 RID: 7
		public static readonly string BotWorkerType = "Bot";

		// Token: 0x04000008 RID: 8
		public readonly WorkerTypeService _workerTypeService;
	}
}
