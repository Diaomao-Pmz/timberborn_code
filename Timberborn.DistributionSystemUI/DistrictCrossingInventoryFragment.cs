using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.InventorySystemUI;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemUI
{
	// Token: 0x02000007 RID: 7
	public class DistrictCrossingInventoryFragment : IEntityPanelFragment
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000022F5 File Offset: 0x000004F5
		public DistrictCrossingInventoryFragment(InventoryFragmentBuilderFactory inventoryFragmentBuilderFactory, VisualElementLoader visualElementLoader)
		{
			this._inventoryFragmentBuilderFactory = inventoryFragmentBuilderFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000230C File Offset: 0x0000050C
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/DistrictCrossingInventoryFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._root.ToggleDisplayStyle(false);
			this._inventoryFragment = this._inventoryFragmentBuilderFactory.CreateBuilder(this._root).ShowRowLimit().ShowNoGoodInStockMessage().Build();
			return this._root;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002369 File Offset: 0x00000569
		public void ShowFragment(BaseComponent entity)
		{
			this._districtCrossingInventory = entity.GetComponent<DistrictCrossingInventory>();
			if (this._districtCrossingInventory)
			{
				this._root.ToggleDisplayStyle(true);
				this._inventoryFragment.ShowFragment(this._districtCrossingInventory.Inventory);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023A6 File Offset: 0x000005A6
		public void ClearFragment()
		{
			this._districtCrossingInventory = null;
			this._inventoryFragment.ClearFragment();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023C6 File Offset: 0x000005C6
		public void UpdateFragment()
		{
			if (this._districtCrossingInventory)
			{
				this._inventoryFragment.UpdateFragment();
			}
		}

		// Token: 0x0400000F RID: 15
		public readonly InventoryFragmentBuilderFactory _inventoryFragmentBuilderFactory;

		// Token: 0x04000010 RID: 16
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000011 RID: 17
		public InventoryFragment _inventoryFragment;

		// Token: 0x04000012 RID: 18
		public DistrictCrossingInventory _districtCrossingInventory;

		// Token: 0x04000013 RID: 19
		public VisualElement _root;
	}
}
