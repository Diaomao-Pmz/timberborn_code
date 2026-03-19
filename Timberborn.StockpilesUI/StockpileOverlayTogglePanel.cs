using System;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.KeyBindingSystemUI;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000029 RID: 41
	public class StockpileOverlayTogglePanel : ILoadableSingleton
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00004AB3 File Offset: 0x00002CB3
		public StockpileOverlayTogglePanel(StockpileOverlay stockpileOverlay, VisualElementLoader visualElementLoader, UILayout uiLayout, ITooltipRegistrar tooltipRegistrar, BindableToggleFactory bindableToggleFactory, KeyBindingTooltipFactory keyBindingTooltipFactory, EventBus eventBus)
		{
			this._stockpileOverlay = stockpileOverlay;
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._tooltipRegistrar = tooltipRegistrar;
			this._bindableToggleFactory = bindableToggleFactory;
			this._keyBindingTooltipFactory = keyBindingTooltipFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/SquareToggle");
			this._tooltipRegistrar.Register(this._root, new Func<string>(this.GetTooltip));
			Toggle toggle = UQueryExtensions.Q<Toggle>(this._root, "Toggle", null);
			toggle.AddToClassList(StockpileOverlayTogglePanel.StockpileOverlayClass);
			this._bindableToggleFactory.CreateAndBind(toggle, StockpileOverlayTogglePanel.ToggleStockpileOverlayKey, new Action<bool>(this.OnOverlayToggled), () => this._enabled);
			this._stockpileOverlayToggle = this._stockpileOverlay.GetStockpileOverlayToggle();
			this._eventBus.Register(this);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004B94 File Offset: 0x00002D94
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopRightButton(this._root, 2);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004BA8 File Offset: 0x00002DA8
		public string TooltipLocKey
		{
			get
			{
				if (!this._enabled)
				{
					return StockpileOverlayTogglePanel.ShowOverlayLocKey;
				}
				return StockpileOverlayTogglePanel.HideOverlayLocKey;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004BBD File Offset: 0x00002DBD
		public void OnOverlayToggled(bool showOverlay)
		{
			if (showOverlay)
			{
				this._stockpileOverlayToggle.EnableOverlay();
				this._enabled = true;
				return;
			}
			this._stockpileOverlayToggle.DisableOverlay();
			this._enabled = false;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004BE7 File Offset: 0x00002DE7
		public string GetTooltip()
		{
			return this._keyBindingTooltipFactory.Create(this.TooltipLocKey, StockpileOverlayTogglePanel.ToggleStockpileOverlayKey, StockpileOverlayTogglePanel.ShowStockpileOverlayKey);
		}

		// Token: 0x040000B5 RID: 181
		public static readonly string StockpileOverlayClass = "square-toggle--stockpile-overlay";

		// Token: 0x040000B6 RID: 182
		public static readonly string ShowOverlayLocKey = "Inventory.StockpileOverlay.Show";

		// Token: 0x040000B7 RID: 183
		public static readonly string HideOverlayLocKey = "Inventory.StockpileOverlay.Hide";

		// Token: 0x040000B8 RID: 184
		public static readonly string ToggleStockpileOverlayKey = "ToggleStockpileOverlay";

		// Token: 0x040000B9 RID: 185
		public static readonly string ShowStockpileOverlayKey = "ShowStockpileOverlay";

		// Token: 0x040000BA RID: 186
		public readonly StockpileOverlay _stockpileOverlay;

		// Token: 0x040000BB RID: 187
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000BC RID: 188
		public readonly UILayout _uiLayout;

		// Token: 0x040000BD RID: 189
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x040000BE RID: 190
		public readonly BindableToggleFactory _bindableToggleFactory;

		// Token: 0x040000BF RID: 191
		public readonly KeyBindingTooltipFactory _keyBindingTooltipFactory;

		// Token: 0x040000C0 RID: 192
		public readonly EventBus _eventBus;

		// Token: 0x040000C1 RID: 193
		public StockpileOverlayToggle _stockpileOverlayToggle;

		// Token: 0x040000C2 RID: 194
		public bool _enabled;

		// Token: 0x040000C3 RID: 195
		public VisualElement _root;
	}
}
