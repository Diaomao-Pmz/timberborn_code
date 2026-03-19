using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.GameFactionSystem;
using Timberborn.GameOver;
using Timberborn.MainMenuSceneLoading;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameOverUI
{
	// Token: 0x02000004 RID: 4
	public class GameOverBox : IPanelController, ILoadableSingleton, IPanelBlocker
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public GameOverBox(PanelStack panelStack, EventBus eventBus, VisualElementLoader visualElementLoader, MainMenuSceneLoader mainMenuSceneLoader, DevModeManager devModeManager, FactionService factionService)
		{
			this._panelStack = panelStack;
			this._eventBus = eventBus;
			this._visualElementLoader = visualElementLoader;
			this._mainMenuSceneLoader = mainMenuSceneLoader;
			this._devModeManager = devModeManager;
			this._factionService = factionService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F0 File Offset: 0x000002F0
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/GameOverBox");
			UQueryExtensions.Q<Label>(this._root, "Flavor", null).text = this._factionService.Current.GameOverFlavor.Value;
			UQueryExtensions.Q<Label>(this._root, "Info", null).text = this._factionService.Current.GameOverMessage.Value;
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).ToggleDisplayStyle(false);
			UQueryExtensions.Q<Button>(this._root, "ContinueButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._panelStack.Pop(this);
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "ExitButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._mainMenuSceneLoader.SaveAndOpenMainMenu();
			}, 0);
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021D2 File Offset: 0x000003D2
		[OnEvent]
		public void OnGameOverEvent(GameOverEvent gameOverEvent)
		{
			if (!this._devModeManager.Enabled)
			{
				this._panelStack.PushOverlay(this);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021ED File Offset: 0x000003ED
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021F5 File Offset: 0x000003F5
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F8 File Offset: 0x000003F8
		public void OnUICancelled()
		{
		}

		// Token: 0x04000006 RID: 6
		public readonly PanelStack _panelStack;

		// Token: 0x04000007 RID: 7
		public readonly EventBus _eventBus;

		// Token: 0x04000008 RID: 8
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		public readonly MainMenuSceneLoader _mainMenuSceneLoader;

		// Token: 0x0400000A RID: 10
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400000B RID: 11
		public readonly FactionService _factionService;

		// Token: 0x0400000C RID: 12
		public VisualElement _root;
	}
}
