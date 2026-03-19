using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.FireworkSystem;
using Timberborn.InventorySystemUI;
using UnityEngine.UIElements;

namespace Timberborn.FireworkSystemUI
{
	// Token: 0x02000006 RID: 6
	public class FireworkLauncherInventoryFragment : IEntityPanelFragment
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002567 File Offset: 0x00000767
		public FireworkLauncherInventoryFragment(VisualElementLoader visualElementLoader, InventoryFragmentBuilderFactory inventoryFragmentBuilderFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._inventoryFragmentBuilderFactory = inventoryFragmentBuilderFactory;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002580 File Offset: 0x00000780
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/FireworkLauncherInventoryFragment");
			this._inventoryFragment = this._inventoryFragmentBuilderFactory.CreateBuilder(this._root).ShowEmptyRows().ShowRowLimit().Build();
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025DB File Offset: 0x000007DB
		public void ShowFragment(BaseComponent entity)
		{
			this._fireworkLauncher = entity.GetComponent<FireworkLauncher>();
			if (this._fireworkLauncher)
			{
				this._root.ToggleDisplayStyle(true);
				this._inventoryFragment.ShowFragment(this._fireworkLauncher.Inventory);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002618 File Offset: 0x00000818
		public void UpdateFragment()
		{
			if (this._fireworkLauncher)
			{
				this._inventoryFragment.UpdateFragment();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002632 File Offset: 0x00000832
		public void ClearFragment()
		{
			this._fireworkLauncher = null;
			this._inventoryFragment.ClearFragment();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0400001A RID: 26
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001B RID: 27
		public readonly InventoryFragmentBuilderFactory _inventoryFragmentBuilderFactory;

		// Token: 0x0400001C RID: 28
		public VisualElement _root;

		// Token: 0x0400001D RID: 29
		public InventoryFragment _inventoryFragment;

		// Token: 0x0400001E RID: 30
		public FireworkLauncher _fireworkLauncher;
	}
}
