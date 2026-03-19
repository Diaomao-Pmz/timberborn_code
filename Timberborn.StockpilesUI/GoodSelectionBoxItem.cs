using System;
using Timberborn.CoreUI;
using Timberborn.ResourceCountingSystem;
using Timberborn.ResourceCountingSystemUI;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000007 RID: 7
	public class GoodSelectionBoxItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public VisualElement Root { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public GoodSelectionBoxItem(ContextualResourceCountingService contextualResourceCountingService, VisualElement root, string goodId, VisualElement counter)
		{
			this._contextualResourceCountingService = contextualResourceCountingService;
			this.Root = root;
			this._goodId = goodId;
			this._counter = counter;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002130 File Offset: 0x00000330
		public void Update()
		{
			ResourceCount contextualResourceCount = this._contextualResourceCountingService.GetContextualResourceCount(this._goodId);
			this._counter.SetHeightAsPercent(contextualResourceCount.FillRate);
			this._counter.parent.ToggleDisplayStyle(contextualResourceCount.AvailableStock > 0);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217B File Offset: 0x0000037B
		public void UpdateSelectedState(string selectedGoodId)
		{
			this.Root.EnableInClassList(GoodSelectionBoxItem.SelectedItemClass, selectedGoodId == this._goodId);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string SelectedItemClass = "selected-item";

		// Token: 0x0400000A RID: 10
		public readonly ContextualResourceCountingService _contextualResourceCountingService;

		// Token: 0x0400000B RID: 11
		public readonly string _goodId;

		// Token: 0x0400000C RID: 12
		public readonly VisualElement _counter;
	}
}
