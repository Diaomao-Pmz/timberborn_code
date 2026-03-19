using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.GameDistricts;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000015 RID: 21
	public class DistrictListPanel
	{
		// Token: 0x06000087 RID: 135 RVA: 0x0000351F File Offset: 0x0000171F
		public DistrictListPanel(VisualElementLoader visualElementLoader, EntitySelectionService entitySelectionService, EventBus eventBus)
		{
			this._visualElementLoader = visualElementLoader;
			this._entitySelectionService = entitySelectionService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003547 File Offset: 0x00001747
		public void Initialize(VisualElement root)
		{
			this._root = UQueryExtensions.Q(root, "DistrictListPanel", null);
			this.InitializeDistrictList();
			this.Hide();
			this._eventBus.Register(this);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003573 File Offset: 0x00001773
		public void UpdateDistrictList()
		{
			this._districtListView.RefreshItems();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003580 File Offset: 0x00001780
		public void Show()
		{
			this._root.ToggleDisplayStyle(true);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000358E File Offset: 0x0000178E
		public void Hide()
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000359C File Offset: 0x0000179C
		[OnEvent]
		public void OnDistrictSelected(DistrictSelectedEvent districtSelectedEvent)
		{
			this.SelectOnList(districtSelectedEvent.DistrictCenter);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000035AA File Offset: 0x000017AA
		[OnEvent]
		public void OnDistrictUnselected(DistrictUnselectedEvent districtUnselectedEvent)
		{
			this._districtListView.ClearSelection();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000035B8 File Offset: 0x000017B8
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			DistrictCenter component = enteredFinishedStateEvent.BlockObject.GetComponent<DistrictCenter>();
			if (component)
			{
				this._districts.Add(component);
				this.UpdateDistrictListAndHeight();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000035EC File Offset: 0x000017EC
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			DistrictCenter component = exitedFinishedStateEvent.BlockObject.GetComponent<DistrictCenter>();
			if (component)
			{
				this._districts.Remove(component);
				this.UpdateDistrictListAndHeight();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003620 File Offset: 0x00001820
		public void SelectOnList(DistrictCenter districtCenter)
		{
			int num = this._districts.IndexOf(districtCenter);
			this._districtListView.SetSelectionWithoutNotify(Enumerables.One<int>(num));
			this._districtListView.ScrollToItem(num);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003658 File Offset: 0x00001858
		public void InitializeDistrictList()
		{
			this._districtListView = UQueryExtensions.Q<ListView>(this._root, "DistrictList", null);
			this._districtListView.makeItem = (() => this._visualElementLoader.LoadVisualElement("Game/Districts/DistrictListPanelItem"));
			this._districtListView.bindItem = delegate(VisualElement ve, int i)
			{
				UQueryExtensions.Q<Label>(ve, "Text", null).text = this._districts[i].DistrictName;
			};
			this._districtListView.itemsSource = this._districts;
			this._districtListView.selectionChanged += this.OnDistrictListSelectionChanged;
			this._districtListView.virtualizationMethod = 1;
			this.UpdateDistrictListHeight();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000036E4 File Offset: 0x000018E4
		public void OnDistrictListSelectionChanged(IEnumerable<object> obj)
		{
			DistrictCenter districtCenter = (DistrictCenter)obj.SingleOrDefault<object>();
			if (districtCenter)
			{
				this._entitySelectionService.SelectAndFocusOn(districtCenter);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003711 File Offset: 0x00001911
		public void UpdateDistrictListAndHeight()
		{
			this.UpdateDistrictListHeight();
			this.UpdateDistrictList();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003720 File Offset: 0x00001920
		public void UpdateDistrictListHeight()
		{
			int num = Math.Min(this._districts.Count * DistrictListPanel.ListViewItemHeight, DistrictListPanel.MaxListViewHeight);
			this._districtListView.style.height = (float)num;
		}

		// Token: 0x04000049 RID: 73
		public static readonly int ListViewItemHeight = 31;

		// Token: 0x0400004A RID: 74
		public static readonly int MaxListViewHeight = 155;

		// Token: 0x0400004B RID: 75
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400004C RID: 76
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400004D RID: 77
		public readonly EventBus _eventBus;

		// Token: 0x0400004E RID: 78
		public readonly List<DistrictCenter> _districts = new List<DistrictCenter>();

		// Token: 0x0400004F RID: 79
		public VisualElement _root;

		// Token: 0x04000050 RID: 80
		public ListView _districtListView;
	}
}
