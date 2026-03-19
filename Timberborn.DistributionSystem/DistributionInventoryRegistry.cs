using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.StockpilePrioritySystem;
using Timberborn.Stockpiles;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000008 RID: 8
	public class DistributionInventoryRegistry : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000012 RID: 18 RVA: 0x00002210 File Offset: 0x00000410
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x00002248 File Offset: 0x00000448
		public event EventHandler<string> GoodStorageChanged;

		// Token: 0x06000014 RID: 20 RVA: 0x0000227D File Offset: 0x0000047D
		public DistributionInventoryRegistry(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022B8 File Offset: 0x000004B8
		public ReadOnlyHashSet<Inventory> DistrictCrossingInventories
		{
			get
			{
				return this._districtCrossingInventories.AsReadOnlyHashSet<Inventory>();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022C5 File Offset: 0x000004C5
		public ReadOnlyHashSet<Inventory> StoringInventories(string goodId)
		{
			return this._storingInventories[goodId].AsReadOnlyHashSet<Inventory>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022D8 File Offset: 0x000004D8
		public ReadOnlyHashSet<Inventory> StockInventories(string goodId)
		{
			return this._stockInventories[goodId].AsReadOnlyHashSet<Inventory>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022EB File Offset: 0x000004EB
		public ReadOnlyHashSet<Inventory> CapacityInventories(string goodId)
		{
			return this._capacityInventories[goodId].AsReadOnlyHashSet<Inventory>();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022FE File Offset: 0x000004FE
		public void Awake()
		{
			DistrictInventoryRegistry component = base.GetComponent<DistrictInventoryRegistry>();
			component.InventoryRegistered += this.OnInventoryRegistered;
			component.InventoryUnregistered += this.OnInventoryUnregistered;
			this.InitializeRegistries();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002330 File Offset: 0x00000530
		public void InitializeRegistries()
		{
			foreach (string key in this._goodService.Goods)
			{
				this._storingInventories[key] = new HashSet<Inventory>();
				this._stockInventories[key] = new HashSet<Inventory>();
				this._capacityInventories[key] = new HashSet<Inventory>();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B8 File Offset: 0x000005B8
		public void OnInventoryRegistered(object sender, Inventory inventory)
		{
			this.UpdateRegistries(inventory);
			if (inventory.ComponentName == DistrictCrossingInventoryInitializer.InventoryComponentName)
			{
				this._districtCrossingInventories.Add(inventory);
			}
			inventory.InventoryChanged += this.OnInventoryChanged;
			inventory.UnwantedStockDisappeared += this.OnUnwantedStockDisappeared;
			StockpilePriorityChangeListener component = inventory.GetComponent<StockpilePriorityChangeListener>();
			if (component != null)
			{
				component.PriorityChanged += this.OnPriorityChanged;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000242C File Offset: 0x0000062C
		public void OnInventoryUnregistered(object sender, Inventory inventory)
		{
			foreach (StorableGoodAmount storableGoodAmount in inventory.AllowedGoods)
			{
				this.RemoveInventory(inventory, storableGoodAmount.StorableGood.GoodId);
			}
			this._districtCrossingInventories.Remove(inventory);
			inventory.InventoryChanged -= this.OnInventoryChanged;
			inventory.UnwantedStockDisappeared -= this.OnUnwantedStockDisappeared;
			StockpilePriorityChangeListener component = inventory.GetComponent<StockpilePriorityChangeListener>();
			if (component != null)
			{
				component.PriorityChanged -= this.OnPriorityChanged;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024E0 File Offset: 0x000006E0
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.UpdateInventory((Inventory)sender, e.GoodId);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024F5 File Offset: 0x000006F5
		public void OnUnwantedStockDisappeared(object sender, EventArgs e)
		{
			this.UpdateRegistries((Inventory)sender);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002504 File Offset: 0x00000704
		public void OnPriorityChanged(object sender, EventArgs e)
		{
			Stockpile component = ((StockpilePriorityChangeListener)sender).GetComponent<Stockpile>();
			this.UpdateRegistries(component.Inventory);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000252C File Offset: 0x0000072C
		public void UpdateRegistries(Inventory inventory)
		{
			foreach (StorableGoodAmount storableGoodAmount in inventory.AllowedGoods)
			{
				this.UpdateInventory(inventory, storableGoodAmount.StorableGood.GoodId);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002594 File Offset: 0x00000794
		public void UpdateInventory(Inventory inventory, string goodId)
		{
			if (DistributionInventoryRegistry.IsStoringInventory(inventory, goodId))
			{
				this._storingInventories[goodId].Add(inventory);
			}
			else
			{
				this._storingInventories[goodId].Remove(inventory);
			}
			if (DistributionInventoryRegistry.IsStockInventory(inventory, goodId))
			{
				this._stockInventories[goodId].Add(inventory);
			}
			else
			{
				this._stockInventories[goodId].Remove(inventory);
			}
			if (DistributionInventoryRegistry.IsCapacityInventory(inventory, goodId))
			{
				this._capacityInventories[goodId].Add(inventory);
			}
			else
			{
				this._capacityInventories[goodId].Remove(inventory);
			}
			EventHandler<string> goodStorageChanged = this.GoodStorageChanged;
			if (goodStorageChanged == null)
			{
				return;
			}
			goodStorageChanged(this, goodId);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002648 File Offset: 0x00000848
		public void RemoveInventory(Inventory inventory, string goodId)
		{
			this._storingInventories[goodId].Remove(inventory);
			this._stockInventories[goodId].Remove(inventory);
			this._capacityInventories[goodId].Remove(inventory);
			EventHandler<string> goodStorageChanged = this.GoodStorageChanged;
			if (goodStorageChanged == null)
			{
				return;
			}
			goodStorageChanged(this, goodId);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026A0 File Offset: 0x000008A0
		public static bool IsStoringInventory(Inventory inventory, string goodId)
		{
			return inventory.Gives(goodId) && inventory.Takes(goodId) && inventory.LimitedAmount(goodId) > 0;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026C0 File Offset: 0x000008C0
		public static bool IsStockInventory(Inventory inventory, string goodId)
		{
			return inventory.Gives(goodId);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026C9 File Offset: 0x000008C9
		public static bool IsCapacityInventory(Inventory inventory, string goodId)
		{
			return inventory.Takes(goodId) && inventory.LimitedAmount(goodId) > 0;
		}

		// Token: 0x0400000C RID: 12
		public readonly IGoodService _goodService;

		// Token: 0x0400000D RID: 13
		public readonly Dictionary<string, HashSet<Inventory>> _storingInventories = new Dictionary<string, HashSet<Inventory>>();

		// Token: 0x0400000E RID: 14
		public readonly Dictionary<string, HashSet<Inventory>> _stockInventories = new Dictionary<string, HashSet<Inventory>>();

		// Token: 0x0400000F RID: 15
		public readonly Dictionary<string, HashSet<Inventory>> _capacityInventories = new Dictionary<string, HashSet<Inventory>>();

		// Token: 0x04000010 RID: 16
		public readonly HashSet<Inventory> _districtCrossingInventories = new HashSet<Inventory>();
	}
}
