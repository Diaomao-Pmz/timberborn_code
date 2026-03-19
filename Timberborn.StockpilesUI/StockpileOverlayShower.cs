using System;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000027 RID: 39
	public class StockpileOverlayShower : ILoadableSingleton, IInputProcessor
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000490A File Offset: 0x00002B0A
		public StockpileOverlayShower(StockpileOverlay stockpileOverlay, InputService inputService)
		{
			this._stockpileOverlay = stockpileOverlay;
			this._inputService = inputService;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004920 File Offset: 0x00002B20
		public void Load()
		{
			this._stockpileOverlayToggle = this._stockpileOverlay.GetStockpileOverlayToggle();
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000493F File Offset: 0x00002B3F
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyHeld(StockpileOverlayShower.ShowStockpileOverlayKey))
			{
				if (!this._isShown)
				{
					this.Enable();
				}
			}
			else if (this._isShown)
			{
				this.Disable();
			}
			return false;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004972 File Offset: 0x00002B72
		public void Enable()
		{
			this._isShown = true;
			this._stockpileOverlayToggle.EnableOverlay();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004986 File Offset: 0x00002B86
		public void Disable()
		{
			this._isShown = false;
			this._stockpileOverlayToggle.DisableOverlay();
		}

		// Token: 0x040000AD RID: 173
		public static readonly string ShowStockpileOverlayKey = "ShowStockpileOverlay";

		// Token: 0x040000AE RID: 174
		public readonly StockpileOverlay _stockpileOverlay;

		// Token: 0x040000AF RID: 175
		public readonly InputService _inputService;

		// Token: 0x040000B0 RID: 176
		public StockpileOverlayToggle _stockpileOverlayToggle;

		// Token: 0x040000B1 RID: 177
		public bool _isShown;
	}
}
