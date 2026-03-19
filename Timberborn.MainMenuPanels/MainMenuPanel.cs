using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.ApplicationLifetime;
using Timberborn.CoreUI;
using Timberborn.ExperimentalModeSystem;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.GameSaveRepositorySystemUI;
using Timberborn.Localization;
using Timberborn.MainMenuModdingUI;
using Timberborn.MapRepositorySystemUI;
using Timberborn.SettingsSystemUI;
using Timberborn.SingletonSystem;
using Timberborn.StoreSystem;
using Timberborn.TooltipSystem;
using Timberborn.WebNavigation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuPanels
{
	// Token: 0x02000006 RID: 6
	public class MainMenuPanel : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002C04 File Offset: 0x00000E04
		public MainMenuPanel(GameSaveRepository gameSaveRepository, LoadGameBox loadGameBox, LoadMapBox loadMapBox, NewGameFactionPanel newGameFactionPanel, NewMapBox newMapBox, ISettingsController settingsController, CreditsBox creditsBox, PanelStack panelStack, UrlOpener urlOpener, VisualElementLoader visualElementLoader, ExperimentalMode experimentalMode, ValidatingGameLoader validatingGameLoader, ITooltipRegistrar tooltipRegistrar, IStore store, ILoc loc, MainMenuSoundController mainMenuSoundController, ModManagerBox modManagerBox)
		{
			this._gameSaveRepository = gameSaveRepository;
			this._loadGameBox = loadGameBox;
			this._loadMapBox = loadMapBox;
			this._newGameFactionPanel = newGameFactionPanel;
			this._newMapBox = newMapBox;
			this._settingsController = settingsController;
			this._creditsBox = creditsBox;
			this._panelStack = panelStack;
			this._urlOpener = urlOpener;
			this._visualElementLoader = visualElementLoader;
			this._experimentalMode = experimentalMode;
			this._validatingGameLoader = validatingGameLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._store = store;
			this._loc = loc;
			this._mainMenuSoundController = mainMenuSoundController;
			this._modManagerBox = modManagerBox;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C9C File Offset: 0x00000E9C
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MainMenu/MainMenuPanel");
			UQueryExtensions.Q<Button>(this._root, "NewGameButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.NewGameClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "LoadGameButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.LoadGameClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "NewMapButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.NewMapClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "LoadMapButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.LoadMapClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "ModManagerButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ModManagerClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "SettingsButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SettingsClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "FeedbackButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.FeedbackClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "ExitButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(MainMenuPanel.ExitClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "CreditsButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.CreditsClicked), 0);
			UQueryExtensions.Q<Label>(this._root, "Experimental", null).ToggleDisplayStyle(this._experimentalMode.IsExperimental);
			this._continueButton = UQueryExtensions.Q<Button>(this._root, "ContinueButton", null);
			this._continueButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ContinueClicked), 0);
			Button button = UQueryExtensions.Q<Button>(this._root, "DiscordButton", null);
			button.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.DiscordClicked), 0);
			this._tooltipRegistrar.Register(button, UrlOpener.DiscordUrl);
			Button button2 = UQueryExtensions.Q<Button>(this._root, "MerchandiseButton", null);
			button2.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.MerchandiseClicked), 0);
			this._tooltipRegistrar.Register(button2, UrlOpener.MerchandiseUrl);
			Button button3 = UQueryExtensions.Q<Button>(this._root, "UpdateInfoLink", null);
			if (string.IsNullOrEmpty(this._store.FullUpdateUrl))
			{
				button3.ToggleDisplayStyle(false);
			}
			else
			{
				button3.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.UpdateInfoLinkClicked), 0);
				this._tooltipRegistrar.Register(button3, this._store.ShortUpdateUrl ?? this._store.FullUpdateUrl);
			}
			UQueryExtensions.Q<Label>(this._root, "UpdateInfoHeader", null).text = (this._experimentalMode.IsExperimental ? this._loc.T(MainMenuPanel.UpdateInfoHeaderExperimentalLocKey) : this._loc.T(MainMenuPanel.UpdateInfoHeaderLocKey));
			UQueryExtensions.Q<Label>(this._root, "UpdateInfoText", null).text = MainMenuPanel.GetBulletListWithIndentation(this._loc.T(this._store.UpdateInfoTextLocKey));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002F92 File Offset: 0x00001192
		public void Show()
		{
			this._continueButton.ToggleDisplayStyle(this._gameSaveRepository.GetMostRecentSave() != null);
			this._panelStack.Push(this);
			this._mainMenuSoundController.PlayThemeMusic();
			Time.timeScale = 1f;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002FD1 File Offset: 0x000011D1
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002FD9 File Offset: 0x000011D9
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002FDC File Offset: 0x000011DC
		public void OnUICancelled()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002FDE File Offset: 0x000011DE
		public void ContinueClicked(ClickEvent evt)
		{
			this.LoadMostRecentSave();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002FE6 File Offset: 0x000011E6
		public void NewGameClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPush(this._newGameFactionPanel);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002FF9 File Offset: 0x000011F9
		public void LoadGameClicked(ClickEvent evt)
		{
			this._loadGameBox.Open();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003006 File Offset: 0x00001206
		public void NewMapClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPush(this._newMapBox);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003019 File Offset: 0x00001219
		public void LoadMapClicked(ClickEvent evt)
		{
			this._loadMapBox.Open();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003026 File Offset: 0x00001226
		public void ModManagerClicked(ClickEvent evt)
		{
			this._modManagerBox.Open();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003033 File Offset: 0x00001233
		public void SettingsClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPush(this._settingsController);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003046 File Offset: 0x00001246
		public void CreditsClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPush(this._creditsBox);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003059 File Offset: 0x00001259
		public void FeedbackClicked(ClickEvent evt)
		{
			this._urlOpener.OpenFeatureUpvote();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003066 File Offset: 0x00001266
		public static void ExitClicked(ClickEvent evt)
		{
			GameQuitter.Quit();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000306D File Offset: 0x0000126D
		public void DiscordClicked(ClickEvent evt)
		{
			this._urlOpener.OpenDiscord();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000307A File Offset: 0x0000127A
		public void MerchandiseClicked(ClickEvent evt)
		{
			this._urlOpener.OpenMerchandise();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003087 File Offset: 0x00001287
		public void UpdateInfoLinkClicked(ClickEvent evt)
		{
			this._urlOpener.OpenUrl(this._store.FullUpdateUrl);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000030A0 File Offset: 0x000012A0
		public void LoadMostRecentSave()
		{
			SaveReference mostRecentSave = this._gameSaveRepository.GetMostRecentSave();
			this.LoadSaveIfExists(mostRecentSave);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000030C0 File Offset: 0x000012C0
		public void LoadSaveIfExists(SaveReference saveReference)
		{
			if (this._gameSaveRepository.SaveExists(saveReference))
			{
				this._validatingGameLoader.LoadGame(saveReference);
				return;
			}
			Debug.LogWarning(string.Format("Save: {0} doesn't exist, failed to load.", saveReference));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000030F0 File Offset: 0x000012F0
		public static string GetBulletListWithIndentation(string inputText)
		{
			IEnumerable<string> values = from line in inputText.Split(new char[]
			{
				'\r',
				'\n'
			}, StringSplitOptions.RemoveEmptyEntries)
			select "<line-indent=-8px>" + line + "</line-indent>";
			return string.Join(Environment.NewLine, values);
		}

		// Token: 0x04000032 RID: 50
		public static readonly string UpdateInfoHeaderLocKey = "MainMenu.UpdateInfoHeader";

		// Token: 0x04000033 RID: 51
		public static readonly string UpdateInfoHeaderExperimentalLocKey = "MainMenu.UpdateInfoHeaderExperimental";

		// Token: 0x04000034 RID: 52
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000035 RID: 53
		public readonly LoadGameBox _loadGameBox;

		// Token: 0x04000036 RID: 54
		public readonly LoadMapBox _loadMapBox;

		// Token: 0x04000037 RID: 55
		public readonly NewGameFactionPanel _newGameFactionPanel;

		// Token: 0x04000038 RID: 56
		public readonly NewMapBox _newMapBox;

		// Token: 0x04000039 RID: 57
		public readonly ISettingsController _settingsController;

		// Token: 0x0400003A RID: 58
		public readonly CreditsBox _creditsBox;

		// Token: 0x0400003B RID: 59
		public readonly PanelStack _panelStack;

		// Token: 0x0400003C RID: 60
		public readonly UrlOpener _urlOpener;

		// Token: 0x0400003D RID: 61
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400003E RID: 62
		public readonly ExperimentalMode _experimentalMode;

		// Token: 0x0400003F RID: 63
		public readonly ValidatingGameLoader _validatingGameLoader;

		// Token: 0x04000040 RID: 64
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000041 RID: 65
		public readonly IStore _store;

		// Token: 0x04000042 RID: 66
		public readonly ILoc _loc;

		// Token: 0x04000043 RID: 67
		public readonly MainMenuSoundController _mainMenuSoundController;

		// Token: 0x04000044 RID: 68
		public readonly ModManagerBox _modManagerBox;

		// Token: 0x04000045 RID: 69
		public VisualElement _root;

		// Token: 0x04000046 RID: 70
		public Button _continueButton;
	}
}
