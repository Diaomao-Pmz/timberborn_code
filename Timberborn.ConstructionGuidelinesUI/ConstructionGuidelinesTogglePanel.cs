using System;
using Timberborn.ConstructionGuidelines;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.KeyBindingSystemUI;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.ConstructionGuidelinesUI
{
	// Token: 0x02000004 RID: 4
	public class ConstructionGuidelinesTogglePanel : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public ConstructionGuidelinesTogglePanel(ConstructionGuidelinesRenderingService constructionGuidelinesRenderingService, VisualElementLoader visualElementLoader, UILayout uiLayout, ITooltipRegistrar tooltipRegistrar, BindableToggleFactory bindableToggleFactory, KeyBindingTooltipFactory keyBindingTooltipFactory, EventBus eventBus)
		{
			this._constructionGuidelinesRenderingService = constructionGuidelinesRenderingService;
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._tooltipRegistrar = tooltipRegistrar;
			this._bindableToggleFactory = bindableToggleFactory;
			this._keyBindingTooltipFactory = keyBindingTooltipFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/SquareToggle");
			this._tooltipRegistrar.Register(this._root, new Func<string>(this.GetTooltip));
			this._toggle = UQueryExtensions.Q<Toggle>(this._root, "Toggle", null);
			this._toggle.AddToClassList(ConstructionGuidelinesTogglePanel.ConstructionGuidelinesClass);
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002170 File Offset: 0x00000370
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._bindableToggleFactory.CreateAndBind(this._toggle, ConstructionGuidelinesTogglePanel.ToggleGuidelinesKey, new Action<bool>(this.OnGridToggled), () => this._enabled);
			this._uiLayout.AddTopRightButton(this._root, 3);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021BE File Offset: 0x000003BE
		public void OnGridToggled(bool toggleState)
		{
			if (toggleState)
			{
				this._constructionGuidelinesRenderingService.EnableGuidelines();
				return;
			}
			this._constructionGuidelinesRenderingService.DisableGuidelines();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021DC File Offset: 0x000003DC
		public string GetTooltip()
		{
			string headerLocKey = this._enabled ? ConstructionGuidelinesTogglePanel.HideGridRenderingLocKey : ConstructionGuidelinesTogglePanel.ShowGridRenderingLocKey;
			return this._keyBindingTooltipFactory.Create(headerLocKey, ConstructionGuidelinesTogglePanel.ToggleGuidelinesKey, ConstructionGuidelinesTogglePanel.ShowGuidelinesKey);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ConstructionGuidelinesClass = "square-toggle--construction-guidelines";

		// Token: 0x04000007 RID: 7
		public static readonly string ShowGridRenderingLocKey = "GridRendering.Visibility.Show";

		// Token: 0x04000008 RID: 8
		public static readonly string HideGridRenderingLocKey = "GridRendering.Visibility.Hide";

		// Token: 0x04000009 RID: 9
		public static readonly string ToggleGuidelinesKey = "ToggleGuidelines";

		// Token: 0x0400000A RID: 10
		public static readonly string ShowGuidelinesKey = "ShowGuidelines";

		// Token: 0x0400000B RID: 11
		public readonly ConstructionGuidelinesRenderingService _constructionGuidelinesRenderingService;

		// Token: 0x0400000C RID: 12
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000D RID: 13
		public readonly UILayout _uiLayout;

		// Token: 0x0400000E RID: 14
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000F RID: 15
		public readonly BindableToggleFactory _bindableToggleFactory;

		// Token: 0x04000010 RID: 16
		public readonly KeyBindingTooltipFactory _keyBindingTooltipFactory;

		// Token: 0x04000011 RID: 17
		public readonly EventBus _eventBus;

		// Token: 0x04000012 RID: 18
		public VisualElement _root;

		// Token: 0x04000013 RID: 19
		public bool _inConstructionMode;

		// Token: 0x04000014 RID: 20
		public Toggle _toggle;

		// Token: 0x04000015 RID: 21
		public bool _enabled;
	}
}
