using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GoodStackSystem;
using Timberborn.InventorySystemUI;
using UnityEngine.UIElements;

namespace Timberborn.GoodStackSystemUI
{
	// Token: 0x02000004 RID: 4
	public class GoodStackFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public GoodStackFragment(VisualElementLoader visualElementLoader, InventoryFragmentBuilderFactory inventoryFragmentBuilderFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._inventoryFragmentBuilderFactory = inventoryFragmentBuilderFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/GoodStackFragment");
			this._inventoryFragment = this._inventoryFragmentBuilderFactory.CreateBuilder(this._root).Build();
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210E File Offset: 0x0000030E
		public void ShowFragment(BaseComponent entity)
		{
			this._goodStack = entity.GetComponent<GoodStack>();
			if (this._goodStack)
			{
				this._inventoryFragment.ShowFragment(this._goodStack.Inventory);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000213F File Offset: 0x0000033F
		public void ClearFragment()
		{
			this._goodStack = null;
			this._inventoryFragment.ClearFragment();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002153 File Offset: 0x00000353
		public void UpdateFragment()
		{
			if (this._goodStack && this._goodStack.Enabled)
			{
				this._inventoryFragment.UpdateFragment();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly InventoryFragmentBuilderFactory _inventoryFragmentBuilderFactory;

		// Token: 0x04000008 RID: 8
		public InventoryFragment _inventoryFragment;

		// Token: 0x04000009 RID: 9
		public GoodStack _goodStack;

		// Token: 0x0400000A RID: 10
		public VisualElement _root;
	}
}
