using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.MapItemsUI;
using Timberborn.MapRepositorySystem;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorPersistenceUI
{
	// Token: 0x02000009 RID: 9
	public class SaveMapBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000024A8 File Offset: 0x000006A8
		public SaveMapBox(VisualElementLoader visualElementLoader, MapItemProvider mapItemProvider, PanelStack panelStack, IExplorerOpener explorerOpener, MapPersistenceController mapPersistenceController, ILoc loc, InputService inputService)
		{
			this._visualElementLoader = visualElementLoader;
			this._mapItemProvider = mapItemProvider;
			this._panelStack = panelStack;
			this._explorerOpener = explorerOpener;
			this._mapPersistenceController = mapPersistenceController;
			this._loc = loc;
			this._inputService = inputService;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024FC File Offset: 0x000006FC
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Options/SaveBox");
			UQueryExtensions.Q<Label>(this._root, "Header", null).text = this._loc.T(SaveMapBox.HeaderLocKey);
			this._mapName = UQueryExtensions.Q<TextField>(this._root, "SaveName", null);
			this._mapName.maxLength = 50;
			this._mapName.focusable = true;
			this._mapName.RegisterCallback<ChangeEvent<string>>(delegate(ChangeEvent<string> _)
			{
				this.UpdateSaveButton();
			}, 0);
			UQueryExtensions.Q<TextElement>(this._mapName, null, null).SetConfirmCancelActions(this._inputService, new Action(this.SaveMap), new Action(this.OnUICancelled));
			this._mapList = UQueryExtensions.Q<ListView>(this._root, "ItemList", null);
			this._mapList.makeItem = new Func<VisualElement>(this.CreateAndBind);
			this._mapList.bindItem = delegate(VisualElement ve, int i)
			{
				UQueryExtensions.Q<Label>(ve, "Text", null).text = this._maps[i].MapFileReference.Name;
			};
			this._mapList.itemsSource = this._maps;
			this._mapList.selectionChanged += this.InsertName;
			this._mapList.virtualizationMethod = 1;
			this._save = UQueryExtensions.Q<Button>(this._root, "SaveButton", null);
			this._save.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSaveButtonClicked), 0);
			this._save.SetEnabled(false);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "BrowseDirectoryButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnBrowseDirectoryButtonClicked), 0);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026B8 File Offset: 0x000008B8
		public void Open(Action successAction)
		{
			this._successAction = successAction;
			this._panelStack.HideAndPushOverlay(this);
			this._mapList.ClearSelection();
			this._mapList.ScrollToItem(0);
			this._mapName.Focus();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026EF File Offset: 0x000008EF
		public VisualElement GetPanel()
		{
			this._maps.AddRange(this._mapItemProvider.GetUserMaps());
			this._mapList.RefreshItems();
			return this._root;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002718 File Offset: 0x00000918
		public bool OnUIConfirmed()
		{
			if (this.MapNameValid)
			{
				this.SaveMap();
				return true;
			}
			return false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000272B File Offset: 0x0000092B
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002733 File Offset: 0x00000933
		public bool MapNameValid
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this._mapName.value);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002748 File Offset: 0x00000948
		public VisualElement CreateAndBind()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Options/ListViewItem");
			visualElement.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnMapListElementClick), 0);
			return visualElement;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000276D File Offset: 0x0000096D
		public void OnMapListElementClick(ClickEvent evt)
		{
			if (evt.clickCount == 2)
			{
				this.SaveMap();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000277E File Offset: 0x0000097E
		public void UpdateSaveButton()
		{
			this._save.SetEnabled(this.MapNameValid);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002791 File Offset: 0x00000991
		public void OnSaveButtonClicked(ClickEvent evt)
		{
			this.SaveMap();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000279C File Offset: 0x0000099C
		public void InsertName(IEnumerable<object> obj)
		{
			MapItem mapItem = obj.SingleOrDefault<object>() as MapItem;
			if (mapItem != null)
			{
				MapFileReference mapFileReference = mapItem.MapFileReference;
				if (mapFileReference.Resource)
				{
					throw new ArgumentException("Unexpected resource map.");
				}
				this._mapName.SetValueWithoutNotify(mapFileReference.Name);
				this._save.SetEnabled(true);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027F1 File Offset: 0x000009F1
		public void OnBrowseDirectoryButtonClicked(ClickEvent evt)
		{
			this._explorerOpener.OpenDirectory(MapRepository.UserMapsDirectory);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002804 File Offset: 0x00000A04
		public void SaveMap()
		{
			string value = this._mapName.value;
			if (this.MapNameValid)
			{
				this._mapPersistenceController.SaveAs(value, new Action(this.MapSavedCallback));
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000283D File Offset: 0x00000A3D
		public void MapSavedCallback()
		{
			Action successAction = this._successAction;
			this.Close();
			if (successAction == null)
			{
				return;
			}
			successAction();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002855 File Offset: 0x00000A55
		public void Close()
		{
			this._mapName.value = string.Empty;
			this.UpdateSaveButton();
			this._maps.Clear();
			this._successAction = null;
			this._panelStack.Pop(this);
		}

		// Token: 0x04000019 RID: 25
		public static readonly string HeaderLocKey = "MapEditor.SaveMap.Header";

		// Token: 0x0400001A RID: 26
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001B RID: 27
		public readonly MapItemProvider _mapItemProvider;

		// Token: 0x0400001C RID: 28
		public readonly PanelStack _panelStack;

		// Token: 0x0400001D RID: 29
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x0400001E RID: 30
		public readonly MapPersistenceController _mapPersistenceController;

		// Token: 0x0400001F RID: 31
		public readonly ILoc _loc;

		// Token: 0x04000020 RID: 32
		public readonly InputService _inputService;

		// Token: 0x04000021 RID: 33
		public VisualElement _root;

		// Token: 0x04000022 RID: 34
		public ListView _mapList;

		// Token: 0x04000023 RID: 35
		public TextField _mapName;

		// Token: 0x04000024 RID: 36
		public Button _save;

		// Token: 0x04000025 RID: 37
		public readonly List<MapItem> _maps = new List<MapItem>();

		// Token: 0x04000026 RID: 38
		public Action _successAction;
	}
}
