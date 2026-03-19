using System;
using System.IO;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Modding;
using Timberborn.ModdingUI;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuModdingUI
{
	// Token: 0x02000009 RID: 9
	public class ModManagerBox : IPanelController, ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000290A File Offset: 0x00000B0A
		public ModManagerBox(VisualElementLoader visualElementLoader, PanelStack panelStack, ModRepository modRepository, IExplorerOpener explorerOpener, ModListView modListView, ModUploaderBox modUploaderBox, CreateModBox createModBox)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._modRepository = modRepository;
			this._explorerOpener = explorerOpener;
			this._modListView = modListView;
			this._modUploaderBox = modUploaderBox;
			this._createModBox = createModBox;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002948 File Offset: 0x00000B48
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Modding/ModManagerBox");
			Label restartWarning = UQueryExtensions.Q<Label>(this._root, "RestartWarning", null);
			restartWarning.ToggleDisplayStyle(false);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "ConfirmButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "BrowseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._explorerOpener.OpenDirectory(Path.Combine(UserDataFolder.Folder, UserFolderModsProvider.ModsDirectoryName));
				restartWarning.ToggleDisplayStyle(true);
			}, 0);
			this._uploadButton = UQueryExtensions.Q<Button>(this._root, "UploadButton", null);
			this._uploadButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._modUploaderBox.Show();
			}, 0);
			this._modListView.Initialize(this._root, this._modRepository.Mods);
			this._modListView.ListChanged += delegate(object _, EventArgs _)
			{
				restartWarning.ToggleDisplayStyle(true);
			};
			this._downloadButton = UQueryExtensions.Q<Button>(this._root, "DownloadButton", null);
			this._downloadButton.ToggleDisplayStyle(false);
			this._downloadButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._downloadAction();
				restartWarning.ToggleDisplayStyle(true);
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "CreateModButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._createModBox.Open();
			}, 0);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AC9 File Offset: 0x00000CC9
		public void SetDownloadAction(Action action)
		{
			Asserts.FieldIsNotNull<ModManagerBox>(this, action, "action");
			this._downloadAction = action;
			this._downloadButton.ToggleDisplayStyle(true);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002AEA File Offset: 0x00000CEA
		public void Open()
		{
			this._panelStack.HideAndPushOverlay(this);
			this._modListView.ResetScroll();
			this._uploadButton.ToggleDisplayStyle(this._modUploaderBox.HasUploader);
			this._isShown = true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B20 File Offset: 0x00000D20
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B28 File Offset: 0x00000D28
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B2B File Offset: 0x00000D2B
		public void OnUICancelled()
		{
			this._isShown = false;
			this._panelStack.Pop(this);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B40 File Offset: 0x00000D40
		public void UpdateSingleton()
		{
			if (this._isShown)
			{
				this._modListView.Update();
			}
		}

		// Token: 0x0400002A RID: 42
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002B RID: 43
		public readonly PanelStack _panelStack;

		// Token: 0x0400002C RID: 44
		public readonly ModRepository _modRepository;

		// Token: 0x0400002D RID: 45
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x0400002E RID: 46
		public readonly ModListView _modListView;

		// Token: 0x0400002F RID: 47
		public readonly ModUploaderBox _modUploaderBox;

		// Token: 0x04000030 RID: 48
		public readonly CreateModBox _createModBox;

		// Token: 0x04000031 RID: 49
		public VisualElement _root;

		// Token: 0x04000032 RID: 50
		public Button _uploadButton;

		// Token: 0x04000033 RID: 51
		public Button _downloadButton;

		// Token: 0x04000034 RID: 52
		public Action _downloadAction;

		// Token: 0x04000035 RID: 53
		public bool _isShown;
	}
}
