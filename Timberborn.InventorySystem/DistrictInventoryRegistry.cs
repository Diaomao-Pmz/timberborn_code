using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Goods;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000007 RID: 7
	public class DistrictInventoryRegistry : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000016 RID: 22 RVA: 0x000025A4 File Offset: 0x000007A4
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x000025DC File Offset: 0x000007DC
		public event EventHandler<Inventory> InventoryRegistered;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000018 RID: 24 RVA: 0x00002614 File Offset: 0x00000814
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x0000264C File Offset: 0x0000084C
		public event EventHandler<Inventory> InventoryUnregistered;

		// Token: 0x0600001A RID: 26 RVA: 0x00002681 File Offset: 0x00000881
		public DistrictInventoryRegistry(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002690 File Offset: 0x00000890
		public ReadOnlyHashSet<Inventory> Inventories
		{
			get
			{
				return this._publicInventoryRegistry.Inventories;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000269D File Offset: 0x0000089D
		public void Awake()
		{
			this._publicInventoryRegistry = new InventoryRegistry(this._goodService.Goods);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026B5 File Offset: 0x000008B5
		public void Add(Inventory inventory)
		{
			if (inventory.PublicInput || inventory.PublicOutput)
			{
				this._publicInventoryRegistry.RegisterInventory(inventory);
			}
			EventHandler<Inventory> inventoryRegistered = this.InventoryRegistered;
			if (inventoryRegistered == null)
			{
				return;
			}
			inventoryRegistered(this, inventory);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026E5 File Offset: 0x000008E5
		public void Remove(Inventory inventory)
		{
			if (inventory.PublicInput || inventory.PublicOutput)
			{
				this._publicInventoryRegistry.UnregisterInventory(inventory);
			}
			EventHandler<Inventory> inventoryUnregistered = this.InventoryUnregistered;
			if (inventoryUnregistered == null)
			{
				return;
			}
			inventoryUnregistered(this, inventory);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002715 File Offset: 0x00000915
		public ReadOnlyHashSet<Inventory> ActiveInventoriesWithStock(string goodId)
		{
			return this._publicInventoryRegistry.InventoriesWithStock(goodId);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002723 File Offset: 0x00000923
		public ReadOnlyHashSet<Inventory> ActiveInventoriesWithCapacity(string goodId)
		{
			return this._publicInventoryRegistry.InventoriesWithCapacity(goodId);
		}

		// Token: 0x0400000D RID: 13
		public readonly IGoodService _goodService;

		// Token: 0x0400000E RID: 14
		public InventoryRegistry _publicInventoryRegistry;
	}
}
