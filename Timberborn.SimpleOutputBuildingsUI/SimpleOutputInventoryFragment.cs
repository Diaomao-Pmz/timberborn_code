using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InventorySystemUI;
using Timberborn.SimpleOutputBuildings;
using UnityEngine.UIElements;

namespace Timberborn.SimpleOutputBuildingsUI
{
	// Token: 0x02000006 RID: 6
	public class SimpleOutputInventoryFragment : IEntityPanelFragment
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002119 File Offset: 0x00000319
		public SimpleOutputInventoryFragment(VisualElementLoader visualElementLoader, InventoryFragmentBuilderFactory inventoryFragmentBuilderFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._inventoryFragmentBuilderFactory = inventoryFragmentBuilderFactory;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002130 File Offset: 0x00000330
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/SimpleOutputInventoryFragment");
			this._root.ToggleDisplayStyle(false);
			this._inventoryFragment = this._inventoryFragmentBuilderFactory.CreateBuilder(this._root).ShowRowLimit().ShowNoGoodInStockMessage().Build();
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000218C File Offset: 0x0000038C
		public void ShowFragment(BaseComponent entity)
		{
			this._simpleOutputInventory = entity.GetComponent<SimpleOutputInventory>();
			if (this._simpleOutputInventory && entity.GetComponent<SimpleOutputInventoryFragmentEnabler>())
			{
				this._inventoryFragment.ShowFragment(this._simpleOutputInventory.Inventory);
				return;
			}
			this._simpleOutputInventory = null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021DD File Offset: 0x000003DD
		public void ClearFragment()
		{
			this._simpleOutputInventory = null;
			this._inventoryFragment.ClearFragment();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FD File Offset: 0x000003FD
		public void UpdateFragment()
		{
			if (this._simpleOutputInventory && this._simpleOutputInventory.Enabled)
			{
				this._inventoryFragment.UpdateFragment();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public readonly InventoryFragmentBuilderFactory _inventoryFragmentBuilderFactory;

		// Token: 0x04000009 RID: 9
		public InventoryFragment _inventoryFragment;

		// Token: 0x0400000A RID: 10
		public SimpleOutputInventory _simpleOutputInventory;

		// Token: 0x0400000B RID: 11
		public VisualElement _root;
	}
}
