using System;
using Timberborn.CoreUI;
using Timberborn.ResourceCountingSystem;
using Timberborn.ResourceCountingSystemUI;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.TopBarSystem
{
	// Token: 0x02000009 RID: 9
	public class TopBarCounterRow : ITopBarCounter
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000023E5 File Offset: 0x000005E5
		public TopBarCounterRow(ContextualResourceCountingService contextualResourceCountingService, string goodId, VisualElement root, Label counter, VisualElement fillGauge, bool alwaysVisible = false)
		{
			this._contextualResourceCountingService = contextualResourceCountingService;
			this._root = root;
			this._goodId = goodId;
			this._counter = counter;
			this._fillGauge = fillGauge;
			this._alwaysVisible = alwaysVisible;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002421 File Offset: 0x00000621
		public void UpdateValues()
		{
			this.UpdateAndGetStock();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000242C File Offset: 0x0000062C
		public int UpdateAndGetStock()
		{
			ResourceCount contextualResourceCount = this._contextualResourceCountingService.GetContextualResourceCount(this._goodId);
			this._fillGauge.SetHeightAsPercent(contextualResourceCount.FillRate);
			int availableStock = contextualResourceCount.AvailableStock;
			if (this._previousAmount != availableStock)
			{
				this._counter.text = NumberFormatter.Format(availableStock);
				this._previousAmount = availableStock;
			}
			this._root.ToggleDisplayStyle(this._alwaysVisible || contextualResourceCount.AllStock > 0);
			return availableStock;
		}

		// Token: 0x04000015 RID: 21
		public readonly ContextualResourceCountingService _contextualResourceCountingService;

		// Token: 0x04000016 RID: 22
		public readonly VisualElement _root;

		// Token: 0x04000017 RID: 23
		public readonly string _goodId;

		// Token: 0x04000018 RID: 24
		public readonly Label _counter;

		// Token: 0x04000019 RID: 25
		public readonly VisualElement _fillGauge;

		// Token: 0x0400001A RID: 26
		public readonly bool _alwaysVisible;

		// Token: 0x0400001B RID: 27
		public int _previousAmount = -1;
	}
}
