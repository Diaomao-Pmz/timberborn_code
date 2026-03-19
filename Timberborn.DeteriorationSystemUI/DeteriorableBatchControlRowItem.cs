using System;
using Timberborn.BatchControl;
using Timberborn.DeteriorationSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.DeteriorationSystemUI
{
	// Token: 0x02000004 RID: 4
	public class DeteriorableBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public DeteriorableBatchControlRowItem(VisualElement root, Label progressLabel, Deteriorable deteriorable)
		{
			this.Root = root;
			this._progressLabel = progressLabel;
			this._deteriorable = deteriorable;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E3 File Offset: 0x000002E3
		public void UpdateRowItem()
		{
			this._progressLabel.text = NumberFormatter.FormatAsPercentFloored((double)this._deteriorable.DeteriorationProgress);
		}

		// Token: 0x04000007 RID: 7
		public readonly Label _progressLabel;

		// Token: 0x04000008 RID: 8
		public readonly Deteriorable _deteriorable;
	}
}
