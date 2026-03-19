using System;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.MapEditorNaturalResources;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorNaturalResourcesUI
{
	// Token: 0x0200000D RID: 13
	public class NaturalResourceLayerToggle : ILoadableSingleton
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002925 File Offset: 0x00000B25
		public NaturalResourceLayerToggle(NaturalResourceLayerService naturalResourceLayerService, VisualElementLoader visualElementLoader, UILayout uiLayout, ITooltipRegistrar tooltipRegistrar, BindableToggleFactory bindableToggleFactory, EventBus eventBus)
		{
			this._naturalResourceLayerService = naturalResourceLayerService;
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._tooltipRegistrar = tooltipRegistrar;
			this._bindableToggleFactory = bindableToggleFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000295C File Offset: 0x00000B5C
		public void Load()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/SquareToggle");
			this._tooltipRegistrar.RegisterLocalizable(visualElement, () => this.TooltipLocKey);
			Toggle toggle = UQueryExtensions.Q<Toggle>(visualElement, "Toggle", null);
			toggle.AddToClassList(NaturalResourceLayerToggle.NaturalResourceLayerClass);
			this._bindableToggle = this._bindableToggleFactory.CreateAndBind(toggle, NaturalResourceLayerToggle.ToggleNaturalResourcesKey, new Action<bool>(this.ToggleNaturalResourcesLayer), () => this._naturalResourceLayerService.Enabled);
			this._uiLayout.AddTopRightButton(visualElement, 3);
			this._eventBus.Register(this);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029F2 File Offset: 0x00000BF2
		[OnEvent]
		public void OnNaturalResourceLayerChanged(NaturalResourceLayerChangedEvent naturalResourceLayerChangedEvent)
		{
			this._bindableToggle.Update();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000029FF File Offset: 0x00000BFF
		public string TooltipLocKey
		{
			get
			{
				if (!this._naturalResourceLayerService.Enabled)
				{
					return NaturalResourceLayerToggle.ShowNaturalResourcesLocKey;
				}
				return NaturalResourceLayerToggle.HideNaturalResourcesLocKey;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A19 File Offset: 0x00000C19
		public void ToggleNaturalResourcesLayer(bool enableResources)
		{
			if (enableResources)
			{
				this._naturalResourceLayerService.Enable();
				return;
			}
			this._naturalResourceLayerService.Disable();
		}

		// Token: 0x04000029 RID: 41
		public static readonly string NaturalResourceLayerClass = "square-toggle--natural-resources";

		// Token: 0x0400002A RID: 42
		public static readonly string ShowNaturalResourcesLocKey = "NaturalResources.Visibility.Show";

		// Token: 0x0400002B RID: 43
		public static readonly string HideNaturalResourcesLocKey = "NaturalResources.Visibility.Hide";

		// Token: 0x0400002C RID: 44
		public static readonly string ToggleNaturalResourcesKey = "ToggleNaturalResources";

		// Token: 0x0400002D RID: 45
		public readonly NaturalResourceLayerService _naturalResourceLayerService;

		// Token: 0x0400002E RID: 46
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002F RID: 47
		public readonly UILayout _uiLayout;

		// Token: 0x04000030 RID: 48
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000031 RID: 49
		public readonly BindableToggleFactory _bindableToggleFactory;

		// Token: 0x04000032 RID: 50
		public readonly EventBus _eventBus;

		// Token: 0x04000033 RID: 51
		public BindableToggle _bindableToggle;
	}
}
