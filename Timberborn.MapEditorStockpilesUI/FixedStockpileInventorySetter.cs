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
	// Token: 0x02000006 RID: 6
	internal class FixedStockpileInventorySetter : BaseComponent, IAwakableComponent, IStartableComponent, IDuplicable<FixedStockpileInventorySetter>, IDuplicable
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002706 File Offset: 0x00000906
		public FixedStockpileInventorySetter(FixedStockpileGoodProvider fixedStockpileGoodProvider)
		{
			this._fixedStockpileGoodProvider = fixedStockpileGoodProvider;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002715 File Offset: 0x00000915
		public void Awake()
		{
			this._stockpile = base.GetComponent<Stockpile>();
			this._fixedStockpile = base.GetComponent<FixedStockpile>();
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000273B File Offset: 0x0000093B
		public void Start()
		{
			this.ValidateAndInitializeInventory();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002744 File Offset: 0x00000944
		public void SetGoodId(string goodId)
		{
			int totalAmountInStock = this._stockpile.Inventory.TotalAmountInStock;
			this.ResetInventoryGood(goodId);
			if (totalAmountInStock > 0)
			{
				this._stockpile.Inventory.Give(new GoodAmount(goodId, totalAmountInStock));
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002784 File Offset: 0x00000984
		public void SetAmount(int amount)
		{
			this.ClearInventory();
			this._stockpile.Inventory.Give(new GoodAmount(this._singleGoodAllower.AllowedGood, amount));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027B0 File Offset: 0x000009B0
		public void DuplicateFrom(FixedStockpileInventorySetter source)
		{
			if (this._stockpile.WhitelistedGoodType == source._stockpile.WhitelistedGoodType)
			{
				this.SetGoodId(source._singleGoodAllower.AllowedGood);
				this.SetAmount(source._stockpile.Inventory.TotalAmountInStock);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002804 File Offset: 0x00000A04
		private void ValidateAndInitializeInventory()
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

		// Token: 0x06000029 RID: 41 RVA: 0x00002869 File Offset: 0x00000A69
		private void ResetInventoryGood(string goodId)
		{
			this.ClearInventory();
			this._singleGoodAllower.Allow(goodId);
			this._fixedStockpile.SetFixedGood(goodId);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000288C File Offset: 0x00000A8C
		private void ClearInventory()
		{
			foreach (GoodAmount good in this._stockpile.Inventory.Stock.ToImmutableArray<GoodAmount>())
			{
				this._stockpile.Inventory.Take(good);
			}
		}

		// Token: 0x04000019 RID: 25
		private readonly FixedStockpileGoodProvider _fixedStockpileGoodProvider;

		// Token: 0x0400001A RID: 26
		private Stockpile _stockpile;

		// Token: 0x0400001B RID: 27
		private FixedStockpile _fixedStockpile;

		// Token: 0x0400001C RID: 28
		private SingleGoodAllower _singleGoodAllower;
	}
}
