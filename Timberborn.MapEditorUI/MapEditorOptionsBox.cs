using System;
using Timberborn.ApplicationLifetime;
using Timberborn.CoreUI;
using Timberborn.KeyBindingSystemUI;
using Timberborn.MainMenuSceneLoading;
using Timberborn.MapEditorPersistenceUI;
using Timberborn.Options;
using Timberborn.SettingsSystemUI;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorUI
{
	// Token: 0x02000006 RID: 6
	internal class MapEditorOptionsBox : IOptionsBox, IPanelController, ILoadableSingleton
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002566 File Offset: 0x00000766
		public MapEditorOptionsBox(VisualElementLoader visualElementLoader, MainMenuSceneLoader mainMenuSceneLoader, DialogBoxShower dialogBoxShower, ISettingsController settingsController, KeyBindingsBox keyBindingsBox, PanelStack panelStack, MapSaverLoader mapSaverLoader)
		{
			this._visualElementLoader = visualElementLoader;
			this._mainMenuSceneLoader = mainMenuSceneLoader;
			this._dialogBoxShower = dialogBoxShower;
			this._settingsController = settingsController;
			this._keyBindingsBox = keyBindingsBox;
			this._panelStack = panelStack;
			this._mapSaverLoader = mapSaverLoader;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025A4 File Offset: 0x000007A4
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/MapEditorOptionsBox");
			this._root.Q("Resume", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnResumeClicked), TrickleDown.NoTrickleDown);
			this._root.Q("Save", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSaveClicked), TrickleDown.NoTrickleDown);
			this._root.Q("SaveAs", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSaveAsClicked), TrickleDown.NoTrickleDown);
			this._root.Q("Load", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnLoadClicked), TrickleDown.NoTrickleDown);
			this._root.Q("NewMap", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnNewMapClicked), TrickleDown.NoTrickleDown);
			this._root.Q("Bindings", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnBindingsClicked), TrickleDown.NoTrickleDown);
			this._root.Q("Settings", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSettingsClicked), TrickleDown.NoTrickleDown);
			this._root.Q("ExitToMenu", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnExitToMenuClicked), TrickleDown.NoTrickleDown);
			this._root.Q("ExitToDesktop", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnExitToDesktopClicked), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002702 File Offset: 0x00000902
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000270A File Offset: 0x0000090A
		public void Show()
		{
			this._panelStack.PushOverlay(this);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002718 File Offset: 0x00000918
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000271B File Offset: 0x0000091B
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002723 File Offset: 0x00000923
		private void OnResumeClicked(ClickEvent evt)
		{
			this.Close();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000272B File Offset: 0x0000092B
		private void OnSaveClicked(ClickEvent evt)
		{
			this._mapSaverLoader.Save(null);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002739 File Offset: 0x00000939
		private void OnSaveAsClicked(ClickEvent evt)
		{
			this._mapSaverLoader.SaveAs(null);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002747 File Offset: 0x00000947
		private void OnLoadClicked(ClickEvent evt)
		{
			this._mapSaverLoader.LoadMap();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002754 File Offset: 0x00000954
		private void OnNewMapClicked(ClickEvent evt)
		{
			this._mapSaverLoader.NewMap();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002761 File Offset: 0x00000961
		private void OnBindingsClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPushOverlay(this._keyBindingsBox);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002774 File Offset: 0x00000974
		private void OnSettingsClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPushOverlay(this._settingsController);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002787 File Offset: 0x00000987
		private void OnExitToMenuClicked(ClickEvent evt)
		{
			this.ShowExitDialog(new Action(this._mainMenuSceneLoader.SaveAndOpenMainMenu));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027A0 File Offset: 0x000009A0
		private void OnExitToDesktopClicked(ClickEvent evt)
		{
			this.ShowExitDialog(new Action(GameQuitter.Quit));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027B4 File Offset: 0x000009B4
		private void Close()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027C2 File Offset: 0x000009C2
		private void ShowExitDialog(Action exitAction)
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(MapEditorOptionsBox.ExitChangesLostPromptLocKey).SetConfirmButton(exitAction).SetDefaultCancelButton().Show();
		}

		// Token: 0x0400001E RID: 30
		private static readonly string ExitChangesLostPromptLocKey = "Menu.ExitChangesLostPrompt";

		// Token: 0x0400001F RID: 31
		private readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000020 RID: 32
		private readonly MainMenuSceneLoader _mainMenuSceneLoader;

		// Token: 0x04000021 RID: 33
		private readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000022 RID: 34
		private readonly ISettingsController _settingsController;

		// Token: 0x04000023 RID: 35
		private readonly KeyBindingsBox _keyBindingsBox;

		// Token: 0x04000024 RID: 36
		private readonly PanelStack _panelStack;

		// Token: 0x04000025 RID: 37
		private readonly MapSaverLoader _mapSaverLoader;

		// Token: 0x04000026 RID: 38
		private VisualElement _root;
	}
}
