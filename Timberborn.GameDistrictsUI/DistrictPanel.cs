using System;
using System.Collections.Generic;
using Timberborn.BatchControl;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityNaming;
using Timberborn.GameDistricts;
using Timberborn.InputSystemUI;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000016 RID: 22
	public class DistrictPanel : ILoadableSingleton, IHideableByBatchControl
	{
		// Token: 0x06000098 RID: 152 RVA: 0x000037AC File Offset: 0x000019AC
		public DistrictPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, DistrictContextService districtContextService, DistrictListPanel districtListPanel, EntitySelectionService entitySelectionService, DistrictCenterRegistry districtCenterRegistry, ILoc loc, EventBus eventBus, BindableButtonFactory bindableButtonFactory, ITooltipRegistrar tooltipRegistrar)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._districtContextService = districtContextService;
			this._districtListPanel = districtListPanel;
			this._entitySelectionService = entitySelectionService;
			this._districtCenterRegistry = districtCenterRegistry;
			this._loc = loc;
			this._eventBus = eventBus;
			this._bindableButtonFactory = bindableButtonFactory;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000380C File Offset: 0x00001A0C
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/Districts/DistrictPanel");
			this._districtHeader = UQueryExtensions.Q<VisualElement>(this._root, "DistrictHeader", null);
			this._districtNameButton = UQueryExtensions.Q<Button>(this._root, "DistrictName", null);
			this._districtNameButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleDistrictSelection), 0);
			this.ResetDistrictNameButtonLabel();
			this._extensionToggler = UQueryExtensions.Q<Button>(this._root, "ExtensionToggler", null);
			this._extensionToggler.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleDistrictSelection), 0);
			this._extensionToggler.AddToClassList(DistrictPanel.HiddenClass);
			Button button = UQueryExtensions.Q<Button>(this._root, "PreviousDistrict", null);
			Button button2 = UQueryExtensions.Q<Button>(this._root, "NextDistrict", null);
			this._bindableButtonFactory.CreateAndBind(button, DistrictPanel.PreviousDistrictKey, new Action(this.SelectPreviousDistrict));
			this._bindableButtonFactory.CreateAndBind(button2, DistrictPanel.NextDistrictKey, new Action(this.SelectNextDistrict));
			this._tooltipRegistrar.Register(this._districtHeader, this._loc.T(DistrictPanel.ShowDistrictListKey));
			this._tooltipRegistrar.RegisterWithKeyBinding(button, DistrictPanel.PreviousDistrictKey);
			this._tooltipRegistrar.RegisterWithKeyBinding(button2, DistrictPanel.NextDistrictKey);
			this._districtListPanel.Initialize(this._root);
			this._eventBus.Register(this);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000397A File Offset: 0x00001B7A
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopRight(this._root, 9);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000398F File Offset: 0x00001B8F
		[OnEvent]
		public void OnEntityNameChanged(EntityNameChangedEvent entityNameChangedEvent)
		{
			if (entityNameChangedEvent.Entity.HasComponent<DistrictCenter>())
			{
				this.UpdateDistrictList();
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000039A4 File Offset: 0x00001BA4
		[OnEvent]
		public void OnDistrictSelected(DistrictSelectedEvent districtSelectedEvent)
		{
			this.SetDistrictNameButtonLabel();
			this._districtHeader.AddToClassList(DistrictPanel.PanelDistrictClass);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000039BC File Offset: 0x00001BBC
		[OnEvent]
		public void OnDistrictUnselected(DistrictUnselectedEvent districtUnselectedEvent)
		{
			this.ResetDistrictNameButtonLabel();
			this._districtHeader.RemoveFromClassList(DistrictPanel.PanelDistrictClass);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000039D4 File Offset: 0x00001BD4
		public void Show()
		{
			this._root.ToggleDisplayStyle(true);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000039E2 File Offset: 0x00001BE2
		public void Hide()
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000039F0 File Offset: 0x00001BF0
		public IReadOnlyList<DistrictCenter> DistrictCenters
		{
			get
			{
				return this._districtCenterRegistry.FinishedDistrictCenters;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003A02 File Offset: 0x00001C02
		public int IndexOfCurrentlySelectedDistrict
		{
			get
			{
				return this.DistrictCenters.IndexOf(this._districtContextService.SelectedDistrict);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003A1A File Offset: 0x00001C1A
		public void UpdateDistrictList()
		{
			if (this._districtContextService.SelectedDistrict)
			{
				this.SetDistrictNameButtonLabel();
			}
			this._districtListPanel.UpdateDistrictList();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003A3F File Offset: 0x00001C3F
		public void ResetDistrictNameButtonLabel()
		{
			this.SetDistrictNameButtonLabel(this._loc.T(DistrictPanel.GlobalViewLocKey));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003A58 File Offset: 0x00001C58
		public void ToggleDistrictSelection(ClickEvent evt)
		{
			if (this._districtSelectionToggled)
			{
				this._districtListPanel.Hide();
				this._extensionToggler.AddToClassList(DistrictPanel.HiddenClass);
				this._districtSelectionToggled = false;
				return;
			}
			this._districtListPanel.Show();
			this._extensionToggler.RemoveFromClassList(DistrictPanel.HiddenClass);
			this._districtSelectionToggled = true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003AB2 File Offset: 0x00001CB2
		public void SetDistrictNameButtonLabel()
		{
			this.SetDistrictNameButtonLabel(this._districtContextService.SelectedDistrict.DistrictName);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003ACA File Offset: 0x00001CCA
		public void SetDistrictNameButtonLabel(string label)
		{
			this._districtNameButton.text = label;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public void SelectPreviousDistrict()
		{
			this.SelectDistrict(this.IndexOfPreviousDistrict());
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public int IndexOfPreviousDistrict()
		{
			if (!this._districtContextService.SelectedDistrict)
			{
				return 0;
			}
			int num = this.IndexOfCurrentlySelectedDistrict - 1;
			if (num < 0)
			{
				num = this.DistrictCenters.Count - 1;
			}
			return num;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003B25 File Offset: 0x00001D25
		public void SelectNextDistrict()
		{
			this.SelectDistrict(this.IndexOfNextDistrict());
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003B34 File Offset: 0x00001D34
		public int IndexOfNextDistrict()
		{
			if (!this._districtContextService.SelectedDistrict)
			{
				return 0;
			}
			int num = this.IndexOfCurrentlySelectedDistrict + 1;
			if (num >= this.DistrictCenters.Count)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003B70 File Offset: 0x00001D70
		public void SelectDistrict(int index)
		{
			if (index < this.DistrictCenters.Count)
			{
				DistrictCenter target = this.DistrictCenters[index];
				this._entitySelectionService.SelectAndFocusOn(target);
			}
		}

		// Token: 0x04000051 RID: 81
		public static readonly string HiddenClass = "extension-clamp--hidden";

		// Token: 0x04000052 RID: 82
		public static readonly string PanelDistrictClass = "panel--district";

		// Token: 0x04000053 RID: 83
		public static readonly string GlobalViewLocKey = "Districts.GlobalView";

		// Token: 0x04000054 RID: 84
		public static readonly string PreviousDistrictKey = "PreviousDistrict";

		// Token: 0x04000055 RID: 85
		public static readonly string ShowDistrictListKey = "GameUI.ShowDistrictList";

		// Token: 0x04000056 RID: 86
		public static readonly string NextDistrictKey = "NextDistrict";

		// Token: 0x04000057 RID: 87
		public readonly UILayout _uiLayout;

		// Token: 0x04000058 RID: 88
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000059 RID: 89
		public readonly DistrictContextService _districtContextService;

		// Token: 0x0400005A RID: 90
		public readonly DistrictListPanel _districtListPanel;

		// Token: 0x0400005B RID: 91
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400005C RID: 92
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x0400005D RID: 93
		public readonly ILoc _loc;

		// Token: 0x0400005E RID: 94
		public readonly EventBus _eventBus;

		// Token: 0x0400005F RID: 95
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x04000060 RID: 96
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000061 RID: 97
		public Button _districtNameButton;

		// Token: 0x04000062 RID: 98
		public Button _extensionToggler;

		// Token: 0x04000063 RID: 99
		public bool _districtSelectionToggled;

		// Token: 0x04000064 RID: 100
		public VisualElement _districtHeader;

		// Token: 0x04000065 RID: 101
		public VisualElement _root;
	}
}
