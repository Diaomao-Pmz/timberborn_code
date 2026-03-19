using System;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.Localization;
using Timberborn.PlatformUtilities;
using Timberborn.SaveMetadataSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x0200000B RID: 11
	public class LoadGameBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000259C File Offset: 0x0000079C
		public LoadGameBox(GameSaveRepository gameSaveRepository, DialogBoxShower dialogBoxShower, IExplorerOpener explorerOpener, ILoc loc, VisualElementLoader visualElementLoader, PanelStack panelStack, ValidatingGameLoader validatingGameLoader, SettlementList settlementList, SaveList saveList, GameSaveModBox gameSaveModBox, GameSaveDeserializer gameSaveDeserializer, SaveMetadataSerializer saveMetadataSerializer)
		{
			this._gameSaveRepository = gameSaveRepository;
			this._dialogBoxShower = dialogBoxShower;
			this._explorerOpener = explorerOpener;
			this._loc = loc;
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._validatingGameLoader = validatingGameLoader;
			this._settlementList = settlementList;
			this._saveList = saveList;
			this._gameSaveModBox = gameSaveModBox;
			this._gameSaveDeserializer = gameSaveDeserializer;
			this._saveMetadataSerializer = saveMetadataSerializer;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000260C File Offset: 0x0000080C
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Options/LoadGameBox");
			this._load = UQueryExtensions.Q<Button>(this._root, "LoadButton", null);
			this._load.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.LoadGame();
			}, 0);
			this._deleteSettlement = UQueryExtensions.Q<Button>(this._root, "DeleteSettlementButton", null);
			this._deleteSettlement.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnDeleteSettlementButtonClicked), 0);
			this._deleteSave = UQueryExtensions.Q<Button>(this._root, "DeleteSaveButton", null);
			this._deleteSave.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnDeleteSaveButtonClicked), 0);
			this._showSavedMods = UQueryExtensions.Q<Button>(this._root, "ShowSavedModsButton", null);
			this._showSavedMods.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnShowSavedModsButtonClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "BrowseDirectoryButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._explorerOpener.OpenDirectory(this._gameSaveRepository.DefaultSaveDirectory);
			}, 0);
			this._saveList.Initialize(this._root, new Action(this.OnSaveSelectionChanged), new Action(this.OnDoubleClickActionRequested));
			this._settlementList.Initialize(this._root);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000276B File Offset: 0x0000096B
		public void Open()
		{
			this._panelStack.HideAndPushOverlay(this);
			this._settlementList.LoadSettlements(new Action(this.OnSettlementSelectionChanged));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002790 File Offset: 0x00000990
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002798 File Offset: 0x00000998
		public bool OnUIConfirmed()
		{
			return this.LoadGame();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027A0 File Offset: 0x000009A0
		public void OnUICancelled()
		{
			this._settlementList.Clear();
			this._saveList.Clear();
			this._panelStack.Pop(this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027C4 File Offset: 0x000009C4
		public void OnDoubleClickActionRequested()
		{
			this.LoadGame();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027D0 File Offset: 0x000009D0
		public bool LoadGame()
		{
			GameSaveItem gameSaveItem;
			if (this._saveList.TryGetSelectedSave(out gameSaveItem))
			{
				if (this._gameSaveRepository.SaveExists(gameSaveItem.SaveReference))
				{
					this._validatingGameLoader.LoadGame(gameSaveItem.SaveReference);
					return true;
				}
				Debug.LogWarning("Save: " + gameSaveItem.DisplayName + " doesn't exist, failed to load.");
			}
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002830 File Offset: 0x00000A30
		public void OnDeleteSettlementButtonClicked(ClickEvent evt)
		{
			SettlementReference settlement;
			if (this._settlementList.TryGetSelectedSettlement(out settlement))
			{
				this._dialogBoxShower.Create().SetMessage(this._loc.T<string>(LoadGameBox.DeleteSettlementPromptLocKey, settlement.SettlementName)).SetConfirmButton(delegate()
				{
					this._settlementList.DeleteSettlement(settlement);
				}).SetDefaultCancelButton().Show();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028A4 File Offset: 0x00000AA4
		public void OnDeleteSaveButtonClicked(ClickEvent evt)
		{
			GameSaveItem gameSaveItem;
			if (this._saveList.TryGetSelectedSave(out gameSaveItem))
			{
				this._dialogBoxShower.Create().SetMessage(this._loc.T<string>(LoadGameBox.DeleteSavePromptLocKey, gameSaveItem.DisplayName)).SetConfirmButton(delegate()
				{
					this.DeleteSave(gameSaveItem);
				}).SetDefaultCancelButton().Show();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002918 File Offset: 0x00000B18
		public void OnShowSavedModsButtonClicked(ClickEvent evt)
		{
			GameSaveItem gameSaveItem;
			if (this._saveList.TryGetSelectedSave(out gameSaveItem))
			{
				this._gameSaveModBox.Show(gameSaveItem);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002940 File Offset: 0x00000B40
		public void DeleteSave(GameSaveItem gameSaveItem)
		{
			this._saveList.DeleteSave(gameSaveItem);
			this.RemoveSettlementWithoutSavesFromList(gameSaveItem.SaveReference.SettlementReference);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000295F File Offset: 0x00000B5F
		public void RemoveSettlementWithoutSavesFromList(SettlementReference settlementReference)
		{
			if (this._saveList.Count == 0)
			{
				this._settlementList.RemoveSettlementFromList(settlementReference);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000297C File Offset: 0x00000B7C
		public void OnSettlementSelectionChanged()
		{
			SettlementReference settlement;
			if (this._settlementList.TryGetSelectedSettlement(out settlement))
			{
				this._deleteSettlement.SetEnabled(true);
				this._saveList.UpdateSaves(settlement);
				return;
			}
			this._deleteSettlement.SetEnabled(false);
			this._saveList.UpdateSaves(null);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029CC File Offset: 0x00000BCC
		public void OnSaveSelectionChanged()
		{
			GameSaveItem gameSaveItem;
			bool flag = this._saveList.TryGetSelectedSave(out gameSaveItem);
			this._load.SetEnabled(flag);
			this._deleteSave.SetEnabled(flag);
			this._showSavedMods.ToggleDisplayStyle(false);
			if (flag)
			{
				SaveMetadata saveMetadata = this._gameSaveDeserializer.ReadFromSaveFile<SaveMetadata>(gameSaveItem.SaveReference, this._saveMetadataSerializer);
				if (saveMetadata != null && saveMetadata.Mods.Any<ModReference>())
				{
					this._showSavedMods.ToggleDisplayStyle(true);
					this._showSavedMods.text = this._loc.T<int>(LoadGameBox.ShowSavedModsLocKey, saveMetadata.Mods.Length);
				}
			}
		}

		// Token: 0x0400001F RID: 31
		public static readonly string DeleteSettlementPromptLocKey = "Saving.DeleteSettlementPrompt";

		// Token: 0x04000020 RID: 32
		public static readonly string DeleteSavePromptLocKey = "Saving.DeleteSavePrompt";

		// Token: 0x04000021 RID: 33
		public static readonly string ShowSavedModsLocKey = "Modding.ShowSavedMods";

		// Token: 0x04000022 RID: 34
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000023 RID: 35
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000024 RID: 36
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x04000025 RID: 37
		public readonly ILoc _loc;

		// Token: 0x04000026 RID: 38
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000027 RID: 39
		public readonly PanelStack _panelStack;

		// Token: 0x04000028 RID: 40
		public readonly ValidatingGameLoader _validatingGameLoader;

		// Token: 0x04000029 RID: 41
		public readonly SettlementList _settlementList;

		// Token: 0x0400002A RID: 42
		public readonly SaveList _saveList;

		// Token: 0x0400002B RID: 43
		public readonly GameSaveModBox _gameSaveModBox;

		// Token: 0x0400002C RID: 44
		public readonly GameSaveDeserializer _gameSaveDeserializer;

		// Token: 0x0400002D RID: 45
		public readonly SaveMetadataSerializer _saveMetadataSerializer;

		// Token: 0x0400002E RID: 46
		public VisualElement _root;

		// Token: 0x0400002F RID: 47
		public Button _deleteSettlement;

		// Token: 0x04000030 RID: 48
		public Button _deleteSave;

		// Token: 0x04000031 RID: 49
		public Button _load;

		// Token: 0x04000032 RID: 50
		public Button _showSavedMods;
	}
}
