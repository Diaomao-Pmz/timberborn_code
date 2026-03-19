using System;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.LevelVisibilitySystem;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.LevelVisibilitySystemUI
{
	// Token: 0x02000007 RID: 7
	public class LevelVisibilityPanel : ILevelVisibilityPanel, ILoadableSingleton
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002258 File Offset: 0x00000458
		public LevelVisibilityPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, ILevelVisibilityService levelVisibilityService, LevelVisibilitySelector levelVisibilitySelector, EventBus eventBus, BindableButtonFactory bindableButtonFactory, ITooltipRegistrar tooltipRegistrar, MapEditorMode mapEditorMode)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._levelVisibilityService = levelVisibilityService;
			this._levelVisibilitySelector = levelVisibilitySelector;
			this._eventBus = eventBus;
			this._bindableButtonFactory = bindableButtonFactory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022A8 File Offset: 0x000004A8
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/LevelVisibilityPanel");
			this._content = UQueryExtensions.Q<VisualElement>(this._root, "Content", null);
			Button button = UQueryExtensions.Q<Button>(this._root, "Up", null);
			Button button2 = UQueryExtensions.Q<Button>(this._root, "Down", null);
			this._upButton = this._bindableButtonFactory.CreateAndBind(button, LevelVisibilityPanel.RaiseVisibleLayerKey, delegate
			{
				this.ChangeMaxVisibleLevel(1);
			});
			this._downButton = this._bindableButtonFactory.CreateAndBind(button2, LevelVisibilityPanel.LowerVisibleLayerKey, delegate
			{
				this.ChangeMaxVisibleLevel(-1);
			});
			this._tooltipRegistrar.RegisterWithKeyBinding(button, LevelVisibilityPanel.RaiseVisibleLayerKey);
			this._tooltipRegistrar.RegisterWithKeyBinding(button2, LevelVisibilityPanel.LowerVisibleLayerKey);
			this._resetButton = UQueryExtensions.Q<Button>(this._root, "Reset", null);
			this._resetButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._levelVisibilityService.ResetMaxVisibleLevel();
			}, 0);
			this._tooltipRegistrar.RegisterLocalizable(this._resetButton, LevelVisibilityPanel.ResetTooltipKey);
			this._levelButtonWrapper = UQueryExtensions.Q<VisualElement>(this._root, "LevelButtonWrapper", null);
			this._tooltipRegistrar.RegisterLocalizable(this._levelButtonWrapper, LevelVisibilityPanel.InfoTooltipKey);
			this._tooltipRegistrar.RegisterLocalizable(UQueryExtensions.Q<VisualElement>(this._root, "LevelIcon", null), LevelVisibilityPanel.InfoTooltipKey);
			this._levelButton = UQueryExtensions.Q<RepeatButton>(this._root, "LevelButton", null);
			this._levelButton.SetAction(new Action(this.StartLevelSelection), 0L, long.MaxValue);
			this._level = UQueryExtensions.Q<Label>(this._root, "Level", null);
			this.UpdateButtons();
			this._eventBus.Register(this);
			if (this._mapEditorMode.IsMapEditor)
			{
				this._root.AddToClassList(LevelVisibilityPanel.LevelVisibilityPanelEditorClass);
				return;
			}
			this._root.AddToClassList(LevelVisibilityPanel.LevelVisibilityPanelGameClass);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002491 File Offset: 0x00000691
		public void TogglePanelHighlight(bool state)
		{
			this._root.EnableInClassList(LevelVisibilityPanel.HighlightClass, state);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024A4 File Offset: 0x000006A4
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopRight(this._root, this._mapEditorMode.IsMapEditor ? 2 : 4);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024C8 File Offset: 0x000006C8
		[OnEvent]
		public void OnMaxVisibleLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			this.UpdateButtons();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024C8 File Offset: 0x000006C8
		[OnEvent]
		public void OnHidingLevelsChanged(HidingLevelsChangedEvent hidingLevelsChangedEvent)
		{
			this.UpdateButtons();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024D0 File Offset: 0x000006D0
		public void StartLevelSelection()
		{
			this._levelVisibilitySelector.StartLevelSelection(new Action<int>(this.ChangeMaxVisibleLevel), new Action(this.EndLevelSelection));
			this._levelButton.AddToClassList(LevelVisibilityPanel.HeldLevelButtonClass);
			this._levelButtonWrapper.AddToClassList(LevelVisibilityPanel.HeldLevelButtonClass);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002520 File Offset: 0x00000720
		public void EndLevelSelection()
		{
			this._levelButton.RemoveFromClassList(LevelVisibilityPanel.HeldLevelButtonClass);
			this._levelButtonWrapper.RemoveFromClassList(LevelVisibilityPanel.HeldLevelButtonClass);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002542 File Offset: 0x00000742
		public void ChangeMaxVisibleLevel(int change)
		{
			this._levelVisibilityService.SetMaxVisibleLevel(this._levelVisibilityService.MaxVisibleLevel + change);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000255C File Offset: 0x0000075C
		public void UpdateButtons()
		{
			bool levelIsAtMax = this._levelVisibilityService.LevelIsAtMax;
			bool levelIsAtMin = this._levelVisibilityService.LevelIsAtMin;
			this._level.text = (levelIsAtMax ? "∞" : this._levelVisibilityService.MaxVisibleLevel.ToString());
			this._resetButton.SetEnabled(!levelIsAtMax);
			this._content.EnableInClassList(LevelVisibilityPanel.MaxLevelBackgroundClass, levelIsAtMax);
			this._content.EnableInClassList(LevelVisibilityPanel.NotMaxLevelBackgroundClass, !levelIsAtMax);
			if (levelIsAtMax)
			{
				this._upButton.Disable();
			}
			else
			{
				this._upButton.Enable();
			}
			if (levelIsAtMin)
			{
				this._downButton.Disable();
			}
			else
			{
				this._downButton.Enable();
			}
			bool enabled = !levelIsAtMin || !levelIsAtMax;
			this._levelButton.SetEnabled(enabled);
			this._levelButtonWrapper.SetEnabled(enabled);
		}

		// Token: 0x04000010 RID: 16
		public static readonly string HeldLevelButtonClass = "level-visibility-panel__level-button--held";

		// Token: 0x04000011 RID: 17
		public static readonly string LevelVisibilityPanelGameClass = "level-visibility-panel--game";

		// Token: 0x04000012 RID: 18
		public static readonly string LevelVisibilityPanelEditorClass = "level-visibility-panel--map-editor";

		// Token: 0x04000013 RID: 19
		public static readonly string MaxLevelBackgroundClass = "square-large--transparent-purple";

		// Token: 0x04000014 RID: 20
		public static readonly string NotMaxLevelBackgroundClass = "square-large--light-red";

		// Token: 0x04000015 RID: 21
		public static readonly string HighlightClass = "highlight";

		// Token: 0x04000016 RID: 22
		public static readonly string RaiseVisibleLayerKey = "RaiseVisibleLayer";

		// Token: 0x04000017 RID: 23
		public static readonly string LowerVisibleLayerKey = "LowerVisibleLayer";

		// Token: 0x04000018 RID: 24
		public static readonly string InfoTooltipKey = "LevelVisibility.InfoTooltip";

		// Token: 0x04000019 RID: 25
		public static readonly string ResetTooltipKey = "LevelVisibility.ResetTooltip";

		// Token: 0x0400001A RID: 26
		public readonly UILayout _uiLayout;

		// Token: 0x0400001B RID: 27
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001C RID: 28
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400001D RID: 29
		public readonly LevelVisibilitySelector _levelVisibilitySelector;

		// Token: 0x0400001E RID: 30
		public readonly EventBus _eventBus;

		// Token: 0x0400001F RID: 31
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x04000020 RID: 32
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000021 RID: 33
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000022 RID: 34
		public VisualElement _root;

		// Token: 0x04000023 RID: 35
		public VisualElement _content;

		// Token: 0x04000024 RID: 36
		public BindableButton _upButton;

		// Token: 0x04000025 RID: 37
		public BindableButton _downButton;

		// Token: 0x04000026 RID: 38
		public VisualElement _levelButtonWrapper;

		// Token: 0x04000027 RID: 39
		public RepeatButton _levelButton;

		// Token: 0x04000028 RID: 40
		public Button _resetButton;

		// Token: 0x04000029 RID: 41
		public Label _level;

		// Token: 0x0400002A RID: 42
		public bool _mouseOnLevelButton;
	}
}
