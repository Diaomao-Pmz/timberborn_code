using System;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000016 RID: 22
	public class BatchControlRowGroupChildrenCounter : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000033B9 File Offset: 0x000015B9
		public VisualElement Root { get; }

		// Token: 0x0600006D RID: 109 RVA: 0x000033C1 File Offset: 0x000015C1
		public BatchControlRowGroupChildrenCounter(VisualElement root, Label counter)
		{
			this.Root = root;
			this._counter = counter;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000033D7 File Offset: 0x000015D7
		public void SetRowGroup(BatchControlRowGroup rowGroup)
		{
			this._rowGroup = rowGroup;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000033E0 File Offset: 0x000015E0
		public void UpdateRowItem()
		{
			this._counter.text = " (" + this._rowGroup.VisibleChildrenCount.ToString() + ")";
		}

		// Token: 0x0400004D RID: 77
		public readonly Label _counter;

		// Token: 0x0400004E RID: 78
		public BatchControlRowGroup _rowGroup;
	}
}
