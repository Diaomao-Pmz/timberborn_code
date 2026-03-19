using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.GameSaveRepositorySystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x0200000F RID: 15
	public class SaveList
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002BCC File Offset: 0x00000DCC
		public SaveList(GameSaveRepository gameSaveRepository, SaveThumbnailCache saveThumbnailCache, GameSaveItemFactory gameSaveItemFactory, GameSaveItemElementFactory gameSaveItemElementFactory)
		{
			this._gameSaveRepository = gameSaveRepository;
			this._saveThumbnailCache = saveThumbnailCache;
			this._gameSaveItemFactory = gameSaveItemFactory;
			this._gameSaveItemElementFactory = gameSaveItemElementFactory;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002BFC File Offset: 0x00000DFC
		public int Count
		{
			get
			{
				return this._saves.Count;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C0C File Offset: 0x00000E0C
		public void Initialize(VisualElement root, Action onSaveSelectionChanged, Action doubleClickAction)
		{
			Asserts.FieldIsNull<SaveList>(this, this._saveListView, "_saveListView");
			this._saveListView = UQueryExtensions.Q<ListView>(root, "Saves", null);
			this._doubleClickAction = doubleClickAction;
			this._saveListView.makeItem = new Func<VisualElement>(this.CreateAndBind);
			this._saveListView.bindItem = delegate(VisualElement ve, int i)
			{
				this._gameSaveItemElementFactory.Bind(ve, this._saves[i]);
			};
			this._saveListView.itemsSource = this._saves;
			this._saveListView.selectionChanged += this.OnSaveSelectionChanged;
			this._saveListView.virtualizationMethod = 1;
			this._thumbnail = UQueryExtensions.Q<VisualElement>(root, "Thumbnail", null);
			this._thumbnailImage = UQueryExtensions.Q<Image>(root, "ThumbnailImage", null);
			this._onSaveSelectionChanged = onSaveSelectionChanged;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public void Clear()
		{
			this._saves.Clear();
			this._saveListView.Clear();
			this._saveListView.ClearSelection();
			this._saveThumbnailCache.Clear();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CFE File Offset: 0x00000EFE
		public bool TryGetSelectedSave(out GameSaveItem selectedSave)
		{
			selectedSave = (this._saveListView.selectedItem as GameSaveItem);
			return selectedSave != null;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D18 File Offset: 0x00000F18
		public void DeleteSave(GameSaveItem gameSaveItem)
		{
			int selectedIndex = this._saveListView.selectedIndex;
			this._gameSaveRepository.DeleteSave(gameSaveItem.SaveReference);
			this._saves.Remove(gameSaveItem);
			this._saveListView.RefreshItems();
			this.SelectSaveOrLast(selectedIndex);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D64 File Offset: 0x00000F64
		public void UpdateSaves(SettlementReference settlement)
		{
			this._saves.Clear();
			if (settlement != null)
			{
				this._saves.AddRange(this._gameSaveItemFactory.CreateForSettlement(settlement));
			}
			this._saveListView.RefreshItems();
			this._saveListView.ClearSelection();
			this._saveListView.SetSelection(0);
			this._saveListView.ScrollToItem(0);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002DCA File Offset: 0x00000FCA
		public VisualElement CreateAndBind()
		{
			VisualElement visualElement = this._gameSaveItemElementFactory.Create();
			visualElement.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClickEvent), 0);
			return visualElement;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DEA File Offset: 0x00000FEA
		public void OnClickEvent(ClickEvent evt)
		{
			if (evt.clickCount == 2)
			{
				this._doubleClickAction();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E00 File Offset: 0x00001000
		public void OnSaveSelectionChanged(IEnumerable<object> selectedSaves)
		{
			GameSaveItem gameSaveItem;
			if (this.TryGetSelectedSave(out gameSaveItem))
			{
				this._thumbnailImage.image = this._saveThumbnailCache.GetThumbnail(gameSaveItem.SaveReference);
				this._thumbnail.ToggleDisplayStyle(true);
			}
			else
			{
				this._thumbnailImage.image = null;
				this._thumbnail.ToggleDisplayStyle(false);
			}
			Action onSaveSelectionChanged = this._onSaveSelectionChanged;
			if (onSaveSelectionChanged == null)
			{
				return;
			}
			onSaveSelectionChanged();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E6C File Offset: 0x0000106C
		public void SelectSaveOrLast(int index)
		{
			this._saveListView.ClearSelection();
			int selection = Mathf.Min(index, this._saves.Count - 1);
			this._saveListView.SetSelection(selection);
		}

		// Token: 0x0400003B RID: 59
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x0400003C RID: 60
		public readonly SaveThumbnailCache _saveThumbnailCache;

		// Token: 0x0400003D RID: 61
		public readonly GameSaveItemFactory _gameSaveItemFactory;

		// Token: 0x0400003E RID: 62
		public readonly GameSaveItemElementFactory _gameSaveItemElementFactory;

		// Token: 0x0400003F RID: 63
		public readonly List<GameSaveItem> _saves = new List<GameSaveItem>();

		// Token: 0x04000040 RID: 64
		public ListView _saveListView;

		// Token: 0x04000041 RID: 65
		public VisualElement _thumbnail;

		// Token: 0x04000042 RID: 66
		public Image _thumbnailImage;

		// Token: 0x04000043 RID: 67
		public Action _onSaveSelectionChanged;

		// Token: 0x04000044 RID: 68
		public Action _doubleClickAction;
	}
}
