using System;
using Timberborn.BatchControl;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200001B RID: 27
	public class WellbeingBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003C4F File Offset: 0x00001E4F
		public VisualElement Root { get; }

		// Token: 0x0600008B RID: 139 RVA: 0x00003C57 File Offset: 0x00001E57
		public WellbeingBatchControlRowItem(VisualElement root, WellbeingSummary wellbeingSummary)
		{
			this.Root = root;
			this._wellbeingSummary = wellbeingSummary;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003C6D File Offset: 0x00001E6D
		public void UpdateRowItem()
		{
			this._wellbeingSummary.UpdateContent();
		}

		// Token: 0x04000083 RID: 131
		public readonly WellbeingSummary _wellbeingSummary;
	}
}
