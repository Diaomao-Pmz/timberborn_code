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
	// Token: 0x02000007 RID: 7
	public class SaveMapBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002498 File Offset: 0x00000698
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

		// Token: 0x06000017 RID: 23 RVA: 0x000024EC File Offset: 0x000006EC
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Options/SaveBox");
			this._root.Q("Header", null).text = this._loc.T(SaveMapBox.HeaderLocKey);
			this._mapName = this._root.Q("SaveName", null);
			this._mapName.maxLength = 50;
			this._mapName.focusable = true;
			this._mapName.RegisterCallback<ChangeEvent<string>>(delegate(ChangeEvent<string> _)
			{
				this.UpdateSaveButton();
			}, TrickleDown.NoTrickleDown);
			this._mapName.Q(null, null).SetConfirmCancelActions(this._inputService, new Action(this.SaveMap), new Action(this.OnUICancelled));
			this._mapList = this._root.Q("ItemList", null);
			this._mapList.makeItem = new Func<VisualElement>(this.CreateAndBind);
			this._mapList.bindItem = delegate(VisualElement ve, int i)
			{
				ve.Q("Text", null).text = this._maps[i].MapFileReference.Name;
			};
			this._mapList.itemsSource = this._maps;
			this._mapList.selectionChanged += this.InsertName;
			this._mapList.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
			this._save = this._root.Q("SaveButton", null);
			this._save.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSaveButtonClicked), TrickleDown.NoTrickleDown);
			this._save.SetEnabled(false);
			this._root.Q("CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, TrickleDown.NoTrickleDown);
			this._root.Q("BrowseDirectoryButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnBrowseDirectoryButtonClicked), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026A8 File Offset: 0x000008A8
		public void Open(Action successAction)
		{
			this._successAction = successAction;
			this._panelStack.HideAndPushOverlay(this);
			this._mapList.ClearSelection();
			this._mapList.ScrollToItem(0);
			this._mapName.Focus();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026DF File Offset: 0x000008DF
		public VisualElement GetPanel()
		{
			this._maps.AddRange(this._mapItemProvider.GetUserMaps());
			this._mapList.RefreshItems();
			return this._root;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002708 File Offset: 0x00000908
		public bool OnUIConfirmed()
		{
			if (this.MapNameValid)
			{
				this.SaveMap();
				return true;
			}
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000271B File Offset: 0x0000091B
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002723 File Offset: 0x00000923
		private bool MapNameValid
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this._mapName.value);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002738 File Offset: 0x00000938
		private VisualElement CreateAndBind()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Options/ListViewItem");
			visualElement.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnMapListElementClick), TrickleDown.NoTrickleDown);
			return visualElement;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000275D File Offset: 0x0000095D
		private void OnMapListElementClick(ClickEvent evt)
		{
			if (evt.clickCount == 2)
			{
				this.SaveMap();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000276E File Offset: 0x0000096E
		private void UpdateSaveButton()
		{
			this._save.SetEnabled(this.MapNameValid);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002781 File Offset: 0x00000981
		private void OnSaveButtonClicked(ClickEvent evt)
		{
			this.SaveMap();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000278C File Offset: 0x0000098C
		private void InsertName(IEnumerable<object> obj)
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

		// Token: 0x06000022 RID: 34 RVA: 0x000027E1 File Offset: 0x000009E1
		private void OnBrowseDirectoryButtonClicked(ClickEvent evt)
		{
			this._explorerOpener.OpenDirectory(MapRepository.UserMapsDirectory);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027F4 File Offset: 0x000009F4
		private void SaveMap()
		{
			string value = this._mapName.value;
			if (this.MapNameValid)
			{
				this._mapPersistenceController.SaveAs(value, new Action(this.MapSavedCallback));
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000282D File Offset: 0x00000A2D
		private void MapSavedCallback()
		{
			Action successAction = this._successAction;
			this.Close();
			if (successAction == null)
			{
				return;
			}
			successAction();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002845 File Offset: 0x00000A45
		private void Close()
		{
			this._mapName.value = string.Empty;
			this.UpdateSaveButton();
			this._maps.Clear();
			this._successAction = null;
			this._panelStack.Pop(this);
		}

		// Token: 0x04000011 RID: 17
		private static readonly string HeaderLocKey = "MapEditor.SaveMap.Header";

		// Token: 0x04000012 RID: 18
		private readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000013 RID: 19
		private readonly MapItemProvider _mapItemProvider;

		// Token: 0x04000014 RID: 20
		private readonly PanelStack _panelStack;

		// Token: 0x04000015 RID: 21
		private readonly IExplorerOpener _explorerOpener;

		// Token: 0x04000016 RID: 22
		private readonly MapPersistenceController _mapPersistenceController;

		// Token: 0x04000017 RID: 23
		private readonly ILoc _loc;

		// Token: 0x04000018 RID: 24
		private readonly InputService _inputService;

		// Token: 0x04000019 RID: 25
		private VisualElement _root;

		// Token: 0x0400001A RID: 26
		private ListView _mapList;

		// Token: 0x0400001B RID: 27
		private TextField _mapName;

		// Token: 0x0400001C RID: 28
		private Button _save;

		// Token: 0x0400001D RID: 29
		private readonly List<MapItem> _maps = new List<MapItem>();

		// Token: 0x0400001E RID: 30
		private Action _successAction;
	}
}
