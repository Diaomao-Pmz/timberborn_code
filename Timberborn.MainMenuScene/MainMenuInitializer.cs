using System;
using Timberborn.AnalyticsUI;
using Timberborn.Intro;
using Timberborn.MainMenuPanels;
using Timberborn.MainMenuSceneLoading;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.StoreSystem;
using Timberborn.TitleScreenUI;

namespace Timberborn.MainMenuScene
{
	// Token: 0x0200000A RID: 10
	public class MainMenuInitializer : ILoadableSingleton
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000023D8 File Offset: 0x000005D8
		public MainMenuInitializer(TitleScreen titleScreen, AutoStarter autoStarter, IStore store, WelcomeScreenBox welcomeScreenBox, MacOSPermissionsChecker macOSPermissionsChecker, InitialLanguageChooser initialLanguageChooser, PlayerDataLoader playerDataLoader, MainMenuPanel mainMenuPanel, ISceneLoader sceneLoader, TitleScreenFooter titleScreenFooter, AnalyticsConsentBox analyticsConsentBox, EditorBuildValidator editorBuildValidator, AssetBundleValidator assetBundleValidator, IntroBox introBox)
		{
			this._titleScreen = titleScreen;
			this._autoStarter = autoStarter;
			this._store = store;
			this._welcomeScreenBox = welcomeScreenBox;
			this._macOSPermissionsChecker = macOSPermissionsChecker;
			this._initialLanguageChooser = initialLanguageChooser;
			this._playerDataLoader = playerDataLoader;
			this._mainMenuPanel = mainMenuPanel;
			this._sceneLoader = sceneLoader;
			this._titleScreenFooter = titleScreenFooter;
			this._analyticsConsentBox = analyticsConsentBox;
			this._editorBuildValidator = editorBuildValidator;
			this._assetBundleValidator = assetBundleValidator;
			this._introBox = introBox;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002458 File Offset: 0x00000658
		public void Load()
		{
			if (this._store.GameIsAllowedToStart)
			{
				this._editorBuildValidator.Validate();
				this._assetBundleValidator.Validate();
				this._titleScreen.Initialize();
				MainMenuSceneParameters mainMenuSceneParameters;
				if (!this._sceneLoader.TryGetSceneParameters<MainMenuSceneParameters>(out mainMenuSceneParameters))
				{
					this._macOSPermissionsChecker.CheckPermissions(new Action(this.LoadPlayerSettings));
					return;
				}
				if (mainMenuSceneParameters.ShowWelcomeScreen)
				{
					this.ShowWelcomeScreen();
					return;
				}
				this.ShowMainMenuPanel();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024CF File Offset: 0x000006CF
		public void LoadPlayerSettings()
		{
			this._playerDataLoader.Load(new Action(this.CheckAutoStarting));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024E8 File Offset: 0x000006E8
		public void CheckAutoStarting()
		{
			this._autoStarter.CheckAutoStarting(new Action(this.ShowIntroScreen));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002501 File Offset: 0x00000701
		public void ShowIntroScreen()
		{
			this._introBox.Show(new Action(this.CheckInitialLanguage));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000251A File Offset: 0x0000071A
		public void CheckInitialLanguage()
		{
			this._initialLanguageChooser.CheckInitialLanguage(new Action(this.CheckAnalyticsConsent));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002533 File Offset: 0x00000733
		public void CheckAnalyticsConsent()
		{
			this._analyticsConsentBox.Show(new Action(this.ShowWelcomeScreen));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000254C File Offset: 0x0000074C
		public void ShowWelcomeScreen()
		{
			this._welcomeScreenBox.Show(new Action(this.ShowMainMenuPanel));
			this._titleScreenFooter.Show();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002570 File Offset: 0x00000770
		public void ShowMainMenuPanel()
		{
			this._mainMenuPanel.Show();
			this._titleScreenFooter.Show();
		}

		// Token: 0x04000014 RID: 20
		public readonly TitleScreen _titleScreen;

		// Token: 0x04000015 RID: 21
		public readonly AutoStarter _autoStarter;

		// Token: 0x04000016 RID: 22
		public readonly IStore _store;

		// Token: 0x04000017 RID: 23
		public readonly WelcomeScreenBox _welcomeScreenBox;

		// Token: 0x04000018 RID: 24
		public readonly MacOSPermissionsChecker _macOSPermissionsChecker;

		// Token: 0x04000019 RID: 25
		public readonly InitialLanguageChooser _initialLanguageChooser;

		// Token: 0x0400001A RID: 26
		public readonly PlayerDataLoader _playerDataLoader;

		// Token: 0x0400001B RID: 27
		public readonly MainMenuPanel _mainMenuPanel;

		// Token: 0x0400001C RID: 28
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x0400001D RID: 29
		public readonly TitleScreenFooter _titleScreenFooter;

		// Token: 0x0400001E RID: 30
		public readonly AnalyticsConsentBox _analyticsConsentBox;

		// Token: 0x0400001F RID: 31
		public readonly EditorBuildValidator _editorBuildValidator;

		// Token: 0x04000020 RID: 32
		public readonly AssetBundleValidator _assetBundleValidator;

		// Token: 0x04000021 RID: 33
		public readonly IntroBox _introBox;
	}
}
