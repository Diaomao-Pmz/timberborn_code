using System;
using Timberborn.BatchControl;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200000F RID: 15
	public class WorkplaceBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002BD3 File Offset: 0x00000DD3
		public VisualElement Root { get; }

		// Token: 0x06000044 RID: 68 RVA: 0x00002BDB File Offset: 0x00000DDB
		public WorkplaceBatchControlRowItem(VisualElement root, Workplace workplace, Label info, Button increase, Button decrease)
		{
			this.Root = root;
			this._workplace = workplace;
			this._info = info;
			this._increase = increase;
			this._decrease = decrease;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C08 File Offset: 0x00000E08
		public void UpdateRowItem()
		{
			int numberOfAssignedWorkers = this._workplace.NumberOfAssignedWorkers;
			int desiredWorkers = this._workplace.DesiredWorkers;
			int maxWorkers = this._workplace.MaxWorkers;
			this._info.text = string.Format("{0} / {1}", numberOfAssignedWorkers, desiredWorkers);
			this._decrease.SetEnabled(desiredWorkers > 1);
			this._increase.SetEnabled(desiredWorkers < maxWorkers);
		}

		// Token: 0x04000046 RID: 70
		public readonly Workplace _workplace;

		// Token: 0x04000047 RID: 71
		public readonly Label _info;

		// Token: 0x04000048 RID: 72
		public readonly Button _increase;

		// Token: 0x04000049 RID: 73
		public readonly Button _decrease;
	}
}
