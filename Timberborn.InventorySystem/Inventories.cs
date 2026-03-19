using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200000F RID: 15
	public class Inventories : BaseComponent
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002CDB File Offset: 0x00000EDB
		public ReadOnlyList<Inventory> AllInventories
		{
			get
			{
				return this._inventories.AsReadOnlyList<Inventory>();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public ReadOnlyList<Inventory> EnabledInventories
		{
			get
			{
				return this._enabledInventories.AsReadOnlyList<Inventory>();
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public void AddInventory(Inventory inventory)
		{
			this._inventories.Add(inventory);
			if (inventory.Enabled)
			{
				this._enabledInventories.Add(inventory);
			}
			inventory.InventoryEnabled += delegate(object _, EventArgs _)
			{
				this._enabledInventories.Add(inventory);
			};
			inventory.InventoryDisabled += delegate(object _, EventArgs _)
			{
				this._enabledInventories.Remove(inventory);
			};
		}

		// Token: 0x0400001D RID: 29
		public readonly List<Inventory> _inventories = new List<Inventory>();

		// Token: 0x0400001E RID: 30
		public readonly List<Inventory> _enabledInventories = new List<Inventory>();
	}
}
