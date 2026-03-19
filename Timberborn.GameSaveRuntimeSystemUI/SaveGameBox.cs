using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.GameSaveRepositorySystemUI;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.PlatformUtilities;
using Timberborn.SettlementNameSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.GameSaveRuntimeSystemUI
{
	// Token: 0x02000007 RID: 7
	public class SaveGameBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021D4 File Offset: 0x000003D4
		public SaveGameBox(GameSaver gameSaver, DialogBoxShower dialogBoxShower, SaveNameProvider saveNameProvider, ILoc loc, GameSaveRepository gameSaveRepository, SettlementReferenceService settlementReferenceService, VisualElementLoader visualElementLoader, PanelStack panelStack, IExplorerOpener explorerOpener, GameSaveItemFactory gameSaveItemFactory, GameSaveItemElementFactory gameSaveItemElementFactory, InputService inputService)
		{
			this._gameSaver = gameSaver;
			this._dialogBoxShower = dialogBoxShower;
			this._saveNameProvider = saveNameProvider;
			this._loc = loc;
			this._gameSaveRepository = gameSaveRepository;
			this._settlementReferenceService = settlementReferenceService;
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._explorerOpener = explorerOpener;
			this._gameSaveItemFactory = gameSaveItemFactory;
			this._gameSaveItemElementFactory = gameSaveItemElementFactory;
			this._inputService = inputService;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002250 File Offset: 0x00000450
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Options/SaveBox");
			this._saveName = UQueryExtensions.Q<TextField>(this._root, "SaveName", null);
			this._saveName.maxLength = 50;
			this._saveName.focusable = true;
			this._saveName.RegisterCallback<ChangeEvent<string>>(delegate(ChangeEvent<string> _)
			{
				this.UpdateSaveButton();
			}, 0);
			UQueryExtensions.Q<TextElement>(this._saveName, null, null).SetConfirmCancelActions(this._inputService, new Action(this.TrySaveGame), new Action(this.Close));
			this._saveList = UQueryExtensions.Q<ListView>(this._root, "ItemList", null);
			this._saveList.makeItem = new Func<VisualElement>(this.CreateAndBind);
			this._saveList.virtualizationMethod = 1;
			this._saveList.bindItem = delegate(VisualElement ve, int i)
			{
				this._gameSaveItemElementFactory.Bind(ve, this._saveItems[i]);
			};
			this._saveList.itemsSource = this._saveItems;
			this._saveList.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.UpdateSaveName();
			}, 0);
			this._saveButton = UQueryExtensions.Q<Button>(this._root, "SaveButton", null);
			this._saveButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSaveButtonButtonClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Close();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "BrowseDirectoryButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnBrowseDirectoryButtonClicked), 0);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023DC File Offset: 0x000005DC
		public void Open()
		{
			this._saveItems.AddRange(from save in this._gameSaveItemFactory.CreateForSettlement(this._settlementReferenceService.SettlementReference)
			where !save.IsAutosave
			select save);
			this._saveName.value = this._saveNameProvider.GetDefaultSaveName(this._saveItems.AsReadOnlyList<GameSaveItem>());
			this._panelStack.HideAndPushOverlay(this);
			this._saveList.RefreshItems();
			this._saveList.ClearSelection();
			this._saveList.ScrollToItem(0);
			this._saveName.Focus();
			this._isShown = true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000248F File Offset: 0x0000068F
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002497 File Offset: 0x00000697
		public bool OnUIConfirmed()
		{
			if (this.SaveNameEntered)
			{
				this.TrySaveGame();
				return true;
			}
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024AA File Offset: 0x000006AA
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000024B2 File Offset: 0x000006B2
		public bool SaveNameEntered
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this._saveName.value);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024C7 File Offset: 0x000006C7
		public VisualElement CreateAndBind()
		{
			VisualElement visualElement = this._gameSaveItemElementFactory.Create();
			visualElement.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSavedGameClick), 0);
			return visualElement;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024E7 File Offset: 0x000006E7
		public void OnSavedGameClick(ClickEvent evt)
		{
			if (evt.clickCount == 2)
			{
				this.TrySaveGame();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024F8 File Offset: 0x000006F8
		public void UpdateSaveButton()
		{
			this._saveButton.SetEnabled(this.SaveNameEntered);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000250B File Offset: 0x0000070B
		public void OnSaveButtonButtonClicked(ClickEvent evt)
		{
			this.TrySaveGame();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002514 File Offset: 0x00000714
		public void UpdateSaveName()
		{
			GameSaveItem gameSaveItem = this._saveList.selectedItem as GameSaveItem;
			if (gameSaveItem != null)
			{
				this._saveName.SetValueWithoutNotify(gameSaveItem.DisplayName);
				this._saveButton.SetEnabled(true);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002554 File Offset: 0x00000754
		public void OnBrowseDirectoryButtonClicked(ClickEvent evt)
		{
			string directory = this._gameSaveRepository.SettlementReferenceIntoDirectoryName(this._settlementReferenceService.SettlementReference);
			this._explorerOpener.OpenDirectory(directory);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002584 File Offset: 0x00000784
		public void TrySaveGame()
		{
			if (this.SaveNameEntered)
			{
				this.ValidateAndSave(new SaveReference(this._saveName.value, this._settlementReferenceService.SettlementReference));
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025B0 File Offset: 0x000007B0
		public void ValidateAndSave(SaveReference saveReference)
		{
			if (this._gameSaveRepository.NameIsInvalid(saveReference.SaveName))
			{
				this.ShowError("Name validation failed for: " + saveReference.SaveName);
				return;
			}
			if (this._gameSaveRepository.SaveExists(saveReference))
			{
				this.ShowOverwriteDialog(saveReference);
				return;
			}
			this.SaveGame(saveReference);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002604 File Offset: 0x00000804
		public void ShowError(string message)
		{
			Debug.LogWarning(message);
			this._dialogBoxShower.Create().SetLocalizedMessage(SaveGameBox.ErrorLocKey).Show();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002628 File Offset: 0x00000828
		public void ShowOverwriteDialog(SaveReference saveReference)
		{
			this._dialogBoxShower.Create().SetMessage(this._loc.T<string>(SaveGameBox.SaveExistsLocKey, saveReference.SaveName)).SetConfirmButton(delegate()
			{
				this.SaveGame(saveReference);
			}, this._loc.T(CommonLocKeys.OverwriteKey)).SetDefaultCancelButton(this._loc.T(CommonLocKeys.CancelKey)).Show();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026B0 File Offset: 0x000008B0
		public void SaveGame(SaveReference saveReference)
		{
			try
			{
				this._gameSaver.QueueSave(saveReference, new Action(this.Close));
			}
			catch (GameSaverException ex)
			{
				this.ShowError(string.Format("Error occured while saving: {0}", ex.InnerException));
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002700 File Offset: 0x00000900
		public void Close()
		{
			if (this._isShown)
			{
				this._isShown = false;
				this._saveList.ScrollToItem(0);
				this._saveItems.Clear();
				this._panelStack.Pop(this);
			}
		}

		// Token: 0x0400000B RID: 11
		public static readonly string SaveExistsLocKey = "Saving.SaveExists";

		// Token: 0x0400000C RID: 12
		public static readonly string ErrorLocKey = "Saving.Error";

		// Token: 0x0400000D RID: 13
		public readonly GameSaver _gameSaver;

		// Token: 0x0400000E RID: 14
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400000F RID: 15
		public readonly SaveNameProvider _saveNameProvider;

		// Token: 0x04000010 RID: 16
		public readonly ILoc _loc;

		// Token: 0x04000011 RID: 17
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000012 RID: 18
		public readonly SettlementReferenceService _settlementReferenceService;

		// Token: 0x04000013 RID: 19
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000014 RID: 20
		public readonly PanelStack _panelStack;

		// Token: 0x04000015 RID: 21
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x04000016 RID: 22
		public readonly GameSaveItemFactory _gameSaveItemFactory;

		// Token: 0x04000017 RID: 23
		public readonly GameSaveItemElementFactory _gameSaveItemElementFactory;

		// Token: 0x04000018 RID: 24
		public readonly InputService _inputService;

		// Token: 0x04000019 RID: 25
		public VisualElement _root;

		// Token: 0x0400001A RID: 26
		public ListView _saveList;

		// Token: 0x0400001B RID: 27
		public TextField _saveName;

		// Token: 0x0400001C RID: 28
		public Button _saveButton;

		// Token: 0x0400001D RID: 29
		public readonly List<GameSaveItem> _saveItems = new List<GameSaveItem>();

		// Token: 0x0400001E RID: 30
		public bool _isShown;
	}
}
