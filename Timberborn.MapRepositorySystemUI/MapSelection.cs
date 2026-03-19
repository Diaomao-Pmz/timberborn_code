using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.MapItemsUI;
using Timberborn.MapRepositorySystem;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x0200000B RID: 11
	public class MapSelection
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000021 RID: 33 RVA: 0x000024C4 File Offset: 0x000006C4
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x000024FC File Offset: 0x000006FC
		public event EventHandler SelectedMapChanged;

		// Token: 0x06000023 RID: 35 RVA: 0x00002531 File Offset: 0x00000731
		public MapSelection(MapItemElementFactory mapItemElementFactory, MapItemProvider mapItemProvider, SelectedMapPanel selectedMapPanel, EventBus eventBus, IExplorerOpener explorerOpener, MapDownloader mapDownloader)
		{
			this._mapItemElementFactory = mapItemElementFactory;
			this._mapItemProvider = mapItemProvider;
			this._selectedMapPanel = selectedMapPanel;
			this._eventBus = eventBus;
			this._explorerOpener = explorerOpener;
			this._mapDownloader = mapDownloader;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002571 File Offset: 0x00000771
		public void InitializeWithMapGoalsShown(VisualElement root, Action doubleClickAction)
		{
			this.Initialize(root, true, doubleClickAction);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000257C File Offset: 0x0000077C
		public void InitializeWithMapGoalsHidden(VisualElement root, Action doubleClickAction)
		{
			this.Initialize(root, false, doubleClickAction);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002587 File Offset: 0x00000787
		public void Open()
		{
			this._selectedMapPanel.Open();
			this.ShowOfficialMaps();
			this._eventBus.Register(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025A8 File Offset: 0x000007A8
		public void Clear()
		{
			this._mapItemElementFactory.Clear();
			this._maps.Clear();
			this._mapList.Clear();
			this._mapList.ClearSelection();
			this._selectedMapPanel.ClearSelection();
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025F8 File Offset: 0x000007F8
		public bool TryGetSelectedMap(out MapItem selectedMap)
		{
			selectedMap = (this._mapList.selectedItem as MapItem);
			return selectedMap != null;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002614 File Offset: 0x00000814
		[OnEvent]
		public void OnMapRepositoryChanged(MapRepositoryChangedEvent mapRepositoryChangedEvent)
		{
			MapItem selectedMap = this._mapList.selectedItem as MapItem;
			if (this._officialMapsShown)
			{
				this.ShowOfficialMaps();
			}
			else
			{
				this.ShowCustomMaps();
			}
			if (selectedMap != null)
			{
				int num = this._maps.FindIndex((MapItem map) => map.MapFileReference.Equals(selectedMap.MapFileReference));
				if (num >= 0)
				{
					this._mapList.SetSelection(num);
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002684 File Offset: 0x00000884
		public void Initialize(VisualElement root, bool showMapGoals, Action doubleClickAction)
		{
			this._mapList = UQueryExtensions.Q<ListView>(root, "MapList", null);
			this._mapList.makeItem = new Func<VisualElement>(this.CreateAndBind);
			this._mapList.bindItem = delegate(VisualElement ve, int i)
			{
				this._mapItemElementFactory.Bind(ve, this._maps[i], showMapGoals);
			};
			this._mapList.itemsSource = this._maps;
			this._doubleClickAction = doubleClickAction;
			this._mapList.selectionChanged += this.OnSelectionChanged;
			this._mapList.virtualizationMethod = 1;
			if (showMapGoals)
			{
				this._selectedMapPanel.InitializeWithFlexibleStartInfoShown(root);
			}
			else
			{
				this._selectedMapPanel.InitializeWithFlexibleStartInfoHidden(root);
			}
			this._officialMapsButton = UQueryExtensions.Q<Button>(root, "OfficialMapsButton", null);
			this._officialMapsButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ShowOfficialMaps();
			}, 0);
			this._customMapsButton = UQueryExtensions.Q<Button>(root, "CustomMapsButton", null);
			this._customMapsButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ShowCustomMaps();
			}, 0);
			this._downloadButton = UQueryExtensions.Q<Button>(root, "DownloadButton", null);
			this._downloadButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnDownloadClicked), 0);
			this._browseButton = UQueryExtensions.Q<Button>(root, "BrowseButton", null);
			this._browseButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnBrowseClicked), 0);
			this._emptyLabel = UQueryExtensions.Q<Label>(root, "EmptyText", null);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027FC File Offset: 0x000009FC
		public VisualElement CreateAndBind()
		{
			VisualElement visualElement = this._mapItemElementFactory.Create();
			visualElement.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClickMapItem), 0);
			return visualElement;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000281C File Offset: 0x00000A1C
		public void OnClickMapItem(ClickEvent clickEvent)
		{
			if (clickEvent.clickCount == 2)
			{
				this._doubleClickAction();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002834 File Offset: 0x00000A34
		public void OnSelectionChanged(IEnumerable<object> obj)
		{
			MapItem mapItem;
			if (this.TryGetSelectedMap(out mapItem))
			{
				this._selectedMapPanel.Update(mapItem);
			}
			else
			{
				this._selectedMapPanel.ClearSelection();
			}
			EventHandler selectedMapChanged = this.SelectedMapChanged;
			if (selectedMapChanged == null)
			{
				return;
			}
			selectedMapChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000287A File Offset: 0x00000A7A
		public void ShowOfficialMaps()
		{
			this.ShowMaps(this._mapItemProvider.GetOfficialMaps());
			this.UpdateVisualElements(true);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002894 File Offset: 0x00000A94
		public void ShowCustomMaps()
		{
			this.ShowMaps(this._mapItemProvider.GetCustomMaps());
			this.UpdateVisualElements(false);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028B0 File Offset: 0x00000AB0
		public void ShowMaps(IEnumerable<MapItem> mapsToShow)
		{
			this._maps.Clear();
			this._maps.AddRange(mapsToShow);
			this._mapList.RefreshItems();
			this._mapList.ClearSelection();
			this._mapList.SetSelection(0);
			this._mapList.ScrollToItem(0);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002904 File Offset: 0x00000B04
		public void UpdateVisualElements(bool officialMapsShown)
		{
			this._officialMapsShown = officialMapsShown;
			this._officialMapsButton.EnableInClassList(MapSelection.SelectedClass, this._officialMapsShown);
			this._customMapsButton.EnableInClassList(MapSelection.SelectedClass, !this._officialMapsShown);
			this._emptyLabel.ToggleDisplayStyle(this._maps.Count == 0);
			this._downloadButton.ToggleDisplayStyle(!officialMapsShown && this._mapDownloader.HasDownloader);
			this._browseButton.ToggleDisplayStyle(!officialMapsShown);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000298B File Offset: 0x00000B8B
		public void OnDownloadClicked(ClickEvent evt)
		{
			this._mapDownloader.Download();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002998 File Offset: 0x00000B98
		public void OnBrowseClicked(ClickEvent evt)
		{
			this._explorerOpener.OpenDirectory(MapRepository.UserMapsDirectory);
		}

		// Token: 0x04000019 RID: 25
		public static readonly string SelectedClass = "selected";

		// Token: 0x0400001B RID: 27
		public readonly MapItemElementFactory _mapItemElementFactory;

		// Token: 0x0400001C RID: 28
		public readonly MapItemProvider _mapItemProvider;

		// Token: 0x0400001D RID: 29
		public readonly SelectedMapPanel _selectedMapPanel;

		// Token: 0x0400001E RID: 30
		public readonly EventBus _eventBus;

		// Token: 0x0400001F RID: 31
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x04000020 RID: 32
		public readonly MapDownloader _mapDownloader;

		// Token: 0x04000021 RID: 33
		public ListView _mapList;

		// Token: 0x04000022 RID: 34
		public Button _officialMapsButton;

		// Token: 0x04000023 RID: 35
		public Button _customMapsButton;

		// Token: 0x04000024 RID: 36
		public Button _downloadButton;

		// Token: 0x04000025 RID: 37
		public Button _browseButton;

		// Token: 0x04000026 RID: 38
		public Label _emptyLabel;

		// Token: 0x04000027 RID: 39
		public readonly List<MapItem> _maps = new List<MapItem>();

		// Token: 0x04000028 RID: 40
		public bool _officialMapsShown;

		// Token: 0x04000029 RID: 41
		public Action _doubleClickAction;
	}
}
