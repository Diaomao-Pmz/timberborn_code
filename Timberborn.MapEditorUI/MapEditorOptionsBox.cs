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
	// Token: 0x02000009 RID: 9
	public class MapEditorOptionsBox : IOptionsBox, IPanelController, ILoadableSingleton
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000027CF File Offset: 0x000009CF
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

		// Token: 0x06000027 RID: 39 RVA: 0x0000280C File Offset: 0x00000A0C
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/MapEditorOptionsBox");
			UQueryExtensions.Q<Button>(this._root, "Resume", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnResumeClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "Save", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSaveClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "SaveAs", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSaveAsClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "Load", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnLoadClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "NewMap", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnNewMapClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "Bindings", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnBindingsClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "Settings", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSettingsClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "ExitToMenu", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnExitToMenuClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "ExitToDesktop", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnExitToDesktopClicked), 0);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000296A File Offset: 0x00000B6A
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002972 File Offset: 0x00000B72
		public void Show()
		{
			this._panelStack.PushOverlay(this);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002980 File Offset: 0x00000B80
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002983 File Offset: 0x00000B83
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002983 File Offset: 0x00000B83
		public void OnResumeClicked(ClickEvent evt)
		{
			this.Close();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000298B File Offset: 0x00000B8B
		public void OnSaveClicked(ClickEvent evt)
		{
			this._mapSaverLoader.Save(null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002999 File Offset: 0x00000B99
		public void OnSaveAsClicked(ClickEvent evt)
		{
			this._mapSaverLoader.SaveAs(null);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029A7 File Offset: 0x00000BA7
		public void OnLoadClicked(ClickEvent evt)
		{
			this._mapSaverLoader.LoadMap();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029B4 File Offset: 0x00000BB4
		public void OnNewMapClicked(ClickEvent evt)
		{
			this._mapSaverLoader.NewMap();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029C1 File Offset: 0x00000BC1
		public void OnBindingsClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPushOverlay(this._keyBindingsBox);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029D4 File Offset: 0x00000BD4
		public void OnSettingsClicked(ClickEvent evt)
		{
			this._panelStack.HideAndPushOverlay(this._settingsController);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029E7 File Offset: 0x00000BE7
		public void OnExitToMenuClicked(ClickEvent evt)
		{
			this.ShowExitDialog(new Action(this._mainMenuSceneLoader.SaveAndOpenMainMenu));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A00 File Offset: 0x00000C00
		public void OnExitToDesktopClicked(ClickEvent evt)
		{
			this.ShowExitDialog(new Action(GameQuitter.Quit));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A14 File Offset: 0x00000C14
		public void Close()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A22 File Offset: 0x00000C22
		public void ShowExitDialog(Action exitAction)
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(MapEditorOptionsBox.ExitChangesLostPromptLocKey).SetConfirmButton(exitAction).SetDefaultCancelButton().Show();
		}

		// Token: 0x0400002B RID: 43
		public static readonly string ExitChangesLostPromptLocKey = "Menu.ExitChangesLostPrompt";

		// Token: 0x0400002C RID: 44
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002D RID: 45
		public readonly MainMenuSceneLoader _mainMenuSceneLoader;

		// Token: 0x0400002E RID: 46
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400002F RID: 47
		public readonly ISettingsController _settingsController;

		// Token: 0x04000030 RID: 48
		public readonly KeyBindingsBox _keyBindingsBox;

		// Token: 0x04000031 RID: 49
		public readonly PanelStack _panelStack;

		// Token: 0x04000032 RID: 50
		public readonly MapSaverLoader _mapSaverLoader;

		// Token: 0x04000033 RID: 51
		public VisualElement _root;
	}
}
