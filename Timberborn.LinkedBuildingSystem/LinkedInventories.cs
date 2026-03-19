using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.InventorySystem;

namespace Timberborn.LinkedBuildingSystem
{
	// Token: 0x0200000D RID: 13
	public class LinkedInventories : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002974 File Offset: 0x00000B74
		public void Awake()
		{
			this._inventories = base.GetComponent<Inventories>();
			foreach (Inventory inventory in this._inventories.AllInventories)
			{
				inventory.InventoryCapacityReservationChanged += this.OnInventoryCapacityReservationChanged;
			}
			base.GetComponent<LinkedBuilding>().BuildingLinked += this.OnBuildingLinked;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029FC File Offset: 0x00000BFC
		public void Reserve(Inventory inventory, GoodAmount goodAmount)
		{
			using (this._mirrorOperationLock.Lock())
			{
				inventory.ReserveCapacity(goodAmount);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A38 File Offset: 0x00000C38
		public void Unreserve(Inventory inventory, GoodAmount goodAmount)
		{
			using (this._mirrorOperationLock.Lock())
			{
				inventory.UnreserveCapacity(goodAmount);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A74 File Offset: 0x00000C74
		public void OnInventoryCapacityReservationChanged(object sender, InventoryAmountChangedEventArgs e)
		{
			if (this._mirrorOperationLock.IsUnlocked)
			{
				this.MirrorCapacityReservation((Inventory)sender, e.GoodAmount);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A96 File Offset: 0x00000C96
		public void OnBuildingLinked(object sender, LinkedBuilding e)
		{
			this._linked = e.GetComponent<LinkedInventories>();
			if (e.IsLinked)
			{
				this.MirrorReservations();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public void MirrorCapacityReservation(Inventory inventory, GoodAmount goodAmount)
		{
			Inventory linkedInventory = this.GetLinkedInventory(inventory);
			if (goodAmount.Amount > 0)
			{
				this._linked.Reserve(linkedInventory, goodAmount);
				return;
			}
			this._linked.Unreserve(linkedInventory, new GoodAmount(goodAmount.GoodId, -goodAmount.Amount));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B04 File Offset: 0x00000D04
		public Inventory GetLinkedInventory(Inventory myInventory)
		{
			foreach (Inventory inventory in this._linked._inventories.AllInventories)
			{
				if (inventory.ComponentName == myInventory.ComponentName)
				{
					return inventory;
				}
			}
			throw new ArgumentOutOfRangeException(string.Format("Couldn't find linked inventory in {0} for {1}", base.GameObject, myInventory.ComponentName));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B94 File Offset: 0x00000D94
		public void MirrorReservations()
		{
			foreach (Inventory myInventory in this._inventories.AllInventories)
			{
				Inventory linkedInventory = this.GetLinkedInventory(myInventory);
				this.MirrorInventoryReservations(myInventory, linkedInventory);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public void MirrorInventoryReservations(Inventory myInventory, Inventory linkedInventory)
		{
			ReadOnlyList<GoodAmount> readOnlyList = myInventory.ReservedCapacity();
			ReadOnlyList<GoodAmount> readOnlyList2 = linkedInventory.ReservedCapacity();
			foreach (GoodAmount goodAmount in readOnlyList)
			{
				this._linked.Reserve(linkedInventory, goodAmount);
			}
			foreach (GoodAmount goodAmount2 in readOnlyList2)
			{
				this.Reserve(myInventory, goodAmount2);
			}
		}

		// Token: 0x0400001A RID: 26
		public Inventories _inventories;

		// Token: 0x0400001B RID: 27
		public LinkedInventories _linked;

		// Token: 0x0400001C RID: 28
		public readonly MirrorOperationLock _mirrorOperationLock = new MirrorOperationLock();
	}
}
