using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DwellingSystem;
using UnityEngine.UIElements;

namespace Timberborn.DwellingSystemUI
{
	// Token: 0x02000007 RID: 7
	public class DwellingBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem, IFinishableBatchControlRowItem
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022E3 File Offset: 0x000004E3
		public VisualElement Root { get; }

		// Token: 0x06000010 RID: 16 RVA: 0x000022EB File Offset: 0x000004EB
		public DwellingBatchControlRowItem(VisualElement root, Dwelling dwelling, Label info)
		{
			this.Root = root;
			this._dwelling = dwelling;
			this._info = info;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002308 File Offset: 0x00000508
		public void UpdateRowItem()
		{
			this._info.text = string.Format("{0} / {1}", this._dwelling.NumberOfDwellers, this._dwelling.TotalSlots);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000233F File Offset: 0x0000053F
		public void SetFinishedState(bool isFinished)
		{
			this.Root.ToggleDisplayStyle(isFinished);
		}

		// Token: 0x04000012 RID: 18
		public readonly Dwelling _dwelling;

		// Token: 0x04000013 RID: 19
		public readonly Label _info;
	}
}
