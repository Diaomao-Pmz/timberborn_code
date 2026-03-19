using System;
using Timberborn.BatchControl;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000022 RID: 34
	public class WorkplaceWorkerTypeBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003D52 File Offset: 0x00001F52
		public VisualElement Root { get; }

		// Token: 0x060000A8 RID: 168 RVA: 0x00003D5A File Offset: 0x00001F5A
		public WorkplaceWorkerTypeBatchControlRowItem(VisualElement root, WorkerTypeToggle workerTypeToggle)
		{
			this.Root = root;
			this._workerTypeToggle = workerTypeToggle;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003D70 File Offset: 0x00001F70
		public void UpdateRowItem()
		{
			this._workerTypeToggle.Update();
		}

		// Token: 0x0400009F RID: 159
		public readonly WorkerTypeToggle _workerTypeToggle;
	}
}
