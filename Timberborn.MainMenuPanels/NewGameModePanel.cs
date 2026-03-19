using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.FactionSystem;
using Timberborn.GameSceneLoading;
using Timberborn.Localization;
using Timberborn.MapItemsUI;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuPanels
{
	// Token: 0x02000010 RID: 16
	public class NewGameModePanel : IPanelController, ILoadableSingleton
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public NewGameModePanel(VisualElementLoader visualElementLoader, GameSceneLoader gameSceneLoader, PanelStack panelStack, ILoc loc, CustomNewGameModeController customNewGameModeController, GameModeSpecService gameModeSpecService, TutorialToggleController tutorialToggleController)
		{
			this._visualElementLoader = visualElementLoader;
			this._gameSceneLoader = gameSceneLoader;
			this._panelStack = panelStack;
			this._loc = loc;
			this._customNewGameModeController = customNewGameModeController;
			this._gameModeSpecService = gameModeSpecService;
			this._tutorialToggleController = tutorialToggleController;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003B2C File Offset: 0x00001D2C
		public void Load()
		{
			this._gameModeSpecs = this._gameModeSpecService.GetSpecsOrdered();
			GameModeSpec defaultSpec = this._gameModeSpecService.GetDefaultSpec();
			this._root = this._visualElementLoader.LoadVisualElement("MainMenu/NewGameModePanel");
			this._tutorialToggleController.Initialize(this._root);
			this._summary = UQueryExtensions.Q<Label>(this._root, "SummaryText", null);
			this._nextButton = UQueryExtensions.Q<Button>(this._root, "NextButton", null);
			this._nextButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.StartNewGame();
			}, 0);
			this._nextButton.text = this._loc.T(NewGameModePanel.StartLocKey);
			Button button = UQueryExtensions.Q<Button>(this._root, "BackButton", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			button.text = this._loc.T(CommonLocKeys.NavigationBackKey);
			this._modeDescription = UQueryExtensions.Q<Label>(this._root, "ModeDescription", null);
			this._customModeSettings = UQueryExtensions.Q(this._root, "CustomModeSettings", null);
			this._customizeButton = UQueryExtensions.Q<Button>(this._root, "CustomizeButton", null);
			this._customizeButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnCustomizeButtonClicked();
			}, 0);
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this._root, "Modes", null);
			ImmutableArray<GameModeSpec>.Enumerator enumerator = this._gameModeSpecs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				GameModeSpec gameModeSpec = enumerator.Current;
				Button modeButton = (Button)this._visualElementLoader.LoadVisualElement("MainMenu/NewGameModeButton");
				modeButton.text = this._loc.T(gameModeSpec.DisplayNameLocKey);
				modeButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					this.OnPredefinedModeButtonClicked(modeButton, gameModeSpec);
				}, 0);
				visualElement.Add(modeButton);
				this._modeButtons.Add(modeButton);
				if (gameModeSpec == defaultSpec)
				{
					this.OnPredefinedModeButtonClicked(modeButton, gameModeSpec);
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003D4E File Offset: 0x00001F4E
		public void SelectFactionAndMap(FactionSpec factionSpec, MapItem map)
		{
			this._map = map;
			this._factionSpec = factionSpec;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003D5E File Offset: 0x00001F5E
		public VisualElement GetPanel()
		{
			this._visible = true;
			this.UpdateSummary();
			this._tutorialToggleController.SetFaction(this._factionSpec);
			return this._root;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003D84 File Offset: 0x00001F84
		public bool OnUIConfirmed()
		{
			this.StartNewGame();
			return true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003D8D File Offset: 0x00001F8D
		public void OnUICancelled()
		{
			this._visible = false;
			this._panelStack.Pop(this);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public void StartNewGame()
		{
			GameModeSpec gameMode;
			if (this.TryGetValidatedGameMode(out gameMode))
			{
				this._gameSceneLoader.StartNewGame(new NewGameConfiguration(this._factionSpec.Id, this._map.MapFileReference, gameMode, string.Empty));
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003DE7 File Offset: 0x00001FE7
		public bool TryGetValidatedGameMode(out GameModeSpec gameMode)
		{
			if (this._predefinedGameMode != null)
			{
				gameMode = this._predefinedGameMode;
				return true;
			}
			return this._customNewGameModeController.TryGetValidatedGameMode(out gameMode);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E10 File Offset: 0x00002010
		public void OnPredefinedModeButtonClicked(Button button, GameModeSpec predefinedGameMode)
		{
			this._modeDescription.text = this._loc.T(predefinedGameMode.DescriptionLocKey);
			this._modeDescription.ToggleDisplayStyle(true);
			this._customModeSettings.ToggleDisplayStyle(false);
			this._customizeButton.ToggleDisplayStyle(true);
			this._tutorialToggleController.ShowMainToggle();
			this.SelectModeButton(button, predefinedGameMode);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E70 File Offset: 0x00002070
		public void OnCustomizeButtonClicked()
		{
			this._modeDescription.ToggleDisplayStyle(false);
			this._customModeSettings.ToggleDisplayStyle(true);
			this._customizeButton.ToggleDisplayStyle(false);
			this._customNewGameModeController.Initialize(this._root, this._predefinedGameMode, new Action(this.UpdateNextButton));
			this._tutorialToggleController.HideMainToggle();
			this.SelectModeButton(null, null);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003ED8 File Offset: 0x000020D8
		public void SelectModeButton(Button button, GameModeSpec predefinedGameMode)
		{
			foreach (Button button2 in this._modeButtons)
			{
				button2.EnableInClassList(NewGameModePanel.SelectedModeClass, button2 == button);
			}
			this._selectedModeButton = button;
			this._predefinedGameMode = predefinedGameMode;
			this.UpdateSummary();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003F48 File Offset: 0x00002148
		public void UpdateSummary()
		{
			if (this._visible)
			{
				TextElement summary = this._summary;
				string[] array = new string[5];
				array[0] = this._factionSpec.DisplayName.Value;
				array[1] = " - ";
				array[2] = this._map.DisplayName;
				array[3] = " - ";
				int num = 4;
				Button selectedModeButton = this._selectedModeButton;
				array[num] = (((selectedModeButton != null) ? selectedModeButton.text : null) ?? this._loc.T(NewGameModePanel.CustomModeLocKey));
				summary.text = string.Concat(array);
				this.UpdateNextButton();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003FD4 File Offset: 0x000021D4
		public void UpdateNextButton()
		{
			GameModeSpec gameModeSpec;
			this._nextButton.SetEnabled(this.TryGetValidatedGameMode(out gameModeSpec));
		}

		// Token: 0x04000070 RID: 112
		public static readonly string StartLocKey = "NewGameConfigurationPanel.Start";

		// Token: 0x04000071 RID: 113
		public static readonly string CustomModeLocKey = "NewGameConfigurationPanel.Custom";

		// Token: 0x04000072 RID: 114
		public static readonly string SelectedModeClass = "new-game-mode-panel__mode--selected";

		// Token: 0x04000073 RID: 115
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000074 RID: 116
		public readonly GameSceneLoader _gameSceneLoader;

		// Token: 0x04000075 RID: 117
		public readonly PanelStack _panelStack;

		// Token: 0x04000076 RID: 118
		public readonly ILoc _loc;

		// Token: 0x04000077 RID: 119
		public readonly CustomNewGameModeController _customNewGameModeController;

		// Token: 0x04000078 RID: 120
		public readonly GameModeSpecService _gameModeSpecService;

		// Token: 0x04000079 RID: 121
		public readonly TutorialToggleController _tutorialToggleController;

		// Token: 0x0400007A RID: 122
		public ImmutableArray<GameModeSpec> _gameModeSpecs;

		// Token: 0x0400007B RID: 123
		public MapItem _map;

		// Token: 0x0400007C RID: 124
		public FactionSpec _factionSpec;

		// Token: 0x0400007D RID: 125
		public GameModeSpec _predefinedGameMode;

		// Token: 0x0400007E RID: 126
		public VisualElement _root;

		// Token: 0x0400007F RID: 127
		public Label _summary;

		// Token: 0x04000080 RID: 128
		public Button _nextButton;

		// Token: 0x04000081 RID: 129
		public readonly List<Button> _modeButtons = new List<Button>();

		// Token: 0x04000082 RID: 130
		public Button _customizeButton;

		// Token: 0x04000083 RID: 131
		public Button _selectedModeButton;

		// Token: 0x04000084 RID: 132
		public Label _modeDescription;

		// Token: 0x04000085 RID: 133
		public VisualElement _customModeSettings;

		// Token: 0x04000086 RID: 134
		public bool _visible;
	}
}
