using System;
using Timberborn.BatchControl;
using UnityEngine.UIElements;

namespace Timberborn.StockpilePriorityUISystem
{
	// Token: 0x02000004 RID: 4
	public class StockpilePriorityBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public StockpilePriorityBatchControlRowItem(VisualElement root, StockpilePriorityToggle stockpilePriorityToggle)
		{
			this.Root = root;
			this._stockpilePriorityToggle = stockpilePriorityToggle;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
		public void UpdateRowItem()
		{
			this._stockpilePriorityToggle.Update();
		}

		// Token: 0x04000007 RID: 7
		public readonly StockpilePriorityToggle _stockpilePriorityToggle;
	}
}
