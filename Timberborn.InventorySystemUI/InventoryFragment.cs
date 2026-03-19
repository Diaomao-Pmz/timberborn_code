using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.InventorySystem;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x02000010 RID: 16
	public class InventoryFragment
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public InventoryFragment(InformationalRowsFactory informationalRowsFactory, VisualElement root, ScrollView rowsRoot, bool showEmptyRows, bool showRowLimit, Label noGoodInStockMessage, bool showNoGoodInStockMessage)
		{
			this._informationalRowsFactory = informationalRowsFactory;
			this._root = root;
			this._rowsRoot = rowsRoot;
			this._showEmptyRows = showEmptyRows;
			this._showRowLimit = showRowLimit;
			this._noGoodInStockMessage = noGoodInStockMessage;
			this._showNoGoodInStockMessage = showNoGoodInStockMessage;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C48 File Offset: 0x00000E48
		public void ShowFragment(Inventory inventory)
		{
			this._inventory = inventory;
			bool flag = inventory.HasComponent<InventoryLimitRowHiderSpec>();
			IEnumerable<InformationalRow> collection = (this._showRowLimit && !flag) ? this._informationalRowsFactory.CreateRowsWithLimits(this._inventory, this._rowsRoot) : this._informationalRowsFactory.CreateRowsWithoutLimits(this._inventory, this._rowsRoot);
			this._rows.AddRange(collection);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CAB File Offset: 0x00000EAB
		public void ClearFragment()
		{
			this._rowsRoot.Clear();
			this._rows.Clear();
			this._inventory = null;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CCA File Offset: 0x00000ECA
		public void UpdateFragment()
		{
			if (this._inventory && this._inventory.Enabled)
			{
				this._root.ToggleDisplayStyle(true);
				this.UpdateInventoryRows();
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D08 File Offset: 0x00000F08
		public void UpdateInventoryRows()
		{
			bool flag = true;
			foreach (InformationalRow informationalRow in this._rows)
			{
				if (this._showEmptyRows || informationalRow.CurrentAmount > 0)
				{
					informationalRow.ShowUpdated();
					flag = false;
				}
				else
				{
					informationalRow.Hide();
				}
			}
			this._noGoodInStockMessage.ToggleDisplayStyle(this._showNoGoodInStockMessage && flag);
			this._rowsRoot.ToggleDisplayStyle(!this._showNoGoodInStockMessage || (this._showNoGoodInStockMessage && !flag));
		}

		// Token: 0x04000036 RID: 54
		public readonly InformationalRowsFactory _informationalRowsFactory;

		// Token: 0x04000037 RID: 55
		public readonly VisualElement _root;

		// Token: 0x04000038 RID: 56
		public readonly ScrollView _rowsRoot;

		// Token: 0x04000039 RID: 57
		public readonly bool _showEmptyRows;

		// Token: 0x0400003A RID: 58
		public readonly Label _noGoodInStockMessage;

		// Token: 0x0400003B RID: 59
		public readonly bool _showNoGoodInStockMessage;

		// Token: 0x0400003C RID: 60
		public readonly List<InformationalRow> _rows = new List<InformationalRow>();

		// Token: 0x0400003D RID: 61
		public Inventory _inventory;

		// Token: 0x0400003E RID: 62
		public readonly bool _showRowLimit;

		// Token: 0x02000011 RID: 17
		public class Builder
		{
			// Token: 0x06000048 RID: 72 RVA: 0x00002DB0 File Offset: 0x00000FB0
			public Builder(VisualElementLoader visualElementLoader, InformationalRowsFactory informationalRowsFactory, VisualElement root)
			{
				this._visualElementLoader = visualElementLoader;
				this._informationalRowsFactory = informationalRowsFactory;
				this._root = root;
			}

			// Token: 0x06000049 RID: 73 RVA: 0x00002DCD File Offset: 0x00000FCD
			public InventoryFragment.Builder ShowEmptyRows()
			{
				this._showEmptyRows = true;
				return this;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002DD7 File Offset: 0x00000FD7
			public InventoryFragment.Builder ShowRowLimit()
			{
				this._showRowLimit = true;
				return this;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00002DE1 File Offset: 0x00000FE1
			public InventoryFragment.Builder ShowNoGoodInStockMessage()
			{
				this._showNoGoodInStockMessage = true;
				return this;
			}

			// Token: 0x0600004C RID: 76 RVA: 0x00002DEC File Offset: 0x00000FEC
			public InventoryFragment Build()
			{
				string elementName = "Game/EntityPanel/InventoryFragment";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				InventoryFragment result = new InventoryFragment(this._informationalRowsFactory, this._root, UQueryExtensions.Q<ScrollView>(visualElement, "Content", null), this._showEmptyRows, this._showRowLimit, UQueryExtensions.Q<Label>(visualElement, "IsEmpty", null), this._showNoGoodInStockMessage);
				this._root.Add(visualElement);
				this._root.ToggleDisplayStyle(false);
				return result;
			}

			// Token: 0x0400003F RID: 63
			public readonly VisualElementLoader _visualElementLoader;

			// Token: 0x04000040 RID: 64
			public readonly InformationalRowsFactory _informationalRowsFactory;

			// Token: 0x04000041 RID: 65
			public readonly VisualElement _root;

			// Token: 0x04000042 RID: 66
			public bool _showEmptyRows;

			// Token: 0x04000043 RID: 67
			public bool _showRowLimit;

			// Token: 0x04000044 RID: 68
			public bool _showNoGoodInStockMessage;

			// Token: 0x04000045 RID: 69
			public string _simpleHeaderLocKey;
		}
	}
}
