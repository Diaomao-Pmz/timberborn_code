using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.TickSystem;

namespace Timberborn.DistributionSystem
{
	// Token: 0x0200000C RID: 12
	public class DistrictCrossingAutoExporter : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000297D File Offset: 0x00000B7D
		public void Awake()
		{
			this._districtCrossing = base.GetComponent<DistrictCrossing>();
			this._districtCrossingInventory = base.GetComponent<DistrictCrossingInventory>();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002997 File Offset: 0x00000B97
		public override void Tick()
		{
			if (this._districtCrossing.CanExport && this.Inventory.IsUnblocked)
			{
				this.TryExport();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000029B9 File Offset: 0x00000BB9
		public Inventory Inventory
		{
			get
			{
				return this._districtCrossingInventory.Inventory;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029C8 File Offset: 0x00000BC8
		public void TryExport()
		{
			for (int i = this.Inventory.Stock.Count - 1; i >= 0; i--)
			{
				GoodAmount goodAmount = this.Inventory.Stock[i];
				if (this.Inventory.UnreservedAmountInStock(goodAmount.GoodId) > 0)
				{
					this.TryExportStock(goodAmount);
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A28 File Offset: 0x00000C28
		public void TryExportStock(GoodAmount goodAmount)
		{
			DistributableGood linkedDistributableGood;
			if (this._districtCrossing.TryGetLinkedDistrictDistributableGood(goodAmount.GoodId, out linkedDistributableGood))
			{
				DistributableGood myDistributableGood = this._districtCrossing.GetMyDistributableGood(goodAmount.GoodId);
				if (this._districtCrossing.CanExportGood(myDistributableGood, linkedDistributableGood))
				{
					this.ExportStock(myDistributableGood, linkedDistributableGood);
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A78 File Offset: 0x00000C78
		public void ExportStock(DistributableGood myDistributableGood, DistributableGood linkedDistributableGood)
		{
			int amountToExport = this._districtCrossing.GetAmountToExport(myDistributableGood, linkedDistributableGood);
			if (amountToExport > 0)
			{
				this._districtCrossingInventory.TransferStock(myDistributableGood.GoodId, amountToExport);
			}
		}

		// Token: 0x04000016 RID: 22
		public DistrictCrossing _districtCrossing;

		// Token: 0x04000017 RID: 23
		public DistrictCrossingInventory _districtCrossingInventory;
	}
}
