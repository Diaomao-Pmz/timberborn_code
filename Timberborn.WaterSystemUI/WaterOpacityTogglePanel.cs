using System;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.KeyBindingSystemUI;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using Timberborn.WaterSystemRendering;
using UnityEngine.UIElements;

namespace Timberborn.WaterSystemUI
{
	// Token: 0x02000005 RID: 5
	public class WaterOpacityTogglePanel : ILoadableSingleton
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000024D6 File Offset: 0x000006D6
		public WaterOpacityTogglePanel(WaterOpacityService waterOpacityService, VisualElementLoader visualElementLoader, UILayout uiLayout, ITooltipRegistrar tooltipRegistrar, BindableToggleFactory bindableToggleFactory, KeyBindingTooltipFactory keyBindingTooltipFactory, EventBus eventBus)
		{
			this._waterOpacityService = waterOpacityService;
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._tooltipRegistrar = tooltipRegistrar;
			this._bindableToggleFactory = bindableToggleFactory;
			this._keyBindingTooltipFactory = keyBindingTooltipFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002514 File Offset: 0x00000714
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/SquareToggle");
			this._tooltipRegistrar.Register(this._root, new Func<string>(this.GetTooltip));
			this._toggle = UQueryExtensions.Q<Toggle>(this._root, "Toggle", null);
			this._toggle.AddToClassList(WaterOpacityTogglePanel.WaterOpacityClass);
			this._waterOpacityToggle = this._waterOpacityService.GetWaterOpacityToggle();
			this._eventBus.Register(this);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002598 File Offset: 0x00000798
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._bindableToggleFactory.CreateAndBind(this._toggle, WaterOpacityTogglePanel.ToggleWaterVisibilityKey, new Action<bool>(this.OnWaterToggled), () => this._waterOpacityToggle.Hidden);
			this._uiLayout.AddTopRightButton(this._root, 1);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000025E6 File Offset: 0x000007E6
		public string TooltipLocKey
		{
			get
			{
				if (!this._waterOpacityToggle.Hidden)
				{
					return WaterOpacityTogglePanel.HideWaterLocKey;
				}
				return WaterOpacityTogglePanel.ShowWaterLocKey;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002600 File Offset: 0x00000800
		public void OnWaterToggled(bool hideWater)
		{
			if (hideWater)
			{
				this._waterOpacityToggle.HideWater();
				return;
			}
			this._waterOpacityToggle.ShowWater();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000261C File Offset: 0x0000081C
		public string GetTooltip()
		{
			return this._keyBindingTooltipFactory.Create(this.TooltipLocKey, WaterOpacityTogglePanel.ToggleWaterVisibilityKey, null);
		}

		// Token: 0x0400000E RID: 14
		public static readonly string WaterOpacityClass = "square-toggle--water-opacity";

		// Token: 0x0400000F RID: 15
		public static readonly string ShowWaterLocKey = "WaterOpacity.Visibility.Show";

		// Token: 0x04000010 RID: 16
		public static readonly string HideWaterLocKey = "WaterOpacity.Visibility.Hide";

		// Token: 0x04000011 RID: 17
		public static readonly string ToggleWaterVisibilityKey = "ToggleWaterVisibility";

		// Token: 0x04000012 RID: 18
		public readonly WaterOpacityService _waterOpacityService;

		// Token: 0x04000013 RID: 19
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000014 RID: 20
		public readonly UILayout _uiLayout;

		// Token: 0x04000015 RID: 21
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000016 RID: 22
		public readonly BindableToggleFactory _bindableToggleFactory;

		// Token: 0x04000017 RID: 23
		public readonly KeyBindingTooltipFactory _keyBindingTooltipFactory;

		// Token: 0x04000018 RID: 24
		public readonly EventBus _eventBus;

		// Token: 0x04000019 RID: 25
		public WaterOpacityToggle _waterOpacityToggle;

		// Token: 0x0400001A RID: 26
		public VisualElement _root;

		// Token: 0x0400001B RID: 27
		public Toggle _toggle;
	}
}
