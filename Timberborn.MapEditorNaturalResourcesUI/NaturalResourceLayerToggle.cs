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
	// Token: 0x02000009 RID: 9
	internal class NaturalResourceLayerToggle : ILoadableSingleton
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002431 File Offset: 0x00000631
		public NaturalResourceLayerToggle(NaturalResourceLayerService naturalResourceLayerService, VisualElementLoader visualElementLoader, UILayout uiLayout, ITooltipRegistrar tooltipRegistrar, BindableToggleFactory bindableToggleFactory, EventBus eventBus)
		{
			this._naturalResourceLayerService = naturalResourceLayerService;
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._tooltipRegistrar = tooltipRegistrar;
			this._bindableToggleFactory = bindableToggleFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002468 File Offset: 0x00000668
		public void Load()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/SquareToggle");
			this._tooltipRegistrar.RegisterLocalizable(visualElement, () => this.TooltipLocKey);
			Toggle toggle = visualElement.Q("Toggle", null);
			toggle.AddToClassList(NaturalResourceLayerToggle.NaturalResourceLayerClass);
			this._bindableToggle = this._bindableToggleFactory.CreateAndBind(toggle, NaturalResourceLayerToggle.ToggleNaturalResourcesKey, new Action<bool>(this.ToggleNaturalResourcesLayer), () => this._naturalResourceLayerService.Enabled);
			this._uiLayout.AddTopRightButton(visualElement, 3);
			this._eventBus.Register(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024FE File Offset: 0x000006FE
		[OnEvent]
		public void OnNaturalResourceLayerChanged(NaturalResourceLayerChangedEvent naturalResourceLayerChangedEvent)
		{
			this._bindableToggle.Update();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000250B File Offset: 0x0000070B
		private string TooltipLocKey
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

		// Token: 0x06000023 RID: 35 RVA: 0x00002525 File Offset: 0x00000725
		private void ToggleNaturalResourcesLayer(bool enableResources)
		{
			if (enableResources)
			{
				this._naturalResourceLayerService.Enable();
				return;
			}
			this._naturalResourceLayerService.Disable();
		}

		// Token: 0x0400000E RID: 14
		private static readonly string NaturalResourceLayerClass = "square-toggle--natural-resources";

		// Token: 0x0400000F RID: 15
		private static readonly string ShowNaturalResourcesLocKey = "NaturalResources.Visibility.Show";

		// Token: 0x04000010 RID: 16
		private static readonly string HideNaturalResourcesLocKey = "NaturalResources.Visibility.Hide";

		// Token: 0x04000011 RID: 17
		private static readonly string ToggleNaturalResourcesKey = "ToggleNaturalResources";

		// Token: 0x04000012 RID: 18
		private readonly NaturalResourceLayerService _naturalResourceLayerService;

		// Token: 0x04000013 RID: 19
		private readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000014 RID: 20
		private readonly UILayout _uiLayout;

		// Token: 0x04000015 RID: 21
		private readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000016 RID: 22
		private readonly BindableToggleFactory _bindableToggleFactory;

		// Token: 0x04000017 RID: 23
		private readonly EventBus _eventBus;

		// Token: 0x04000018 RID: 24
		private BindableToggle _bindableToggle;
	}
}
