using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.InventorySystem;
using Timberborn.InventorySystemUI;
using UnityEngine.UIElements;

namespace Timberborn.ConstructionSitesUI
{
	// Token: 0x02000007 RID: 7
	public class ConstructionSiteFragmentInventory
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002488 File Offset: 0x00000688
		public ConstructionSiteFragmentInventory(InformationalRowsFactory informationalRowsFactory)
		{
			this._informationalRowsFactory = informationalRowsFactory;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024A2 File Offset: 0x000006A2
		public void InitializeFragment(VisualElement root)
		{
			this._inventoryRoot = UQueryExtensions.Q<VisualElement>(root, "ConstructionSiteInventoryFragment", null);
			this._inventoryContent = UQueryExtensions.Q<ScrollView>(this._inventoryRoot, "Content", null);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024CD File Offset: 0x000006CD
		public void ShowFragment(Inventory inventory)
		{
			this._inventory = inventory;
			this._rows.AddRange(this._informationalRowsFactory.CreateRowsWithLimits(this._inventory, this._inventoryContent));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024F8 File Offset: 0x000006F8
		public void ClearFragment()
		{
			this._inventoryContent.Clear();
			this._rows.Clear();
			this._inventory = null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002518 File Offset: 0x00000718
		public void UpdateFragment()
		{
			if (this._inventory && this._inventory.Enabled)
			{
				this._inventoryRoot.ToggleDisplayStyle(this._rows.Count > 0);
				using (List<InformationalRow>.Enumerator enumerator = this._rows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						InformationalRow informationalRow = enumerator.Current;
						informationalRow.ShowUpdated();
					}
					return;
				}
			}
			this._inventoryRoot.ToggleDisplayStyle(false);
		}

		// Token: 0x0400001A RID: 26
		public readonly InformationalRowsFactory _informationalRowsFactory;

		// Token: 0x0400001B RID: 27
		public Inventory _inventory;

		// Token: 0x0400001C RID: 28
		public VisualElement _inventoryRoot;

		// Token: 0x0400001D RID: 29
		public ScrollView _inventoryContent;

		// Token: 0x0400001E RID: 30
		public readonly List<InformationalRow> _rows = new List<InformationalRow>();
	}
}
