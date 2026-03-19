using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EnterableSystem;
using UnityEngine.UIElements;

namespace Timberborn.AttractionsUI
{
	// Token: 0x02000004 RID: 4
	public class AttractionBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem, IFinishableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C7 File Offset: 0x000002C7
		public AttractionBatchControlRowItem(VisualElement root, Label capacityLabel, Enterable enterable)
		{
			this.Root = root;
			this._capacityLabel = capacityLabel;
			this._enterable = enterable;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E4 File Offset: 0x000002E4
		public void UpdateRowItem()
		{
			this._capacityLabel.text = string.Format("{0} / {1}", this._enterable.NumberOfEnterersInside, this._enterable.EnterableSpec.CapacityFinished);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002120 File Offset: 0x00000320
		public void SetFinishedState(bool isFinished)
		{
			this.Root.ToggleDisplayStyle(isFinished);
		}

		// Token: 0x04000007 RID: 7
		public readonly Label _capacityLabel;

		// Token: 0x04000008 RID: 8
		public readonly Enterable _enterable;
	}
}
