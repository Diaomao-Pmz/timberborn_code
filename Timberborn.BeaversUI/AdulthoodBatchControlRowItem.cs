using System;
using Timberborn.BatchControl;
using Timberborn.Beavers;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.BeaversUI
{
	// Token: 0x02000004 RID: 4
	public class AdulthoodBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public AdulthoodBatchControlRowItem(VisualElement root, Label progressLabel, Child child)
		{
			this.Root = root;
			this._progressLabel = progressLabel;
			this._child = child;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E5 File Offset: 0x000002E5
		public void UpdateRowItem()
		{
			this._progressLabel.text = NumberFormatter.FormatAsPercentFloored((double)this._child.GrowthProgress);
		}

		// Token: 0x04000007 RID: 7
		public readonly Label _progressLabel;

		// Token: 0x04000008 RID: 8
		public readonly Child _child;
	}
}
