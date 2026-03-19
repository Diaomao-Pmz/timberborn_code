using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.ErrorReporting;
using Timberborn.FactionSystem;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.Localization;
using Timberborn.MainMenuSceneLoading;
using Timberborn.PlayerDataSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x0200000A RID: 10
	public class DevModeSettingsController : IUpdatableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002480 File Offset: 0x00000680
		public DevModeSettingsController(DevModeManager devModeManager, FactionUnlockingService factionUnlockingService, ILocalizationService localizationService, ILoc loc, MainMenuSceneLoader mainMenuSceneLoader, IPlayerDataService playerDataService, GameSaveRepository gameSaveRepository, EventBus eventBus)
		{
			this._devModeManager = devModeManager;
			this._factionUnlockingService = factionUnlockingService;
			this._localizationService = localizationService;
			this._loc = loc;
			this._mainMenuSceneLoader = mainMenuSceneLoader;
			this._playerDataService = playerDataService;
			this._gameSaveRepository = gameSaveRepository;
			this._eventBus = eventBus;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024DC File Offset: 0x000006DC
		public void Initialize(VisualElement root, Action cancelAction)
		{
			this._wrapper = UQueryExtensions.Q<VisualElement>(root, "Developer", null);
			this._testLabel = UQueryExtensions.Q<Label>(root, "DeveloperTestLabel", null);
			this._testLabel.ToggleDisplayStyle(false);
			UQueryExtensions.Q<VisualElement>(root, "Developer", null).style.display = 0;
			UQueryExtensions.Q<Button>(root, "LockFactions", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._factionUnlockingService.LockAllFactions();
			}, 0);
			UQueryExtensions.Q<Button>(root, "UnlockFactions", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._factionUnlockingService.UnlockAllFactions();
			}, 0);
			UQueryExtensions.Q<Button>(root, "ClearPlayerPrefs", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnClearPlayerPrefsClick(cancelAction);
			}, 0);
			UQueryExtensions.Q<Button>(root, "TestLanguages", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnTestLanguagesClick();
			}, 0);
			this._testExceptionButton = UQueryExtensions.Q<Button>(root, "TestException", null);
			this._testExceptionButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnTestExceptionClick();
			}, 0);
			UQueryExtensions.Q<Button>(root, "ShowCleanLog", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnShowCleanLogClick();
			}, 0);
			this._eventBus.Register(this);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002615 File Offset: 0x00000815
		public void Unload()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002623 File Offset: 0x00000823
		public void Update()
		{
			this._wrapper.ToggleDisplayStyle(this._devModeManager.Enabled);
			this._testExceptionButton.ToggleDisplayStyle(this._gameSaveRepository.DevelopmentSettlementExists());
			this._testLabel.ToggleDisplayStyle(false);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002660 File Offset: 0x00000860
		public void UpdateSingleton()
		{
			if (this._textsToTest.Count > 0)
			{
				this._testLabel.ToggleDisplayStyle(true);
				this._testLabel.text = this.DequeueText();
				while (this._textsToTest.Count > 0)
				{
					if (this._testLabel.text.Length >= 5000)
					{
						return;
					}
					Label testLabel = this._testLabel;
					testLabel.text += " ";
					Label testLabel2 = this._testLabel;
					testLabel2.text += this.DequeueText();
				}
			}
			else if (this._languageTestInProgress)
			{
				this._mainMenuSceneLoader.SaveAndOpenMainMenu();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000270A File Offset: 0x0000090A
		[OnEvent]
		public void OnDevModeToggledEvent(DevModeToggledEvent _)
		{
			this.Update();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002712 File Offset: 0x00000912
		public void OnClearPlayerPrefsClick(Action cancelAction)
		{
			PlayerPrefs.DeleteAll();
			this._playerDataService.RemoveAll();
			cancelAction();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000272C File Offset: 0x0000092C
		public void OnTestLanguagesClick()
		{
			string currentLanguage = this._localizationService.CurrentLanguage;
			foreach (LanguageInfo languageInfo in this._localizationService.AvailableLanguages)
			{
				this._localizationService.Load(languageInfo.LocalizationCode);
				foreach (string item in this._loc.GetRawTexts())
				{
					this._textsToTest.Enqueue(item);
				}
			}
			this._localizationService.Load(currentLanguage);
			this._wrapper.parent.parent.parent.ToggleDisplayStyle(false);
			this._languageTestInProgress = true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000280C File Offset: 0x00000A0C
		public string DequeueText()
		{
			return this._textsToTest.Dequeue().Replace(Environment.NewLine, " ");
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002828 File Offset: 0x00000A28
		public void OnTestExceptionClick()
		{
			this._devModeManager.Disable();
			throw new Exception("Test");
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000283F File Offset: 0x00000A3F
		public void OnShowCleanLogClick()
		{
			this._testLabel.ToggleDisplayStyle(true);
			this._testLabel.text = PlayerLogCleaner.GetCleanedPlayerLog();
		}

		// Token: 0x0400001A RID: 26
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400001B RID: 27
		public readonly FactionUnlockingService _factionUnlockingService;

		// Token: 0x0400001C RID: 28
		public readonly ILocalizationService _localizationService;

		// Token: 0x0400001D RID: 29
		public readonly ILoc _loc;

		// Token: 0x0400001E RID: 30
		public readonly MainMenuSceneLoader _mainMenuSceneLoader;

		// Token: 0x0400001F RID: 31
		public readonly IPlayerDataService _playerDataService;

		// Token: 0x04000020 RID: 32
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000021 RID: 33
		public readonly EventBus _eventBus;

		// Token: 0x04000022 RID: 34
		public VisualElement _wrapper;

		// Token: 0x04000023 RID: 35
		public Button _testExceptionButton;

		// Token: 0x04000024 RID: 36
		public Label _testLabel;

		// Token: 0x04000025 RID: 37
		public bool _languageTestInProgress;

		// Token: 0x04000026 RID: 38
		public readonly Queue<string> _textsToTest = new Queue<string>();
	}
}
