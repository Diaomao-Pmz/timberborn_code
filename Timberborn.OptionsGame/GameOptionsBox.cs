using System;
using Timberborn.CoreUI;
using Timberborn.ExperimentalModeSystem;
using Timberborn.GameExitSystem;
using Timberborn.GameSaveRepositorySystemUI;
using Timberborn.GameSaveRuntimeSystemUI;
using Timberborn.KeyBindingSystemUI;
using Timberborn.Options;
using Timberborn.SettingsSystemUI;
using Timberborn.SingletonSystem;
using Timberborn.Versioning;
using Timberborn.WebNavigation;
using UnityEngine.UIElements;

namespace Timberborn.OptionsGame
{
	// Token: 0x02000004 RID: 4
	public class GameOptionsBox : IOptionsBox, IPanelController, IPanelBlocker, ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BC File Offset: 0x000002BC
		public GameOptionsBox(VisualElementLoader visualElementLoader, PanelStack panelStack, UrlOpener urlOpener, LoadGameBox loadGameBox, ISettingsController settingsController, KeyBindingsBox keyBindingsBox, SaveGameBox saveGameBox, GoodbyeBoxFactory goodbyeBoxFactory, ExperimentalMode experimentalMode)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._urlOpener = urlOpener;
			this._loadGameBox = loadGameBox;
			this._settingsController = settingsController;
			this._keyBindingsBox = keyBindingsBox;
			this._saveGameBox = saveGameBox;
			this._goodbyeBoxFactory = goodbyeBoxFactory;
			this._experimentalMode = experimentalMode;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002114 File Offset: 0x00000314
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/GameOptionsBox");
			UQueryExtensions.Q<Button>(this._root, "ResumeButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ResumeClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "SaveGameButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SaveGameClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "LoadGameButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.LoadGameClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "BindingsButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.BindingsClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "SettingsButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SettingsClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "FeedbackButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.FeedbackClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "ExitToMenuButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ExitToMenuClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "ExitToDesktopButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ExitToDesktopClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "DiscordButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.DiscordClicked), 0);
			UQueryExtensions.Q<Label>(this._root, "GameVersion", null).text = GameVersions.CurrentVersion.Formatted;
			UQueryExtensions.Q<Label>(this._root, "Experimental", null).ToggleDisplayStyle(this._experimentalMode.IsExperimental);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022B6 File Offset: 0x000004B6
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022BE File Offset: 0x000004BE
		public void Show()
		{
			this._panelStack.PushOverlay(this);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022CC File Offset: 0x000004CC
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022CF File Offset: 0x000004CF
		public void OnUICancelled()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022CF File Offset: 0x000004CF
		public void ResumeClicked(ClickEvent evt)
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022DD File Offset: 0x000004DD
		public void SaveGameClicked(ClickEvent evt)
		{
			this._saveGameBox.Open();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022EA File Offset: 0x000004EA
		public void LoadGameClicked(ClickEvent evt)
		{
			this._loadGameBox.Open();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022F7 File Offset: 0x000004F7
		public void BindingsClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPushOverlay(this._keyBindingsBox);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000230A File Offset: 0x0000050A
		public void SettingsClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPushOverlay(this._settingsController);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000231D File Offset: 0x0000051D
		public void FeedbackClicked(ClickEvent evt)
		{
			this._urlOpener.OpenFeatureUpvote();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000232A File Offset: 0x0000052A
		public void ExitToMenuClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPushOverlay(this._goodbyeBoxFactory.ShowExitToMainMenu());
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002342 File Offset: 0x00000542
		public void ExitToDesktopClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPushOverlay(this._goodbyeBoxFactory.ShowExitToDesktop());
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000235A File Offset: 0x0000055A
		public void DiscordClicked(ClickEvent evt)
		{
			this._urlOpener.OpenDiscord();
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly PanelStack _panelStack;

		// Token: 0x04000008 RID: 8
		public readonly UrlOpener _urlOpener;

		// Token: 0x04000009 RID: 9
		public readonly LoadGameBox _loadGameBox;

		// Token: 0x0400000A RID: 10
		public readonly ISettingsController _settingsController;

		// Token: 0x0400000B RID: 11
		public readonly KeyBindingsBox _keyBindingsBox;

		// Token: 0x0400000C RID: 12
		public readonly SaveGameBox _saveGameBox;

		// Token: 0x0400000D RID: 13
		public readonly GoodbyeBoxFactory _goodbyeBoxFactory;

		// Token: 0x0400000E RID: 14
		public readonly ExperimentalMode _experimentalMode;

		// Token: 0x0400000F RID: 15
		public VisualElement _root;
	}
}
