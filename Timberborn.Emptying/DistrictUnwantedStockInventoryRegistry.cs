using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.InventorySystem;

namespace Timberborn.Emptying
{
	// Token: 0x02000006 RID: 6
	public class DistrictUnwantedStockInventoryRegistry : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022A9 File Offset: 0x000004A9
		public ReadOnlyList<Inventory> InventoriesWithUnwantedStock
		{
			get
			{
				return this._inventoriesWithUnwantedStock.AsReadOnlyList<Inventory>();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022B6 File Offset: 0x000004B6
		public void Awake()
		{
			DistrictInventoryRegistry component = base.GetComponent<DistrictInventoryRegistry>();
			component.InventoryRegistered += this.OnInventoryRegistered;
			component.InventoryUnregistered += this.OnInventoryUnregistered;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E1 File Offset: 0x000004E1
		public void OnInventoryRegistered(object sender, Inventory inventory)
		{
			inventory.InventoryChanged += this.OnInventoryChanged;
			if (inventory.HasUnwantedStock)
			{
				this.Add(inventory);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002304 File Offset: 0x00000504
		public void OnInventoryUnregistered(object sender, Inventory inventory)
		{
			inventory.InventoryChanged -= this.OnInventoryChanged;
			this.Remove(inventory);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000231F File Offset: 0x0000051F
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.UpdateState((Inventory)sender);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000232D File Offset: 0x0000052D
		public void UpdateState(Inventory inventory)
		{
			if (inventory.HasUnwantedStock)
			{
				this.Add(inventory);
				return;
			}
			this.Remove(inventory);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002346 File Offset: 0x00000546
		public void Add(Inventory inventory)
		{
			if (!this._inventoriesWithUnwantedStock.Contains(inventory))
			{
				this._inventoriesWithUnwantedStock.Add(inventory);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002362 File Offset: 0x00000562
		public void Remove(Inventory inventory)
		{
			this._inventoriesWithUnwantedStock.Remove(inventory);
		}

		// Token: 0x04000009 RID: 9
		public readonly List<Inventory> _inventoriesWithUnwantedStock = new List<Inventory>();
	}
}
