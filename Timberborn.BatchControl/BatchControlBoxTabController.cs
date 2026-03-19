using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000009 RID: 9
	public class BatchControlBoxTabController : IUpdatableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000025BA File Offset: 0x000007BA
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000025C2 File Offset: 0x000007C2
		public BatchControlTab CurrentTab { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025CB File Offset: 0x000007CB
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000025D3 File Offset: 0x000007D3
		public int LastOpenedTabIndex { get; private set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000025DC File Offset: 0x000007DC
		public BatchControlBoxTabController(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, IAssetLoader assetLoader, ILoc loc, EventBus eventBus, EntityRegistry entityRegistry, IEnumerable<BatchControlModule> batchControlModules)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._assetLoader = assetLoader;
			this._loc = loc;
			this._eventBus = eventBus;
			this._entityRegistry = entityRegistry;
			this._batchControlModules = batchControlModules.ToImmutableArray<BatchControlModule>();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000263F File Offset: 0x0000083F
		public IEnumerable<BatchControlTab> Tabs
		{
			get
			{
				return this._tabs.Keys;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000264C File Offset: 0x0000084C
		public void Initialize(VisualElement root)
		{
			this._tabButtons = UQueryExtensions.Q<VisualElement>(root, "TabButtons", null);
			this._content = UQueryExtensions.Q<VisualElement>(root, "Content", null);
			this._middleRow = UQueryExtensions.Q<VisualElement>(root, "MiddleRow", null);
			this._header = UQueryExtensions.Q<Label>(root, "Header", null);
			this.AddTabs();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026A7 File Offset: 0x000008A7
		public void UpdateEntities()
		{
			this._entities.AddRange(this._entityRegistry.Entities);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026C4 File Offset: 0x000008C4
		public int GetTabIndex(BatchControlTab batchControlTab)
		{
			return this._tabs.Keys.IndexOf(batchControlTab);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026D8 File Offset: 0x000008D8
		public void ShowTab(int index)
		{
			BatchControlTab batchControlTab = this._tabs.Keys.ElementAt(index);
			if (batchControlTab != this.CurrentTab)
			{
				this.SetNewTab(batchControlTab);
				this.UpdateActiveButtonClass(index);
				this.LastOpenedTabIndex = index;
				this._eventBus.Post(new BatchControlTabShownEvent(batchControlTab));
				this.CurrentTab.UpdateRowsVisibility();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002734 File Offset: 0x00000934
		public void UpdateSingleton()
		{
			BatchControlTab currentTab = this.CurrentTab;
			if (currentTab != null && currentTab.IsDirty)
			{
				this.Refresh();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002759 File Offset: 0x00000959
		public void LateUpdateSingleton()
		{
			BatchControlTab currentTab = this.CurrentTab;
			if (currentTab == null)
			{
				return;
			}
			currentTab.UpdateContent();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000276C File Offset: 0x0000096C
		public void Clear()
		{
			foreach (BatchControlTab batchControlTab in this._tabs.Keys.ToList<BatchControlTab>())
			{
				batchControlTab.Clear();
				this._tabs[batchControlTab] = null;
			}
			BatchControlTab currentTab = this.CurrentTab;
			if (currentTab != null)
			{
				currentTab.HideTab();
			}
			this._content.Clear();
			this.CurrentTab = null;
			this._entities.Clear();
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002810 File Offset: 0x00000A10
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			EntityComponent entity = entityDeletedEvent.Entity;
			this._entities.Remove(entity);
			foreach (BatchControlTab batchControlTab in this.Tabs)
			{
				batchControlTab.RemoveEntityRows(entity);
			}
			BatchControlTab currentTab = this.CurrentTab;
			if (currentTab == null)
			{
				return;
			}
			currentTab.UpdateRowsVisibility();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002880 File Offset: 0x00000A80
		public void AddTabs()
		{
			Dictionary<int, BatchControlTab> dictionary = new Dictionary<int, BatchControlTab>();
			foreach (BatchControlModule batchControlModule in this._batchControlModules)
			{
				foreach (KeyValuePair<int, BatchControlTab> keyValuePair in batchControlModule.Tabs)
				{
					int num;
					BatchControlTab batchControlTab;
					keyValuePair.Deconstruct(ref num, ref batchControlTab);
					int key3 = num;
					BatchControlTab value = batchControlTab;
					dictionary.Add(key3, value);
				}
			}
			int num2 = 0;
			foreach (int key2 in from key in dictionary.Keys
			orderby key
			select key)
			{
				BatchControlTab batchControlTab2 = dictionary[key2];
				this._tabs.Add(batchControlTab2, null);
				this.AddTabButton(batchControlTab2, num2++);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002994 File Offset: 0x00000B94
		public void AddTabButton(BatchControlTab batchControlTab, int tabIndex)
		{
			string elementName = "Game/BatchControl/BatchControlTabButton";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			Button button = UQueryExtensions.Q<Button>(visualElement, "BatchControlTabButton", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ShowTab(tabIndex);
			}, 0);
			this._tooltipRegistrar.Register(button, this._loc.T(batchControlTab.TabNameLocKey));
			string path = Path.Combine(BatchControlBoxTabController.SpriteDirectory, batchControlTab.TabImage);
			Sprite sprite = this._assetLoader.Load<Sprite>(path);
			UQueryExtensions.Q<Image>(visualElement, "BatchControlTabImage", null).sprite = sprite;
			this._tabButtons.Add(visualElement);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A48 File Offset: 0x00000C48
		public void SetNewTab(BatchControlTab batchControlTab)
		{
			if (this.CurrentTab == null)
			{
				this._eventBus.Register(this);
			}
			BatchControlTab currentTab = this.CurrentTab;
			if (currentTab != null)
			{
				currentTab.HideTab();
			}
			this._middleRow.ToggleDisplayStyle(batchControlTab.MiddleRowVisible);
			this._header.text = this._loc.T(batchControlTab.TabNameLocKey);
			this._content.Clear();
			this._content.Add(this.GetTabElement(batchControlTab));
			BatchControlTab currentTab2 = this.CurrentTab;
			if (currentTab2 == null)
			{
				return;
			}
			currentTab2.ShowTab();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public void UpdateActiveButtonClass(int index)
		{
			foreach (VisualElement visualElement in this._tabButtons.Children())
			{
				visualElement.RemoveFromClassList(BatchControlBoxTabController.ActiveButtonClass);
			}
			this._tabButtons[index].AddToClassList(BatchControlBoxTabController.ActiveButtonClass);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B40 File Offset: 0x00000D40
		public VisualElement GetTabElement(BatchControlTab batchControlTab)
		{
			VisualElement visualElement = this._tabs[batchControlTab] ?? batchControlTab.GetContent(this._entities);
			this._tabs[batchControlTab] = visualElement;
			this.CurrentTab = batchControlTab;
			return visualElement;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B80 File Offset: 0x00000D80
		public void Refresh()
		{
			this.CurrentTab.Clear();
			this._tabs[this.CurrentTab] = null;
			this._content.Clear();
			this._content.Add(this.GetTabElement(this.CurrentTab));
			this.CurrentTab.UpdateRowsVisibility();
			this.CurrentTab.IsDirty = false;
		}

		// Token: 0x04000020 RID: 32
		public static readonly string SpriteDirectory = "Sprites/BatchControl";

		// Token: 0x04000021 RID: 33
		public static readonly string ActiveButtonClass = "batch-control-panel__tab-button--active";

		// Token: 0x04000024 RID: 36
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000025 RID: 37
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000026 RID: 38
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000027 RID: 39
		public readonly ILoc _loc;

		// Token: 0x04000028 RID: 40
		public readonly EventBus _eventBus;

		// Token: 0x04000029 RID: 41
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x0400002A RID: 42
		public readonly ImmutableArray<BatchControlModule> _batchControlModules;

		// Token: 0x0400002B RID: 43
		public readonly Dictionary<BatchControlTab, VisualElement> _tabs = new Dictionary<BatchControlTab, VisualElement>();

		// Token: 0x0400002C RID: 44
		public readonly List<EntityComponent> _entities = new List<EntityComponent>();

		// Token: 0x0400002D RID: 45
		public VisualElement _tabButtons;

		// Token: 0x0400002E RID: 46
		public VisualElement _content;

		// Token: 0x0400002F RID: 47
		public VisualElement _middleRow;

		// Token: 0x04000030 RID: 48
		public Label _header;
	}
}
