using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.LinkedBuildingSystem;

namespace Timberborn.DistributionSystem
{
	// Token: 0x0200000D RID: 13
	public class DistrictCrossingInventory : BaseComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002AB2 File Offset: 0x00000CB2
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002ABA File Offset: 0x00000CBA
		public Inventory Inventory { get; private set; }

		// Token: 0x06000042 RID: 66 RVA: 0x00002AC3 File Offset: 0x00000CC3
		public void Awake()
		{
			this._linkedInventories = base.GetComponent<LinkedInventories>();
			base.GetComponent<LinkedBuilding>().BuildingLinked += this.OnBuildingLinked;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void InitializeEntity()
		{
			if (this._linked)
			{
				this.ReserveStock();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AFD File Offset: 0x00000CFD
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<DistrictCrossingInventory>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B18 File Offset: 0x00000D18
		public void TransferStock(string goodId, int amount)
		{
			if (this._mirrorOperationLock.IsUnlocked)
			{
				amount = Math.Min(amount, this.Inventory.UnreservedAmountInStock(goodId));
				if (amount > 0)
				{
					GoodAmount goodAmount = new GoodAmount(goodId, amount);
					this.Inventory.Take(goodAmount);
					this._linked.GiveStock(goodAmount);
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B6B File Offset: 0x00000D6B
		public int IncomingStock(string goodId)
		{
			return this.Inventory.ReservedCapacity(goodId) - this._linked.Inventory.AmountInStock(goodId);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B8B File Offset: 0x00000D8B
		public void OnEnterFinishedState()
		{
			this.Inventory.Enable();
			this.Inventory.InventoryStockChanged += this.OnInventoryStockChanged;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BAF File Offset: 0x00000DAF
		public void OnExitFinishedState()
		{
			this.Inventory.Disable();
			this.Inventory.InventoryStockChanged -= this.OnInventoryStockChanged;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public void OnInventoryStockChanged(object sender, InventoryAmountChangedEventArgs e)
		{
			if (e.GoodAmount.Amount > 0)
			{
				this._linked.Reserve(e.GoodAmount);
				this.TransferStock(e.GoodAmount.GoodId, e.GoodAmount.Amount);
				return;
			}
			if (e.GoodAmount.Amount < 0)
			{
				this._linked.Unreserve(new GoodAmount(e.GoodAmount.GoodId, -e.GoodAmount.Amount));
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C6B File Offset: 0x00000E6B
		public void OnBuildingLinked(object sender, LinkedBuilding e)
		{
			this._linked = e.GetComponent<DistrictCrossingInventory>();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C7C File Offset: 0x00000E7C
		public void ReserveStock()
		{
			foreach (GoodAmount goodAmount in this.Inventory.Stock)
			{
				this._linked.Reserve(goodAmount);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002CDC File Offset: 0x00000EDC
		public void GiveStock(GoodAmount goodAmount)
		{
			using (this._mirrorOperationLock.Lock())
			{
				this.Inventory.Give(goodAmount);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D20 File Offset: 0x00000F20
		public void Reserve(GoodAmount goodAmount)
		{
			this._linkedInventories.Reserve(this.Inventory, goodAmount);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D34 File Offset: 0x00000F34
		public void Unreserve(GoodAmount goodAmount)
		{
			this._linkedInventories.Unreserve(this.Inventory, goodAmount);
		}

		// Token: 0x04000019 RID: 25
		public LinkedInventories _linkedInventories;

		// Token: 0x0400001A RID: 26
		public DistrictCrossingInventory _linked;

		// Token: 0x0400001B RID: 27
		public readonly MirrorOperationLock _mirrorOperationLock = new MirrorOperationLock();
	}
}
