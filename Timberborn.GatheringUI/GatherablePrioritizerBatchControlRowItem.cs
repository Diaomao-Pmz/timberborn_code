using System;
using Timberborn.BatchControl;
using Timberborn.DropdownSystem;
using Timberborn.Gathering;
using UnityEngine.UIElements;

namespace Timberborn.GatheringUI
{
	// Token: 0x02000005 RID: 5
	public class GatherablePrioritizerBatchControlRowItem : IBatchControlRowItem, IClearableBatchControlRowItem
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000023F7 File Offset: 0x000005F7
		public VisualElement Root { get; }

		// Token: 0x0600000C RID: 12 RVA: 0x000023FF File Offset: 0x000005FF
		public GatherablePrioritizerBatchControlRowItem(VisualElement root, Dropdown dropdown, GatherablePrioritizer gatherablePrioritizer)
		{
			this.Root = root;
			this._dropdown = dropdown;
			this._gatherablePrioritizer = gatherablePrioritizer;
			this._gatherablePrioritizer.PrioritizedGatherableChanged += this.OnPrioritizedGatherableChanged;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002433 File Offset: 0x00000633
		public void ClearRowItem()
		{
			this._gatherablePrioritizer.PrioritizedGatherableChanged -= this.OnPrioritizedGatherableChanged;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000244C File Offset: 0x0000064C
		public void OnPrioritizedGatherableChanged(object sender, EventArgs e)
		{
			this._dropdown.UpdateSelectedValue();
		}

		// Token: 0x04000019 RID: 25
		public readonly Dropdown _dropdown;

		// Token: 0x0400001A RID: 26
		public readonly GatherablePrioritizer _gatherablePrioritizer;
	}
}
