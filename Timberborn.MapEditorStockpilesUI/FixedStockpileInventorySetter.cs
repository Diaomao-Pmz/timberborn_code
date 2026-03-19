using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Stockpiles;

namespace Timberborn.MapEditorStockpilesUI
{
	// Token: 0x02000009 RID: 9
	public class FixedStockpileInventorySetter : BaseComponent, IAwakableComponent, IStartableComponent, IDuplicable<FixedStockpileInventorySetter>, IDuplicable
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002748 File Offset: 0x00000948
		public FixedStockpileInventorySetter(FixedStockpileGoodProvider fixedStockpileGoodProvider)
		{
			this._fixedStockpileGoodProvider = fixedStockpileGoodProvider;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002757 File Offset: 0x00000957
		public void Awake()
		{
			this._stockpile = base.GetComponent<Stockpile>();
			this._fixedStockpile = base.GetComponent<FixedStockpile>();
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000277D File Offset: 0x0000097D
		public void Start()
		{
			this.ValidateAndInitializeInventory();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002788 File Offset: 0x00000988
		public void SetGoodId(string goodId)
		{
			int totalAmountInStock = this._stockpile.Inventory.TotalAmountInStock;
			this.ResetInventoryGood(goodId);
			if (totalAmountInStock > 0)
			{
				this._stockpile.Inventory.Give(new GoodAmount(goodId, totalAmountInStock));
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027C8 File Offset: 0x000009C8
		public void SetAmount(int amount)
		{
			this.ClearInventory();
			this._stockpile.Inventory.Give(new GoodAmount(this._singleGoodAllower.AllowedGood, amount));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027F4 File Offset: 0x000009F4
		public void DuplicateFrom(FixedStockpileInventorySetter source)
		{
			if (this._stockpile.WhitelistedGoodType == source._stockpile.WhitelistedGoodType)
			{
				this.SetGoodId(source._singleGoodAllower.AllowedGood);
				this.SetAmount(source._stockpile.Inventory.TotalAmountInStock);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002848 File Offset: 0x00000A48
		public void ValidateAndInitializeInventory()
		{
			ImmutableArray<string> goods = this._fixedStockpileGoodProvider.GetGoods(this._stockpile.WhitelistedGoodType);
			bool hasAllowedGood = this._singleGoodAllower.HasAllowedGood;
			if (!hasAllowedGood || !goods.Contains(this._singleGoodAllower.AllowedGood))
			{
				this.ResetInventoryGood(goods.First<string>());
			}
			if (!hasAllowedGood)
			{
				this.SetAmount(this._stockpile.MaxCapacity);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028AD File Offset: 0x00000AAD
		public void ResetInventoryGood(string goodId)
		{
			this.ClearInventory();
			this._singleGoodAllower.Allow(goodId);
			this._fixedStockpile.SetFixedGood(goodId);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028D0 File Offset: 0x00000AD0
		public void ClearInventory()
		{
			foreach (GoodAmount good in this._stockpile.Inventory.Stock.ToImmutableArray<GoodAmount>())
			{
				this._stockpile.Inventory.Take(good);
			}
		}

		// Token: 0x04000024 RID: 36
		public readonly FixedStockpileGoodProvider _fixedStockpileGoodProvider;

		// Token: 0x04000025 RID: 37
		public Stockpile _stockpile;

		// Token: 0x04000026 RID: 38
		public FixedStockpile _fixedStockpile;

		// Token: 0x04000027 RID: 39
		public SingleGoodAllower _singleGoodAllower;
	}
}
