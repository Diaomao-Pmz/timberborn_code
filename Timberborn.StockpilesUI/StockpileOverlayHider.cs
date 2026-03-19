using System;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000025 RID: 37
	public class StockpileOverlayHider : ILoadableSingleton
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000044C8 File Offset: 0x000026C8
		public StockpileOverlayHider(EventBus eventBus, StockpileOverlay stockpileOverlay)
		{
			this._eventBus = eventBus;
			this._stockpileOverlay = stockpileOverlay;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000044DE File Offset: 0x000026DE
		public void Load()
		{
			this._eventBus.Register(this);
			this._stockpileOverlayToggle = this._stockpileOverlay.GetStockpileOverlayToggle();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000044FD File Offset: 0x000026FD
		[OnEvent]
		public void OnUIVisibilityChanged(UIVisibilityChangedEvent uiVisibilityChangedEvent)
		{
			if (uiVisibilityChangedEvent.UIVisible)
			{
				this._stockpileOverlayToggle.ShowOverlay();
				return;
			}
			this._stockpileOverlayToggle.HideOverlay();
		}

		// Token: 0x0400009B RID: 155
		public readonly EventBus _eventBus;

		// Token: 0x0400009C RID: 156
		public readonly StockpileOverlay _stockpileOverlay;

		// Token: 0x0400009D RID: 157
		public StockpileOverlayToggle _stockpileOverlayToggle;
	}
}
