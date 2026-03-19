using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.GameSaveRepositorySystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000014 RID: 20
	public class SettlementList
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00003219 File Offset: 0x00001419
		public SettlementList(GameSaveRepository gameSaveRepository, VisualElementLoader visualElementLoader, DevModeManager devModeManager)
		{
			this._gameSaveRepository = gameSaveRepository;
			this._visualElementLoader = visualElementLoader;
			this._devModeManager = devModeManager;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003244 File Offset: 0x00001444
		public void Initialize(VisualElement root)
		{
			Asserts.FieldIsNull<SettlementList>(this, this._settlementListView, "_settlementListView");
			this._settlementListView = UQueryExtensions.Q<ListView>(root, "Settlements", null);
			this._settlementListView.makeItem = (() => this._visualElementLoader.LoadVisualElement("Options/ListViewItem"));
			this._settlementListView.bindItem = new Action<VisualElement, int>(this.BindSettlement);
			this._settlementListView.itemsSource = this._settlements;
			this._settlementListView.selectionChanged += this.OnSelectionChanged;
			this._settlementListView.virtualizationMethod = 1;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000032D8 File Offset: 0x000014D8
		public void LoadSettlements(Action onSettlementSelected)
		{
			this._onSettlementSelected = onSettlementSelected;
			this._settlements.AddRange(this._gameSaveRepository.GetAllSettlements());
			this._settlementListView.RefreshItems();
			this._settlementListView.SetSelection(0);
			this._settlementListView.ScrollToItem(0);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003325 File Offset: 0x00001525
		public void Clear()
		{
			this._onSettlementSelected = null;
			this._settlements.Clear();
			this._settlementListView.Clear();
			this._settlementListView.ClearSelection();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000334F File Offset: 0x0000154F
		public bool TryGetSelectedSettlement(out SettlementReference selectedSettlement)
		{
			selectedSettlement = (this._settlementListView.selectedItem as SettlementReference);
			return selectedSettlement != null;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000336B File Offset: 0x0000156B
		public void DeleteSettlement(SettlementReference settlement)
		{
			this._gameSaveRepository.DeleteSettlement(settlement);
			this.RemoveSettlementFromList(settlement);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003380 File Offset: 0x00001580
		public void RemoveSettlementFromList(SettlementReference settlementName)
		{
			int selectedIndex = this._settlementListView.selectedIndex;
			this._settlements.Remove(settlementName);
			this._settlementListView.RefreshItems();
			this.SelectSettlementOrLast(selectedIndex);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000033B8 File Offset: 0x000015B8
		public void OnSelectionChanged(IEnumerable<object> obj)
		{
			Action onSettlementSelected = this._onSettlementSelected;
			if (onSettlementSelected == null)
			{
				return;
			}
			onSettlementSelected();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000033CC File Offset: 0x000015CC
		public void BindSettlement(VisualElement visualElement, int i)
		{
			DevModeManager devModeManager = this._devModeManager;
			if (devModeManager != null && devModeManager.Enabled)
			{
				UQueryExtensions.Q<Label>(visualElement, "Text", null).text = Path.GetFileName(this._settlements[i].SaveDirectory)[0].ToString() + "/" + this._settlements[i].SettlementName;
				return;
			}
			UQueryExtensions.Q<Label>(visualElement, "Text", null).text = this._settlements[i].SettlementName;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003460 File Offset: 0x00001660
		public void SelectSettlementOrLast(int index)
		{
			this._settlementListView.ClearSelection();
			int selection = Mathf.Min(index, this._settlements.Count - 1);
			this._settlementListView.SetSelection(selection);
		}

		// Token: 0x04000058 RID: 88
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000059 RID: 89
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400005A RID: 90
		public ListView _settlementListView;

		// Token: 0x0400005B RID: 91
		public readonly List<SettlementReference> _settlements = new List<SettlementReference>();

		// Token: 0x0400005C RID: 92
		public Action _onSettlementSelected;

		// Token: 0x0400005D RID: 93
		public readonly DevModeManager _devModeManager;
	}
}
