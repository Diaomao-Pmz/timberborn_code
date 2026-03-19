using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.InventorySystem;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x02000014 RID: 20
	public class InventoryRowUpdater
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002F54 File Offset: 0x00001154
		public InventoryRowUpdater(GoodDescriber goodDescriber, InformationalRowsFactory informationalRowsFactory)
		{
			this._goodDescriber = goodDescriber;
			this._informationalRowsFactory = informationalRowsFactory;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F6C File Offset: 0x0000116C
		public void AddRows(ScrollView inventoryContent, Inventory inventory, List<InformationalRow> rows, RecipeSpec recipeSpec)
		{
			List<StorableGoodAmount> goods = (from good in inventory.AllowedGoods
			orderby this._goodDescriber.Describe(good.StorableGood.GoodId)
			select good).ToList<StorableGoodAmount>();
			if (recipeSpec != null)
			{
				this.AddRecipeRows(inventoryContent, inventory, rows, goods, recipeSpec);
			}
			this.AddRemainingRows(inventoryContent, inventory, rows, goods);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FBC File Offset: 0x000011BC
		public void UpdateRowsVisibility(VisualElement root, VisualElement isEmpty, Inventory inventory, List<InformationalRow> rows)
		{
			if (inventory && inventory.Enabled)
			{
				bool flag = false;
				root.ToggleDisplayStyle(true);
				foreach (InformationalRow informationalRow in rows)
				{
					if (InventoryRowUpdater.ShouldShow(inventory, informationalRow.GoodId))
					{
						informationalRow.ShowUpdated();
						flag = true;
					}
					else
					{
						informationalRow.Hide();
					}
				}
				isEmpty.ToggleDisplayStyle(!flag);
				return;
			}
			root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003050 File Offset: 0x00001250
		public void AddRecipeRows(ScrollView inventoryContent, Inventory inventory, List<InformationalRow> rows, List<StorableGoodAmount> goods, RecipeSpec currentRecipe)
		{
			foreach (StorableGoodAmount storableGoodAmount in from good in goods
			where good.StorableGood.IsOnlyGivable
			select good)
			{
				StorableGood storableGood = storableGoodAmount.StorableGood;
				if (InventoryRowUpdater.IsInputOrFuel(currentRecipe, storableGood.GoodId))
				{
					InformationalRow item = this._informationalRowsFactory.CreateInputRowWithLimit(storableGood, inventory, inventoryContent);
					rows.Add(item);
				}
			}
			foreach (StorableGoodAmount storableGoodAmount2 in from good in goods
			where good.StorableGood.IsOnlyTakeable
			select good)
			{
				StorableGood storableGood2 = storableGoodAmount2.StorableGood;
				if (InventoryRowUpdater.IsOutput(currentRecipe, storableGood2.GoodId))
				{
					InformationalRow item2 = this._informationalRowsFactory.CreateOutputRowWithLimit(storableGood2, inventory, inventoryContent);
					rows.Add(item2);
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000316C File Offset: 0x0000136C
		public void AddRemainingRows(ScrollView inventoryContent, Inventory inventory, List<InformationalRow> rows, List<StorableGoodAmount> goods)
		{
			foreach (StorableGoodAmount storableGoodAmount in goods)
			{
				StorableGood storableGood = storableGoodAmount.StorableGood;
				if (rows.All((InformationalRow row) => row.GoodId != storableGood.GoodId))
				{
					InformationalRow item = this._informationalRowsFactory.CreateSimpleRowWithoutLimit(storableGood, inventory, inventoryContent);
					rows.Add(item);
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000031F8 File Offset: 0x000013F8
		public static bool IsInputOrFuel(RecipeSpec currentRecipe, string goodId)
		{
			return currentRecipe.Ingredients.Any((GoodAmountSpec goodAmount) => goodAmount.Id == goodId) || currentRecipe.Fuel == goodId;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003240 File Offset: 0x00001440
		public static bool IsOutput(RecipeSpec currentRecipe, string goodId)
		{
			return currentRecipe.Products.Any((GoodAmountSpec goodAmount) => goodAmount.Id == goodId);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003274 File Offset: 0x00001474
		public static bool ShouldShow(Inventory inventory, string goodId)
		{
			bool flag = inventory.LimitedAmount(goodId) > 0;
			bool flag2 = inventory.AmountInStock(goodId) > 0;
			return flag || flag2;
		}

		// Token: 0x04000048 RID: 72
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000049 RID: 73
		public readonly InformationalRowsFactory _informationalRowsFactory;
	}
}
